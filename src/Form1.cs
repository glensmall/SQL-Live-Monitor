/*
 * 
 *  Monitors SQL 2000, 2005 & 2008.  Might work on SQL 7 but is not tested 
 * 
 *  Currently uses system tables with backward compatability for sql 2000
 *  Future implementations will replace sys.sysprocesses with DMV's
 *  and also some counters will replace certain sql query data
 *  
 *  Code has a lot of optimisations in here - which at first glance seem to break standard OO programming methods
 *  however this is intentional as the standard OO constructed can reduce an applications performance
 *  For this reason, the crucial sections have been altered to use global and public variables rather than accessors etc ...
 *  I have also unrolled loops, embedded code rather than calling functions, and stored daat rather than calculating it at runtime.
 *  All these efforts have seriously improved application performance.
 * 
*/

using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Management;
using Microsoft.Win32;

namespace SQLMonitor
{
    // This is used by the thread to enable writing to the form
    //delegate void SetTextValueCallBack(CPerfData TheData);
    delegate void SetTextValueCallBack();

    public partial class Form1 : Form
    {
        #region Variables
        // My Variables
        // Made Global in this way for performance
        Thread t, ProcCacheT;
        bool KeepRunning, UseSQLAuth, UseAltWin, IsClustered, Stopped, IsDefault, CleanupException, Loading, ExitClicked, QuickQuit;
        OdbcConnection Conn, Conn1;
        string ServerName, SvrCounter, SQLUser, SQLPass, ClusterNode, SQLDriver, TrustedConnection, WinUser, WinPass;
        CPerfData PerfDat;
        public static CConfig MyappConfig;
        RegistryKey rk, cpurk;
        int siInternal, siIndex, SQLMemPercent, numProcs, CPUQueueValue;
        long days, hours, mins, secs;
        long StartTick, EndTick;
        public static StreamWriter sw, Log, blocking;
        float SQLTargetMem, SQLTotalMem, BufferCacheSize, ProcCache, LogSpacePercent;
        OdbcCommand blockedCmd;
        string ConnectString;
        #endregion
    
        public Form1()
        {
            InitializeComponent();
           

            // Create out COnfig Object
            MyappConfig = new CConfig();

            // Get the version of this application from the assembly
            String[] VersionArray = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            
            // Print the version in the title of the form
            this.Text = "SQL Live Monitor - v" + VersionArray[0] + "." + VersionArray[1] + " (Build " + VersionArray[2] + ")";

            // init some variables
            UseAltWin = false;
            KeepRunning = false;
            SvrCounter = "SQLServer";
            Stopped = true;
            ExitClicked = false;
            QuickQuit = false;
            SQLDriver = "Driver={SQL Server}";

            // set the server to default to this computer
            // Store the local computer name for use later as well.
            ServerName = SystemInformation.ComputerName;           
            txtServer.Text = ServerName;

            // Create a new PerfDat Object so we can access perfmon etc ....
            PerfDat = new CPerfData();

            // Create or open a log file so we can log what we are doing as wel go
            Log = new StreamWriter(MyappConfig.Path + "\\SQLLiveMonitor" + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".log", true);
            Log.AutoFlush = true;
            Log.WriteLine("------------------------------------------");

            // Log the file version strong to the log file for reference
            Log.WriteLine(DateTime.Now + " Application Initialised - Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString());

            /* Now that we have a log file and the app version - we can do all the other stuff */

            // Get the list of saved servers (if any).
            MyappConfig.GetServers();

            // Populate the drop down if we have any
            if (MyappConfig.SavedServers != null)
            {
                // loop through and add them in
                foreach (string Key in MyappConfig.SavedServers)
                {
                    cmbServerList.Items.Add(Key);
                }
            }

            // Add Default
            cmbServerList.Items.Add("(Select)");
            cmbServerList.Text = "(Select)";
        
        }

        /// <summary>
        /// Called to do the cleanup
        /// param true is closing, false if not
        /// </summary>
        /// <param name="isExit"></param>
        private void DoCleanUp(bool isExit)
        {    
            // Do we need to clean up any logman counter data ?
            if (MyappConfig.PAL)
            {
                // yes so run the batch files that were created earlier
                ProcessStartInfo proc = new ProcessStartInfo(MyappConfig.Path + "\\Stop.bat");
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                Process st = Process.Start(proc);
                
                // Wait for that to finish before carrying on
                st.WaitForExit();
                st.Dispose();
                
                // are we closing down the whole app
                if (isExit)
                {
                    // do we need to deal with PAL ?
                    if (MyappConfig.PAL)
                    {
                        ProcessStartInfo proc1 = new ProcessStartInfo(MyappConfig.Path + "\\Cleanup.bat");
                        proc1.WindowStyle = ProcessWindowStyle.Hidden;
                        Process cu = Process.Start(proc1);
                        //Process.Start(Path + "\\Cleanup.bat");
                        cu.WaitForExit();
                        cu.Dispose();

                        // get rid of the files since we are closing down
                        System.IO.File.Delete(MyappConfig.Path + "\\Start.bat");
                        System.IO.File.Delete(MyappConfig.Path + "\\Stop.bat");
                        System.IO.File.Delete(MyappConfig.Path + "\\AddCounters.bat");
                        System.IO.File.Delete(MyappConfig.Path + "\\Counters.txt");
                        System.IO.File.Delete(MyappConfig.Path + "\\Cleanup.bat");
                    }
                }
            }

            if(!CleanupException)
            {
                // Don't check the thread if the user has just hit the top X
                // We need to rely on the KeepRunning flag to end that one.
                // Any attempt to stop the thread will crash the app
                // DOn't know why yet.
                if (!QuickQuit)
                {
                    // is the thread object valid ?
                    if (t != null)
                    {
                        // is the thread running?
                        while (t.IsAlive)
                        {
                            // ok so we need to wait for this to stop.  It has recieved a stop signal
                            // elsewhere.
                            // wait until the thread has actually finished
                            Thread.Sleep(1000);
                        }
                    }
                }
            }

            if (!Stopped)
            {
                if (MyappConfig.CaptureBlocking)
                {
                    blocking.Flush();
                    blocking.Close();
                    blocking.Dispose();
                    blockedCmd.Dispose();
                }
                  
                // close the logging file
                if (MyappConfig.LogData)
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }

                // Remove all the perf counters from our perf object
                PerfDat.FlushCounters();

                // Reset the controls
                if (IsClustered)
                {
                    txtClusterNode.Enabled = true;
                }

                //if (!ChkDefault.Checked)
                //{
                //    txtInstance.Enabled = true;
                //}

                if (!CleanupException)
                {
                    // set the lables
                    lblVersion.Text = "NOT CONNECTED";
                    lblAWE.Text = "NOT CONNECTED";
                    lblPAE.Text = "NOT CONNECTED";
                    lbl3GB.Text = "NOT CONNECTED";
                }

                // sort the controls
                Conn.Close();
                Conn1.Close();
                Conn.Dispose();
                Conn1.Dispose();

                if (!CleanupException)
                {
                    ResetFormControls();
                }
            }
        }

        /// <summary>
        /// Called when the user presses exit
        /// Confirms before ending
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Flag it up
            ExitClicked = true;

            try
            {
                // added to prevent things from really going fanny up
                if (KeepRunning == true)
                {
                    Log.WriteLine(DateTime.Now + " Stopping Threads");
                    while (t.IsAlive)
                    {
                        // tell the thread to stop
                        KeepRunning = false;
                        // wait until the thread has actually finished
                        Thread.Sleep(1000);
                    }
                                        
                }

                DoCleanUp(true);
            }

            catch (Exception ExitE)
            {
                MessageBox.Show("Error closing down the application: " + ExitE.Message);
            }
            
            Application.Exit(); 
        }

        /// <summary>
        /// Function to get the data we need from the registry
        /// Future development will allow this to apss windows credentials to a remote PC as required
        /// </summary>
        public void GetRegistryData()
        {
            // used to hold the returned registry string
            string pae = null;

            // Are we trying to switch Windows Credentials on the local box ?
            if ((UseAltWin) && (SystemInformation.ComputerName.ToUpper() == ServerName.ToUpper()))
            {
                // yes, and we can't do this - so reset (would cause a WMI exception)
                UseAltWin = false;

                // and log it
                Log.WriteLine(DateTime.Now + " User attempted to switch Windows Credentials on a local connection.  Using current logged in user instead");
            }

            if (UseAltWin)
            {
                try
                {
                    Log.WriteLine(DateTime.Now + " Accessing remote registry via WMI");
                    // ok we need to use WMI to be able to pass in alternate windows credentials
                    // Shame the standard reg class doesn't allow use to do this

                    // Define a connection object so that we specify the alternate creds
                    ConnectionOptions oConn = new ConnectionOptions();
                    oConn.Username = WinUser;
                    oConn.Password = WinPass;

                    // create the WMI object and point it at the required repo.  Note this can actually be used for local as well as remote
                    // if the user selects SQLAuth and alternate creds
                    ManagementScope scope = new ManagementScope(@"\\" + ServerName + @"\root\default", oConn);
                    ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);

                    // Returns a specific value for a specified key 
                    ManagementBaseObject inParams = registry.GetMethodParameters("GetStringValue");

                    // Specify the key we want in HKLM
                    inParams["hDefKey"] = 2147483650;
                    inParams["sSubKeyName"] = @"System\CurrentControlSet\Control";
                    inParams["sValueName"] = "SystemStartOptions";
                    ManagementBaseObject outParams = registry.InvokeMethod("GetStringValue", inParams, null);

                    // now get the value and store it in our variable
                    pae = outParams["sValue"].ToString();

                    // Now we open the other key to find out the number of CPU's
                    inParams = null;
                    inParams = registry.GetMethodParameters("EnumKey");
                    inParams["hDefKey"] = 2147483650;
                    inParams["sSubKeyName"] = @"Hardware\Description\System\CentralProcessor";
                    outParams = registry.InvokeMethod("EnumKey", inParams, null);

                    // get the data and count it.  Cast it as Object doesn;t have GetEnumerator
                    string[] t = (string[])outParams["sNames"];

                    // 1 entry per proc.  Not interested in core type, just core count.
                    numProcs = t.Length;
                   
                    // clean-up our WMI objects
                    inParams.Dispose();
                    outParams.Dispose();
                    registry.Dispose();
                }

                catch (Exception wmi_e)
                {
                    Log.WriteLine(DateTime.Now + " ERROR: " + wmi_e.Message);
                    MessageBox.Show("Error accessing remote registry via WMI: " + wmi_e.Message);
                }
            }
            else
            {
                // Since we are not using alternate credentials - we will just go ahead and use the standard registry library
                // which is the best option really.  
                try
                {
                    // is the computername of this box we are running on the same as the box we are connecting to ?
                    // convert both to upper case.  Found that a difference in case meant some checks were using remote restiry
                    // rather than local.  No major issue really, but this reflects the correct usage now
                    if (SystemInformation.ComputerName.ToUpper() == ServerName.ToUpper())
                    {
                        // yes so access the local registry
                        Log.WriteLine(DateTime.Now + " Accessing Local Registry");
                        rk = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control");

                        // grab the CPU key while we are here
                        cpurk = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor");
                    }
                    else
                    {
                        // no so we need to use the remote registry service - lets hope it's enabled
                        Log.WriteLine(DateTime.Now + " Accessing Remote Registry on " + ServerName);
                        RegistryKey Myrk = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName);

                        rk = Myrk.OpenSubKey(@"System\CurrentControlSet\Control");

                        // grab the CPU key while we are here
                        cpurk = Myrk.OpenSubKey(@"Hardware\Description\System\CentralProcessor");
                    }



                    // Get the string value from the registry we have connected to
                    pae = rk.GetValue("SystemStartOptions").ToString();

                    // find out how many CPU's we have.
                    numProcs = cpurk.SubKeyCount;

                    // Close the registry since we are now done.
                    rk.Close();
                    cpurk.Close();

                }
                // catch this error and deal with it
                catch (Exception MyE)
                {
                    Log.WriteLine(DateTime.Now + " ERROR Could not access the registry on " + ServerName + ": " + MyE.Message);
                    MessageBox.Show("Could not access the registry on " + ServerName + ": " + MyE.Message);
                }
            }

            // Now we have the values - deal with them

            // Test for PAE
            if (pae.Contains("PAE"))
            {
                Log.WriteLine(DateTime.Now + " Server Has PAE Enabled");
                lblPAE.Text = "ENABLED";
            }
            else
            {
                Log.WriteLine(DateTime.Now + " Server Has PAE Disabled");
                lblPAE.Text = "DISABLED";
            }

            // TEST for /3GB
            if (pae.Contains("3GB"))
            {
                Log.WriteLine(DateTime.Now + " Server Has /3GB Enabled");
                lbl3GB.Text = "ENABLED";
            }
            else
            {
                Log.WriteLine(DateTime.Now + " Server Has /3GB Disabled");
                lbl3GB.Text = "DISABLED";
            }

            // now display this figure in the form
            grpCPU.Text = "CPUs (" + numProcs.ToString() + ")";
        }

                 
        /// <summary>
        /// Called to start the gathering of data
        /// Will kick off the thread etc ......
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Stopped = false;
            siIndex = 0;
            IsDefault = true; 
            CleanupException = false;

            #region Form Validation

            // verify we have a server name
            if (txtServer.Text.Length == 0)
            {
                MessageBox.Show("You Must Enter a Server Name");
                return;
            }

            // verify we have a instance
            if (chkClustered.Checked == true && txtClusterNode.Text.Length == 0)
            {
                MessageBox.Show("You Must Specify The Name of The Active CLuster Node");
                return;
            }

            
            // are we using SQL authentication
            if (UseSQLAuth)
            {
                if (SQLUser.Length == 0)
                {
                    MessageBox.Show("You must specify a Username for SQL Authentication");
                    return;
                }

                if (SQLPass.Length == 0)
                {
                    MessageBox.Show("WARNING:  You are using a blank password for SQL Authentication.  This is VERY bad practise !!!!");
                }
            }

#endregion

            // set the controls
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            txtServer.Enabled = false;
            //ChkDefault.Enabled = false;
            //txtInstance.Enabled = false;
            btnOptions.Enabled = false;
            chkClustered.Enabled = false;
            chkSQLAuth.Enabled = false;
            txtClusterNode.Enabled = false;
            KeepRunning = true;
            ServerName = txtServer.Text;
            ClusterNode = txtClusterNode.Text;

            // if we're using the native client, we need to pass a different param to the driver for trusted connections
            if (SQLDriver.Contains("Native"))
            {
                TrustedConnection = "Trusted_Connection=yes";
            }
            else
            {
                 TrustedConnection = "Trusted_Connection=true";
            }

            // Set the connection string before we splt out the server and instacne name
            // fix added in 1.25 - where connection to default instance would fail.
            ConnectString = ServerName;

            // bug fix found in 1.33  Performance acounter addition failed if user specifies a port number in the connectin string
            // split on a comma and then grab the first part and re-set it as the server name, since we have already definced the connection string
            string[] portcheck = ServerName.Split(',');
            ServerName = portcheck[0];

            // Split the server name from the instance
            // now that we have removed the instance specific input box (1.21)
            if (ServerName.Contains(@"\"))
            {
                string[] temp = ServerName.Split('\\');
                ServerName = temp[0];
                                           
                SvrCounter = @"MSSQL$" + temp[1];
                IsDefault = false;
            }

            #region PAL Counters
            // does the user want to use PAL counters as well
            if (MyappConfig.PAL)
            {
                // create an Options object so we can create the logman counters
                Options dlg = new Options();

                // Are we a default instance
                if (IsDefault)
                {
                    // clustered ??
                    if (IsClustered)
                    {
                        dlg.AddPALCounters(ClusterNode, "SQLServer");
                    }
                    else
                    {
                        dlg.AddPALCounters(ServerName, "SQLServer");
                    }
                }
                // named instance
                else
                {
                    // clustered
                    if (IsClustered)
                    {
                        dlg.AddPALCounters(ClusterNode, SvrCounter);
                    }
                    else
                    {
                        dlg.AddPALCounters(ServerName, SvrCounter);
                    }
                }

                dlg.Dispose();
            }

            #endregion

            // do we log to CSV ?
            if (MyappConfig.LogData)
            {
                Log.WriteLine(DateTime.Now + " Logging Data to CSV at " + siInternal + " Second intervals");
                sw = new StreamWriter(MyappConfig.Path + "\\" + ServerName + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".csv");
            }

            if (MyappConfig.CaptureBlocking)
            {
                Log.WriteLine(DateTime.Now + " Logging Blocked Processes to CSV. May impact application performance");
                blocking = new StreamWriter(MyappConfig.Path + "\\" + ServerName + "_Blocked_Processes" + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".csv");
            }


            #region Connection String Setup
            // set up the database connection
            // we make 2.  one dedicated for hte main loop - the other for the detailed view.
            // this is here to remove the contention of data down a single connection.
            // a full connaction pool is over kill.         

            if (UseSQLAuth)
            {
                Conn = new OdbcConnection("" + SQLDriver + ";Server=" + ConnectString + ";Database=master;Uid=" + SQLUser + ";Pwd=" + SQLPass + ";");
                Conn1 = new OdbcConnection("" + SQLDriver + ";Server=" + ConnectString + ";Database=master;Uid=" + SQLUser + ";Pwd=" + SQLPass + ";");     
            }
            else
            {
                Conn = new OdbcConnection("" + SQLDriver + ";Server=" + ConnectString + ";Database=master;" + TrustedConnection + ";");
                Conn1 = new OdbcConnection("" + SQLDriver + ";Server=" + ConnectString + ";Database=master;" + TrustedConnection + ";");
            }   

            // define the connection timeout at 10 seconds
            Conn.ConnectionTimeout = 10;
            Conn1.ConnectionTimeout = 10;

            #endregion

            if (UseSQLAuth)
            {
                Log.WriteLine(DateTime.Now + " Connecting to Database [" + ServerName + "] Using SQL Authentication.  Detail not shown for security");
            }
            else
            {
                Log.WriteLine(DateTime.Now + " Connecting to Database [" + Conn.ConnectionString + "]");
            }
           
            // try and open the connection
            try
            {
                // open the connection to the database
                Conn.Open();
                Conn1.Open();
            }
            catch(Exception ee)
            {
                // ok so that didn;t work - reset everything and allow the user to change their settings
                Log.WriteLine(DateTime.Now + " Failed to connect [" + ee.Message + "]");
                MessageBox.Show("Error connecting to the specified SQL Server [" + ee.Message + "]");
                KeepRunning = false;
                ResetFormControls();
                Stopped = true;
                Conn.Dispose();
                Conn1.Dispose();
                return;
            }


            #region Get SQL Version
            // get the version of SQL we've connect to and put it in the title bar
            OdbcCommand VersionCommand = new OdbcCommand(@"SELECT  SERVERPROPERTY('productversion') As Version, SERVERPROPERTY ('productlevel') As ServicePack, SERVERPROPERTY ('edition') As Edition, SERVERPROPERTY('IsClustered') As Clust");
            VersionCommand.Connection = Conn;
            OdbcDataReader vdr = VersionCommand.ExecuteReader();

            // read it, format it and display it
            vdr.Read();           
            
            // need to work out the version so we query the correct table names later
            if (vdr[0].ToString().StartsWith("8.00") || vdr[0].ToString().StartsWith("7.00"))
            {
                // we are running a version lower than 2005
                // means we will use sysprocesses to query sql
                MyappConfig.pre2005 = true;
                MyappConfig.is2014 = false;
            }
            else if(vdr[0].ToString().StartsWith("10"))
            {
                // we are running on 2005 or above
                // means we will use sys.sysprocesses to query sql
                MyappConfig.pre2005 = false;
                MyappConfig.is2014 = false;
            }
            else if(vdr[0].ToString().StartsWith("11"))
            {
                // quick dirty fix for SQL 2012.  will address properly in Verion 2.
                MyappConfig.pre2005 = false;
                MyappConfig.denali = true;
                MyappConfig.is2014 = false;
            }
            else if (vdr[0].ToString().StartsWith("12"))
            {
                // quick dirty fix for SQL 2012.  will address properly in Verion 2.
                MyappConfig.pre2005 = false;
                MyappConfig.denali = false;
                MyappConfig.is2014 = true;
            }
            else
            {
                // quick fix to handle 2016 onwards.  Dealing with items that have been removed.
                MyappConfig.pre2005 = false;
                MyappConfig.denali = false;
                MyappConfig.is2014 = true;
            }

            // display the version to the user (on the form) and log it
            lblVersion.Text = vdr[0] + " - " + vdr[1] + " - " + vdr[2];
            Log.WriteLine(DateTime.Now + " Server Version is - " + lblVersion.Text);

            // are we clustered
            if (string.Compare("1", vdr[3].ToString()) == 0)
            {
                lblVersion.Text += " (CLUSTERED)";
            }
           
            vdr.Close();

            #endregion

            #region AWE & PAE & /3GB Check

            // enable advanced options in sp_configure
            // some poeple won;t like this because it is an alteration without change control
            // but what the eye doesn't see the chef gets away with :-)
            VersionCommand.CommandText = "exec sp_configure 'show advanced options', '1'";
            VersionCommand.ExecuteNonQuery();
            vdr.Close();

            // Enable it
            VersionCommand.CommandText = "RECONFIGURE WITH OVERRIDE";
            VersionCommand.ExecuteNonQuery();

            if(MyappConfig.denali == true)
            {
                lblAWE.Text = "NOT APPLICABLE";
                Log.WriteLine(DateTime.Now + " SQL 2012 does not contain AWE info.");
            }
            else
            {
                if (MyappConfig.is2014 == true)
                {
                    lblAWE.Text = "NOT VALID";
                    Log.WriteLine(DateTime.Now + " 2014 - Skipping AWE");
                }
                else
                {
                    // AWE CHECK
                    VersionCommand.CommandText = " exec sp_configure 'awe enabled'";
                    vdr = VersionCommand.ExecuteReader();

                    vdr.Read();

                    // work out of we have AWE enabled - and display accordingly
                    if (string.Compare("1", vdr[4].ToString()) == 0)
                    {
                        lblAWE.Text = "ENABLED";
                        Log.WriteLine(DateTime.Now + " Server Has AWE Enabled");
                    }
                    else
                    {
                        lblAWE.Text = "DISABLED";
                        Log.WriteLine(DateTime.Now + " Server Has  AWE Disabled");
                    }

                    // clean-up what we have just used - no need to hang onto this memory
                    vdr.Close();
                }
            }

            
            vdr.Dispose();
            VersionCommand.Dispose();

            // Get the info from the registry
            // moved to a function in version 1.26 to handle remote registry authentication
            // via WMI calls
            GetRegistryData();

            #endregion

            Log.WriteLine(DateTime.Now + " Running second thread");

            // Calculate the CPU Queue Length so we don't do it over and over inline
            // Calc once - use many times :-)
            CPUQueueValue = (numProcs * MyappConfig.thresCPUQueue);

            // ok we got this far so se the title to be that which we are connected to
            this.Text = ServerName;

            // Finished doing all that stuff - so now we create a new thread and let it run
            // this enables the form to remain active while the data is being gathered in real time
            t = new Thread(ThreadFunction);
            t.Start();

            // Start the perfmon counters if we're doing the PAL stuff
            if(MyappConfig.PAL)
            {
                Log.WriteLine(DateTime.Now + " Logging PAL Counters via PerfMon");

                // run the batch file we created.
                ProcessStartInfo proc = new ProcessStartInfo(MyappConfig.Path + "\\Start.bat");
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                Process mp = Process.Start(proc);

                mp.WaitForExit();
                mp.Dispose();
            }

            // enable the user to change the disk counters now we have go running.
            btnDiskUpdate.Enabled = true;
            
        }

        /// <summary>
        /// Called to stop the information gathering
        /// will get the thread to stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            Log.WriteLine(DateTime.Now + " Stopping ........");
            
            // tell the thread to stop
            KeepRunning = false;

            // call the clean-up function
            DoCleanUp(false);

            // set the stopped flag
            Stopped = true;

            Log.WriteLine(DateTime.Now + " Stopped");

            btnDiskUpdate.Enabled = false;

            // Make sure these boxes are enabled 
            chkClustered.Enabled = true;
            chkSQLAuth.Enabled = true;

            String[] VersionArray = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            this.Text = "SQL Live Monitor - v" + VersionArray[0] + "." + VersionArray[1] + " (Build " + VersionArray[2] + ")";
        }

        /// <summary>
        /// Called to write out the header information to the CSV file if the user
        /// has selected to save data to csv
        /// </summary>
        private void WriteHeaderInfo()
        {
            // write out the date and time colums
            sw.Write("Date Time,");

            // write out each of the performance coiunters from the array
            foreach (PerformanceCounter p in PerfDat.PerfCounters)
            {
                // write them in instance format
                if (p.InstanceName != null)
                {
                    sw.Write(p.CategoryName + "(" + p.InstanceName + ")\\" + p.CounterName + ",");
                }
                else
                {
                    sw.Write(p.CategoryName + "\\" + p.CounterName + ",");
                }
            }

            // now wrie out the disk counters
            foreach (PerformanceCounter d in PerfDat.DiskCounters)
            {
                if (d.InstanceName != null)
                {
                    sw.Write(d.CategoryName + "(" + d.InstanceName + ")\\" + d.CounterName + ",");
                }
                else
                {
                    sw.Write(d.CategoryName + "\\" + d.CounterName + ",");
                }
            }

            // now write the remaining columns that are not related to perfmon objects.
            sw.Write("SQL System Processes,SQL User Processes,SQL Total Processes,SQL Blocked Processes,SQL Active Processes,Hosts\n");
        }


        /// <summary>
        /// Function to reset the controls on the form back to their defaul values.
        /// Called at Stop and on Exception
        /// </summary>
        public void ResetFormControls()
        {
            // Enable all the form controls again.
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            txtServer.Enabled = true;
            //ChkDefault.Enabled = true;
            btnOptions.Enabled = true;
            chkClustered.Enabled = true;
            chkSQLAuth.Enabled = true;

            // Set the check boxes as required and their text fields
            if (chkClustered.Checked == true)
            {
                txtClusterNode.Enabled = true;
            }
            else
            {
                txtClusterNode.Enabled = false;
            }
        }

        /// <summary>
        /// This is the main thread function what will go and gather the data I'm after
        /// This will run and run until we set the "keeprunning" flag to false.
        /// I've added plenty of checks for KeepRunning so that the thread can 
        /// end as soon as signaled to do so.
        /// </summary>
        public void ThreadFunction()
        {
            // this will be used to read data from SQL
            OdbcDataReader dr;

            // needed to handle connections to sql 2000
            // will replace this in future with dmv and will replace Odbc with SqlClient
            string tablename;

            // are we on 2000 or 2005 / 2008
            if (MyappConfig.pre2005)
            {
                tablename = "sysprocesses";
            }
            else
            {
                tablename = "sys.sysprocesses";
            }
            
            // 1 big query to get the process and lock information from SQL
            // do it all at once to save running many queries against the box and causing a performance issue
            // we call this so many times so SQL with cache this plan (well that's the theory anyway ;-))
            string QueryString = @"select count(loginame) as SystemLogins, 
                                                                (select count(loginame) from " + tablename + @" where hostname != '') As UserLogins,
	                                                            (select count(*) from " + tablename + @") As Total,
	                                                            (select count(*) from " + tablename + @" where status = 'runnning') As Active,
                                                                (select count(*) from " + tablename + @" where blocked != 0) As Blocked,
                                                                (select count(distinct hostname) from " + tablename + @" where hostname != '') As Hosts                                          
                                                            from " + tablename + @"
                                                            where loginame = 'sa' and hostname = ''";


            // create a command object to gather process information
            // will return 3 cols (Total Processes, Num System Processes, Num Running Queries)
            OdbcCommand ProcessCommand = new OdbcCommand(QueryString);

            #region Blocking Setup

            if (MyappConfig.CaptureBlocking)
            {
                // Write out the blocking Header Info
                blocking.WriteLine("DateTime, Blocking SPID, Blocking Statement, Blocked SPID, Blocked Statement");

                blockedCmd = new OdbcCommand("select spid, blocked from " + tablename + " where blocked > 0");
                blockedCmd.Connection = Conn;
            }

            #endregion

            // Double check before we add the counters - just in-case
            if (!KeepRunning)
            {
                return;
            }

            Log.WriteLine(DateTime.Now + " Adding Performance Counters ......");
            
            // Add the perf counters that we want to gather.
            // this will also populate an arry used to help cleanup as well.
            // Time it for logging
            long AddCounterStart = DateTime.Now.Ticks;

            // enter a try block in case this goes wrong.
            try
            {
                if (IsClustered)
                {
                    PerfDat.AddCounters(SvrCounter, @"_Total", ClusterNode, MyappConfig.pre2005, MyappConfig.denali, MyappConfig.is2014);
                    PerfDat.AddDiskCounters(txtDrive.Text, ClusterNode);
                }
                else
                {
                    PerfDat.AddCounters(SvrCounter, @"_Total", ServerName, MyappConfig.pre2005, MyappConfig.denali, MyappConfig.is2014);
                    PerfDat.AddDiskCounters(txtDrive.Text, ServerName);
                }
            }

            // Catch block for the add counters phase.  If it all goes brown here then we want to exit
            // otherwise the app will hang
            catch (Exception MyE)
            {
                Log.WriteLine(DateTime.Now + "ERROR ADDING COUNTERS " + MyE.StackTrace);
                MessageBox.Show("ERROR ADDING PERFORMANCE COUNTERS: " + MyE.Message + "\n Additional information will be in the log file");

                CleanupException = true;
                // if this fails - then stop
                DoCleanUp(false);

                // set the stopped flag
                Stopped = true;

                Log.WriteLine(DateTime.Now + " Stopped");

                btnDiskUpdate.Enabled = false;
                ProcessCommand.Dispose();

                ResetFormControls();
                return;
            }

            // Now calculate how long that all took
            long AddCountersEnd = DateTime.Now.Ticks;
            long AddCounterDiff = (AddCountersEnd - AddCounterStart) / 10000;
            long CounterNum = PerfDat.PerfCounters.Count + PerfDat.DiskCounters.Count;

            Log.WriteLine(DateTime.Now + " Added " + CounterNum.ToString() + " Counters in " + AddCounterDiff.ToString() + " ms");

            // Set-up for updating the form from another thread
            // The thread can't modify the forma directly because of .net security - so we delegate the updating
            // and this defines the funcation that will do the actual updating
            SetTextValueCallBack MyCallBack = new SetTextValueCallBack(UpdateFormData);

            // Write out the header information if required
            if (MyappConfig.LogData)
            {
                WriteHeaderInfo();
            }

            // end of header info

            Log.WriteLine(DateTime.Now + " Entering Loop");

            // Check again before we enter the loop
            if (!KeepRunning)
            {
                return;
            }

            // This is where we loop until we are told to stop
            // note we only check at the start of each loop
            while (KeepRunning)
            {
                try
                {
                    /* DATA GATHERING & RENDERING*/
                   
                    // Lots of optimisations here. Some unrolled loops - functions inlined etc .....
                                        
                    #region Update Perfmon Counter Values
                    // Get the next value from each of the perfmon counters - cast where needed
                    // This loop has been unrolled for performance.  Originally a foreach loop however this
                    // way is much faster, and has less overhead on the running system.
                    PerfDat.TransactionsSecValue = PerfDat.TransactionsSec.NextValue();
                    PerfDat.CPUValue = (int)PerfDat.CPUPercent.NextValue();
                    PerfDat.CompilesSecValue = PerfDat.CompilesSec.NextValue();
                    PerfDat.TargetMemoryValue = PerfDat.TargetMemory.NextValue();
                    PerfDat.TotalMemoryValue = PerfDat.TotalMemory.NextValue();
                    PerfDat.BufferCacheSizeValue = PerfDat.BufferCacheSize.NextValue();
                    PerfDat.BufferHitRatioValue = PerfDat.BufferCacheHitRatio.NextValue();
                    PerfDat.LocksSecValue = PerfDat.LocksPerSecond.NextValue();
                    PerfDat.PageLookupsValue = PerfDat.UserPageLookups.NextValue();
                    PerfDat.PageLifeValue = PerfDat.PageLife.NextValue();
                    PerfDat.UserConnectionsValue = PerfDat.UserConnections.NextValue();
                    PerfDat.LazyWritesValue = PerfDat.LazyWrites.NextValue();
                    PerfDat.ReadAheadsValue = PerfDat.ReadAheads.NextValue();
                    PerfDat.CheckpointsValue = PerfDat.Checkpoints.NextValue();
                    PerfDat.BatchesSecValue = PerfDat.BatchesSec.NextValue();
                    PerfDat.DiskReadsSecValue = PerfDat.DiskReads.NextValue();
                    PerfDat.DiskWritesSecValue = PerfDat.DiskWrites.NextValue();
                    PerfDat.LogFlushesValue = PerfDat.LogFlushes.NextValue();
                    PerfDat.LoginsSecValue = PerfDat.LoginsSec.NextValue();
                    PerfDat.ProcCacheValue = PerfDat.ProcedureCacheSize.NextValue();
                    PerfDat.ProcHitRatioValue = PerfDat.ProcedureCacheHitRatio.NextValue();
                    PerfDat.RecompilesSecValue = PerfDat.RecompilesSec.NextValue();
                    PerfDat.MemPagesSecValue = PerfDat.MemPagesSec.NextValue();
                    PerfDat.ProcQueueLengthValue = PerfDat.ProcQueueLength.NextValue();
                    PerfDat.ConnectionMemoryValuel = PerfDat.ConnectionMemory.NextValue();
                    PerfDat.WorkFilesValue = PerfDat.WorkFiles.NextValue();
                    PerfDat.WorkTablesValue = PerfDat.WorkTables.NextValue();
                    PerfDat.TempDBLogSizeValue = PerfDat.TempDBLogSize.NextValue();
                    PerfDat.TempDBSizeValue = PerfDat.TempDBSize.NextValue();
                    PerfDat.FrePTEsValue = PerfDat.FreePTEs.NextValue();
                    PerfDat.PagedPoolValue = PerfDat.PagedPool.NextValue();
                    PerfDat.NonPagedPoolValue = PerfDat.NonPagedPool.NextValue();
                    
                    PerfDat.TempLogSpaceUsedValue = PerfDat.TempLogSpaceUsed.NextValue();

                    // We don't capture this prior to SQL 2005
                    if (!MyappConfig.pre2005)
                    {
                        PerfDat.MsdtcValue = PerfDat.msdtc.NextValue();
                        PerfDat.TempObjectCreateValue = PerfDat.TempObjectCreate.NextValue();
                        PerfDat.TempObjectDestroyValue = PerfDat.TempObjectDestroy.NextValue();
                        PerfDat.TempActiveTablesValue = PerfDat.TempActiveTables.NextValue();
                    }

                    PerfDat.AvailSysRam = PerfDat.AvailableSystemRam.NextValue();
                    PerfDat.GrantsPending = (long)PerfDat.MemGrantsPending.NextValue();
                    PerfDat.StolenPages = (long)PerfDat.StolePages.NextValue();
                    PerfDat.FullScansSec = PerfDat.FullScans.NextValue();

                    PerfDat.Waits = (int)PerfDat.LockWaits.NextValue();
                    PerfDat.AvgWaitTime = PerfDat.AvgLockWaitTime.NextValue();

                    // Just in case we are updating the disk counters
                    // we need to pause if we are so that we don't generate an exception
                    // if the disk counter doesn't yet exist.
                    while (MyappConfig.TSleep)
                    {
                        Thread.Sleep(1000);
                    }

                    // Disk info counters
                    PerfDat.AvgDiskQueueValue = PerfDat.AvgDiskQueueLength.NextValue();
                    PerfDat.AvgDiskReadValue = PerfDat.AvgDiskReadSec.NextValue();
                    PerfDat.AvgDiskWriteValue = PerfDat.AvgDiskWriteSec.NextValue();
                    PerfDat.DiskIdleValue = PerfDat.DiskIdleTime.NextValue();
                    PerfDat.DiskReadBytesValue = PerfDat.DiskReadBytesSec.NextValue();
                    PerfDat.DiskWriteBytesValue = PerfDat.DiskWriteBytesSec.NextValue();

                    #endregion

                    // Stop check
                    if (!KeepRunning)
                    {
                        break;
                    }

                    #region Query and Get MS
                    // make sure the connection object is set for this query
                    ProcessCommand.Connection = Conn1;

                    // how many processes (measure time taken)
                    StartTick = DateTime.Now.Ticks;

                    dr = ProcessCommand.ExecuteReader();

                    EndTick = DateTime.Now.Ticks;

                    // response time in ms (ticks are 100 ns intervals)
                    // nano = 1 million millisecond - so do the simple maths
                    //PerfDat.ResponseMS = ((EndTick - StartTick) * 100) / 1000000;
 
                    PerfDat.ResponseMS = (EndTick - StartTick) / 10000;  // Optimisation given that / cancels out the *

                    // Read the data using the DataReader
                    dr.Read();
                    PerfDat.SystemProcesses = (int)dr[0];
                    PerfDat.UserProcesses = (int)dr[1];
                    PerfDat.TotalProcesses = (int)dr[2];
                    PerfDat.BlockedProcesses = (int)dr[4];
                    PerfDat.ActiveProcesses = (int)dr[3];
                    PerfDat.Hosts = (int)dr[5];
                    
                    /*
                    PerfDat.Waits = (int)dr[6];

                    // just in case the value returned NULL
                    if (dr[7] == DBNull.Value)
                    {
                        PerfDat.AvgWaitTime = 0;
                    }
                    else
                    {
                        PerfDat.AvgWaitTime = Convert.ToInt32(dr[7]);
                    }
                    
                    */

                    dr.Close();

                    #endregion

                    // Stop check
                    if (!KeepRunning)
                    {
                        break;
                    }

                    #region Get Blocking Info

                    
                    // Capture Blocking information to file if required
                    if (PerfDat.BlockedProcesses > 0)
                    {
                        // Get all the blocked processes
                        // set this again because we may over write it later on
                        blockedCmd.CommandText = "select spid, blocked from " + tablename + " where blocked > 0";
                        OdbcDataAdapter ad = new OdbcDataAdapter(blockedCmd);
                        DataTable ds = new DataTable();
                        ad.Fill(ds);
                        ad.Dispose();

                        DataTableReader dtr = new DataTableReader(ds);
                        OdbcDataReader ddr;

                        // go thorugh each blocked process and get the input buffers for each spid.
                        // spid is at 0 blocked proc is at 1
                        while (dtr.Read())
                        {
                            string spid = dtr[0].ToString();
                            string blocked = dtr[1].ToString();

                            // get the buffer for spid
                            blockedCmd.CommandText = "DBCC INPUTBUFFER(" + spid + ")";

                            ddr = blockedCmd.ExecuteReader();
                            ddr.Read();
                            
                            string spid_input_buffer = ddr[2].ToString().Replace("\r\n", " ");
                            ddr.Close();

                            // get the buffer for blocked - this is the blocking process - not the blocked process !!!!
                            blockedCmd.CommandText = "DBCC INPUTBUFFER(" + blocked + ")";

                            ddr = blockedCmd.ExecuteReader();
                            ddr.Read();
                           
                            string blocked_input_buffer = ddr[2].ToString().Replace("\r\n", " ");
                            ddr.Close();
                            ddr.Dispose();
                            
                            blocking.WriteLine(DateTime.Now + "," + blocked + "," + blocked_input_buffer + "," + spid + "," + spid_input_buffer);
                        }

                        dtr.Close();
                        dtr.Dispose();
                    }
                    // end of blocking capture
                    
                    #endregion
                }

                    
                // Check block for the main section above - plenty can go wring above, although i've done what I
                // can to prevent it.
                catch (Exception GetDataException)
                {
                    Log.WriteLine(DateTime.Now + " EXCEPTION in main loop: " + GetDataException.Message);
                    //MessageBox.Show("Error getting the performance data: " + GetDataException.Message);
                }

                              
                // Stop check
                if (!KeepRunning)
                {
                    break;
                }

                /* Now we're done - render
               * We have to use the Invoke() function because the thread
               * does not have permission to monify the forms controls
               * so we pass in a class structure and let the form do it for us
              */
                this.Invoke(MyCallBack);
               
                /* WAITING */
                // wait for 1 sec and then go again.
                Thread.Sleep(1000);

            } // End of while loop
            
            // we are done so we don't need this anymore
            ProcessCommand.Dispose();    
        
        } // end of function

        // -----------------------------------------------------------------------------
        /* THE FOLLOWING FUNCTIONS GET CALLED BY THE CALLBACK TO UPDATE THE FORM DATA */
        /* Threads don't have permission to update a form directly, so this is delegated */
        /* in this fashion for cross thread security                                    */
        // -----------------------------------------------------------------------------
        private void UpdateFormData()
        {
            // SQL Response Time in MS
            lblResponse.Text = PerfDat.ResponseMS.ToString();

            // Session data
            lblLogicalConnections.Text  = PerfDat.Hosts.ToString();
            lblLoginsSec.Text           = PerfDat.LoginsSecValue.ToString("0");
            lblUserConnections.Text     = PerfDat.UserConnectionsValue.ToString("0");
            lblBatches.Text             = PerfDat.BatchesSecValue.ToString("0.00");

            #region CPU Related
            // display the queue length for the CPU - is it too high ?
            if (PerfDat.ProcQueueLengthValue > CPUQueueValue)
            {
                lblProcQueueLength.ForeColor = System.Drawing.Color.Red;
                lblProcQueueLength.Text = PerfDat.ProcQueueLengthValue.ToString("0");
            }
            else
            {
                lblProcQueueLength.ForeColor = System.Drawing.Color.Black;
                lblProcQueueLength.Text = PerfDat.ProcQueueLengthValue.ToString("0");
            }

            // display CPU load
            lblCPU.Text = PerfDat.CPUValue.ToString("0") + @" %";

            // Colour code the CPU data according to the standard alert value
            if (PerfDat.CPUValue > MyappConfig.thresCPU)
            {
                progCPU.ProgressBarColor = System.Drawing.Color.Red;
                lblCPU.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                progCPU.ProgressBarColor = System.Drawing.Color.Green;
                lblCPU.ForeColor = System.Drawing.Color.Black;
            }

            progCPU.Value               = (int)PerfDat.CPUValue;

            #endregion

            // Stop check
            if (!KeepRunning)
            {
                return;
            }

            #region Transactions and Processes

            lblTransSec.Text            = PerfDat.TransactionsSecValue.ToString("0.00");
            lblCompiles.Text            = PerfDat.CompilesSecValue.ToString("0.00");
            lblRecompiles.Text          = PerfDat.RecompilesSecValue.ToString("0.00");
            lblProcTotal.Text           = PerfDat.TotalProcesses.ToString();
            lblProcSys.Text             = PerfDat.SystemProcesses.ToString();
            lblProcUser.Text            = PerfDat.UserProcesses.ToString();
            lblActive.Text              = PerfDat.ActiveProcesses.ToString();
            lblBlocked.Text             = PerfDat.BlockedProcesses.ToString();

            if (PerfDat.BlockedProcesses > 0)
            {
                lblBlocked.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblBlocked.ForeColor = System.Drawing.Color.Black;
            }

            if (!MyappConfig.pre2005)
            {
                lbldtc.Text = PerfDat.MsdtcValue.ToString("0.00");
            }

            #endregion

            lblPageLookups.Text         = PerfDat.PageLookupsValue.ToString("0.00");

            #region Locks
            // Locks
            lblLocksSec.Text    = PerfDat.LocksSecValue.ToString("0.00");
            lblLockWaits.Text   = PerfDat.Waits.ToString("0.00");

            if (PerfDat.AvgWaitTime > 1000)
            {
                lblLockWaitTime.Text = (PerfDat.AvgWaitTime / 1000).ToString("0.00" + " Secs");
            }
            else
            {
                lblLockWaitTime.Text = PerfDat.AvgWaitTime.ToString("0" + " ms");
            }


            //lblLatchWaits.Text  = PerfDat.AvgLockWaitTime.ToString("0.00");
            //lblIOWaits.Text     = PerfDat.IOLatchWaits.ToString("0.00");

#endregion

            // Stop check
            if (!KeepRunning)
            {
                return;
            }

            lblFullScans.Text = PerfDat.FullScansSec.ToString("0.00");

            if (PerfDat.FullScansSec > 2)
            {
                lblFullScans.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblFullScans.ForeColor = System.Drawing.Color.Black;
            }

            #region Memory Related

            // SQL Memory
            SQLTotalMem = PerfDat.TotalMemoryValue / 1024;
            SQLTargetMem = PerfDat.TargetMemoryValue / 1024;

            // Display the memory details in GB or MB format depending on the value
            if (SQLTargetMem > 1000)
            {
                lblTargetMemory.Text = (SQLTargetMem / 1024).ToString("0.00 GB");
            }
            else
            {
                lblTargetMemory.Text = SQLTargetMem.ToString("0.00 MB");
            }

            if (SQLTotalMem > 1000)
            {
                lblTotalMemory.Text = (SQLTotalMem / 1024).ToString("0.00 GB");
            }
            else
            {
                lblTotalMemory.Text = SQLTotalMem.ToString("0.00 MB");
            }
    

            if (PerfDat.AvailSysRam > 1000)
            {
                lblAvailSysRam.Text = (PerfDat.AvailSysRam / 1024).ToString("0.00 GB");
            }
            else
            {
                lblAvailSysRam.Text = PerfDat.AvailSysRam.ToString("0.00 MB");
            }

            // Memory pages / sec
            if (PerfDat.MemPagesSecValue > MyappConfig.thresMemPagesSec)
            {
                lblPagesSec.ForeColor = System.Drawing.Color.Red;
                lblPagesSec.Text = PerfDat.MemPagesSecValue.ToString("0.00");
            }
            else
            {
                lblPagesSec.ForeColor = System.Drawing.Color.Black;
                lblPagesSec.Text = PerfDat.MemPagesSecValue.ToString("0.00");
            }
            
            SQLMemPercent               = (int)((PerfDat.TotalMemoryValue / PerfDat.TargetMemoryValue) * 100);
            progSQLMemory.Value         = SQLMemPercent;
            lblSQLMemoryPercent.Text    = SQLMemPercent.ToString() + @" %";

            // connection memory - converted to MB
            lblConMem.Text = (PerfDat.ConnectionMemoryValuel / 1024).ToString("0.00" + " MB");

            // FreePTEs
            if (PerfDat.FrePTEsValue < 10000)
            {
                lblFreePTEs.ForeColor = System.Drawing.Color.Yellow;
                lblFreePTEs.Text = PerfDat.FrePTEsValue.ToString("0");
            }
            else if (PerfDat.FrePTEsValue < 5000)
            {
                lblFreePTEs.ForeColor = System.Drawing.Color.Red;
                lblFreePTEs.Text = PerfDat.FrePTEsValue.ToString("0");
            }
            else
            {
                lblFreePTEs.ForeColor = System.Drawing.Color.Black;
                lblFreePTEs.Text = PerfDat.FrePTEsValue.ToString("0");
            }

            // Paged Pool - convert bytes to MB
            lblPagedPool.Text = (PerfDat.PagedPoolValue / 1048576).ToString("0.00 MB");

            // Non Paged Pool
            lblNonPagedPool.Text = (PerfDat.NonPagedPoolValue / 1048576).ToString("0.00 MB");

            #endregion

            // Stop check
            if (!KeepRunning)
            {
                return;
            }

            #region Buffer and Proc Cache
            // Need to calculate the size of the cache before rendering
            //BufferCacheSize = (((PerfDat.BufferCacheSizeValue * 8192) / 1024) / 1024);
            BufferCacheSize = ((PerfDat.BufferCacheSizeValue * 8192) / 1048576); // Optimisation for above - we combine to 2 divided into 1
                       
            lblBufferCacheSize.Text     = BufferCacheSize.ToString("0.00 MB");

            lblBufferHitRatioPercent.Text    = PerfDat.BufferHitRatioValue.ToString("0") + @" %";
            progBufferHitRatio.Value         = (int)PerfDat.BufferHitRatioValue;

            if (PerfDat.BufferHitRatioValue < MyappConfig.thresBufferCache)
            {
                progBufferHitRatio.ProgressBarColor = System.Drawing.Color.Red;

                lblBufferHitRatioPercent.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                progBufferHitRatio.ProgressBarColor = System.Drawing.Color.Green;
                lblBufferHitRatioPercent.ForeColor = System.Drawing.Color.Black;
            }

            // convert 8K pages to MB for proc cache size
            //ProcCache = (((PerfDat.ProcCacheValue * 8192) / 1024) / 1024);
            ProcCache = ((PerfDat.ProcCacheValue * 8192) / 1048576); // Optimisation for above - we combine to 2 divided into 1
            

            lblProcCache.Text = ProcCache.ToString("0.00 MB");
            progProcCache.Value = (int)PerfDat.ProcHitRatioValue;
            lblProcCachePercent.Text = progProcCache.Value.ToString() + @" %";

            if (PerfDat.ProcHitRatioValue < MyappConfig.thresProcCache)
            {
                progProcCache.ProgressBarColor = System.Drawing.Color.Red;
                lblProcCachePercent.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                progProcCache.ProgressBarColor = System.Drawing.Color.Green;
                lblProcCachePercent.ForeColor = System.Drawing.Color.Black;
            }

            #endregion

            // Stop check
            if (!KeepRunning)
            {
                return;
            }

            lblGrantsPending.Text = PerfDat.GrantsPending.ToString();
            lblStolenPages.Text = PerfDat.StolenPages.ToString();

            #region Page Life
            // convert seconds to days, hours, minutes, seconds
            // Replaced modulo with multiply which should be less clock cycles
            // a small perf improvement - but they all add up.

            days = (int)PerfDat.PageLifeValue / 86400;
            secs = (int)PerfDat.PageLifeValue % 86400;
            

            /*
            // do this to make sure we actually pass in data to the rest of the processing
            // otherwise we will only pass in zero.
            
            if (days > 0)
            {
                //secs -= days * 86400;
                secs = days % 86400;
            }
            else
            {
                secs = (int)PerfDat.PageLifeValue % 86400;
                //secs = 304704 % 86400;
            }
            */
            
            hours = secs / 3600;
            secs = secs % 3600;
            
            //secs -= hours * 3600;

            mins = secs / 60;
            secs = secs % 60;
            
            //secs -= mins * 60;

            lblLifeExpect.Text = days.ToString("0") + " D " + hours.ToString("0") + " H " + mins.ToString("0") + " M " + secs.ToString("0") + " S";

            if (PerfDat.PageLifeValue < MyappConfig.thresPageLife)
            {
                lblLifeExpect.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblLifeExpect.ForeColor = System.Drawing.Color.Black;
            }

            #endregion

            // Stop check
            if (!KeepRunning)
            {
                return;
            }

            // Just in case we are updating the disk counters
            while (MyappConfig.TSleep)
            {
                Thread.Sleep(1000);
            }

            #region Disk Information
            // Disk access
            lblDiskReads.Text   = PerfDat.DiskReadsSecValue.ToString("0.00");
            lblDiskWrites.Text = PerfDat.DiskWritesSecValue.ToString("0.00");
            lblLazyWrites.Text = PerfDat.LazyWritesValue.ToString("0.00");

            if (PerfDat.LazyWritesValue > 20)
            {
                lblLazyWrites.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblLazyWrites.ForeColor = System.Drawing.Color.Black;
            }

            lblReadAheads.Text = PerfDat.ReadAheadsValue.ToString("0.00");
            lblCheckpoints.Text = PerfDat.CheckpointsValue.ToString("0.00");
            lblLogFlushes.Text = PerfDat.LogFlushesValue.ToString("0.00");

            // Disk information
            lblAvgDiskReads.Text = PerfDat.AvgDiskReadValue.ToString("0.00");

            // Color code the disk counters
            if (PerfDat.AvgDiskReadValue > 0.15 && PerfDat.AvgDiskReadValue < 0.25)
            {
                lblAvgDiskReads.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (PerfDat.AvgDiskReadValue > 0.25)
            {
                lblAvgDiskReads.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblAvgDiskReads.ForeColor = System.Drawing.Color.Black;
            }


            lblAvgDiskWrites.Text = PerfDat.AvgDiskWriteValue.ToString("0.00");

            if (PerfDat.AvgDiskWriteValue > 0.15 && PerfDat.AvgDiskWriteValue < 0.25)
            {
                lblAvgDiskWrites.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (PerfDat.AvgDiskWriteValue > 0.25)
            {
                lblAvgDiskWrites.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblAvgDiskWrites.ForeColor = System.Drawing.Color.Black;
            }

            lblDiskBytesRead.Text = (PerfDat.DiskReadBytesValue / 1024).ToString("0.00");
            lblDiskBytesWrite.Text = (PerfDat.DiskWriteBytesValue / 1024).ToString("0.00");
            lblDiskQueue.Text = PerfDat.AvgDiskQueueValue.ToString("0.00");
            lblDiskIdle.Text = PerfDat.DiskIdleValue.ToString("0.00");

            if (PerfDat.DiskIdleValue < MyappConfig.thresDiskIdle)
            {
                lblDiskIdle.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblDiskIdle.ForeColor = System.Drawing.Color.Black;
            }

            #endregion

            // Stop check
            if (!KeepRunning)
            {
                return;
            }

            #region TempDB Stats

            lblActiveTempTables.Text = PerfDat.TempActiveTablesValue.ToString("0.00");
            lblWorkTblCreate.Text = PerfDat.WorkTablesValue.ToString("0.00");
            lblWorkFilesCreate.Text = PerfDat.WorkFilesValue.ToString("0.00");

            LogSpacePercent = (PerfDat.TempLogSpaceUsedValue / PerfDat.TempDBLogSizeValue) * 100;

            // Calc and display the size of the database
            if (PerfDat.TempDBSizeValue > 1024)
            {
                lblTempDBSize.Text = (PerfDat.TempDBSizeValue / 1024).ToString("0.00 MB");
            }
            else
            {
                lblTempDBSize.Text = PerfDat.TempDBSizeValue.ToString("0.00 KB");
            }

            // and do the same for the log file
            if (PerfDat.TempDBLogSizeValue > 1024)
            {
                lblTempDBLogSize.Text = (PerfDat.TempDBLogSizeValue / 1024).ToString("0.00 MB") + " (" + LogSpacePercent.ToString("0.00") + " %)";
            }
            else
            {
                lblTempDBLogSize.Text = PerfDat.TempDBLogSizeValue.ToString("0.00 KB") + " (" + LogSpacePercent.ToString("0.00") + " %)";
            }

            lblTempObjectCreate.Text = PerfDat.TempObjectCreateValue.ToString("0.00");
            lblTempObjectDestroy.Text = PerfDat.TempObjectDestroyValue.ToString("0.00");

            #endregion

            // Stop check
            if (!KeepRunning)
            {
                return;
            }

            #region Log To CSV
            // and now write out the data according to the selected interval
            // since the thread sleeps every 1 seconds - we should be in sync (almost ;-))
            if (MyappConfig.LogData)
            {
                if (++siIndex >= siInternal)
                {
                    sw.Write(DateTime.Now + "," + lblTransSec.Text + "," + PerfDat.CPUValue.ToString() + "," + lblCompiles.Text + "," + lblRecompiles.Text + "," + SQLTargetMem.ToString() + "," + SQLTotalMem.ToString() + "," + BufferCacheSize.ToString() + "," + PerfDat.BufferHitRatioValue.ToString() + "," + lblLocksSec.Text + "," + lblPageLookups.Text + "," + PerfDat.PageLifeValue.ToString() + "," + lblUserConnections.Text + "," + lblLazyWrites.Text + "," + lblReadAheads.Text + "," + lblCheckpoints.Text + "," + lblBatches.Text + "," + lblDiskReads.Text + "," + lblDiskWrites.Text + "," + lblLogFlushes.Text + "," + lblLoginsSec.Text + "," + ProcCache.ToString() + "," + PerfDat.ProcHitRatioValue.ToString() + "," + lblPagesSec.Text + "," + lblProcQueueLength.Text + "," + lblLockWaits.Text + "," + lblLockWaitTime.Text + "," + lblConMem.Text + "," + lblWorkTblCreate.Text + "," + lblWorkFilesCreate.Text + "," + PerfDat.TempDBSizeValue + "," + PerfDat.TempDBLogSizeValue + "," + lblTempObjectCreate.Text + "," + lblTempObjectDestroy.Text + "," + lblActiveTempTables.Text + "," + PerfDat.TempLogSpaceUsedValue + "," + PerfDat.FrePTEsValue + "," + PerfDat.PagedPoolValue + "," + PerfDat.NonPagedPoolValue + "," + lbldtc.Text + "," + PerfDat.AvailSysRam.ToString() + "," + lblStolenPages.Text + "," + lblGrantsPending.Text + "," + lblFullScans.Text + "," + lblDiskIdle.Text + "," + lblAvgDiskReads.Text + "," + lblAvgDiskWrites.Text + "," + lblDiskQueue.Text + "," + lblDiskReads.Text + "," + lblDiskWrites.Text + "," + lblProcSys.Text + "," + lblProcUser.Text + "," + lblProcTotal.Text + "," + lblBlocked.Text + "," + lblActive.Text + "," + lblLogicalConnections.Text + "\n");
                    siIndex = 0;
                }
            }

            #endregion
        }
        /*
        
        */
        /* END OF FUNCTIONS FOR CALLBACK */

        #region Event Handlers

        
        /// <summary>
        /// Will display the dialog displaying info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 dlg = new AboutBox1();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        // Runs when you pres this button
        private void btnDiskUpdate_Click(object sender, EventArgs e)
        {
            if (Stopped)
            {
                // Do Nothing - for now.  might display something here later on
            }
            else
            {
                try
                {
                    Log.WriteLine(DateTime.Now + " Pausing thread to update disk counters");
                    // disable the button so they don't spam it
                    btnDiskUpdate.Enabled = false;
                    MyappConfig.TSleep = true;

                    Log.WriteLine(DateTime.Now + " Removing old disk counters");
                    // Flush the existing disk counters
                    PerfDat.RemoveDiskCounters();

                    Log.WriteLine(DateTime.Now + " Adding new disk counters for dive " + txtDrive.Text);
                    // Add the new instance
                    PerfDat.AddDiskCounters(txtDrive.Text, ServerName);

                    // Are we logging this data to CSV ?
                    if (MyappConfig.LogData)
                    {
                        // Yes so write out new header into
                        Log.WriteLine(DateTime.Now + " adding new headings to CSV file");

                        sw.Write("\n\n");
                        WriteHeaderInfo();
                    }

                    Log.WriteLine(DateTime.Now + " Resuming");
                    MyappConfig.TSleep = false;
                    // re-enable now we are done.
                    btnDiskUpdate.Enabled = true;
                }

                catch (Exception MyE)
                {
                    Log.WriteLine(DateTime.Now + "ERROR UPDATING DISK " + MyE.Message);
                }

                // Need To make sure the app continues running even if the disk update failed.
                finally
                {
                    MyappConfig.TSleep = false;
                    // re-enable now we are done.
                    btnDiskUpdate.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Will open a new dialg to alow setting some options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOptions_Click(object sender, EventArgs e)
        {
            // create the dialog
            Options dlg = new Options();
            dlg.LogData = MyappConfig.LogData;
            dlg.PALData = MyappConfig.PAL;
            dlg.SetLogDataCheck(MyappConfig.LogData, MyappConfig.PAL);           
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // use if else combination otherwise the form level variables won't pickup
                // if the user selects to turn off logging.
                if (dlg.LogData)
                {
                    MyappConfig.LogData = true;
                    siInternal = dlg.si;
                }
                else
                {
                    MyappConfig.LogData = false;
                }

                if (dlg.PALData)
                {
                    MyappConfig.PAL = true;
                }
                else
                {
                    MyappConfig.PAL = false;
                }
            }

            dlg.Dispose();
        }

        // Handle SQL Authentication
        private void chkSQLAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (!Loading)
            {
                if (chkSQLAuth.Checked)
                {
                    SQLAuthDlg dlg = new SQLAuthDlg();
                    dlg.UID = SQLUser;
                    dlg.PASS = SQLPass;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        SQLUser = dlg.UID;
                        SQLPass = dlg.PASS;

                        // flag to say we need to use different windows crededtials
                        if (dlg.UseAlt == true)
                        {
                            WinUser = dlg.WUSER;
                            WinPass = dlg.WPASS;
                            UseAltWin = true;
                        }
                        else
                        {
                            // flag to say we don't
                            UseAltWin = false;
                        }

                        UseSQLAuth = true;
                    }
                    else
                    {
                        chkSQLAuth.Checked = false;
                        chkSQLAuth.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    UseSQLAuth = false;
                    UseAltWin = false;
                }
            }
        }

        /// <summary>
        /// Called when a user clicks the Procedure Cache Link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkProcCache_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }

            ProcCacheDetails dlg;

            dlg = new ProcCacheDetails(MyappConfig.pre2005);
            dlg.ProcCon = Conn;


            //dlg.RenderInfo();

            // OK so we start this in a new thread, and we waint until it's finished
            // using a 2nd thread rather than a background worker because its easier to 
            // check completion and progress this way (less code).
            // ultimatly both do the same job.
            ProcCacheT = new Thread(Form1.ProcCacheThreadFunc);
            ProcCacheT.Start(dlg);

            Processing box = new Processing();
            box.Show();

            while (ProcCacheT.IsAlive)
            {
                box.IncBarPos();
                Thread.Sleep(100);
            }

            box.Dispose();

            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Function to display the proc cache dialog.
        /// Using a Thread so that the main form does not block
        /// and give the user the impression that is has crashed
        /// </summary>
        public static void ProcCacheThreadFunc(object theDlg)
        {
            // grab the dialog object and call it's render method
            ProcCacheDetails t = (ProcCacheDetails)theDlg;
            t.RenderInfo();
        }

        /// <summary>
        /// Called when the user clicks the SQL Memory Link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkSQLMemory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }

            SQLMemory dlg = new SQLMemory(MyappConfig.pre2005, MyappConfig.is2014);
            dlg.ProcCon = Conn;

            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Called when the user clicks the proceses link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkTotalProc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessInfo dlg;

            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }


            dlg = new ProcessInfo(MyappConfig.pre2005);
            dlg.ProcCon = Conn;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Called when the users clicks the loocks link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLocks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Locks dlg;

            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }

            dlg = new Locks(MyappConfig.pre2005);
            dlg.ProcCon = Conn;

            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Called when the user clicks the users link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkUsers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UsrCons dlg;

            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }


            dlg = new UsrCons(MyappConfig.pre2005);
            dlg.ProcCon = Conn;          
            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Called to enable connection to a cluster node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkClustered_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClustered.Checked)
            {
                IsClustered = true;
                txtClusterNode.Enabled = true;
                lblSQLLabel.Text = "SQL Virtual Name";
                txtServer.Text = "";
            }
            else
            {
                IsClustered = false;
                txtClusterNode.Enabled = false;
                lblSQLLabel.Text = "SQL Server Name";
                txtServer.Text = SystemInformation.ComputerName;
            }
        }  
        
     

        private void lnkLatchWaits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }

            if (MyappConfig.pre2005)
            {
                MessageBox.Show("Detailed Data not available on this version of SQL Server");
                return;
            }

            
            LatchStats dlg = new LatchStats();
            dlg.ProcCon = Conn;

            dlg.ShowDialog();
            dlg.Dispose();
        }

        #endregion

        /// <summary>
        /// Called to view the lock information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Locks dlg;

            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }


            dlg = new Locks(MyappConfig.pre2005);
            dlg.ProcCon = Conn;

            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Called to provide information about the top 20 expensive queries.
        /// Opens a new dialog to provide the information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkTopQueires_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // We only want to do this for 2005 and above.
            // will modify this later to include 2000
            if (MyappConfig.pre2005)
            {
                MessageBox.Show("This feature is not available on this version of SQL at the moment");
                return;
            }

            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }

            QueryInfo dlg = new QueryInfo(MyappConfig.pre2005);
            dlg.ProcCon = Conn;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Called to produce a dialog for viewing detailed info from TempDB via DVM
        /// Available in 2005 + only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkTempDBDMV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // We only want to do this for 2005 and above.
            // will modify this later to include 2000
            if (MyappConfig.pre2005)
            {
                MessageBox.Show("This feature is not available on this version of SQL at the moment");
                return;
            }

            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }

            TempDBInfo dlg = new TempDBInfo(MyappConfig.pre2005);
            dlg.ProcCon = Conn;
            dlg.ShowDialog();
            dlg.Dispose();
        }


        // When called will display DMV info on wait time
        private void lnkWaitTime_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // We only want to do this for 2005 and above.
            // will modify this later to include 2000
            //if (MyappConfig.pre2005)
            //{
            //    MessageBox.Show("This feature is not available on this version of SQL at the moment");
            //    return;
            //}

            if (!KeepRunning)
            {
                MessageBox.Show("Detailed Data not available until the application is running");
                return;
            }

            WaitInfo dlg = new WaitInfo(MyappConfig.pre2005);
            dlg.ProcCon = Conn;
            dlg.ShowDialog();
            dlg.Dispose();
        }

        /// <summary>
        /// Will save the information to a config file and will enable it to be displayed in the dropdown box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveServer_Click(object sender, EventArgs e)
        {
            // Check to see if we are adding the first one in
            if (MyappConfig.SavedServers == null)
            {
                MyappConfig.SavedServers = new NameValueCollection();
            }

            // Now we get the values and form a stirng to put it all in
            // pos1 = clustered pos2 = ClusterNodev  pos3 = sql auth pos4 = sqlauthusername pos5=sqlauthpass
            string Values = "";

            // are we clustered
            if (chkClustered.Checked)
            {
                Values = "1:" + txtClusterNode.Text + ":";
            }
            else
            {
                Values = "0::";
            }

            // using SQL Auth
            if (chkSQLAuth.Checked)
            {
                Values += "1:" + SQLUser + ":" + SQLPass;
            }
            else
            {
                Values += "0::";
            }

            // Does this entry already exist ?
            if (MyappConfig.SavedServers.Get(txtServer.Text) == null)
            {
                // No so add this to the NVP
                MyappConfig.SavedServers.Add(txtServer.Text, Values);
            }
            else
            {
                // yes so modify the existing entry
                MyappConfig.SavedServers.Set(txtServer.Text, Values);
            }

            // and save it to a file
            MyappConfig.SaveServers();

            // Update the list - empty it out first
            cmbServerList.Items.Clear();
            cmbServerList.Items.Add("(Select)");

            // loop through and add them in
            foreach (string Key in MyappConfig.SavedServers)
            {
                cmbServerList.Items.Add(Key);
            }

            cmbServerList.Text = txtServer.Text;
        }

        /// <summary>
        /// This is called when the user chnaged the drop down
        /// // Need to query the NVP list and get the other daya
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // added so that this doesn;t get run when the form loads
            if (cmbServerList.Text.Length < 1 || cmbServerList.Text.Equals(@"(Select)"))
            {
                return;
            }

            // do this so that we can see it in other event handlers
            // dirty fix - will change this later on
            Loading = true;

            // pos1 = clustered pos2 = ClusterNodev  pos3 = sql auth pos4 = sqlauthusername pos5=sqlauthpass
            String[] values = MyappConfig.SavedServers.Get(cmbServerList.Text).Split(':');

            // It's Clustered
            if (values[0].Equals("1"))
            {
                chkClustered.Checked = true;
                txtClusterNode.Text = values[1];
            }

            // SQL Auth
            if (values[2].Equals("1"))
            {
                chkSQLAuth.Checked = true;
                UseSQLAuth = true;
                SQLUser = values[3];
                SQLPass = values[4];               
            }

            // Finally set the server to connect to
            txtServer.Text = cmbServerList.Text;

            Loading = false;
        }

        // Will remove a server from the list
        private void btnRemove_Click(object sender, EventArgs e)
        {
            MyappConfig.SavedServers.Remove(cmbServerList.Text);
            MyappConfig.SaveServers();

            // Update the list
            cmbServerList.Items.Clear();
            cmbServerList.Items.Add("(Select)");
            // loop through and add them in
            foreach (string Key in MyappConfig.SavedServers)
            {
                cmbServerList.Items.Add(Key);
            }

            // Reset
            chkClustered.Checked = false;
            chkSQLAuth.Checked = false;
            txtServer.Text = "";
            cmbServerList.Text = "(Select)";
        }

        // Display the driver selection box
        private void btnDriver_Click(object sender, EventArgs e)
        {
            Driver dlg = new Driver();
      
            // process the driver
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SQLDriver = dlg.theDriver;
            }
        }       
    }
}



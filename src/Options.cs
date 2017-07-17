using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SQLMonitor
{
    public partial class Options : Form
    {
        public bool LogData, PALData;
        public int si, cpuVal;
       
        public Options()
        {
            InitializeComponent();           

            if (LogData)
            {
                this.chkLogData.Checked = true;
                this.chkLogData.CheckState = CheckState.Checked;
            }

            if (PALData)
            {
                this.chkPAL.Checked = true;
                this.chkPAL.CheckState = CheckState.Checked;
            }

            cmbSI.Items.Add(1);
            cmbSI.Items.Add(2);
            cmbSI.Items.Add(5);
            cmbSI.Items.Add(10);
            cmbSI.Items.Add(20);
            cmbSI.Items.Add(30);
            cmbSI.Items.Add(60);

            cmbPalInterval.Items.Add(1);
            cmbPalInterval.Items.Add(2);
            cmbPalInterval.Items.Add(5);
            cmbPalInterval.Items.Add(10);
            cmbPalInterval.Items.Add(15);
            cmbPalInterval.Items.Add(20);
            cmbPalInterval.Items.Add(30);

            cmbFileFormat.Items.Add("csv");
            cmbFileFormat.Items.Add("bin");
            
            #region SetControls
            // Set the controls
            trCPU.Value = Form1.MyappConfig.thresCPU;
            txtCPU.Text = Form1.MyappConfig.thresCPU.ToString();

            trCPUQueue.Value = Form1.MyappConfig.thresCPUQueue;
            txtCPUQueue.Text = Form1.MyappConfig.thresCPUQueue.ToString();

            trBuffHitRatio.Value = Form1.MyappConfig.thresBufferCache;
            txtBuffHitRatio.Text = Form1.MyappConfig.thresBufferCache.ToString();

            trProcHitRatio.Value = Form1.MyappConfig.thresProcCache;
            txtProcHitRatio.Text = Form1.MyappConfig.thresProcCache.ToString();

            trDIskIdle.Value = Form1.MyappConfig.thresDiskIdle;
            txtDiskIdle.Text = Form1.MyappConfig.thresDiskIdle.ToString();

            trPageLife.Value = Form1.MyappConfig.thresPageLife;
            txtPageLife.Text = Form1.MyappConfig.thresPageLife.ToString();

            trPagesSec.Value = Form1.MyappConfig.thresMemPagesSec;
            txtPagesSec.Text = Form1.MyappConfig.thresMemPagesSec.ToString();

            #endregion
        }

      
        public void SetLogDataCheck(bool val, bool val1)
        {
            chkLogData.Checked = val;
            chkPAL.Checked = val1;
        }

        /// <summary>
        /// Post an OK message and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            if (LogData)
            {
                si = Convert.ToInt32(cmbSI.Text);
            }

            if (PALData)
            {
                Form1.MyappConfig.PalFormat = cmbFileFormat.Text;
                Form1.MyappConfig.Palsi = Convert.ToInt32(cmbPalInterval.Text);
            }

            this.Close();
        }

        /// <summary>
        /// Just close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Called when thye user clicks the tick box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLogData.Checked)
            {
                LogData = true;
                cmbSI.Enabled = true;
                cmbSI.SelectedIndex = 0;
            }
            else
            {
                LogData = false;
                cmbSI.Enabled = false;
            }
        }

        public void AddPALCounters(string BoxName, string SQLServer)
        {
            #region Performance Counter Definition

            string[] PalCounters = new string[71]
            {@"\Processor(*)\% Processor Time",
            @"\Network Interface(*)\Bytes Total/sec",
            @"\Network Interface(*)\Current Bandwidth",
            @"\System\Processor Queue Length",
            @"\Processor(*)\% Privileged Time",
            @"\PhysicalDisk(*)\% Idle Time",
            @"\LogicalDisk(*)\% Idle Time",
            @"\PhysicalDisk(*)\Avg. Disk sec/Read",
            @"\PhysicalDisk(*)\Avg. Disk sec/Write",
            @"\LogicalDisk(*)\Avg. Disk sec/Read",
            @"\LogicalDisk(*)\Avg. Disk sec/Write",
            @"\Memory\Free System Page Table Entries",
            @"\Memory\Pool Nonpaged Bytes",
            @"\Memory\Pool Paged Bytes",
            @"\Memory\Available MBytes",
            @"\Memory\Pages/sec",
            @"\Network Interface(*)\Output Queue Length",
            @"\Processor(_Total)\% Processor Time",
            @"\Processor(_Total)\% Privileged Time",
            @"\System\Context Switches/sec",
            @"\Process(*)\Private Bytes",
            @"\Process(*)\Handle Count",
            @"\Process(*)\Thread Count",
            @"\Process(*)\% Processor Time",
            @"\Process(*)\Virtual Bytes",
            @"\LogicalDisk(C:)\Free Megabytes",
            @"\Processor(*)\% Interrupt Time",
            @"\Process(*)\Working Set",
            @"\Memory\System Cache Resident Bytes",
            @"\Process(*)\IO Data Operations/sec",
            @"\Process(*)\IO Other Operations/sec",
            @"\LogicalDisk(*)\Disk Transfers/sec",
            @"\Memory\Pages Input/sec",
            @"\Paging File(*)\% Usage",
            @"\Paging File(*)\% Usage Peak",
            @"\Process(sqlservr)\% Privileged Time",
            @"\Process(sqlservr)\% Processor Time",
            @"\" + SQLServer + @":Access Methods\Forwarded Records/sec",
            @"\" + SQLServer + @":SQL Statistics\Batch Requests/sec",
            @"\" + SQLServer + @":Access Methods\FreeSpace Scans/sec",
            @"\" + SQLServer + @":Access Methods\Full Scans/sec",
            @"\" + SQLServer + @":Access Methods\Index Searches/sec",
            @"\" + SQLServer + @":Access Methods\Page Splits/sec",
            @"\" + SQLServer + @":Access Methods\Scan Point Revalidations/sec",
            @"\" + SQLServer + @":Access Methods\Workfiles Created/sec",
            @"\" + SQLServer + @":Access Methods\Worktables Created/sec",
            @"\" + SQLServer + @":Buffer Manager\Buffer cache hit ratio",
            @"\" + SQLServer + @":Buffer Manager\Lazy writes/sec",
            @"\" + SQLServer + @":Buffer Manager\Checkpoint pages/sec",
            @"\" + SQLServer + @":Buffer Manager\Free pages",
            @"\" + SQLServer + @":Buffer Manager\Page life expectancy",
            @"\" + SQLServer + @":Buffer Manager\Page lookups/sec",
            @"\" + SQLServer + @":Buffer Manager\Page reads/sec",
            @"\" + SQLServer + @":Buffer Manager\Page writes/sec",
            @"\" + SQLServer + @":General Statistics\Logins/sec",
            @"\" + SQLServer + @":General Statistics\Logouts/sec",
            @"\" + SQLServer + @":General Statistics\User Connections",
            @"\" + SQLServer + @":Latches\Latch Waits/sec",
            @"\" + SQLServer + @":Latches\Total Latch Wait Time (ms)",
            @"\" + SQLServer + @":Memory Manager\Memory Grants Pending",
            @"\" + SQLServer + @":Memory Manager\Target Server Memory (KB)",
            @"\" + SQLServer + @":Memory Manager\Target Server Memory(KB)",
            @"\" + SQLServer + @":Memory Manager\Total Server Memory (KB)",
            @"\" + SQLServer + @":Memory Manager\Total Server Memory(KB)",
            @"\" + SQLServer + @":SQL Statistics\SQL Compilations/sec",
            @"\" + SQLServer + @":SQL Statistics\SQL Re-Compilations/sec",
            @"\" + SQLServer + @":Locks(_Total)\Lock Requests/sec",
            @"\" + SQLServer + @":Locks(_Total)\Lock Waits/sec",
            @"\" + SQLServer + @":Locks(_Total)\Lock Wait Time (ms)",
            @"\" + SQLServer + @":Locks(_Total)\Lock Timeouts (timeout > 0)/sec",
            @"\" + SQLServer + @":Locks(_Total)\Number of Deadlocks/sec"};

            #endregion

            string Path = Directory.GetCurrentDirectory();

            // make sure nothing is already there
            if (File.Exists(Path + "\\AddCounters.bat"))
            {
                ProcessStartInfo proc1 = new ProcessStartInfo(Path + "\\Cleanup.bat");
                proc1.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(proc1);

                // get rid of the files
                System.IO.File.Delete(Path + "\\Start.bat");
                System.IO.File.Delete(Path + "\\Stop.bat");
                System.IO.File.Delete(Path + "\\AddCounters.bat");
                System.IO.File.Delete(Path + "\\Counters.txt");
            }

            #region Batch File Creation
            // Open a stream is the strictly specified format so that the file is always created
            FileStream PalFS = new FileStream(Path + "\\Counters.txt", FileMode.CreateNew, FileAccess.Write, FileShare.None);
            FileStream cmdFS = new FileStream(Path + "\\AddCounters.bat", FileMode.CreateNew, FileAccess.Write, FileShare.None);

            // due to the characters we are going to write - we need to specify the Encoding
            // otherwise Windows will screw us
            StreamWriter pal = new StreamWriter(PalFS, Encoding.ASCII);
            StreamWriter cmd = new StreamWriter(cmdFS, Encoding.ASCII);

            StreamWriter ss = new StreamWriter(Path + "\\Start.bat");
            StreamWriter sss = new StreamWriter(Path + "\\Stop.bat");

            // depending on the time when this is run - this batch file
            // may already exist
            if(!File.Exists(Path + "\\Cleanup.bat"))
            {
                StreamWriter ssss = new StreamWriter(Path + "\\Cleanup.bat");

                ssss.WriteLine("logman delete mscounters");
                ssss.Flush();
                ssss.Close();
                ssss.Dispose();
            }

            ss.WriteLine("logman start mscounters");
            ss.Flush();
            ss.Close();
            ss.Dispose();

            sss.WriteLine("logman stop mscounters");
            sss.Flush();
            sss.Close();
            sss.Dispose();
           
            // write out the counters to the file
            foreach (string index in PalCounters)
            {
                pal.WriteLine("\\\\" + BoxName + index);
            }

            pal.Flush();
            pal.Close();
            pal.Dispose();
            PalFS.Close();
            PalFS.Dispose();

            // we will create the counters in .blg format so that they can be viewed and manipulated in
            // perfmon if they want to.  PAL will handle this format as well.
            // default sample interval is 15 seconds
            cmd.Write("logman create counter mscounters ");
            cmd.Write("-cf \"");
            cmd.Write(Path + "\\Counters.txt\"");
            cmd.Write(" -f " + Form1.MyappConfig.PalFormat);
            cmd.Write(" -o \"");
            cmd.WriteLine(Path + "\\" + BoxName + "_" + DateTime.Now.Ticks +"." + Form1.MyappConfig.PalFormat + "\"");
            cmd.WriteLine("logman update mscounters -si 00:" + Form1.MyappConfig.Palsi.ToString());

            cmd.Flush();
            cmd.Close();
            cmd.Dispose();
            cmdFS.Close();
            cmdFS.Dispose();

            #endregion

            ProcessStartInfo proc = new ProcessStartInfo(Path + "\\AddCounters.bat");
            proc.WindowStyle = ProcessWindowStyle.Hidden;

            Process mp = Process.Start(proc);
            mp.WaitForExit();
            mp.Dispose();
            //Process.Start(Path + "\\AddCounters.bat");

        }

        private void chkPAL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPAL.Checked)
            {
                PALData = true;

                // enable the other 2 controls as well
                cmbFileFormat.Enabled = true;
                cmbPalInterval.Enabled = true;

                cmbFileFormat.SelectedText = "bin";
                cmbPalInterval.SelectedText = "15";
            }
            else
            {
                // need to claen-up the data if they are not going to be using this
                PALData = false;

                // disable the other 2 controls as well
                cmbFileFormat.Enabled = false;
                cmbPalInterval.Enabled = false;

                string Path = Directory.GetCurrentDirectory();

                if (File.Exists(Path + "\\AddCounters.bat"))
                {
                    ProcessStartInfo proc = new ProcessStartInfo(Path + "\\Cleanup.bat");
                    proc.WindowStyle = ProcessWindowStyle.Hidden;
                    Process cu = Process.Start(proc);

                    cu.WaitForExit();
                    cu.Dispose();
                    
                    // get rid of the files
                    System.IO.File.Delete(Path + "\\Start.bat");
                    System.IO.File.Delete(Path + "\\Stop.bat");
                    System.IO.File.Delete(Path + "\\AddCounters.bat");
                    System.IO.File.Delete(Path + "\\Counters.txt");
                    System.IO.File.Delete(Path + "\\Cleanup.bat");
                }
            }
        }

        #region EventHandlers
        /// <summary>
        /// We need to update the text box to show the value in the box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trCPU_Scroll(object sender, EventArgs e)
        {
            txtCPU.Text = trCPU.Value.ToString();
            Form1.MyappConfig.thresCPU = trCPU.Value;
        }

        /// <summary>
        /// We need to update the slider bar if a user enters data in here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCPU_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trCPU.Value = Convert.ToInt32(txtCPU.Text);
                Form1.MyappConfig.thresCPU = trCPU.Value;
            }
            catch
            {
                MessageBox.Show("You have entered an invalid value");
                trCPU.Value = 75;
                txtCPU.Text = "75";
                Form1.MyappConfig.thresCPU = trCPU.Value;
            }
        }        

        private void trCPUQueue_Scroll(object sender, EventArgs e)
        {
            txtCPUQueue.Text = trCPUQueue.Value.ToString();
            Form1.MyappConfig.thresCPUQueue = trCPUQueue.Value;
        }

        private void trBuffHitRatio_Scroll(object sender, EventArgs e)
        {
            txtBuffHitRatio.Text = trBuffHitRatio.Value.ToString();
            Form1.MyappConfig.thresBufferCache = trBuffHitRatio.Value;
        }

        private void trProcHitRatio_Scroll(object sender, EventArgs e)
        {
            txtProcHitRatio.Text = trProcHitRatio.Value.ToString();
            Form1.MyappConfig.thresProcCache = trProcHitRatio.Value;
        }

        private void trPagesSec_Scroll(object sender, EventArgs e)
        {
            txtPagesSec.Text = trPagesSec.Value.ToString();
            Form1.MyappConfig.thresMemPagesSec = trPagesSec.Value;
        }

        private void trPageLife_Scroll(object sender, EventArgs e)
        {
            txtPageLife.Text = trPageLife.Value.ToString();
            Form1.MyappConfig.thresPageLife = trPageLife.Value;
        }

        private void trDIskIdle_Scroll(object sender, EventArgs e)
        {
            txtDiskIdle.Text = trDIskIdle.Value.ToString();
            Form1.MyappConfig.thresDiskIdle = trDIskIdle.Value;
        }

        private void txtCPUQueue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trCPUQueue.Value = Convert.ToInt32(txtCPUQueue.Text);
                Form1.MyappConfig.thresCPUQueue = trCPUQueue.Value;
            }
            catch
            {
                MessageBox.Show("You have entered an invalid value");
                trCPUQueue.Value = 2;
                txtCPUQueue.Text = "2";
                Form1.MyappConfig.thresCPUQueue = trCPUQueue.Value;
            }
        }

        private void txtBuffHitRatio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trBuffHitRatio.Value = Convert.ToInt32(txtBuffHitRatio.Text);
                Form1.MyappConfig.thresBufferCache = trBuffHitRatio.Value;
            }
            catch
            {
                MessageBox.Show("You have entered an invalid value");
                trBuffHitRatio.Value = 90;
                txtBuffHitRatio.Text = "90";
                Form1.MyappConfig.thresBufferCache = trBuffHitRatio.Value;
            }
        }

        private void txtProcHitRatio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trProcHitRatio.Value = Convert.ToInt32(txtProcHitRatio.Text);
                Form1.MyappConfig.thresProcCache = trProcHitRatio.Value;
            }
            catch
            {
                MessageBox.Show("You have entered an invalid value");
                trProcHitRatio.Value = 80;
                txtProcHitRatio.Text = "80";
                Form1.MyappConfig.thresProcCache = trProcHitRatio.Value;
            }
        }

        private void txtPagesSec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trPagesSec.Value = Convert.ToInt32(txtPagesSec.Text);
                Form1.MyappConfig.thresMemPagesSec = trPagesSec.Value;
            }
            catch
            {
                MessageBox.Show("You have entered an invalid value");
                trPagesSec.Value = 20;
                txtPagesSec.Text = "20";
                Form1.MyappConfig.thresMemPagesSec = trPagesSec.Value;
            }
        }

        private void txtPageLife_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trPageLife.Value = Convert.ToInt32(txtPageLife.Text);
                Form1.MyappConfig.thresPageLife = trPageLife.Value;
            }
            catch
            {
                MessageBox.Show("You have entered an invalid value");
                trPageLife.Value = 300;
                txtPageLife.Text = "300";
                Form1.MyappConfig.thresPageLife = trPageLife.Value;
            }
        }

        private void txtDiskIdle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trDIskIdle.Value = Convert.ToInt32(txtDiskIdle.Text);
                Form1.MyappConfig.thresDiskIdle = trDIskIdle.Value;
            }
            catch
            {
                MessageBox.Show("You have entered an invalid value");
                trDIskIdle.Value = 50;
                txtDiskIdle.Text = "50";
                Form1.MyappConfig.thresDiskIdle = trDIskIdle.Value;
            }
        }

        /// <summary>
        /// Will save the data to an XML file called app.config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.MyappConfig.SaveConfig();

                MessageBox.Show("Data Saved");
            }
            catch (Exception MyE)
            {
                MessageBox.Show("Failed to save the settings: " + MyE.Message);
            }
        }

        /// <summary>
        /// Reverts all the controls to the default values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefaults_Click(object sender, EventArgs e)
        {
            // If I just update teh text field, the event handler will do the rest.
            txtCPU.Text = "75";
            txtCPUQueue.Text = "2";
            txtBuffHitRatio.Text = "90";
            txtProcHitRatio.Text = "80";
            txtDiskIdle.Text = "50";
            txtPageLife.Text = "300";
            txtPagesSec.Text = "20";
        }

        
        /// <summary>
        /// Signals that the users wants to capture blocked information
        /// // may result in slower performance as we get the input buffers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBlocked_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBlocked.Checked)
            {
                Form1.MyappConfig.CaptureBlocking = true;
            }
            else
            {
                Form1.MyappConfig.CaptureBlocking = false;
            }
        }

        #endregion
    }
}

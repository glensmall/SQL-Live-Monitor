using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.Odbc;
using System.IO;
using System.Threading;

namespace SQLMonitor
{
    delegate void DoUpdate(DataSet myds);
 
    public partial class ProcCacheDetails : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005, WorkDone;
        string RowCount, DropDownText;
        bool ShowPlans, UseDelegate;
        BackgroundWorker DoRefresh;
        DoUpdate MyCallBack;

        public ProcCacheDetails(bool version)
        {
            DoRefresh = new BackgroundWorker();

            InitializeComponent();
            InitializeBackgoundWorker();

            pre2005 = version;
            cmbRows.Text = "50";
            DropDownText = "50";
            ShowPlans = false;
            UseDelegate = false;

            MyCallBack = new DoUpdate(UpdateFormData);
        }

        /// <summary>
        /// Callback to allow cross thread manipulation of form controls
        /// </summary>
        private void UpdateFormData(DataSet myds)
        {
            dgProcCache.AutoGenerateColumns = true;
            dgProcCache.DataSource = myds;
            dgProcCache.DataMember = "Table";
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgoundWorker()
        {
            DoRefresh.DoWork += new DoWorkEventHandler(DoRefresh_DoWork);
            DoRefresh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DoRefresh_RunWorkerCompleted);
            DoRefresh.ProgressChanged += new ProgressChangedEventHandler(DoRefresh_ProgressChanged);
        }

        private void DoRefresh_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkDone = false;
            UseDelegate = true;
            Processing box = new Processing();
            box.Show();

            RenderInfo();

            while (WorkDone == false)
            {
                box.IncBarPos();
                Thread.Sleep(100);
            }

            box.Dispose();
            UseDelegate = false;
        }

        
        private void DoRefresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkDone = true;
        }

        private void DoRefresh_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        
          
        /// <summary>
        /// Called when the form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcCacheDetails_Load(object sender, EventArgs e)
        {
                     
        }

        // Function to render the queries to the dialog controls.
        // Usually called when the form loads (other DMV's) but for this dialog, the function is public so that it can
        // be called prior to displaying the form - so that the user doesn't think it's hung.
        public void RenderInfo()
        {
            // How many rows to display
            if (DropDownText == "All")
            {
                RowCount = "";
            }
            else
            {
                RowCount = "top " + DropDownText;
            }

            try
            {
                if (pre2005)
                {
                    MyCom = new OdbcCommand(@"SELECT " + RowCount + @" UseCounts, RefCounts,CacheObjtype, ObjType, DB_NAME(dbid) as DatabaseName, SQL FROM syscacheobjects
                                                ORDER BY dbid,usecounts DESC,objtype");
                }
                else
                {

                    if (ShowPlans)
                    {
                        MyCom = new OdbcCommand(@"select  " + RowCount + @" cp.bucketid, cp.refcounts, qs.plan_generation_num, cp.usecounts, (cp.size_in_bytes / 1024) As size_in_kb, cp.cacheobjtype, cp.objtype, mce.in_use_count, qs.creation_time, qs.last_execution_time, qs.total_worker_time, qs.total_logical_reads, qs.total_logical_writes, qs.total_elapsed_time, mce.is_dirty, mce.disk_ios_count, mce.original_cost, mce.current_cost, qp.query_plan
                                            from sys.dm_exec_cached_plans cp
                                            left outer join sys.dm_exec_query_stats qs on qs.plan_handle = cp.plan_handle
                                            left outer join sys.dm_os_memory_cache_entries mce on mce.memory_object_address = cp.memory_object_address
                                            OUTER APPLY sys.dm_exec_query_plan(cp.[plan_handle]) AS qp
                                            order by usecounts desc");
                    }
                    else
                    {
                        MyCom = new OdbcCommand(@"select  " + RowCount + @" cp.bucketid, cp.refcounts, qs.plan_generation_num, cp.usecounts, (cp.size_in_bytes / 1024) As size_in_kb, cp.cacheobjtype, cp.objtype, mce.in_use_count, qs.creation_time, qs.last_execution_time, qs.total_worker_time, qs.total_logical_reads, qs.total_logical_writes, qs.total_elapsed_time, mce.is_dirty, mce.disk_ios_count, mce.original_cost, mce.current_cost
                                            from sys.dm_exec_cached_plans cp
                                            left outer join sys.dm_exec_query_stats qs on qs.plan_handle = cp.plan_handle
                                            left outer join sys.dm_os_memory_cache_entries mce on mce.memory_object_address = cp.memory_object_address
                                            order by usecounts desc");
                    }
                }

                
                MyCom.Connection = ProcCon;

                ds = new DataSet("ProcCacheData");
                Ad = new OdbcDataAdapter(MyCom);

                Ad.Fill(ds);

                // Enable cross thread safety (a proper pain in the rear to be honest).
                if (UseDelegate)
                {
                    this.Invoke(MyCallBack, new object[] {ds});
                }
                else
                {
                    dgProcCache.AutoGenerateColumns = true;
                    dgProcCache.DataSource = ds;
                    dgProcCache.DataMember = "Table";
                }
            }

            catch (Exception MyE)
            {
                MessageBox.Show(MyE.Message);
                WorkDone = true;
            }

            WorkDone = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open a file to write to
            string Path = Directory.GetCurrentDirectory();
            FileStream FS = new FileStream(Path + "\\ProcedureCacheDetails.csv", FileMode.Append, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(FS, Encoding.ASCII);

            // Write the data
            sw.Write("Date Time,");

            // Write out the column headings
            foreach (DataColumn col in ds.Tables["Table"].Columns)
            {
                sw.Write(col.ColumnName + ",");
            }

            sw.Write("\n");

            // now write out each row and each column
            foreach (DataRow row in ds.Tables["Table"].Rows)
            {
                sw.Write(DateTime.Now.ToString() + ",");

                for (int index = 0; index < ds.Tables["Table"].Columns.Count; index++)
                {
                    sw.Write(row[index].ToString().Replace("\r\n", " ") + ",");
                }
                sw.Write("\n");
            }

            sw.Write("\n\n");

            // Close The File
            sw.Flush();
            FS.Flush();
            sw.Close();
            FS.Close();
            sw.Dispose();
            FS.Dispose();

            MessageBox.Show("Data Saved");
        }

        /// <summary>
        /// Called when the user presses the refresh button
        /// Should just re-call the RenderInfo function to update the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //RenderInfo();
            DoRefresh.RunWorkerAsync();
        }

     
        // Does the user want to see the query plans (will slow down the dialog)
        private void chkShowPlans_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPlans.Checked == true)
            {
                ShowPlans = true;
            }
            else
            {
                ShowPlans = false;
            }
        }

        // We want to save the plan - ask them where to save it to
        private void saveExecutionPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Are we actually viewing the plans ?
            // if not, we can't save it, because we have not gone and got it
            if (ShowPlans)
            {
                // Check to make sure there is actually a plan to save
                string plan = dgProcCache.SelectedRows[0].Cells[18].Value.ToString();

                if (plan.Length < 2)
                {
                    MessageBox.Show("There is no execution plan for this cache entry");
                }
                else
                {
                    // Ask the user where to save the file
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.AddExtension = true;
                    dlg.Filter = "Execution Plan (*.sqlplan)|*.sqlplan";
                    dlg.DefaultExt = ".sqlplan";

                    // Make sure they have confirmed the save
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        string Filename = dlg.FileName;

                        // now write it out - and close the file
                        StreamWriter sw = new StreamWriter(Filename);
                        sw.Write(plan);
                        sw.Close();
                        sw.Dispose();

                        // user can now double click the .sqlplan file which will open in SSMS
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Tick [Show Execution Plans] and refresh First");
            }
        }

        private void cmbRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            // do this so that we can use it in other threads easily.
            DropDownText = cmbRows.Text;
        }

    }
}



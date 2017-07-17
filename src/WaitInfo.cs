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

namespace SQLMonitor
{
    public partial class WaitInfo : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005;

        public WaitInfo(bool version)
        {
            InitializeComponent();
            pre2005 = version;
            cmbCategory.Text = "Summary";

            if (pre2005)
            {
                cmbCategory.Enabled = false;
            }
        }

        /// <summary>
        /// Called to generate the display info
        /// </summary>
        private void RenderInfo(string Category)
        {
            try
            {

                if (pre2005)
                {
                    MyCom = new OdbcCommand(@"DBCC SQLPERF (WAITSTATS)");
                }
                else
                {
                    if (Category.Contains("Top Waits"))
                    {
                        MyCom = new OdbcCommand(@"with Waits As
                                                    (
	                                                    select wait_type, 
		                                                    wait_time_ms / 1000. As wait_time_s,
		                                                    100. * wait_time_ms / SUM(wait_time_ms) OVER() As pct,
		                                                    ROW_NUMBER() OVER(ORDER BY wait_time_ms DESC) As rn,
		                                                    100. * signal_wait_time_ms / wait_time_ms as signal_pct 
	                                                    from sys.dm_os_wait_stats
	                                                    where wait_time_ms > 0
		                                                    and wait_type not like N'%SLEEP%'
		                                                    and wait_type not like N'%IDLE%'
		                                                    and wait_type not like N'%QUEUE%'
		                                                    and wait_type not in( N'CLR_AUTO_EVENT', N'REQUEST_FOR_DEADLOCK_SEARCH', N'SQLTRACE_BUFFER_FLUSH')
                                                    )
                                                    select W1.wait_type,
		                                                    CAST(W1.wait_time_s As NUMERIC(12, 2)) As Wait_Time_In_Seconds,
		                                                    CAST(W1.pct As NUMERIC(5,2)) As Percentage,
		                                                    CAST(SUM(W2.pct) As NUMERIC(5, 2)) As Running_Percent,
		                                                    CAST(W1.signal_pct As NUMERIC(5, 2)) As Signal_Percent
                                                    From Waits W1
	                                                    JOIN Waits As W2 ON W2.rn <= W1.rn
                                                    group by W1.rn, W1.wait_type, W1.wait_time_s, W1.pct, W1.signal_pct
                                                    having SUM(W2.pct) - W1.pct < 80 /* <-- This is our threshold percent value */
	                                                    or W1.rn <= 5
                                                    order by W1.rn");
                    }
                    else if (Category.Contains("Summary"))
                    {
                        MyCom = new OdbcCommand(@"select wait_type, waiting_tasks_count, wait_time_ms, max_wait_time_ms, signal_wait_time_ms
                                                    from sys.dm_os_wait_stats
                                                    order by wait_time_ms desc");
                    }

                    else if (Category.Contains("Schedulers"))
                    {
                        MyCom = new OdbcCommand(@"select count(t.thread_address) As NumThreads, count(w.waiting_task_address) As WaitingWorkers, s.scheduler_id, s.cpu_id
                                                    from sys.dm_os_threads t
                                                    inner join sys.dm_os_schedulers s on s.scheduler_address = t.scheduler_address
                                                    left outer join sys.dm_os_workers tt on tt.thread_address = t.thread_address
                                                    left outer join sys.dm_os_waiting_tasks w on w.waiting_task_address = tt.task_address
                                                    group by s.scheduler_id, s.cpu_id");
                    }
                }          

                MyCom.Connection = ProcCon;

                ds = new DataSet("ProcCacheData");
                Ad = new OdbcDataAdapter(MyCom);
                Ad.Fill(ds);

                dgLocks.AutoGenerateColumns = true;
                dgLocks.DataSource = ds;
                dgLocks.DataMember = "Table";
            }

            catch (Exception LockE)
            {
                MessageBox.Show(LockE.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open a file to write to
            string Path = Directory.GetCurrentDirectory();
            FileStream FS = new FileStream(Path + "\\Wait_Stats_" + cmbCategory.Text + ".csv", FileMode.Append, FileAccess.Write, FileShare.None);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RenderInfo(cmbCategory.Text);
        }

        private void WaitInfo_Load(object sender, EventArgs e)
        {
            // Use the initialised value on the combo box as a default
            RenderInfo(cmbCategory.Text);
        }       
    }
}

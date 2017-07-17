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
    public partial class ProcessInfo : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005;
        string RowCount;

        public ProcessInfo(bool version)
        {
            InitializeComponent();
            pre2005 = version;
            cmbRows.Text = "100";
        }

        private void ProcessInfo_Load(object sender, EventArgs e)
        {
            RenderInfo();
        }

        private void RenderInfo()
        {
            // How many rows to display
            if (cmbRows.Text == "All")
            {
                RowCount = "";
            }
            else
            {
                RowCount = "top " + cmbRows.Text;
            }

            try
            {
                if (pre2005)
                {
                    MyCom = new OdbcCommand(@"select " + RowCount + @" sp.spid,  sp.login_time, DB_NAME(sp.dbid) As DatabaseName,sp.hostname, sp.program_name, sp.loginame, sp.net_address, sp.net_library, sp.blocked, sp.lastwaittype, sp.waitresource, sp.cmd, sp.cpu, sp.physical_io, sp.memusage, sp.open_tran, sp.status
                                                from sysprocesses sp");
                }
                else
                {
                    MyCom = new OdbcCommand(@"select " + RowCount + @" a.session_id, a.login_time, db.name As DatabaseName, a.host_name, a.program_name, a.login_name, conn.client_net_address, conn.net_transport, a.status, req.wait_time, req.wait_type, req.wait_resource, req.blocking_session_id, a.cpu_time, a.memory_usage, a.total_scheduled_time, a.total_elapsed_time, a.reads, a.writes, a.is_user_process, a.row_count, sched.scheduler_id, sched.cpu_id, sched.preemptive_switches_count, sched.context_switches_count, sched.current_tasks_count, sched.current_workers_count, sched.active_workers_count, sched.work_queue_count
                                                from sys.dm_exec_sessions a
                                                left outer join sys.dm_os_tasks c on c.session_id = a.session_id
                                                left outer join sys.dm_exec_requests req on req.session_id = a.session_id
                                                left outer join sys.databases db on db.database_id = req.database_id
                                                left outer join sys.dm_os_schedulers sched on sched.scheduler_id = c.scheduler_id
                                                left outer join sys.dm_exec_connections conn on conn.session_id = a.session_id
                                                order by a.session_id");
                }


                MyCom.Connection = ProcCon;

                ds = new DataSet("ProcCacheData");
                Ad = new OdbcDataAdapter(MyCom);
                Ad.Fill(ds);

                dgProcInfo.AutoGenerateColumns = true;
                dgProcInfo.DataSource = ds;
                dgProcInfo.DataMember = "Table";
            }

            catch (Exception MyE)
            {
                MessageBox.Show(MyE.Message);
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
            FileStream FS = new FileStream(Path + "\\SQLProcessInformation.csv", FileMode.Append, FileAccess.Write, FileShare.None);
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
            RenderInfo();
        }
    }
}

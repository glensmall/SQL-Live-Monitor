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
    public partial class Locks : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005;

        public Locks(bool version)
        {
            InitializeComponent();
            pre2005 = version;
        }

        private void Locks_Load(object sender, EventArgs e)
        {
            // for now just call render
            // can expand on this a bit later depending on the version etc .....
            RenderInfo();
        }

        /// <summary>
        /// Called to generate the display info
        /// </summary>
        private void RenderInfo()
        {
            try
            {
                if (pre2005)
                {
                    
                    MyCom = new OdbcCommand(@"select sp.spid,  sp.login_time, DB_NAME(sp.dbid) As DatabaseName,sp.hostname, sp.program_name, sp.loginame, sp.net_address, sp.net_library, sp.blocked, sp.lastwaittype, sp.waitresource, sp.cmd, sp.cpu, sp.physical_io, sp.memusage, sp.open_tran, sp.status
                                                from sysprocesses sp
                                                where sp.waitresource != ''");
                     
                }
                else
                {
                    // Use this DMV if we are above 2000 as it;s more relevent info really.
                    MyCom = new OdbcCommand(@"select resource_type, resource_subtype, resource_database_id, resource_description, resource_associated_entity_id, resource_lock_partition, request_mode, request_type, request_status, request_reference_count, request_lifetime, request_session_id, request_exec_context_id, request_request_id, request_owner_type, request_owner_id, request_owner_lockspace_id from sys.dm_tran_locks");
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
            FileStream FS = new FileStream(Path + "\\SQLLocks.csv", FileMode.Append, FileAccess.Write, FileShare.None);
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

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
    public partial class UsrCons : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005;
        string RowCount;

        public UsrCons(bool version)
        {
            InitializeComponent();
            pre2005 = version;
            cmbRows.Text = "100";
        }

        private void UsrCons_Load(object sender, EventArgs e)
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
                    MyCom = new OdbcCommand(@"select " + RowCount + @" sp.spid, sp.login_time, DB_NAME(sp.dbid) As DatabaseName,sp.hostname, sp.program_name, sp.loginame, sp.net_address, sp.net_library, sp.blocked, sp.lastwaittype, sp.waitresource, sp.cmd, sp.cpu, sp.physical_io, sp.memusage, sp.open_tran, sp.status
                                                from sysprocesses sp
                                                where sp.hostname != ''");
                }
                else
                {
                    MyCom = new OdbcCommand(@"select " + RowCount + @" c.session_id, req.command, p.program_name, c.connect_time, c.net_transport, c.protocol_type, c.protocol_version, c.endpoint_id, c.encrypt_option, c.auth_scheme, c.node_affinity, c.num_reads, c.num_writes, c.last_read, c.last_write, c.net_packet_size, c.client_net_address, c.client_tcp_port, c.local_net_address, c.local_tcp_port, c.connection_id, c.parent_connection_id 
                                        from sys.dm_exec_connections c
                                        inner join sys.sysprocesses p on p.spid = c.session_id
					left outer join sys.dm_exec_requests req on req.session_id = c.session_id");
                }

                MyCom.Connection = ProcCon;

                ds = new DataSet("ProcCacheData");
                Ad = new OdbcDataAdapter(MyCom);
                Ad.Fill(ds);

                dgCons.AutoGenerateColumns = true;
                dgCons.DataSource = ds;
                dgCons.DataMember = "Table";

                // display the number of user connections at the bottom
                lblUserCons.Text = ds.Tables[0].Rows.Count.ToString();
            }

            catch (Exception MyE)
            {
                MessageBox.Show(MyE.Message);
            }
        }

        /// <summary>
        /// Called by the user to save the on-screen data to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open a file to write to
            string Path = Directory.GetCurrentDirectory();
            FileStream FS = new FileStream(Path + "\\UserConnections.csv", FileMode.Append, FileAccess.Write, FileShare.None);
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

        // Closes the windows
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RenderInfo();
        }
    }
}

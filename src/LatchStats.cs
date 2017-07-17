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
    public partial class LatchStats : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005;

        public LatchStats()
        {
            InitializeComponent();

            
        }

        /// <summary>
        /// Called to generate the display info
        /// </summary>
        private void RenderInfo()
        {
            try
            {

                MyCom = new OdbcCommand(@"select * from sys.dm_os_latch_stats order by waiting_requests_count desc");
                

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Open a file to write to
            string Path = Directory.GetCurrentDirectory();
            FileStream FS = new FileStream(Path + "\\SQLLatchStats.csv", FileMode.Append, FileAccess.Write, FileShare.None);
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

        private void LatchStats_Load(object sender, EventArgs e)
        {
            RenderInfo();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RenderInfo();
        }        
    }
}

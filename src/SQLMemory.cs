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
    public partial class SQLMemory : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        bool pre2005, is2014;

        public SQLMemory(bool pre, bool is14)
        {
            pre2005 = pre;
            is2014 = is14;
            InitializeComponent();

           
        }

        private void SQLMemory_Load(object sender, EventArgs e)
        {
            RenderInfo();
        }

        private void RenderInfo()
        {
            try
            {
                if (pre2005)
                {
                    MyCom = new OdbcCommand(@"DBCC MEMORYSTATUS");
                }
                else if(is2014)
                {
                    MyCom = new OdbcCommand(@"select type, name, memory_node_id, pages_kb, virtual_memory_reserved_kb, virtual_memory_committed_kb, awe_allocated_kb, shared_memory_reserved_kb, shared_memory_committed_kb, page_size_in_bytes from sys.dm_os_memory_clerks");
                }
                else
                {
                    //MyCom = new OdbcCommand(@"DBCC MEMORYSTATUS");
                    MyCom = new OdbcCommand(@"select type, name, memory_node_id, single_pages_kb, multi_pages_kb, virtual_memory_reserved_kb, virtual_memory_committed_kb, awe_allocated_kb, shared_memory_reserved_kb, shared_memory_committed_kb, page_size_bytes from sys.dm_os_memory_clerks");
                }

                MyCom.Connection = ProcCon;

                ds = new DataSet("ProcCacheData");
                Ad = new OdbcDataAdapter(MyCom);
                Ad.Fill(ds);

                
                // create a new table if we're pre 2005
                if (pre2005)
                {
                    DataTable dt = new DataTable("Table");
                    DataColumn column;
                    DataRow row;
                    DataView view;

                    // add the first column
                    column = new DataColumn();
                    column.DataType = Type.GetType("System.String");
                    column.ColumnName = "Key";
                    dt.Columns.Add(column);

                    // add the second column
                    column = new DataColumn();
                    column.DataType = Type.GetType("System.String");
                    column.ColumnName = "Value";
                    dt.Columns.Add(column);

                    // Now we have defined a new data table we can populate it from all these
                    foreach (DataTable dat in ds.Tables)
                    {
                        // write the column names first
                        row = dt.NewRow();
                        row["Key"] = dat.Columns[0].ColumnName.ToUpper();
                        row["Value"] = dat.Columns[1].ColumnName.ToUpper();
                        dt.Rows.Add(row);

                        // add a spacer
                        row = dt.NewRow();
                        row["Key"] = "";
                        row["Value"] = "";
                        dt.Rows.Add(row);

                        // now add sub values
                        foreach (DataRow dr in dat.Rows)
                        {
                            row = dt.NewRow();
                            row["Key"] = dr[0].ToString();
                            row["Value"] = dr[1].ToString();
                            dt.Rows.Add(row);
                        }

                        // add a spacer (or 2
                        row = dt.NewRow();
                        row["Key"] = "";
                        row["Value"] = "";
                        dt.Rows.Add(row);

                        row = dt.NewRow();
                        row["Key"] = "";
                        row["Value"] = "";
                        dt.Rows.Add(row);
                        
                    }

                    // configure the form to display it
                    view = new DataView(dt);
                    dgSQLMemory.AutoGenerateColumns = true;
                    dgSQLMemory.DataSource = view;
                    //dgSQLMemory.DataMember = "Table";
                }
                else
                {
                    dgSQLMemory.AutoGenerateColumns = true;
                    dgSQLMemory.DataSource = ds;

                    dgSQLMemory.DataMember = "Table";
                }
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


        /// <summary>
        /// Will save off the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Open a file to write to
            string Path = Directory.GetCurrentDirectory();
            FileStream FS = new FileStream(Path + "\\SQLMemory.csv", FileMode.Append, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(FS, Encoding.ASCII);

            foreach (DataTable dt in ds.Tables)
            {
                // Write the data
                sw.Write("Date Time,");

                // Write out the column headings
                foreach (DataColumn col in dt.Columns)
                {
                    sw.Write(col.ColumnName + ",");
                }

                sw.Write("\n");

                // now write out each row and each column
                foreach (DataRow row in dt.Rows)
                {
                    sw.Write(DateTime.Now.ToString() + ",");

                    for (int index = 0; index < dt.Columns.Count; index++)
                    {
                        sw.Write(row[index].ToString().Replace("\r\n", " ") + ",");
                    }
                    sw.Write("\n");
                }

                sw.Write("\n\n");
            }

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
            if (pre2005)
            {
                RenderInfo();
            }
            else
            {
                RenderInfo();
            }
        }
    }
}

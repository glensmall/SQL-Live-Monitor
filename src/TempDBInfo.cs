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
    public partial class TempDBInfo : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005;

        public TempDBInfo(bool version)
        {
            InitializeComponent();
            pre2005 = version;
            cmbCategory.Text = "Summary";
        }

        /// <summary>
        /// Called to generate the display info
        /// </summary>
        private void RenderInfo(string Category)
        {
            try
            {
                /*  This will come in later
                if (pre2005)
                {
                    MyCom = new OdbcCommand(@"");
                }
                else
                {
                    MyCom = new OdbcCommand(@"");
                }
                */

                if (Category.Contains("Waiting Workers"))
                {
                    MyCom = new OdbcCommand(@"SELECT session_id, wait_duration_ms, resource_description, wait_type
                                                FROM sys.dm_os_waiting_tasks
                                                WHERE resource_description like '2:%' AND wait_type LIKE 'PAGE%LATCH%'
                                                ORDER BY wait_duration_ms DESC");
                }
                else if (Category.Contains("Summary"))
                {
                    MyCom = new OdbcCommand(@"SELECT
	                                            SUM(user_object_reserved_page_count) * 8.192 AS UserObjectsKB,
	                                            SUM(internal_object_reserved_page_count) * 8.192 AS InternalObjectsKB,
	                                            SUM(version_store_reserved_page_count) * 8.192 AS VersonStoreKB,
	                                            SUM(unallocated_extent_page_count) * 8.192 AS FreeSpaceKB
                                            FROM sys.dm_db_file_space_usage");
                }
                else if (Category.Contains("Deallocations"))
                {
                    MyCom = new OdbcCommand(@"SELECT TOP 10
	                                            tsu.session_id, tsu.request_id, tsu.task_alloc, tsu.task_dealloc,
	                                            erq.command, erq.database_id, DB_NAME(erq.database_id) AS [database_name],
	                                            (SELECT SUBSTRING([text], statement_start_offset/2 + 1,
		                                            (CASE WHEN statement_end_offset = -1
			                                            THEN LEN(CONVERT(nvarchar(max), [text])) * 2
			                                            ELSE statement_end_offset
			                                            END - statement_start_offset) / 2
		                                            )
		                                            FROM sys.dm_exec_sql_text(erq.[sql_handle])) AS query_text,
		                                            qp.query_plan
                                            FROM
	                                            (SELECT session_id, request_id, 
		                                            SUM(internal_objects_alloc_page_count + user_objects_alloc_page_count) as task_alloc,
		                                            SUM(internal_objects_dealloc_page_count + user_objects_dealloc_page_count) as task_dealloc
	                                             FROM sys.dm_db_task_space_usage
	                                             GROUP BY session_id, request_id) AS tsu
	                                            INNER JOIN sys.dm_exec_requests AS erq ON tsu.session_id = erq.session_id AND tsu.request_id = erq.request_id
	                                            OUTER APPLY sys.dm_exec_query_plan(erq.[plan_handle]) AS qp
                                            WHERE tsu.session_id > 50 AND database_id >= 5
                                            ORDER BY tsu.task_alloc DESC");
                }
                else if(Category.Contains("TempDB IO"))
                {
                    MyCom = new OdbcCommand(@"SELECT db_name(database_id) As DatabaseName, sample_ms, num_of_reads, num_of_bytes_read, num_of_writes, num_of_bytes_written, file_id, io_stall_read_ms, io_stall_write_ms, size_on_disk_bytes
                                                FROM sys.dm_io_virtual_file_stats(NULL, NULL)
                                                WHERE DB_NAME([database_id]) = 'tempdb'");
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
            FileStream FS = new FileStream(Path + "\\TEMPDB_" + cmbCategory.Text + ".csv", FileMode.Append, FileAccess.Write, FileShare.None);
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
        
        /// <summary>
        /// Called when the form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempDBInfo_Load(object sender, EventArgs e)
        {
            // Use the initialised value on the combo box as a default
            RenderInfo(cmbCategory.Text);
        }
    }
}

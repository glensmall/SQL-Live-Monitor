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
    public partial class QueryInfo : Form
    {
        public OdbcConnection ProcCon;
        OdbcCommand MyCom;
        OdbcDataAdapter Ad;
        DataSet ds;
        public bool pre2005;

        public QueryInfo(bool version)
        {
            InitializeComponent();
            pre2005 = version;
            cmbCategory.Text = "CPU";
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

                if(Category.Contains("CPU"))
                {
                    MyCom = new OdbcCommand(@"select 
                                                highest_cpu_queries.last_execution_time,
                                                highest_cpu_queries.execution_count,
                                                q.[text],
                                                highest_cpu_queries.total_worker_time,
                                                qp.value As DatabaseID,
                                                q.objectid,
                                                q.number,
                                                q.encrypted
                                                -- highest_cpu_queries.plan_handle
                                            from 
                                                (select top 20 
                                                    qs.last_execution_time,
                                                    qs.execution_count,
                                                    qs.plan_handle, 
                                                    qs.total_worker_time
                                                from 
                                                    sys.dm_exec_query_stats qs
                                                order by qs.total_worker_time desc) as highest_cpu_queries
                                                cross apply sys.dm_exec_sql_text(plan_handle) as q
                                                cross apply sys.dm_exec_plan_attributes(plan_handle) as qp
                                            WHERE DATEDIFF(hour, last_execution_time, getdate()) < 1 -- change hour time frame 
                                            and qp.attribute = 'dbid' 
                                            order by highest_cpu_queries.total_worker_time desc");
                }
                else if (Category.Contains("IO"))
                {
                    MyCom = new OdbcCommand(@"select top 20 getdate() as logtime
                                                , 	rank() over (order by total_logical_reads+total_logical_writes desc,sql_handle,statement_start_offset ) as row_no
                                                ,       (rank() over (order by total_logical_reads+total_logical_writes desc,sql_handle,statement_start_offset ))%2 as l1
                                                ,       creation_time
                                                ,       last_execution_time
                                                ,       (total_worker_time+0.0)/1000 as total_worker_time
                                                ,       (total_worker_time+0.0)/(execution_count*1000) as [AvgCPUTime]
                                                ,       total_logical_reads as [LogicalReads]
                                                ,       total_logical_writes as [LogicalWrites]
                                                ,       execution_count
                                                ,       total_logical_reads+total_logical_writes as [AggIO]
                                                ,       (total_logical_reads+total_logical_writes)/(execution_count+0.0) as [AvgIO]
                                                ,       case when sql_handle IS NULL
                                                                then ' '
                                                                else ( substring(st.text,(qs.statement_start_offset+2)/2,
			                                                (case when qs.statement_end_offset = -1        
				                                                then len(convert(nvarchar(MAX),st.text))*2      
				                                                else qs.statement_end_offset    
			                                                end - qs.statement_start_offset) /2  ) )
                                                        end as query_text 
                                                ,       qp.value as DatabaseID
                                                ,       st.objectid as object_id
                                                from sys.dm_exec_query_stats  qs
                                                cross apply sys.dm_exec_sql_text(sql_handle) st
                                                cross apply sys.dm_exec_plan_attributes(plan_handle) as qp
                                                where total_logical_reads+total_logical_writes > 0 
                                                and qp.attribute = 'dbid' 
                                                order by [AggIO]  desc");
                }
                else if (Category.Contains("Exec Count"))
                {
                    MyCom = new OdbcCommand(@"SELECT TOP 20 getdate() as logtime, 
                                                qs.execution_count,qs.plan_generation_num,
                                                SUBSTRING(qt.text,qs.statement_start_offset/2, 
                                                          (case when qs.statement_end_offset = -1 
                                                          then len(convert(nvarchar(max), qt.text)) * 2 
                                                          else qs.statement_end_offset end -qs.statement_start_offset)/2) 
                                                    as query_text,
                                                    qt.dbid, dbname=db_name(qt.dbid),
                                                    qt.objectid 
                                        FROM sys.dm_exec_query_stats qs
                                        cross apply sys.dm_exec_sql_text(qs.sql_handle) as qt
                                        ORDER BY qs.execution_count DESC");
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
            FileStream FS = new FileStream(Path + "\\" + cmbCategory.Text + ".csv", FileMode.Append, FileAccess.Write, FileShare.None);
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
        /// Called when the form loads.  This will call the render function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryInfo_Load(object sender, EventArgs e)
        {
            // Use the initialised value on the combo box as a default
            RenderInfo(cmbCategory.Text);
        }
    }
}

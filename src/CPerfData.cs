using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Text;
using System.Windows.Forms;
//using System.Drawing;

namespace SQLMonitor
{
    public class CPerfData
    { 
        // create an array to hold the counter objects
        // make sit easy for me to clean-up when we are done
        public ArrayList PerfCounters;
        public ArrayList DiskCounters;

        // We're using public variables rather than accessors because it;s way way faster to get and set at runtime using this method.
        // Sure this isn;t standard coding practise, but this optimisation really works
        public int UserProcesses;
        public int SystemProcesses;
        public int TotalProcesses;
        public int ActiveProcesses;
        public int BlockedProcesses;
        public int Hosts;
        public int Waits;
        public float AvgWaitTime;
        public float TransactionsSecValue;
        public int CPUValue;
        public float CompilesSecValue;
        public float TargetMemoryValue;
        public float BufferCacheSizeValue;
        public float BufferHitRatioValue;
        public float LocksSecValue;
        public float PageLookupsValue;
        public float TotalMemoryValue;
        public float UserConnectionsValue;
        public float PageLifeValue;
        public float LazyWritesValue;
        public float ReadAheadsValue;
        public float CheckpointsValue;
        public float BatchesSecValue;
        public float DiskReadsSecValue;
        public float DiskWritesSecValue;
        public float LogFlushesValue;
        public float LoginsSecValue;
        public float MemPagesSecValue;
        public float ProcCacheValue;
        public float ProcHitRatioValue;
        //public float LogUsedValue;
        public float RecompilesSecValue;
        public float MsdtcValue;
        //public float LockWaitsValue;
        //public float AvgLockWaitTimeValue;
        //public float IOLatchWaits;
        public float AvailSysRam;
        public long ResponseMS;
        public long StolenPages;
        public long GrantsPending;
        public float FullScansSec;
        public float ProcQueueLengthValue;
        public float ConnectionMemoryValuel;
        public float WorkTablesValue;
        public float WorkFilesValue;
        public float TempDBSizeValue;
        public float TempDBLogSizeValue;
        public float TempLogSpaceUsedValue;
        public float TempObjectCreateValue;
        public float TempObjectDestroyValue;
        public float TempActiveTablesValue;
        public float FrePTEsValue;
        public float NonPagedPoolValue;
        public float PagedPoolValue;

        public PerformanceCounter FreePTEs;
        public PerformanceCounter NonPagedPool;
        public PerformanceCounter PagedPool;
        public PerformanceCounter TempLogSpaceUsed;
        public PerformanceCounter WorkTables;
        public PerformanceCounter WorkFiles;
        public PerformanceCounter TempDBSize;
        public PerformanceCounter TempDBLogSize;
        public PerformanceCounter TempObjectCreate;
        public PerformanceCounter TempObjectDestroy;
        public PerformanceCounter TempActiveTables;
        public PerformanceCounter AvailableSystemRam;
        public PerformanceCounter TransactionsSec;
        public PerformanceCounter CPUPercent;
        public PerformanceCounter CompilesSec;
        public PerformanceCounter TargetMemory;
        public PerformanceCounter TotalMemory;
        public PerformanceCounter BufferCacheSize;
        public PerformanceCounter BufferCacheHitRatio;
        public PerformanceCounter LocksPerSecond;
        public PerformanceCounter UserPageLookups;
        public PerformanceCounter PageLife;
        public PerformanceCounter UserConnections;
        public PerformanceCounter LazyWrites;
        public PerformanceCounter ReadAheads;
        public PerformanceCounter Checkpoints;
        public PerformanceCounter BatchesSec;
        public PerformanceCounter DiskReads;
        public PerformanceCounter DiskWrites;
        public PerformanceCounter LogFlushes;
        public PerformanceCounter LoginsSec;
        //public PerformanceCounter LogicalConnections;
        public PerformanceCounter ProcedureCacheSize;
        public PerformanceCounter ProcedureCacheHitRatio;
        //public PerformanceCounter LogFileSize;
        //public PerformanceCounter LogFileSizeUsed;
        public PerformanceCounter RecompilesSec;
        public PerformanceCounter msdtc;
        public PerformanceCounter MemPagesSec;
        public PerformanceCounter MemGrantsPending;
        public PerformanceCounter StolePages;
        public PerformanceCounter FullScans;
        public PerformanceCounter ProcQueueLength;
        public PerformanceCounter LockWaits;
        public PerformanceCounter AvgLockWaitTime;
        public PerformanceCounter ConnectionMemory;

        // This number is the array size that I need to hold the counters
        // increase it each time you add a new perf counter.
        int PerfCounterArraySize = 42;

        public float DiskIdleValue;
        public float AvgDiskReadValue;
        public float AvgDiskWriteValue;
        public float AvgDiskQueueValue;
        public float DiskReadBytesValue;
        public float DiskWriteBytesValue;

        public PerformanceCounter DiskIdleTime;
        public PerformanceCounter AvgDiskReadSec;
        public PerformanceCounter AvgDiskWriteSec;
        public PerformanceCounter AvgDiskQueueLength;
        public PerformanceCounter DiskReadBytesSec;
        public PerformanceCounter DiskWriteBytesSec;
        
        // constructor - creates the arrays that I will use
        public CPerfData()
        {
            // I use a static array for the main perf counters because this will be faster that adding and extending the array each time
            // I need to add a counter to it.  No need to do this on the disk counters as there are so few.
            PerfCounters = new ArrayList(PerfCounterArraySize);
            DiskCounters = new ArrayList();

        }
             
        /// <summary>
        /// Removes all the counters
        /// </summary>
        public void FlushCounters()
        {
            // dispose of all the counter
            foreach (PerformanceCounter perf in PerfCounters)
            {
                if (perf != null)
                {
                    perf.Close();
                    perf.Dispose();
                }
            }

            PerfCounters.Clear();

            foreach (PerformanceCounter p in DiskCounters)
            {
                p.Close();
                p.Dispose();
            }

            DiskCounters.Clear();
        }

        /// <summary>
        /// overrided to do the cleanup
        /// </summary>
        public void Dispose()
        {
            // dispose of all the counter
            FlushCounters();
        }

        /// <summary>
        /// Adds disk counters
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="TheBox"></param>
        public void AddDiskCounters(string instance, string TheBox)
        {
           
            DiskCounters.Clear();

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Adding Disk Counters. Server Parm = [" + TheBox + "]  Instance Param = [" + instance + "]");

            Form1.Log.WriteLine(DateTime.Now +" " +  "Adding Logical Disk Idle Time");
            DiskIdleTime = new PerformanceCounter("LogicalDisk", @"% Idle Time", instance, TheBox);
            DiskCounters.Add(DiskIdleTime);

            Form1.Log.WriteLine(DateTime.Now +" " +  "Adding Logical Disk Avg. Disk sec/Read");
            AvgDiskReadSec = new PerformanceCounter("LogicalDisk", "Avg. Disk sec/Read", instance, TheBox);
            DiskCounters.Add(AvgDiskReadSec);

            Form1.Log.WriteLine(DateTime.Now +" " +  "Adding Logical Disk Avg. Disk sec/Write");
            AvgDiskWriteSec = new PerformanceCounter("LogicalDisk", "Avg. Disk sec/Write", instance, TheBox);
            DiskCounters.Add(AvgDiskWriteSec);

            Form1.Log.WriteLine(DateTime.Now +" " +  "Adding Logical Disk Avg. Disk Queue Length");
            AvgDiskQueueLength = new PerformanceCounter("LogicalDisk", "Avg. Disk Queue Length", instance, TheBox);
            DiskCounters.Add(AvgDiskQueueLength);

            Form1.Log.WriteLine(DateTime.Now +" " +  "Adding Logical Disk Disk Read Bytes/sec");
            DiskReadBytesSec = new PerformanceCounter("LogicalDisk", "Disk Read Bytes/sec", instance, TheBox);
            DiskCounters.Add(DiskReadBytesSec);

            Form1.Log.WriteLine(DateTime.Now +" " +  "Adding Logical Disk Disk Write Bytes.sec");
            DiskWriteBytesSec = new PerformanceCounter("LogicalDisk", "Disk Write Bytes/sec", instance, TheBox);
            DiskCounters.Add(DiskWriteBytesSec);
        }

        // remove all the disk counters
        public void RemoveDiskCounters()
        {
            foreach (PerformanceCounter p in DiskCounters)
            {
                p.Close();
                p.Dispose();
            }

            DiskCounters.Clear();
        }

        /// <summary>
        /// Adds the primary counter pack
        /// </summary>
        /// <param name="svrcounter"></param>
        /// <param name="instance"></param>
        /// <param name="TheBox"></param>
        /// <param name="pre2005"></param>
        public void AddCounters(string svrcounter, string instance, string TheBox, bool pre2005, bool denali, bool is14)
        {
            PerfCounters.Clear();
            int index = 0;

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Adding Main Perfmon Counters. SQL Param = [" + svrcounter + "]  Server Parm = [" + TheBox + "]  Instance Param = [" + instance + "]");

            // create the counters and add them to the array
            // define their callback functions and define the variable that will hold the perf data

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Databases - Transactions/sec");
            TransactionsSec = new PerformanceCounter("" + svrcounter + @":Databases", @"Transactions/sec", instance, TheBox);
            PerfCounters.Insert(index++, TransactionsSec);

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Processor - % Processor Time");
            CPUPercent = new PerformanceCounter("Processor", @"% Processor Time", instance, TheBox);
            PerfCounters.Insert(index++, CPUPercent);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":SQL Statistics - SQL Compilations/sec");
            CompilesSec = new PerformanceCounter("" + svrcounter + @":SQL Statistics", "SQL Compilations/sec", null, TheBox);
            PerfCounters.Insert(index++, CompilesSec);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":SQL Statistics - SQL Re-Compilations/sec");
            RecompilesSec = new PerformanceCounter("" + svrcounter + @":SQL Statistics", "SQL Re-Compilations/sec", null, TheBox);
            PerfCounters.Insert(index++, RecompilesSec);

            if (pre2005)
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Memory Manager - Target Server Memory(KB)");
                TargetMemory = new PerformanceCounter("" + svrcounter + @":Memory Manager", "Target Server Memory(KB)", null, TheBox);
                PerfCounters.Insert(index++, TargetMemory);
            }
            else
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Memory Manager - Target Server Memory (KB)");
                TargetMemory = new PerformanceCounter("" + svrcounter + @":Memory Manager", "Target Server Memory (KB)", null, TheBox);
                PerfCounters.Insert(index++, TargetMemory);
            }

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Memory Manager - Total Server Memory (KB)");
            TotalMemory = new PerformanceCounter("" + svrcounter + @":Memory Manager", "Total Server Memory (KB)", null, TheBox);
            PerfCounters.Insert(index++, TotalMemory);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Database pages");
            BufferCacheSize = new PerformanceCounter("" + svrcounter + @":Buffer Manager", "Database pages", null, TheBox);
            PerfCounters.Insert(index++, BufferCacheSize);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Buffer cache hit ratio");
            BufferCacheHitRatio = new PerformanceCounter("" + svrcounter + @":Buffer Manager", "Buffer cache hit ratio", null, TheBox);
            PerfCounters.Insert(index++, BufferCacheHitRatio);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Locks - Lock Requests/sec");
            LocksPerSecond = new PerformanceCounter("" + svrcounter + @":Locks", @"Lock Requests/sec", instance, TheBox);
            PerfCounters.Insert(index++, LocksPerSecond);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Page lookups/sec");
            UserPageLookups = new PerformanceCounter("" + svrcounter + @":Buffer Manager", "Page lookups/sec", null, TheBox);
            PerfCounters.Insert(index++, UserPageLookups);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Page life expectancy");
            PageLife = new PerformanceCounter("" + svrcounter + @":Buffer Manager", "Page life expectancy", null, TheBox);
            PerfCounters.Insert(index++, PageLife);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":General Statistics - User Connections");
            UserConnections = new PerformanceCounter("" + svrcounter + @":General Statistics", "User Connections", null, TheBox);
            PerfCounters.Insert(index++, UserConnections);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Lazy writes/sec");
            LazyWrites = new PerformanceCounter("" + svrcounter + @":Buffer Manager", @"Lazy writes/sec", null, TheBox);
            PerfCounters.Insert(index++, LazyWrites);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Readahead pages/sec");
            ReadAheads = new PerformanceCounter("" + svrcounter + @":Buffer Manager", @"Readahead pages/sec", null, TheBox);
            PerfCounters.Insert(index++, ReadAheads);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Checkpoint pages/sec");
            Checkpoints = new PerformanceCounter("" + svrcounter + @":Buffer Manager", @"Checkpoint pages/sec", null, TheBox);
            PerfCounters.Insert(index++, Checkpoints);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":SQL Statistics - Batch Requests/sec");
            BatchesSec = new PerformanceCounter("" + svrcounter + @":SQL Statistics", @"Batch Requests/sec", null, TheBox);
            PerfCounters.Insert(index++, BatchesSec);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Page reads/sec");
            DiskReads = new PerformanceCounter("" + svrcounter + @":Buffer Manager", @"Page reads/sec", null, TheBox);
            PerfCounters.Insert(index++, DiskReads);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Page writes/sec");
            DiskWrites = new PerformanceCounter("" + svrcounter + @":Buffer Manager", @"Page writes/sec", null, TheBox);
            PerfCounters.Insert(index++, DiskWrites);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Databases - Log Flushes/sec");
            LogFlushes = new PerformanceCounter("" + svrcounter + @":Databases", @"Log Flushes/sec", instance, TheBox);
            PerfCounters.Insert(index++, LogFlushes);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":General Statistics - Logins/sec");
            LoginsSec = new PerformanceCounter("" + svrcounter + @":General Statistics", @"Logins/sec", null, TheBox);
            PerfCounters.Insert(index++, LoginsSec);


            //LogicalConnections = new PerformanceCounter("" + svrcounter + @":General Statistics", "Logical Connections", null, TheBox);
            //PerfCounters.Insert(index++, LogicalConnections);

            if (pre2005)
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Cache Manager - Cache Pages  ");
                ProcedureCacheSize = new PerformanceCounter("" + svrcounter + @":Cache Manager", "Cache Pages", "Procedure Plans", TheBox);
                PerfCounters.Insert(index++, ProcedureCacheSize);

                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Cache Manager - Cache Hit Ratio");
                ProcedureCacheHitRatio = new PerformanceCounter("" + svrcounter + @":Cache Manager", "Cache Hit Ratio", "Procedure Plans", TheBox);
                PerfCounters.Insert(index++, ProcedureCacheHitRatio);
            }
            else
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Plan Cache - Cache Pages");
                ProcedureCacheSize = new PerformanceCounter("" + svrcounter + @":Plan Cache", "Cache Pages", "SQL Plans", TheBox);
                PerfCounters.Insert(index++, ProcedureCacheSize);

                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Plan Cache - Cache Hit Ratio");
                ProcedureCacheHitRatio = new PerformanceCounter("" + svrcounter + @":Plan Cache", "Cache Hit Ratio", "SQL Plans", TheBox);
                PerfCounters.Insert(index++, ProcedureCacheHitRatio);
            }

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Memory - Pages/sec");
            MemPagesSec = new PerformanceCounter("Memory", @"Pages/sec", null, TheBox);
            PerfCounters.Insert(index++, MemPagesSec);

            Form1.Log.WriteLine(DateTime.Now +" " +  @"System - Processor Queue Length");
            ProcQueueLength = new PerformanceCounter("System", @"Processor Queue Length", null, TheBox);
            PerfCounters.Insert(index++, ProcQueueLength);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Locks - Lock Waits/sec");
            LockWaits = new PerformanceCounter("" + svrcounter + @":Locks", @"Lock Waits/sec", @"_Total", TheBox);
            PerfCounters.Insert(index++, LockWaits);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Locks - Average Wait Time (ms)");
            AvgLockWaitTime = new PerformanceCounter("" + svrcounter + @":Locks", @"Average Wait Time (ms)", @"_Total", TheBox);
            PerfCounters.Insert(index++, AvgLockWaitTime);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Memory Manager - Connection Memory (KB)");
            ConnectionMemory = new PerformanceCounter("" + svrcounter + @":Memory Manager", @"Connection Memory (KB)", null, TheBox);
            PerfCounters.Insert(index++, ConnectionMemory);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Access Methods - Worktables Created/sec");
            WorkTables = new PerformanceCounter("" + svrcounter + @":Access Methods", @"Worktables Created/sec", null, TheBox);
            PerfCounters.Insert(index++, WorkTables);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Access Methods - Workfiles Created/sec");
            WorkFiles = new PerformanceCounter("" + svrcounter + @":Access Methods", @"Workfiles Created/sec", null, TheBox);
            PerfCounters.Insert(index++, WorkFiles);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Databases - Data File(s) Size (KB)");
            TempDBSize = new PerformanceCounter("" + svrcounter + @":Databases", @"Data File(s) Size (KB)", "tempdb", TheBox);
            PerfCounters.Insert(index++, TempDBSize);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Databases - Log File(s) Size (KB)");
            TempDBLogSize = new PerformanceCounter("" + svrcounter + @":Databases", @"Log File(s) Size (KB)", "tempdb", TheBox);
            PerfCounters.Insert(index++, TempDBLogSize);

            // Not available on 2000
            if (pre2005)
            {
                TempObjectCreate = null;
                TempObjectDestroy = null;
                TempActiveTables = null;
                PerfCounters.Insert(index++, TempObjectCreate);
                PerfCounters.Insert(index++, TempObjectDestroy);
                PerfCounters.Insert(index++, TempActiveTables);
            }
            else
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":General Statistics - Temp Tables Creation Rate");
                TempObjectCreate = new PerformanceCounter("" + svrcounter + @":General Statistics", @"Temp Tables Creation Rate", null, TheBox);
                PerfCounters.Insert(index++, TempObjectCreate);

                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":General Statistics - Temp Tables For Destruction");
                TempObjectDestroy = new PerformanceCounter("" + svrcounter + @":General Statistics", @"Temp Tables For Destruction", null, TheBox);
                PerfCounters.Insert(index++, TempObjectDestroy);

                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":General Statistics - Active Temp Tables");
                TempActiveTables = new PerformanceCounter("" + svrcounter + @":General Statistics", @"Active Temp Tables", null, TheBox);
                PerfCounters.Insert(index++, TempActiveTables);
            }


            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Databases - Log File(s) Used Size (KB)");
            TempLogSpaceUsed = new PerformanceCounter("" + svrcounter + @":Databases", @"Log File(s) Used Size (KB)", "tempdb", TheBox);
            PerfCounters.Insert(index++, TempLogSpaceUsed);

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Memory - Free System Page Table Entries");
            FreePTEs = new PerformanceCounter("Memory", @"Free System Page Table Entries", null, TheBox);
            PerfCounters.Insert(index++, FreePTEs);

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Memory - Pool Paged Bytes");
            PagedPool = new PerformanceCounter("Memory", @"Pool Paged Bytes", null, TheBox);
            PerfCounters.Insert(index++, PagedPool);

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Memory - Pool Nonpaged Bytes");
            NonPagedPool = new PerformanceCounter("Memory", @"Pool Nonpaged Bytes", null, TheBox);
            PerfCounters.Insert(index++, NonPagedPool);

            // add new counter above this line - as dtc is easy to find in the csv formatting section

            if (pre2005)
            {
                msdtc = null;
                PerfCounters.Insert(index++, msdtc);
            }
            else
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Exec Statistics - Distributed Query");
                msdtc = new PerformanceCounter("" + svrcounter + @":Exec Statistics", @"Distributed Query", "Execs in progress", TheBox);
                PerfCounters.Insert(index++, msdtc);
            }

            Form1.Log.WriteLine(DateTime.Now +" " +  @"Memory - Available MBytes");
            AvailableSystemRam = new PerformanceCounter(@"Memory", @"Available MBytes", null, TheBox);
            PerfCounters.Insert(index++, AvailableSystemRam);

            if (denali == true || is14 == true)
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Memory Manager- Stolen Server Memory (KB)");
                StolePages = new PerformanceCounter("" + svrcounter + @":Memory Manager", @"Stolen Server Memory (KB)", null, TheBox);
                PerfCounters.Insert(index++, StolePages);
            }
            else
            {
                Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Buffer Manager - Stolen pages");
                StolePages = new PerformanceCounter("" + svrcounter + @":Buffer Manager", @"Stolen pages", null, TheBox);
                PerfCounters.Insert(index++, StolePages);
            }

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Memory Manager - Memory Grants Pending");
            MemGrantsPending = new PerformanceCounter("" + svrcounter + @":Memory Manager", @"Memory Grants Pending", null, TheBox);
            PerfCounters.Insert(index++, MemGrantsPending);

            Form1.Log.WriteLine(DateTime.Now +" " +  svrcounter + @":Access Methods - Full Scans/sec");
            FullScans = new PerformanceCounter("" + svrcounter + @":Access Methods", @"Full Scans/sec", null, TheBox);
            PerfCounters.Insert(index++, FullScans);
           
        }
    }
}

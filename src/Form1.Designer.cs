using System;

namespace SQLMonitor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Do this outside of the content below because we always want this to run
            // Check to see if exit has been clicked - if not we need to do some clean-up
            // here and now otherwise things will all go wrong
            if (!ExitClicked)
            {
                // are we actually running ?
                if (KeepRunning)
                {
                    // stop first
                    KeepRunning = false;

                    // Set flag
                    QuickQuit = true;

                    // Clean-up
                    DoCleanUp(true);
                }
            }

            // Log this all
            Log.WriteLine(DateTime.Now + " Application Closing\n\n");
            Log.Flush();
            Log.Close();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDiskUpdate = new System.Windows.Forms.Button();
            this.txtDrive = new System.Windows.Forms.TextBox();
            this.lblDiskBytesWrite = new System.Windows.Forms.Label();
            this.lblDiskBytesRead = new System.Windows.Forms.Label();
            this.lblDiskQueue = new System.Windows.Forms.Label();
            this.lblAvgDiskWrites = new System.Windows.Forms.Label();
            this.lblAvgDiskReads = new System.Windows.Forms.Label();
            this.lblDiskIdle = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.lblTest = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lnkTotalProc = new System.Windows.Forms.LinkLabel();
            this.lblBlocked = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblProcUser = new System.Windows.Forms.Label();
            this.lblProcSys = new System.Windows.Forms.Label();
            this.lblProcTotal = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lnkUsers = new System.Windows.Forms.LinkLabel();
            this.lblResponse = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblUserConnections = new System.Windows.Forms.Label();
            this.lblLogicalConnections = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblLoginsSec = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblConMem = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.lblStolenPages = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblGrantsPending = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lnkSQLMemory = new System.Windows.Forms.LinkLabel();
            this.lnkProcCache = new System.Windows.Forms.LinkLabel();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblProcCachePercent = new System.Windows.Forms.Label();
            this.lblProcCache = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblLifeExpect = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblBufferHitRatioPercent = new System.Windows.Forms.Label();
            this.lblBufferCacheSize = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblTargetMemory = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblTotalMemory = new System.Windows.Forms.Label();
            this.lblSQLMemoryPercent = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblSQLLabel = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lnkWaitTime = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lnkLatchWaits = new System.Windows.Forms.LinkLabel();
            this.lblLockWaitTime = new System.Windows.Forms.Label();
            this.lblLockWaits = new System.Windows.Forms.Label();
            this.lblLocksSec = new System.Windows.Forms.Label();
            this.grpCPU = new System.Windows.Forms.GroupBox();
            this.lblProcQueueLength = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblCPU = new System.Windows.Forms.Label();
            this.lblTransSec = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBatches = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblPageLookups = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblCompiles = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblRecompiles = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblLogFlushes = new System.Windows.Forms.Label();
            this.lblLazyWrites = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblCheckpoints = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.lblReadAheads = new System.Windows.Forms.Label();
            this.lblDiskWrites = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.lblDiskReads = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.lblAWE = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.lbldtc = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.lblPAE = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkSQLAuth = new System.Windows.Forms.CheckBox();
            this.lblAvailSysRam = new System.Windows.Forms.Label();
            this.lbl3GB = new System.Windows.Forms.Label();
            this.lblFullScans = new System.Windows.Forms.Label();
            this.lblPagesSec = new System.Windows.Forms.Label();
            this.lblPagedPool = new System.Windows.Forms.Label();
            this.lblNonPagedPool = new System.Windows.Forms.Label();
            this.lblFreePTEs = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.label65 = new System.Windows.Forms.Label();
            this.chkClustered = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtClusterNode = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.lnkTopQueires = new System.Windows.Forms.LinkLabel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblTempDBSize = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.lnkTempDBDMV = new System.Windows.Forms.LinkLabel();
            this.lblTempObjectDestroy = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.lblTempObjectCreate = new System.Windows.Forms.Label();
            this.lblCreationRate = new System.Windows.Forms.Label();
            this.lblActiveTempTables = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.lblTempDBLogSize = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.lblWorkFilesCreate = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.lblWorkTblCreate = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.btnSaveServer = new System.Windows.Forms.Button();
            this.cmbServerList = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnDriver = new System.Windows.Forms.Button();
            this.progCPU = new GProgressBar.GProgressBar();
            this.progProcCache = new GProgressBar.GProgressBar();
            this.progBufferHitRatio = new GProgressBar.GProgressBar();
            this.progSQLMemory = new GProgressBar.GProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpCPU.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDiskUpdate);
            this.groupBox1.Controls.Add(this.txtDrive);
            this.groupBox1.Controls.Add(this.lblDiskBytesWrite);
            this.groupBox1.Controls.Add(this.lblDiskBytesRead);
            this.groupBox1.Controls.Add(this.lblDiskQueue);
            this.groupBox1.Controls.Add(this.lblAvgDiskWrites);
            this.groupBox1.Controls.Add(this.lblAvgDiskReads);
            this.groupBox1.Controls.Add(this.lblDiskIdle);
            this.groupBox1.Controls.Add(this.label64);
            this.groupBox1.Controls.Add(this.label63);
            this.groupBox1.Controls.Add(this.label62);
            this.groupBox1.Controls.Add(this.label61);
            this.groupBox1.Controls.Add(this.label60);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this.lblTest);
            this.groupBox1.Location = new System.Drawing.Point(824, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 535);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Disk Storage";
            // 
            // btnDiskUpdate
            // 
            this.btnDiskUpdate.Location = new System.Drawing.Point(110, 28);
            this.btnDiskUpdate.Name = "btnDiskUpdate";
            this.btnDiskUpdate.Size = new System.Drawing.Size(59, 23);
            this.btnDiskUpdate.TabIndex = 64;
            this.btnDiskUpdate.Text = "Update";
            this.btnDiskUpdate.UseVisualStyleBackColor = true;
            this.btnDiskUpdate.Click += new System.EventHandler(this.btnDiskUpdate_Click);
            // 
            // txtDrive
            // 
            this.txtDrive.Location = new System.Drawing.Point(22, 30);
            this.txtDrive.Name = "txtDrive";
            this.txtDrive.Size = new System.Drawing.Size(82, 20);
            this.txtDrive.TabIndex = 63;
            this.txtDrive.Text = "C:";
            // 
            // lblDiskBytesWrite
            // 
            this.lblDiskBytesWrite.Location = new System.Drawing.Point(40, 491);
            this.lblDiskBytesWrite.Name = "lblDiskBytesWrite";
            this.lblDiskBytesWrite.Size = new System.Drawing.Size(93, 13);
            this.lblDiskBytesWrite.TabIndex = 62;
            this.lblDiskBytesWrite.Text = "-";
            this.lblDiskBytesWrite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblDiskBytesWrite, "Displays the number of pages read from disk");
            // 
            // lblDiskBytesRead
            // 
            this.lblDiskBytesRead.Location = new System.Drawing.Point(40, 421);
            this.lblDiskBytesRead.Name = "lblDiskBytesRead";
            this.lblDiskBytesRead.Size = new System.Drawing.Size(93, 13);
            this.lblDiskBytesRead.TabIndex = 61;
            this.lblDiskBytesRead.Text = "-";
            this.lblDiskBytesRead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblDiskBytesRead, "Displays the number of pages read from disk");
            // 
            // lblDiskQueue
            // 
            this.lblDiskQueue.Location = new System.Drawing.Point(40, 344);
            this.lblDiskQueue.Name = "lblDiskQueue";
            this.lblDiskQueue.Size = new System.Drawing.Size(93, 13);
            this.lblDiskQueue.TabIndex = 60;
            this.lblDiskQueue.Text = "-";
            this.lblDiskQueue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblDiskQueue, "Displays the number of pages read from disk");
            // 
            // lblAvgDiskWrites
            // 
            this.lblAvgDiskWrites.Location = new System.Drawing.Point(40, 276);
            this.lblAvgDiskWrites.Name = "lblAvgDiskWrites";
            this.lblAvgDiskWrites.Size = new System.Drawing.Size(93, 13);
            this.lblAvgDiskWrites.TabIndex = 59;
            this.lblAvgDiskWrites.Text = "-";
            this.lblAvgDiskWrites.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblAvgDiskWrites, "Displays the number of pages read from disk");
            // 
            // lblAvgDiskReads
            // 
            this.lblAvgDiskReads.Location = new System.Drawing.Point(40, 204);
            this.lblAvgDiskReads.Name = "lblAvgDiskReads";
            this.lblAvgDiskReads.Size = new System.Drawing.Size(93, 13);
            this.lblAvgDiskReads.TabIndex = 58;
            this.lblAvgDiskReads.Text = "-";
            this.lblAvgDiskReads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblAvgDiskReads, "Displays the number of pages read from disk");
            // 
            // lblDiskIdle
            // 
            this.lblDiskIdle.Location = new System.Drawing.Point(40, 127);
            this.lblDiskIdle.Name = "lblDiskIdle";
            this.lblDiskIdle.Size = new System.Drawing.Size(93, 13);
            this.lblDiskIdle.TabIndex = 57;
            this.lblDiskIdle.Text = "-";
            this.lblDiskIdle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblDiskIdle, "Displays the number of pages read from disk");
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(24, 455);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(124, 13);
            this.label64.TabIndex = 30;
            this.label64.Text = "Disk KB Writes/ Sec";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(25, 382);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(122, 13);
            this.label63.TabIndex = 29;
            this.label63.Text = "Disk KB Read / Sec";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(15, 308);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(142, 13);
            this.label62.TabIndex = 28;
            this.label62.Text = "Avg Disk Queue Length";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(19, 236);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(134, 13);
            this.label61.TabIndex = 27;
            this.label61.Text = "Avg Disk Writes / Sec";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(19, 165);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(134, 13);
            this.label60.TabIndex = 26;
            this.label60.Text = "Avg Disk Reads / Sec";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(50, 96);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(72, 13);
            this.label54.TabIndex = 25;
            this.label54.Text = "% Idle Time";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(31, 59);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(0, 13);
            this.lblTest.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1036, 616);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(56, 20);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(1035, 570);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(56, 19);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(1036, 528);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 22);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lnkTotalProc);
            this.groupBox2.Controls.Add(this.lblBlocked);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lblProcUser);
            this.groupBox2.Controls.Add(this.lblProcSys);
            this.groupBox2.Controls.Add(this.lblProcTotal);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(273, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(99, 284);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL Processes";
            // 
            // lnkTotalProc
            // 
            this.lnkTotalProc.AutoSize = true;
            this.lnkTotalProc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTotalProc.Location = new System.Drawing.Point(28, 39);
            this.lnkTotalProc.Name = "lnkTotalProc";
            this.lnkTotalProc.Size = new System.Drawing.Size(36, 13);
            this.lnkTotalProc.TabIndex = 66;
            this.lnkTotalProc.TabStop = true;
            this.lnkTotalProc.Text = "Total";
            this.lnkTotalProc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTotalProc_LinkClicked);
            // 
            // lblBlocked
            // 
            this.lblBlocked.Location = new System.Drawing.Point(6, 241);
            this.lblBlocked.Name = "lblBlocked";
            this.lblBlocked.Size = new System.Drawing.Size(84, 12);
            this.lblBlocked.TabIndex = 24;
            this.lblBlocked.Text = "-";
            this.lblBlocked.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblBlocked, "Displays the number of processes inside SQL Server that are currently blocked");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(20, 214);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Blocked";
            // 
            // lblProcUser
            // 
            this.lblProcUser.Location = new System.Drawing.Point(6, 176);
            this.lblProcUser.Name = "lblProcUser";
            this.lblProcUser.Size = new System.Drawing.Size(84, 12);
            this.lblProcUser.TabIndex = 16;
            this.lblProcUser.Text = "-";
            this.lblProcUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblProcUser, "Displays the number of user processes inside SQL Server");
            // 
            // lblProcSys
            // 
            this.lblProcSys.Location = new System.Drawing.Point(6, 121);
            this.lblProcSys.Name = "lblProcSys";
            this.lblProcSys.Size = new System.Drawing.Size(84, 12);
            this.lblProcSys.TabIndex = 22;
            this.lblProcSys.Text = "-";
            this.lblProcSys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblProcSys, "Displays the number pf processes inside SQL that belong to the System");
            // 
            // lblProcTotal
            // 
            this.lblProcTotal.Location = new System.Drawing.Point(6, 60);
            this.lblProcTotal.Name = "lblProcTotal";
            this.lblProcTotal.Size = new System.Drawing.Size(84, 12);
            this.lblProcTotal.TabIndex = 15;
            this.lblProcTotal.Text = "-";
            this.lblProcTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblProcTotal, "Displays the total number of processes inside SQL Server");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(30, 152);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "User";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(23, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "System";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lnkUsers);
            this.groupBox3.Controls.Add(this.lblResponse);
            this.groupBox3.Controls.Add(this.label67);
            this.groupBox3.Controls.Add(this.lblActive);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblUserConnections);
            this.groupBox3.Controls.Add(this.lblLogicalConnections);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblLoginsSec);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(13, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(132, 395);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sessions";
            // 
            // lnkUsers
            // 
            this.lnkUsers.AutoSize = true;
            this.lnkUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUsers.Location = new System.Drawing.Point(8, 266);
            this.lnkUsers.Name = "lnkUsers";
            this.lnkUsers.Size = new System.Drawing.Size(107, 13);
            this.lnkUsers.TabIndex = 21;
            this.lnkUsers.TabStop = true;
            this.lnkUsers.Text = "User Connections";
            this.lnkUsers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUsers_LinkClicked);
            // 
            // lblResponse
            // 
            this.lblResponse.Location = new System.Drawing.Point(1, 50);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(122, 13);
            this.lblResponse.TabIndex = 20;
            this.lblResponse.Text = "-";
            this.lblResponse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblResponse, "The number of ms it takes for SQL to respond to this applications queries");
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(17, 28);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(90, 13);
            this.label67.TabIndex = 19;
            this.label67.Text = "Response (ms)";
            // 
            // lblActive
            // 
            this.lblActive.Location = new System.Drawing.Point(6, 364);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(119, 12);
            this.lblActive.TabIndex = 18;
            this.lblActive.Text = "-";
            this.lblActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblActive, "Displays the number of users with queires that are actually running");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 339);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Active Users";
            // 
            // lblUserConnections
            // 
            this.lblUserConnections.Location = new System.Drawing.Point(3, 304);
            this.lblUserConnections.Name = "lblUserConnections";
            this.lblUserConnections.Size = new System.Drawing.Size(119, 12);
            this.lblUserConnections.TabIndex = 16;
            this.lblUserConnections.Text = "-";
            this.lblUserConnections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblUserConnections, "Displays the number of user logins");
            // 
            // lblLogicalConnections
            // 
            this.lblLogicalConnections.Location = new System.Drawing.Point(3, 215);
            this.lblLogicalConnections.Name = "lblLogicalConnections";
            this.lblLogicalConnections.Size = new System.Drawing.Size(119, 24);
            this.lblLogicalConnections.TabIndex = 14;
            this.lblLogicalConnections.Text = "-";
            this.lblLogicalConnections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLogicalConnections, "Displays the number of unique machine connections");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Machines";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoginsSec
            // 
            this.lblLoginsSec.Location = new System.Drawing.Point(1, 135);
            this.lblLoginsSec.Name = "lblLoginsSec";
            this.lblLoginsSec.Size = new System.Drawing.Size(122, 13);
            this.lblLoginsSec.TabIndex = 12;
            this.lblLoginsSec.Text = "-";
            this.lblLoginsSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLoginsSec, "Displays the number of SQL logins per second");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Logins/sec";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblConMem);
            this.groupBox4.Controls.Add(this.label68);
            this.groupBox4.Controls.Add(this.lblStolenPages);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.lblGrantsPending);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lnkSQLMemory);
            this.groupBox4.Controls.Add(this.lnkProcCache);
            this.groupBox4.Controls.Add(this.label35);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.label37);
            this.groupBox4.Controls.Add(this.progProcCache);
            this.groupBox4.Controls.Add(this.lblProcCachePercent);
            this.groupBox4.Controls.Add(this.lblProcCache);
            this.groupBox4.Controls.Add(this.label40);
            this.groupBox4.Controls.Add(this.lblLifeExpect);
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.progBufferHitRatio);
            this.groupBox4.Controls.Add(this.lblBufferHitRatioPercent);
            this.groupBox4.Controls.Add(this.lblBufferCacheSize);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.lblTargetMemory);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.lblTotalMemory);
            this.groupBox4.Controls.Add(this.progSQLMemory);
            this.groupBox4.Controls.Add(this.lblSQLMemoryPercent);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Location = new System.Drawing.Point(498, 68);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(175, 535);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SQL Memory";
            // 
            // lblConMem
            // 
            this.lblConMem.AutoSize = true;
            this.lblConMem.Location = new System.Drawing.Point(113, 114);
            this.lblConMem.Name = "lblConMem";
            this.lblConMem.Size = new System.Drawing.Size(10, 13);
            this.lblConMem.TabIndex = 54;
            this.lblConMem.Text = "-";
            this.toolTip1.SetToolTip(this.lblConMem, "Displays the amount of SQL memory being used for user connections");
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(7, 112);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(101, 13);
            this.label68.TabIndex = 53;
            this.label68.Text = "Connection Mem";
            // 
            // lblStolenPages
            // 
            this.lblStolenPages.AutoSize = true;
            this.lblStolenPages.Location = new System.Drawing.Point(98, 248);
            this.lblStolenPages.Name = "lblStolenPages";
            this.lblStolenPages.Size = new System.Drawing.Size(10, 13);
            this.lblStolenPages.TabIndex = 52;
            this.lblStolenPages.Text = "-";
            this.toolTip1.SetToolTip(this.lblStolenPages, "Displays the number of 8K pages stolen from the buffer pool for other SQL memory " +
                    "needs");
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(10, 248);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(82, 13);
            this.label25.TabIndex = 51;
            this.label25.Text = "Stolen Pages";
            // 
            // lblGrantsPending
            // 
            this.lblGrantsPending.AutoSize = true;
            this.lblGrantsPending.Location = new System.Drawing.Point(107, 86);
            this.lblGrantsPending.Name = "lblGrantsPending";
            this.lblGrantsPending.Size = new System.Drawing.Size(10, 13);
            this.lblGrantsPending.TabIndex = 50;
            this.lblGrantsPending.Text = "-";
            this.toolTip1.SetToolTip(this.lblGrantsPending, "Displays any memory requests that are waiting to be satisfied");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 13);
            this.label13.TabIndex = 49;
            this.label13.Text = "Grants Pending";
            // 
            // lnkSQLMemory
            // 
            this.lnkSQLMemory.AutoSize = true;
            this.lnkSQLMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSQLMemory.Location = new System.Drawing.Point(6, 34);
            this.lnkSQLMemory.Name = "lnkSQLMemory";
            this.lnkSQLMemory.Size = new System.Drawing.Size(95, 13);
            this.lnkSQLMemory.TabIndex = 48;
            this.lnkSQLMemory.TabStop = true;
            this.lnkSQLMemory.Text = "Current Memory";
            this.lnkSQLMemory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSQLMemory_LinkClicked);
            // 
            // lnkProcCache
            // 
            this.lnkProcCache.AutoSize = true;
            this.lnkProcCache.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkProcCache.Location = new System.Drawing.Point(51, 411);
            this.lnkProcCache.Name = "lnkProcCache";
            this.lnkProcCache.Size = new System.Drawing.Size(72, 13);
            this.lnkProcCache.TabIndex = 47;
            this.lnkProcCache.TabStop = true;
            this.lnkProcCache.Text = "Plan Cache";
            this.lnkProcCache.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProcCache_LinkClicked);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(58, 465);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(57, 13);
            this.label35.TabIndex = 46;
            this.label35.Text = "Hit Ratio";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(138, 508);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(25, 13);
            this.label36.TabIndex = 45;
            this.label36.Text = "100";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(10, 508);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(13, 13);
            this.label37.TabIndex = 44;
            this.label37.Text = "0";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProcCachePercent
            // 
            this.lblProcCachePercent.AutoSize = true;
            this.lblProcCachePercent.Location = new System.Drawing.Point(80, 508);
            this.lblProcCachePercent.Name = "lblProcCachePercent";
            this.lblProcCachePercent.Size = new System.Drawing.Size(10, 13);
            this.lblProcCachePercent.TabIndex = 42;
            this.lblProcCachePercent.Text = "-";
            this.lblProcCachePercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProcCache
            // 
            this.lblProcCache.AutoSize = true;
            this.lblProcCache.Location = new System.Drawing.Point(48, 436);
            this.lblProcCache.Name = "lblProcCache";
            this.lblProcCache.Size = new System.Drawing.Size(10, 13);
            this.lblProcCache.TabIndex = 41;
            this.lblProcCache.Text = "-";
            this.toolTip1.SetToolTip(this.lblProcCache, "Displays the current size of the procedure cache");
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(7, 436);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(31, 13);
            this.label40.TabIndex = 40;
            this.label40.Text = "Size";
            // 
            // lblLifeExpect
            // 
            this.lblLifeExpect.Location = new System.Drawing.Point(10, 376);
            this.lblLifeExpect.Name = "lblLifeExpect";
            this.lblLifeExpect.Size = new System.Drawing.Size(150, 22);
            this.lblLifeExpect.TabIndex = 38;
            this.lblLifeExpect.Text = "-";
            this.lblLifeExpect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLifeExpect, "Displays the life expectancy of pages in the buffer pool");
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(20, 355);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(131, 13);
            this.label34.TabIndex = 37;
            this.label34.Text = "Page Life Expectancy";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(58, 276);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(57, 13);
            this.label33.TabIndex = 36;
            this.label33.Text = "Hit Ratio";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(138, 319);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(25, 13);
            this.label31.TabIndex = 35;
            this.label31.Text = "100";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(10, 319);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(13, 13);
            this.label32.TabIndex = 34;
            this.label32.Text = "0";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBufferHitRatioPercent
            // 
            this.lblBufferHitRatioPercent.AutoSize = true;
            this.lblBufferHitRatioPercent.Location = new System.Drawing.Point(80, 319);
            this.lblBufferHitRatioPercent.Name = "lblBufferHitRatioPercent";
            this.lblBufferHitRatioPercent.Size = new System.Drawing.Size(10, 13);
            this.lblBufferHitRatioPercent.TabIndex = 32;
            this.lblBufferHitRatioPercent.Text = "-";
            this.lblBufferHitRatioPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBufferCacheSize
            // 
            this.lblBufferCacheSize.AutoSize = true;
            this.lblBufferCacheSize.Location = new System.Drawing.Point(51, 220);
            this.lblBufferCacheSize.Name = "lblBufferCacheSize";
            this.lblBufferCacheSize.Size = new System.Drawing.Size(10, 13);
            this.lblBufferCacheSize.TabIndex = 31;
            this.lblBufferCacheSize.Text = "-";
            this.toolTip1.SetToolTip(this.lblBufferCacheSize, "Displays the current size of the buffer pool");
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(10, 220);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(31, 13);
            this.label30.TabIndex = 30;
            this.label30.Text = "Size";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(51, 189);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(81, 13);
            this.label29.TabIndex = 29;
            this.label29.Text = "Buffer Cache";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(135, 169);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(25, 13);
            this.label27.TabIndex = 28;
            this.label27.Text = "100";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTargetMemory
            // 
            this.lblTargetMemory.AutoSize = true;
            this.lblTargetMemory.Location = new System.Drawing.Point(90, 59);
            this.lblTargetMemory.Name = "lblTargetMemory";
            this.lblTargetMemory.Size = new System.Drawing.Size(10, 13);
            this.lblTargetMemory.TabIndex = 3;
            this.lblTargetMemory.Text = "-";
            this.toolTip1.SetToolTip(this.lblTargetMemory, "Displays the amount of dynamic memory SQL is willing to allocate");
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(7, 169);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(13, 13);
            this.label28.TabIndex = 27;
            this.label28.Text = "0";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalMemory
            // 
            this.lblTotalMemory.AutoSize = true;
            this.lblTotalMemory.Location = new System.Drawing.Point(105, 37);
            this.lblTotalMemory.Name = "lblTotalMemory";
            this.lblTotalMemory.Size = new System.Drawing.Size(10, 13);
            this.lblTotalMemory.TabIndex = 2;
            this.lblTotalMemory.Text = "-";
            this.toolTip1.SetToolTip(this.lblTotalMemory, "Displays the amount of dynamic memory SQL has current allocated");
            // 
            // lblSQLMemoryPercent
            // 
            this.lblSQLMemoryPercent.AutoSize = true;
            this.lblSQLMemoryPercent.Location = new System.Drawing.Point(77, 169);
            this.lblSQLMemoryPercent.Name = "lblSQLMemoryPercent";
            this.lblSQLMemoryPercent.Size = new System.Drawing.Size(10, 13);
            this.lblSQLMemoryPercent.TabIndex = 25;
            this.lblSQLMemoryPercent.Text = "-";
            this.lblSQLMemoryPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(7, 59);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(77, 13);
            this.label26.TabIndex = 1;
            this.label26.Text = "Max Memory";
            // 
            // lblSQLLabel
            // 
            this.lblSQLLabel.AutoSize = true;
            this.lblSQLLabel.Location = new System.Drawing.Point(1014, 141);
            this.lblSQLLabel.Name = "lblSQLLabel";
            this.lblSQLLabel.Size = new System.Drawing.Size(62, 13);
            this.lblSQLLabel.TabIndex = 8;
            this.lblSQLLabel.Text = "SQL Server";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(1014, 157);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(93, 20);
            this.txtServer.TabIndex = 7;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lnkWaitTime);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.linkLabel1);
            this.groupBox5.Controls.Add(this.lnkLatchWaits);
            this.groupBox5.Controls.Add(this.lblLockWaitTime);
            this.groupBox5.Controls.Add(this.lblLockWaits);
            this.groupBox5.Controls.Add(this.lblLocksSec);
            this.groupBox5.Location = new System.Drawing.Point(268, 370);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(110, 233);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Locks";
            // 
            // lnkWaitTime
            // 
            this.lnkWaitTime.AutoSize = true;
            this.lnkWaitTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkWaitTime.Location = new System.Drawing.Point(21, 128);
            this.lnkWaitTime.Name = "lnkWaitTime";
            this.lnkWaitTime.Size = new System.Drawing.Size(65, 13);
            this.lnkWaitTime.TabIndex = 72;
            this.lnkWaitTime.TabStop = true;
            this.lnkWaitTime.Text = "Avg Waits";
            this.lnkWaitTime.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWaitTime_LinkClicked);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 71;
            this.label11.Text = "Lock Waits /sec";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(14, 25);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(75, 13);
            this.linkLabel1.TabIndex = 70;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Locks / sec";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lnkLatchWaits
            // 
            this.lnkLatchWaits.AutoSize = true;
            this.lnkLatchWaits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLatchWaits.Location = new System.Drawing.Point(17, 190);
            this.lnkLatchWaits.Name = "lnkLatchWaits";
            this.lnkLatchWaits.Size = new System.Drawing.Size(72, 13);
            this.lnkLatchWaits.TabIndex = 27;
            this.lnkLatchWaits.TabStop = true;
            this.lnkLatchWaits.Text = "Latch Stats";
            this.lnkLatchWaits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLatchWaits_LinkClicked);
            // 
            // lblLockWaitTime
            // 
            this.lblLockWaitTime.Location = new System.Drawing.Point(14, 154);
            this.lblLockWaitTime.Name = "lblLockWaitTime";
            this.lblLockWaitTime.Size = new System.Drawing.Size(81, 16);
            this.lblLockWaitTime.TabIndex = 25;
            this.lblLockWaitTime.Text = "-";
            this.lblLockWaitTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLockWaitTime, "Displays the number of locks currently being waited on");
            // 
            // lblLockWaits
            // 
            this.lblLockWaits.Location = new System.Drawing.Point(8, 93);
            this.lblLockWaits.Name = "lblLockWaits";
            this.lblLockWaits.Size = new System.Drawing.Size(93, 16);
            this.lblLockWaits.TabIndex = 23;
            this.lblLockWaits.Text = "-";
            this.lblLockWaits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLockWaits, "Displays the number of locks currently being waited on");
            // 
            // lblLocksSec
            // 
            this.lblLocksSec.Location = new System.Drawing.Point(8, 42);
            this.lblLocksSec.Name = "lblLocksSec";
            this.lblLocksSec.Size = new System.Drawing.Size(93, 16);
            this.lblLocksSec.TabIndex = 21;
            this.lblLocksSec.Text = "-";
            this.lblLocksSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLocksSec, "Displays the number of lock requests per second");
            // 
            // grpCPU
            // 
            this.grpCPU.Controls.Add(this.lblProcQueueLength);
            this.grpCPU.Controls.Add(this.label2);
            this.grpCPU.Controls.Add(this.label17);
            this.grpCPU.Controls.Add(this.label16);
            this.grpCPU.Controls.Add(this.progCPU);
            this.grpCPU.Controls.Add(this.lblCPU);
            this.grpCPU.Location = new System.Drawing.Point(13, 469);
            this.grpCPU.Name = "grpCPU";
            this.grpCPU.Size = new System.Drawing.Size(132, 134);
            this.grpCPU.TabIndex = 3;
            this.grpCPU.TabStop = false;
            this.grpCPU.Text = "CPU";
            // 
            // lblProcQueueLength
            // 
            this.lblProcQueueLength.AutoSize = true;
            this.lblProcQueueLength.Location = new System.Drawing.Point(59, 107);
            this.lblProcQueueLength.Name = "lblProcQueueLength";
            this.lblProcQueueLength.Size = new System.Drawing.Size(10, 13);
            this.lblProcQueueLength.TabIndex = 26;
            this.lblProcQueueLength.Text = "-";
            this.lblProcQueueLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Queue Length";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(95, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "100";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "0";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCPU
            // 
            this.lblCPU.AutoSize = true;
            this.lblCPU.Location = new System.Drawing.Point(59, 56);
            this.lblCPU.Name = "lblCPU";
            this.lblCPU.Size = new System.Drawing.Size(10, 13);
            this.lblCPU.TabIndex = 0;
            this.lblCPU.Text = "-";
            this.lblCPU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTransSec
            // 
            this.lblTransSec.Location = new System.Drawing.Point(154, 172);
            this.lblTransSec.Name = "lblTransSec";
            this.lblTransSec.Size = new System.Drawing.Size(93, 13);
            this.lblTransSec.TabIndex = 9;
            this.lblTransSec.Text = "-";
            this.lblTransSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblTransSec, "Displays the number of SQL transactions per second sent from clients");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(151, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Transactions/sec";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = ">-->-->-->-->";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(164, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Batches/sec";
            // 
            // lblBatches
            // 
            this.lblBatches.Location = new System.Drawing.Point(154, 299);
            this.lblBatches.Name = "lblBatches";
            this.lblBatches.Size = new System.Drawing.Size(93, 13);
            this.lblBatches.TabIndex = 13;
            this.lblBatches.Text = "-";
            this.lblBatches.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblBatches, "Displays the number of batches per second sent from clients");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(166, 339);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = ">-->-->-->-->";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(378, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(114, 13);
            this.label18.TabIndex = 15;
            this.label18.Text = "Page Lookups/sec";
            // 
            // lblPageLookups
            // 
            this.lblPageLookups.Location = new System.Drawing.Point(382, 137);
            this.lblPageLookups.Name = "lblPageLookups";
            this.lblPageLookups.Size = new System.Drawing.Size(93, 13);
            this.lblPageLookups.TabIndex = 16;
            this.lblPageLookups.Text = "-";
            this.lblPageLookups.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblPageLookups, "Displays the number of pages being read from the buffer pool per second");
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(398, 164);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "<--<--<--<--<";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(394, 311);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(83, 13);
            this.label20.TabIndex = 18;
            this.label20.Text = "Compiles/sec";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(398, 376);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 19;
            this.label21.Text = ">-->-->-->-->";
            // 
            // lblCompiles
            // 
            this.lblCompiles.Location = new System.Drawing.Point(382, 340);
            this.lblCompiles.Name = "lblCompiles";
            this.lblCompiles.Size = new System.Drawing.Size(93, 13);
            this.lblCompiles.TabIndex = 20;
            this.lblCompiles.Text = "-";
            this.lblCompiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblCompiles, "Displays the number of SQL queries being compiled per second");
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(11, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(85, 13);
            this.label22.TabIndex = 23;
            this.label22.Text = "SQL Version: ";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(89, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(100, 13);
            this.lblVersion.TabIndex = 24;
            this.lblVersion.Text = "NOT CONNECTED";
            this.toolTip1.SetToolTip(this.lblVersion, "Shows the current version - Service Pack level and Product Edition of SQL Server");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(384, 449);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 13);
            this.label23.TabIndex = 25;
            this.label23.Text = "Re-Compiles/sec";
            // 
            // lblRecompiles
            // 
            this.lblRecompiles.Location = new System.Drawing.Point(382, 486);
            this.lblRecompiles.Name = "lblRecompiles";
            this.lblRecompiles.Size = new System.Drawing.Size(93, 13);
            this.lblRecompiles.TabIndex = 26;
            this.lblRecompiles.Text = "-";
            this.lblRecompiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblRecompiles, "Displays the number of SQL queires being recompiled per second");
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(716, 576);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(61, 13);
            this.label24.TabIndex = 27;
            this.label24.Text = ">-->-->-->-->";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(697, 83);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(104, 13);
            this.label38.TabIndex = 28;
            this.label38.Text = "Page Reads/Sec";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(697, 174);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(104, 13);
            this.label39.TabIndex = 29;
            this.label39.Text = "Page Writes/Sec";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(697, 257);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(111, 13);
            this.label42.TabIndex = 30;
            this.label42.Text = "Read Aheads/Sec";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(703, 344);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(105, 13);
            this.label43.TabIndex = 31;
            this.label43.Text = "Checkpoints/Sec";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(703, 428);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(101, 13);
            this.label44.TabIndex = 32;
            this.label44.Text = "Lazy Writes/Sec";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(703, 523);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(103, 13);
            this.label45.TabIndex = 33;
            this.label45.Text = "Log Flushes/Sec";
            // 
            // lblLogFlushes
            // 
            this.lblLogFlushes.Location = new System.Drawing.Point(700, 549);
            this.lblLogFlushes.Name = "lblLogFlushes";
            this.lblLogFlushes.Size = new System.Drawing.Size(93, 13);
            this.lblLogFlushes.TabIndex = 34;
            this.lblLogFlushes.Text = "-";
            this.lblLogFlushes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLogFlushes, "Displays the number fo log fluses per second");
            // 
            // lblLazyWrites
            // 
            this.lblLazyWrites.Location = new System.Drawing.Point(700, 454);
            this.lblLazyWrites.Name = "lblLazyWrites";
            this.lblLazyWrites.Size = new System.Drawing.Size(93, 13);
            this.lblLazyWrites.TabIndex = 36;
            this.lblLazyWrites.Text = "-";
            this.lblLazyWrites.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblLazyWrites, "Displays lazy writer writes per second");
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(716, 479);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(61, 13);
            this.label47.TabIndex = 35;
            this.label47.Text = ">-->-->-->-->";
            // 
            // lblCheckpoints
            // 
            this.lblCheckpoints.Location = new System.Drawing.Point(700, 370);
            this.lblCheckpoints.Name = "lblCheckpoints";
            this.lblCheckpoints.Size = new System.Drawing.Size(93, 13);
            this.lblCheckpoints.TabIndex = 38;
            this.lblCheckpoints.Text = "-";
            this.lblCheckpoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblCheckpoints, "Displays the number fo checkpoints per second");
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(716, 395);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(61, 13);
            this.label48.TabIndex = 37;
            this.label48.Text = ">-->-->-->-->";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(716, 313);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(61, 13);
            this.label46.TabIndex = 40;
            this.label46.Text = "<--<--<--<--<";
            // 
            // lblReadAheads
            // 
            this.lblReadAheads.Location = new System.Drawing.Point(700, 286);
            this.lblReadAheads.Name = "lblReadAheads";
            this.lblReadAheads.Size = new System.Drawing.Size(93, 13);
            this.lblReadAheads.TabIndex = 39;
            this.lblReadAheads.Text = "-";
            this.lblReadAheads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblReadAheads, "Displays the number of Read Aheads");
            // 
            // lblDiskWrites
            // 
            this.lblDiskWrites.Location = new System.Drawing.Point(700, 195);
            this.lblDiskWrites.Name = "lblDiskWrites";
            this.lblDiskWrites.Size = new System.Drawing.Size(93, 13);
            this.lblDiskWrites.TabIndex = 42;
            this.lblDiskWrites.Text = "-";
            this.lblDiskWrites.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblDiskWrites, "Displays the number of pages written to disk");
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(716, 220);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(61, 13);
            this.label50.TabIndex = 41;
            this.label50.Text = ">-->-->-->-->";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(716, 134);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(61, 13);
            this.label49.TabIndex = 44;
            this.label49.Text = "<--<--<--<--<";
            // 
            // lblDiskReads
            // 
            this.lblDiskReads.Location = new System.Drawing.Point(700, 107);
            this.lblDiskReads.Name = "lblDiskReads";
            this.lblDiskReads.Size = new System.Drawing.Size(93, 13);
            this.lblDiskReads.TabIndex = 43;
            this.lblDiskReads.Text = "-";
            this.lblDiskReads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblDiskReads, "Displays the number of pages read from disk");
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(398, 519);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(61, 13);
            this.label51.TabIndex = 45;
            this.label51.Text = ">-->-->-->-->";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(963, 9);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(43, 13);
            this.label52.TabIndex = 46;
            this.label52.Text = "AWE: ";
            // 
            // lblAWE
            // 
            this.lblAWE.AutoSize = true;
            this.lblAWE.Location = new System.Drawing.Point(1007, 9);
            this.lblAWE.Name = "lblAWE";
            this.lblAWE.Size = new System.Drawing.Size(100, 13);
            this.lblAWE.TabIndex = 47;
            this.lblAWE.Text = "NOT CONNECTED";
            this.toolTip1.SetToolTip(this.lblAWE, "Specified whether or not this server has Addressing Window Extensions Enabled");
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(166, 397);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(68, 13);
            this.label56.TabIndex = 48;
            this.label56.Text = "Distributed";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(164, 454);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(61, 13);
            this.label57.TabIndex = 50;
            this.label57.Text = ">-->-->-->-->";
            // 
            // lbldtc
            // 
            this.lbldtc.Location = new System.Drawing.Point(154, 423);
            this.lbldtc.Name = "lbldtc";
            this.lbldtc.Size = new System.Drawing.Size(93, 13);
            this.lbldtc.TabIndex = 49;
            this.lbldtc.Text = "-";
            this.lbldtc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbldtc, "Displays the number of SQL distributed queries");
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(642, 9);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(39, 13);
            this.label58.TabIndex = 51;
            this.label58.Text = "PAE: ";
            // 
            // lblPAE
            // 
            this.lblPAE.AutoSize = true;
            this.lblPAE.Location = new System.Drawing.Point(677, 9);
            this.lblPAE.Name = "lblPAE";
            this.lblPAE.Size = new System.Drawing.Size(100, 13);
            this.lblPAE.TabIndex = 52;
            this.lblPAE.Text = "NOT CONNECTED";
            this.toolTip1.SetToolTip(this.lblPAE, "Specifies whether or not this server has Physical Address Extensions Enabled");
            // 
            // chkSQLAuth
            // 
            this.chkSQLAuth.AutoSize = true;
            this.chkSQLAuth.Location = new System.Drawing.Point(1014, 216);
            this.chkSQLAuth.Name = "chkSQLAuth";
            this.chkSQLAuth.Size = new System.Drawing.Size(72, 17);
            this.chkSQLAuth.TabIndex = 61;
            this.chkSQLAuth.Text = "SQL Auth";
            this.toolTip1.SetToolTip(this.chkSQLAuth, "Check to use SQL Authentication.  Default is to use Integrated Auth (unchecked)");
            this.chkSQLAuth.UseVisualStyleBackColor = true;
            this.chkSQLAuth.CheckedChanged += new System.EventHandler(this.chkSQLAuth_CheckedChanged);
            // 
            // lblAvailSysRam
            // 
            this.lblAvailSysRam.AutoSize = true;
            this.lblAvailSysRam.Location = new System.Drawing.Point(151, 33);
            this.lblAvailSysRam.Name = "lblAvailSysRam";
            this.lblAvailSysRam.Size = new System.Drawing.Size(100, 13);
            this.lblAvailSysRam.TabIndex = 56;
            this.lblAvailSysRam.Text = "NOT CONNECTED";
            this.toolTip1.SetToolTip(this.lblAvailSysRam, "Shows the amount of free RAM on this server");
            // 
            // lbl3GB
            // 
            this.lbl3GB.AutoSize = true;
            this.lbl3GB.Location = new System.Drawing.Point(849, 9);
            this.lbl3GB.Name = "lbl3GB";
            this.lbl3GB.Size = new System.Drawing.Size(100, 13);
            this.lbl3GB.TabIndex = 60;
            this.lbl3GB.Text = "NOT CONNECTED";
            this.toolTip1.SetToolTip(this.lbl3GB, "Specifies whether this server was booted with the 3GB kernel memory switch");
            // 
            // lblFullScans
            // 
            this.lblFullScans.Location = new System.Drawing.Point(382, 244);
            this.lblFullScans.Name = "lblFullScans";
            this.lblFullScans.Size = new System.Drawing.Size(93, 13);
            this.lblFullScans.TabIndex = 66;
            this.lblFullScans.Text = "-";
            this.lblFullScans.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblFullScans, "Displays the number of pages being read from the buffer pool per second");
            // 
            // lblPagesSec
            // 
            this.lblPagesSec.AutoSize = true;
            this.lblPagesSec.Location = new System.Drawing.Point(486, 9);
            this.lblPagesSec.Name = "lblPagesSec";
            this.lblPagesSec.Size = new System.Drawing.Size(100, 13);
            this.lblPagesSec.TabIndex = 69;
            this.lblPagesSec.Text = "NOT CONNECTED";
            this.toolTip1.SetToolTip(this.lblPagesSec, "Shows the amount of free RAM on this server");
            // 
            // lblPagedPool
            // 
            this.lblPagedPool.AutoSize = true;
            this.lblPagedPool.Location = new System.Drawing.Point(436, 42);
            this.lblPagedPool.Name = "lblPagedPool";
            this.lblPagedPool.Size = new System.Drawing.Size(10, 13);
            this.lblPagedPool.TabIndex = 77;
            this.lblPagedPool.Text = "-";
            this.toolTip1.SetToolTip(this.lblPagedPool, "Shows the size of the Kernel Paged Pool");
            // 
            // lblNonPagedPool
            // 
            this.lblNonPagedPool.AutoSize = true;
            this.lblNonPagedPool.Location = new System.Drawing.Point(744, 42);
            this.lblNonPagedPool.Name = "lblNonPagedPool";
            this.lblNonPagedPool.Size = new System.Drawing.Size(10, 13);
            this.lblNonPagedPool.TabIndex = 78;
            this.lblNonPagedPool.Text = "-";
            this.toolTip1.SetToolTip(this.lblNonPagedPool, "Shows the size of the Kernel Non Paged Pool");
            // 
            // lblFreePTEs
            // 
            this.lblFreePTEs.AutoSize = true;
            this.lblFreePTEs.Location = new System.Drawing.Point(934, 42);
            this.lblFreePTEs.Name = "lblFreePTEs";
            this.lblFreePTEs.Size = new System.Drawing.Size(10, 13);
            this.lblFreePTEs.TabIndex = 80;
            this.lblFreePTEs.Text = "-";
            this.toolTip1.SetToolTip(this.lblFreePTEs, "Shows the number of Free SYstem Page Table Entries");
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(12, 33);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(142, 13);
            this.label59.TabIndex = 54;
            this.label59.Text = "Available System RAM: ";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(1036, 675);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(56, 24);
            this.btnAbout.TabIndex = 57;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(1035, 423);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(56, 23);
            this.btnOptions.TabIndex = 58;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(810, 9);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(41, 13);
            this.label65.TabIndex = 59;
            this.label65.Text = "/3GB:";
            // 
            // chkClustered
            // 
            this.chkClustered.AutoSize = true;
            this.chkClustered.Location = new System.Drawing.Point(1014, 253);
            this.chkClustered.Name = "chkClustered";
            this.chkClustered.Size = new System.Drawing.Size(70, 17);
            this.chkClustered.TabIndex = 64;
            this.chkClustered.Text = "Clustered";
            this.chkClustered.UseVisualStyleBackColor = true;
            this.chkClustered.CheckedChanged += new System.EventHandler(this.chkClustered_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1014, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Active Node Name";
            // 
            // txtClusterNode
            // 
            this.txtClusterNode.Enabled = false;
            this.txtClusterNode.Location = new System.Drawing.Point(1014, 298);
            this.txtClusterNode.Name = "txtClusterNode";
            this.txtClusterNode.Size = new System.Drawing.Size(100, 20);
            this.txtClusterNode.TabIndex = 62;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(389, 212);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(92, 13);
            this.label41.TabIndex = 65;
            this.label41.Text = "Full Scans/sec";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(398, 271);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(61, 13);
            this.label53.TabIndex = 67;
            this.label53.Text = "<--<--<--<--<";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(362, 9);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(127, 13);
            this.label66.TabIndex = 68;
            this.label66.Text = "Memory Pages /sec: ";
            // 
            // lnkTopQueires
            // 
            this.lnkTopQueires.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTopQueires.Location = new System.Drawing.Point(1021, 463);
            this.lnkTopQueires.Name = "lnkTopQueires";
            this.lnkTopQueires.Size = new System.Drawing.Size(86, 37);
            this.lnkTopQueires.TabIndex = 70;
            this.lnkTopQueires.TabStop = true;
            this.lnkTopQueires.Text = "Expensive Queries";
            this.lnkTopQueires.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkTopQueires.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTopQueires_LinkClicked);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblTempDBSize);
            this.groupBox6.Controls.Add(this.label71);
            this.groupBox6.Controls.Add(this.lnkTempDBDMV);
            this.groupBox6.Controls.Add(this.lblTempObjectDestroy);
            this.groupBox6.Controls.Add(this.label75);
            this.groupBox6.Controls.Add(this.lblTempObjectCreate);
            this.groupBox6.Controls.Add(this.lblCreationRate);
            this.groupBox6.Controls.Add(this.lblActiveTempTables);
            this.groupBox6.Controls.Add(this.label73);
            this.groupBox6.Controls.Add(this.lblTempDBLogSize);
            this.groupBox6.Controls.Add(this.label72);
            this.groupBox6.Controls.Add(this.lblWorkFilesCreate);
            this.groupBox6.Controls.Add(this.label70);
            this.groupBox6.Controls.Add(this.lblWorkTblCreate);
            this.groupBox6.Controls.Add(this.label69);
            this.groupBox6.Location = new System.Drawing.Point(16, 616);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(983, 85);
            this.groupBox6.TabIndex = 71;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "TempDB Statistics";
            // 
            // lblTempDBSize
            // 
            this.lblTempDBSize.AutoSize = true;
            this.lblTempDBSize.Location = new System.Drawing.Point(416, 26);
            this.lblTempDBSize.Name = "lblTempDBSize";
            this.lblTempDBSize.Size = new System.Drawing.Size(10, 13);
            this.lblTempDBSize.TabIndex = 74;
            this.lblTempDBSize.Text = "-";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(321, 54);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(56, 13);
            this.label71.TabIndex = 73;
            this.label71.Text = "Log Size";
            // 
            // lnkTempDBDMV
            // 
            this.lnkTempDBDMV.AutoSize = true;
            this.lnkTempDBDMV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTempDBDMV.Location = new System.Drawing.Point(767, 54);
            this.lnkTempDBDMV.Name = "lnkTempDBDMV";
            this.lnkTempDBDMV.Size = new System.Drawing.Size(143, 13);
            this.lnkTempDBDMV.TabIndex = 72;
            this.lnkTempDBDMV.TabStop = true;
            this.lnkTempDBDMV.Text = "Detailed TempDB Views";
            this.lnkTempDBDMV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTempDBDMV_LinkClicked);
            // 
            // lblTempObjectDestroy
            // 
            this.lblTempObjectDestroy.Location = new System.Drawing.Point(677, 54);
            this.lblTempObjectDestroy.Name = "lblTempObjectDestroy";
            this.lblTempObjectDestroy.Size = new System.Drawing.Size(84, 13);
            this.lblTempObjectDestroy.TabIndex = 35;
            this.lblTempObjectDestroy.Text = "-";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(550, 54);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(121, 13);
            this.label75.TabIndex = 34;
            this.label75.Text = "Object Destroy /sec";
            // 
            // lblTempObjectCreate
            // 
            this.lblTempObjectCreate.Location = new System.Drawing.Point(667, 26);
            this.lblTempObjectCreate.Name = "lblTempObjectCreate";
            this.lblTempObjectCreate.Size = new System.Drawing.Size(94, 13);
            this.lblTempObjectCreate.TabIndex = 33;
            this.lblTempObjectCreate.Text = "-";
            // 
            // lblCreationRate
            // 
            this.lblCreationRate.AutoSize = true;
            this.lblCreationRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreationRate.Location = new System.Drawing.Point(550, 26);
            this.lblCreationRate.Name = "lblCreationRate";
            this.lblCreationRate.Size = new System.Drawing.Size(115, 13);
            this.lblCreationRate.TabIndex = 32;
            this.lblCreationRate.Text = "Object Create /sec";
            // 
            // lblActiveTempTables
            // 
            this.lblActiveTempTables.Location = new System.Drawing.Point(895, 26);
            this.lblActiveTempTables.Name = "lblActiveTempTables";
            this.lblActiveTempTables.Size = new System.Drawing.Size(70, 13);
            this.lblActiveTempTables.TabIndex = 31;
            this.lblActiveTempTables.Text = "-";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(767, 26);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(120, 13);
            this.label73.TabIndex = 30;
            this.label73.Text = "Active Temp Tables";
            // 
            // lblTempDBLogSize
            // 
            this.lblTempDBLogSize.AutoSize = true;
            this.lblTempDBLogSize.Location = new System.Drawing.Point(383, 54);
            this.lblTempDBLogSize.Name = "lblTempDBLogSize";
            this.lblTempDBLogSize.Size = new System.Drawing.Size(10, 13);
            this.lblTempDBLogSize.TabIndex = 29;
            this.lblTempDBLogSize.Text = "-";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(321, 26);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(89, 13);
            this.label72.TabIndex = 28;
            this.label72.Text = "Database Size";
            // 
            // lblWorkFilesCreate
            // 
            this.lblWorkFilesCreate.AutoSize = true;
            this.lblWorkFilesCreate.Location = new System.Drawing.Point(162, 54);
            this.lblWorkFilesCreate.Name = "lblWorkFilesCreate";
            this.lblWorkFilesCreate.Size = new System.Drawing.Size(10, 13);
            this.lblWorkFilesCreate.TabIndex = 25;
            this.lblWorkFilesCreate.Text = "-";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(15, 54);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(141, 13);
            this.label70.TabIndex = 24;
            this.label70.Text = "Work Files Created/sec";
            // 
            // lblWorkTblCreate
            // 
            this.lblWorkTblCreate.AutoSize = true;
            this.lblWorkTblCreate.Location = new System.Drawing.Point(160, 26);
            this.lblWorkTblCreate.Name = "lblWorkTblCreate";
            this.lblWorkTblCreate.Size = new System.Drawing.Size(10, 13);
            this.lblWorkTblCreate.TabIndex = 23;
            this.lblWorkTblCreate.Text = "-";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(14, 26);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(139, 13);
            this.label69.TabIndex = 22;
            this.label69.Text = "Work Tbls Created/sec";
            // 
            // btnSaveServer
            // 
            this.btnSaveServer.Location = new System.Drawing.Point(1035, 335);
            this.btnSaveServer.Name = "btnSaveServer";
            this.btnSaveServer.Size = new System.Drawing.Size(56, 23);
            this.btnSaveServer.TabIndex = 72;
            this.btnSaveServer.Text = "Save";
            this.btnSaveServer.UseVisualStyleBackColor = true;
            this.btnSaveServer.Click += new System.EventHandler(this.btnSaveServer_Click);
            // 
            // cmbServerList
            // 
            this.cmbServerList.FormattingEnabled = true;
            this.cmbServerList.Location = new System.Drawing.Point(1014, 98);
            this.cmbServerList.MaxDropDownItems = 10;
            this.cmbServerList.MaximumSize = new System.Drawing.Size(97, 0);
            this.cmbServerList.Name = "cmbServerList";
            this.cmbServerList.Size = new System.Drawing.Size(97, 21);
            this.cmbServerList.Sorted = true;
            this.cmbServerList.TabIndex = 73;
            this.cmbServerList.SelectedIndexChanged += new System.EventHandler(this.cmbServerList_SelectedIndexChanged);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(1014, 82);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(77, 13);
            this.label55.TabIndex = 74;
            this.label55.Text = "Saved Servers";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(362, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Paged Pool:";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(642, 42);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(103, 13);
            this.label74.TabIndex = 76;
            this.label74.Text = "Non Paged Pool:";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(864, 42);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(73, 13);
            this.label78.TabIndex = 79;
            this.label78.Text = "Free PTE\'s:";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(1035, 370);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(56, 23);
            this.btnRemove.TabIndex = 81;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnDriver
            // 
            this.btnDriver.Location = new System.Drawing.Point(1036, 180);
            this.btnDriver.Name = "btnDriver";
            this.btnDriver.Size = new System.Drawing.Size(45, 21);
            this.btnDriver.TabIndex = 82;
            this.btnDriver.Text = "Driver";
            this.btnDriver.UseVisualStyleBackColor = true;
            this.btnDriver.Click += new System.EventHandler(this.btnDriver_Click);
            // 
            // progCPU
            // 
            this.progCPU.Location = new System.Drawing.Point(9, 29);
            this.progCPU.Maximum = 100;
            this.progCPU.Minimum = 0;
            this.progCPU.Name = "progCPU";
            this.progCPU.ProgressBarColor = System.Drawing.Color.Green;
            this.progCPU.Size = new System.Drawing.Size(111, 23);
            this.progCPU.TabIndex = 1;
            this.toolTip1.SetToolTip(this.progCPU, "Displays the percents of Total CPU load");
            this.progCPU.Value = 0;
            // 
            // progProcCache
            // 
            this.progProcCache.Location = new System.Drawing.Point(13, 481);
            this.progProcCache.Maximum = 100;
            this.progProcCache.Minimum = 0;
            this.progProcCache.Name = "progProcCache";
            this.progProcCache.ProgressBarColor = System.Drawing.Color.Green;
            this.progProcCache.Size = new System.Drawing.Size(150, 23);
            this.progProcCache.TabIndex = 43;
            this.toolTip1.SetToolTip(this.progProcCache, "Displays the hit ratio of the procedure cache");
            this.progProcCache.Value = 0;
            // 
            // progBufferHitRatio
            // 
            this.progBufferHitRatio.Location = new System.Drawing.Point(13, 292);
            this.progBufferHitRatio.Maximum = 100;
            this.progBufferHitRatio.Minimum = 0;
            this.progBufferHitRatio.Name = "progBufferHitRatio";
            this.progBufferHitRatio.ProgressBarColor = System.Drawing.Color.Green;
            this.progBufferHitRatio.Size = new System.Drawing.Size(150, 23);
            this.progBufferHitRatio.TabIndex = 33;
            this.toolTip1.SetToolTip(this.progBufferHitRatio, "Displays the buffer cache hit ratio");
            this.progBufferHitRatio.Value = 0;
            // 
            // progSQLMemory
            // 
            this.progSQLMemory.Location = new System.Drawing.Point(10, 142);
            this.progSQLMemory.Maximum = 100;
            this.progSQLMemory.Minimum = 0;
            this.progSQLMemory.Name = "progSQLMemory";
            this.progSQLMemory.ProgressBarColor = System.Drawing.Color.Green;
            this.progSQLMemory.Size = new System.Drawing.Size(150, 23);
            this.progSQLMemory.TabIndex = 26;
            this.toolTip1.SetToolTip(this.progSQLMemory, "Displays the percentage of memory used");
            this.progSQLMemory.Value = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1119, 713);
            this.Controls.Add(this.btnDriver);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblFreePTEs);
            this.Controls.Add(this.label78);
            this.Controls.Add(this.lblNonPagedPool);
            this.Controls.Add(this.lblPagedPool);
            this.Controls.Add(this.label74);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.cmbServerList);
            this.Controls.Add(this.btnSaveServer);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.lnkTopQueires);
            this.Controls.Add(this.lblPagesSec);
            this.Controls.Add(this.label66);
            this.Controls.Add(this.label53);
            this.Controls.Add(this.lblFullScans);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.chkClustered);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtClusterNode);
            this.Controls.Add(this.chkSQLAuth);
            this.Controls.Add(this.lbl3GB);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblAvailSysRam);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.lblPAE);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.label57);
            this.Controls.Add(this.lbldtc);
            this.Controls.Add(this.label56);
            this.Controls.Add(this.lblAWE);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.lblDiskReads);
            this.Controls.Add(this.lblDiskWrites);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.lblReadAheads);
            this.Controls.Add(this.lblCheckpoints);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.lblLazyWrites);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.lblLogFlushes);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lblRecompiles);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblCompiles);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lblPageLookups);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblBatches);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTransSec);
            this.Controls.Add(this.grpCPU);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.lblSQLLabel);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Live Monitor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpCPU.ResumeLayout(false);
            this.grpCPU.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblSQLLabel;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox grpCPU;
        private System.Windows.Forms.Label lblCPU;
        private System.Windows.Forms.Label lblTransSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblUserConnections;
        private System.Windows.Forms.Label lblLogicalConnections;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLoginsSec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBatches;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblProcUser;
        private System.Windows.Forms.Label lblProcSys;
        private System.Windows.Forms.Label lblProcTotal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblLocksSec;

        //private System.Windows.Forms.ProgressBar progCPU;
        private GProgressBar.GProgressBar progCPU;

        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblPageLookups;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblCompiles;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Label lblBlocked;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblRecompiles;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblTargetMemory;
        private System.Windows.Forms.Label lblTotalMemory;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;

        //private System.Windows.Forms.ProgressBar progSQLMemory;
        private GProgressBar.GProgressBar progSQLMemory;

        private System.Windows.Forms.Label lblSQLMemoryPercent;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;

        //private System.Windows.Forms.ProgressBar progBufferHitRatio;
        private GProgressBar.GProgressBar progBufferHitRatio;

        private System.Windows.Forms.Label lblBufferHitRatioPercent;
        private System.Windows.Forms.Label lblBufferCacheSize;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblLifeExpect;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;

        //private System.Windows.Forms.ProgressBar progProcCache;
        private GProgressBar.GProgressBar progProcCache;

        private System.Windows.Forms.Label lblProcCachePercent;
        private System.Windows.Forms.Label lblProcCache;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label lblLogFlushes;
        private System.Windows.Forms.Label lblLazyWrites;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label lblCheckpoints;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label lblReadAheads;
        private System.Windows.Forms.Label lblDiskWrites;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label lblDiskReads;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label lblAWE;
        private System.Windows.Forms.Label lblLockWaits;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label lbldtc;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label lblPAE;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblLockWaitTime;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label lblAvailSysRam;
        private System.Windows.Forms.Label lblDiskBytesWrite;
        private System.Windows.Forms.Label lblDiskBytesRead;
        private System.Windows.Forms.Label lblDiskQueue;
        private System.Windows.Forms.Label lblAvgDiskWrites;
        private System.Windows.Forms.Label lblAvgDiskReads;
        private System.Windows.Forms.Label lblDiskIdle;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnDiskUpdate;
        private System.Windows.Forms.TextBox txtDrive;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label lbl3GB;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.CheckBox chkSQLAuth;
        private System.Windows.Forms.LinkLabel lnkProcCache;
        private System.Windows.Forms.LinkLabel lnkSQLMemory;
        private System.Windows.Forms.LinkLabel lnkTotalProc;
        private System.Windows.Forms.LinkLabel lnkUsers;
        private System.Windows.Forms.CheckBox chkClustered;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtClusterNode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblStolenPages;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblGrantsPending;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label lblFullScans;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label lblPagesSec;
        private System.Windows.Forms.Label lblProcQueueLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkLatchWaits;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblConMem;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.LinkLabel lnkTopQueires;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblWorkFilesCreate;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label lblWorkTblCreate;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label lblTempDBLogSize;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label lblCreationRate;
        private System.Windows.Forms.Label lblActiveTempTables;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label lblTempObjectCreate;
        private System.Windows.Forms.LinkLabel lnkTempDBDMV;
        private System.Windows.Forms.Label lblTempObjectDestroy;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label lblTempDBSize;
        private System.Windows.Forms.LinkLabel lnkWaitTime;
        private System.Windows.Forms.Button btnSaveServer;
        private System.Windows.Forms.ComboBox cmbServerList;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label lblPagedPool;
        private System.Windows.Forms.Label lblNonPagedPool;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label lblFreePTEs;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnDriver;
    }
}


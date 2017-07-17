namespace SQLMonitor
{
    partial class Options
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkLogData = new System.Windows.Forms.CheckBox();
            this.cmbSI = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPAL = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBlocked = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDiskIdle = new System.Windows.Forms.TextBox();
            this.txtPageLife = new System.Windows.Forms.TextBox();
            this.trDIskIdle = new System.Windows.Forms.TrackBar();
            this.txtPagesSec = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.trPageLife = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.trPagesSec = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProcHitRatio = new System.Windows.Forms.TextBox();
            this.trProcHitRatio = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBuffHitRatio = new System.Windows.Forms.TextBox();
            this.trBuffHitRatio = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCPUQueue = new System.Windows.Forms.TextBox();
            this.trCPUQueue = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCPU = new System.Windows.Forms.TextBox();
            this.trCPU = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDefaults = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbFileFormat = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPalInterval = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trDIskIdle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trPageLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trPagesSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trProcHitRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBuffHitRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trCPUQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trCPU)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(405, 576);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(405, 532);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkLogData
            // 
            this.chkLogData.AutoSize = true;
            this.chkLogData.Location = new System.Drawing.Point(16, 34);
            this.chkLogData.Name = "chkLogData";
            this.chkLogData.Size = new System.Drawing.Size(186, 17);
            this.chkLogData.TabIndex = 2;
            this.chkLogData.Text = "Log Data for offline analysis (CSV)";
            this.chkLogData.UseVisualStyleBackColor = true;
            this.chkLogData.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cmbSI
            // 
            this.cmbSI.Enabled = false;
            this.cmbSI.FormattingEnabled = true;
            this.cmbSI.Location = new System.Drawing.Point(36, 63);
            this.cmbSI.Name = "cmbSI";
            this.cmbSI.Size = new System.Drawing.Size(39, 21);
            this.cmbSI.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sample Interval";
            // 
            // chkPAL
            // 
            this.chkPAL.AutoSize = true;
            this.chkPAL.Location = new System.Drawing.Point(17, 34);
            this.chkPAL.Name = "chkPAL";
            this.chkPAL.Size = new System.Drawing.Size(149, 17);
            this.chkPAL.TabIndex = 5;
            this.chkPAL.Text = "Log Data for PAL Analysis";
            this.chkPAL.UseVisualStyleBackColor = true;
            this.chkPAL.CheckedChanged += new System.EventHandler(this.chkPAL_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBlocked);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbSI);
            this.groupBox1.Controls.Add(this.chkLogData);
            this.groupBox1.Location = new System.Drawing.Point(15, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 146);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logging";
            // 
            // chkBlocked
            // 
            this.chkBlocked.AutoSize = true;
            this.chkBlocked.Location = new System.Drawing.Point(16, 106);
            this.chkBlocked.Name = "chkBlocked";
            this.chkBlocked.Size = new System.Drawing.Size(192, 17);
            this.chkBlocked.TabIndex = 5;
            this.chkBlocked.Text = "Log Blocked Process Details (CSV)";
            this.chkBlocked.UseVisualStyleBackColor = true;
            this.chkBlocked.CheckedChanged += new System.EventHandler(this.chkBlocked_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDiskIdle);
            this.groupBox2.Controls.Add(this.txtPageLife);
            this.groupBox2.Controls.Add(this.trDIskIdle);
            this.groupBox2.Controls.Add(this.txtPagesSec);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.trPageLife);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.trPagesSec);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtProcHitRatio);
            this.groupBox2.Controls.Add(this.trProcHitRatio);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtBuffHitRatio);
            this.groupBox2.Controls.Add(this.trBuffHitRatio);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCPUQueue);
            this.groupBox2.Controls.Add(this.trCPUQueue);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCPU);
            this.groupBox2.Controls.Add(this.trCPU);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(18, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 403);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thresholds";
            // 
            // txtDiskIdle
            // 
            this.txtDiskIdle.Location = new System.Drawing.Point(316, 357);
            this.txtDiskIdle.Name = "txtDiskIdle";
            this.txtDiskIdle.Size = new System.Drawing.Size(33, 20);
            this.txtDiskIdle.TabIndex = 14;
            this.txtDiskIdle.TextChanged += new System.EventHandler(this.txtDiskIdle_TextChanged);
            // 
            // txtPageLife
            // 
            this.txtPageLife.Location = new System.Drawing.Point(316, 309);
            this.txtPageLife.Name = "txtPageLife";
            this.txtPageLife.Size = new System.Drawing.Size(33, 20);
            this.txtPageLife.TabIndex = 14;
            this.txtPageLife.TextChanged += new System.EventHandler(this.txtPageLife_TextChanged);
            // 
            // trDIskIdle
            // 
            this.trDIskIdle.Location = new System.Drawing.Point(6, 346);
            this.trDIskIdle.Maximum = 100;
            this.trDIskIdle.Name = "trDIskIdle";
            this.trDIskIdle.Size = new System.Drawing.Size(304, 45);
            this.trDIskIdle.TabIndex = 13;
            this.trDIskIdle.Value = 75;
            this.trDIskIdle.Scroll += new System.EventHandler(this.trDIskIdle_Scroll);
            // 
            // txtPagesSec
            // 
            this.txtPagesSec.Location = new System.Drawing.Point(316, 258);
            this.txtPagesSec.Name = "txtPagesSec";
            this.txtPagesSec.Size = new System.Drawing.Size(33, 20);
            this.txtPagesSec.TabIndex = 14;
            this.txtPagesSec.TextChanged += new System.EventHandler(this.txtPagesSec_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 330);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Disk Idle %";
            // 
            // trPageLife
            // 
            this.trPageLife.LargeChange = 100;
            this.trPageLife.Location = new System.Drawing.Point(6, 298);
            this.trPageLife.Maximum = 1000;
            this.trPageLife.Name = "trPageLife";
            this.trPageLife.Size = new System.Drawing.Size(304, 45);
            this.trPageLife.SmallChange = 10;
            this.trPageLife.TabIndex = 13;
            this.trPageLife.Value = 75;
            this.trPageLife.Scroll += new System.EventHandler(this.trPageLife_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Page Life Expectancy";
            // 
            // trPagesSec
            // 
            this.trPagesSec.LargeChange = 100;
            this.trPagesSec.Location = new System.Drawing.Point(6, 247);
            this.trPagesSec.Maximum = 1000;
            this.trPagesSec.Name = "trPagesSec";
            this.trPagesSec.Size = new System.Drawing.Size(304, 45);
            this.trPagesSec.SmallChange = 10;
            this.trPagesSec.TabIndex = 13;
            this.trPagesSec.Value = 75;
            this.trPagesSec.Scroll += new System.EventHandler(this.trPagesSec_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Memory Pages / sec";
            // 
            // txtProcHitRatio
            // 
            this.txtProcHitRatio.Location = new System.Drawing.Point(316, 207);
            this.txtProcHitRatio.Name = "txtProcHitRatio";
            this.txtProcHitRatio.Size = new System.Drawing.Size(33, 20);
            this.txtProcHitRatio.TabIndex = 11;
            this.txtProcHitRatio.TextChanged += new System.EventHandler(this.txtProcHitRatio_TextChanged);
            // 
            // trProcHitRatio
            // 
            this.trProcHitRatio.Location = new System.Drawing.Point(6, 196);
            this.trProcHitRatio.Maximum = 100;
            this.trProcHitRatio.Name = "trProcHitRatio";
            this.trProcHitRatio.Size = new System.Drawing.Size(304, 45);
            this.trProcHitRatio.TabIndex = 10;
            this.trProcHitRatio.Value = 75;
            this.trProcHitRatio.Scroll += new System.EventHandler(this.trProcHitRatio_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Procedure Cache Hit Ratio";
            // 
            // txtBuffHitRatio
            // 
            this.txtBuffHitRatio.Location = new System.Drawing.Point(316, 159);
            this.txtBuffHitRatio.Name = "txtBuffHitRatio";
            this.txtBuffHitRatio.Size = new System.Drawing.Size(33, 20);
            this.txtBuffHitRatio.TabIndex = 8;
            this.txtBuffHitRatio.TextChanged += new System.EventHandler(this.txtBuffHitRatio_TextChanged);
            // 
            // trBuffHitRatio
            // 
            this.trBuffHitRatio.Location = new System.Drawing.Point(6, 148);
            this.trBuffHitRatio.Maximum = 100;
            this.trBuffHitRatio.Name = "trBuffHitRatio";
            this.trBuffHitRatio.Size = new System.Drawing.Size(304, 45);
            this.trBuffHitRatio.TabIndex = 7;
            this.trBuffHitRatio.Value = 75;
            this.trBuffHitRatio.Scroll += new System.EventHandler(this.trBuffHitRatio_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Buffer Cache Hit Ratio";
            // 
            // txtCPUQueue
            // 
            this.txtCPUQueue.Location = new System.Drawing.Point(316, 108);
            this.txtCPUQueue.Name = "txtCPUQueue";
            this.txtCPUQueue.Size = new System.Drawing.Size(33, 20);
            this.txtCPUQueue.TabIndex = 5;
            this.txtCPUQueue.TextChanged += new System.EventHandler(this.txtCPUQueue_TextChanged);
            // 
            // trCPUQueue
            // 
            this.trCPUQueue.Location = new System.Drawing.Point(6, 97);
            this.trCPUQueue.Maximum = 100;
            this.trCPUQueue.Name = "trCPUQueue";
            this.trCPUQueue.Size = new System.Drawing.Size(304, 45);
            this.trCPUQueue.TabIndex = 4;
            this.trCPUQueue.Value = 2;
            this.trCPUQueue.Scroll += new System.EventHandler(this.trCPUQueue_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "CPU Queue (per CPU)";
            // 
            // txtCPU
            // 
            this.txtCPU.Location = new System.Drawing.Point(316, 57);
            this.txtCPU.Name = "txtCPU";
            this.txtCPU.Size = new System.Drawing.Size(33, 20);
            this.txtCPU.TabIndex = 2;
            this.txtCPU.TextChanged += new System.EventHandler(this.txtCPU_TextChanged);
            // 
            // trCPU
            // 
            this.trCPU.Location = new System.Drawing.Point(6, 46);
            this.trCPU.Maximum = 100;
            this.trCPU.Name = "trCPU";
            this.trCPU.Size = new System.Drawing.Size(304, 45);
            this.trCPU.TabIndex = 1;
            this.trCPU.Value = 75;
            this.trCPU.Scroll += new System.EventHandler(this.trCPU_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "CPU\r\n";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(407, 377);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDefaults
            // 
            this.btnDefaults.Location = new System.Drawing.Point(407, 321);
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.Size = new System.Drawing.Size(75, 23);
            this.btnDefaults.TabIndex = 9;
            this.btnDefaults.Text = "Defaults";
            this.btnDefaults.UseVisualStyleBackColor = true;
            this.btnDefaults.Click += new System.EventHandler(this.btnDefaults_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cmbFileFormat);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.chkPAL);
            this.groupBox3.Controls.Add(this.cmbPalInterval);
            this.groupBox3.Location = new System.Drawing.Point(243, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 146);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PAL Analysis";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(82, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "File Format";
            // 
            // cmbFileFormat
            // 
            this.cmbFileFormat.Enabled = false;
            this.cmbFileFormat.FormattingEnabled = true;
            this.cmbFileFormat.Location = new System.Drawing.Point(37, 107);
            this.cmbFileFormat.Name = "cmbFileFormat";
            this.cmbFileFormat.Size = new System.Drawing.Size(39, 21);
            this.cmbFileFormat.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(82, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Sample Interval";
            // 
            // cmbPalInterval
            // 
            this.cmbPalInterval.Enabled = false;
            this.cmbPalInterval.FormattingEnabled = true;
            this.cmbPalInterval.Location = new System.Drawing.Point(37, 63);
            this.cmbPalInterval.Name = "cmbPalInterval";
            this.cmbPalInterval.Size = new System.Drawing.Size(39, 21);
            this.cmbPalInterval.TabIndex = 5;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 617);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnDefaults);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trDIskIdle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trPageLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trPagesSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trProcHitRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBuffHitRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trCPUQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trCPU)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkLogData;
        private System.Windows.Forms.ComboBox cmbSI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkPAL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCPU;
        private System.Windows.Forms.TrackBar trCPU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuffHitRatio;
        private System.Windows.Forms.TrackBar trBuffHitRatio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCPUQueue;
        private System.Windows.Forms.TrackBar trCPUQueue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProcHitRatio;
        private System.Windows.Forms.TrackBar trProcHitRatio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPagesSec;
        private System.Windows.Forms.TrackBar trPagesSec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDiskIdle;
        private System.Windows.Forms.TextBox txtPageLife;
        private System.Windows.Forms.TrackBar trDIskIdle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trPageLife;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDefaults;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbFileFormat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPalInterval;
        private System.Windows.Forms.CheckBox chkBlocked;
    }
}
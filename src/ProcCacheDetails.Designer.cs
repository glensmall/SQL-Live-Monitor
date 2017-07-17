namespace SQLMonitor
{
    partial class ProcCacheDetails
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
            ds.Dispose();
            Ad.Dispose();
            MyCom.Dispose();

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
            this.dgProcCache = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveExecutionPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbRows = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowPlans = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgProcCache)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgProcCache
            // 
            this.dgProcCache.AllowUserToAddRows = false;
            this.dgProcCache.AllowUserToDeleteRows = false;
            this.dgProcCache.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgProcCache.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProcCache.ContextMenuStrip = this.contextMenuStrip1;
            this.dgProcCache.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProcCache.Location = new System.Drawing.Point(3, 16);
            this.dgProcCache.MultiSelect = false;
            this.dgProcCache.Name = "dgProcCache";
            this.dgProcCache.ReadOnly = true;
            this.dgProcCache.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProcCache.Size = new System.Drawing.Size(533, 414);
            this.dgProcCache.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveExecutionPlanToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 26);
            // 
            // saveExecutionPlanToolStripMenuItem
            // 
            this.saveExecutionPlanToolStripMenuItem.Name = "saveExecutionPlanToolStripMenuItem";
            this.saveExecutionPlanToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveExecutionPlanToolStripMenuItem.Text = "Save Execution Plan";
            this.saveExecutionPlanToolStripMenuItem.Click += new System.EventHandler(this.saveExecutionPlanToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgProcCache);
            this.groupBox1.Location = new System.Drawing.Point(11, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 433);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plan Cache Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(565, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(125, 213);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(29, 98);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(29, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(29, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbRows
            // 
            this.cmbRows.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbRows.FormattingEnabled = true;
            this.cmbRows.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "100",
            "200",
            "All"});
            this.cmbRows.Location = new System.Drawing.Point(565, 95);
            this.cmbRows.Name = "cmbRows";
            this.cmbRows.Size = new System.Drawing.Size(121, 21);
            this.cmbRows.TabIndex = 3;
            this.cmbRows.SelectedIndexChanged += new System.EventHandler(this.cmbRows_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(565, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Rows";
            // 
            // chkShowPlans
            // 
            this.chkShowPlans.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkShowPlans.AutoSize = true;
            this.chkShowPlans.Location = new System.Drawing.Point(565, 26);
            this.chkShowPlans.Name = "chkShowPlans";
            this.chkShowPlans.Size = new System.Drawing.Size(132, 17);
            this.chkShowPlans.TabIndex = 5;
            this.chkShowPlans.Text = "Show Execution Plans";
            this.chkShowPlans.UseVisualStyleBackColor = true;
            this.chkShowPlans.CheckedChanged += new System.EventHandler(this.chkShowPlans_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(583, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "(may take a few mins)";
            // 
            // ProcCacheDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 453);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkShowPlans);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRows);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ProcCacheDetails";
            this.Text = "Plan Cache Details";
            this.Load += new System.EventHandler(this.ProcCacheDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgProcCache)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgProcCache;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowPlans;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveExecutionPlanToolStripMenuItem;
        private System.Windows.Forms.Label label2;
    }
}
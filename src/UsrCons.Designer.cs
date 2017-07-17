namespace SQLMonitor
{
    partial class UsrCons
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
            this.dgCons = new System.Windows.Forms.DataGridView();
            this.grpUserCons = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRows = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserCons = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCons)).BeginInit();
            this.grpUserCons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgCons
            // 
            this.dgCons.AllowUserToAddRows = false;
            this.dgCons.AllowUserToDeleteRows = false;
            this.dgCons.AllowUserToOrderColumns = true;
            this.dgCons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCons.Location = new System.Drawing.Point(3, 16);
            this.dgCons.Name = "dgCons";
            this.dgCons.ReadOnly = true;
            this.dgCons.Size = new System.Drawing.Size(503, 280);
            this.dgCons.TabIndex = 0;
            // 
            // grpUserCons
            // 
            this.grpUserCons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUserCons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpUserCons.Controls.Add(this.dgCons);
            this.grpUserCons.Location = new System.Drawing.Point(18, 18);
            this.grpUserCons.Name = "grpUserCons";
            this.grpUserCons.Size = new System.Drawing.Size(509, 299);
            this.grpUserCons.TabIndex = 1;
            this.grpUserCons.TabStop = false;
            this.grpUserCons.Text = "User Connections";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(27, 37);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 22);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Location = new System.Drawing.Point(547, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 165);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(27, 80);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 22);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(27, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 22);
            this.button1.TabIndex = 6;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(544, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Rows";
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
            this.cmbRows.Location = new System.Drawing.Point(544, 86);
            this.cmbRows.Name = "cmbRows";
            this.cmbRows.Size = new System.Drawing.Size(121, 21);
            this.cmbRows.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 331);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "User Connections: ";
            // 
            // lblUserCons
            // 
            this.lblUserCons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUserCons.AutoSize = true;
            this.lblUserCons.Location = new System.Drawing.Point(122, 331);
            this.lblUserCons.Name = "lblUserCons";
            this.lblUserCons.Size = new System.Drawing.Size(10, 13);
            this.lblUserCons.TabIndex = 10;
            this.lblUserCons.Text = "-";
            // 
            // UsrCons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 356);
            this.ControlBox = false;
            this.Controls.Add(this.lblUserCons);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRows);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpUserCons);
            this.Name = "UsrCons";
            this.Text = "User Connections";
            this.Load += new System.EventHandler(this.UsrCons_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCons)).EndInit();
            this.grpUserCons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCons;
        private System.Windows.Forms.GroupBox grpUserCons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserCons;
    }
}
namespace SQLMonitor
{
    partial class Driver
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rad2000ODBC = new System.Windows.Forms.RadioButton();
            this.rad2005Native = new System.Windows.Forms.RadioButton();
            this.rad2008Native = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(163, 83);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(163, 31);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rad2008Native);
            this.groupBox1.Controls.Add(this.rad2005Native);
            this.groupBox1.Controls.Add(this.rad2000ODBC);
            this.groupBox1.Location = new System.Drawing.Point(10, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Driver";
            // 
            // rad2000ODBC
            // 
            this.rad2000ODBC.AutoSize = true;
            this.rad2000ODBC.Checked = true;
            this.rad2000ODBC.Location = new System.Drawing.Point(7, 20);
            this.rad2000ODBC.Name = "rad2000ODBC";
            this.rad2000ODBC.Size = new System.Drawing.Size(82, 17);
            this.rad2000ODBC.TabIndex = 0;
            this.rad2000ODBC.TabStop = true;
            this.rad2000ODBC.Text = "2000 ODBC";
            this.rad2000ODBC.UseVisualStyleBackColor = true;
            this.rad2000ODBC.CheckedChanged += new System.EventHandler(this.rad2000ODBC_CheckedChanged);
            // 
            // rad2005Native
            // 
            this.rad2005Native.AutoSize = true;
            this.rad2005Native.Location = new System.Drawing.Point(7, 54);
            this.rad2005Native.Name = "rad2005Native";
            this.rad2005Native.Size = new System.Drawing.Size(112, 17);
            this.rad2005Native.TabIndex = 1;
            this.rad2005Native.Text = "2005 Native Client";
            this.rad2005Native.UseVisualStyleBackColor = true;
            this.rad2005Native.CheckedChanged += new System.EventHandler(this.rad2005Native_CheckedChanged);
            // 
            // rad2008Native
            // 
            this.rad2008Native.AutoSize = true;
            this.rad2008Native.Location = new System.Drawing.Point(7, 88);
            this.rad2008Native.Name = "rad2008Native";
            this.rad2008Native.Size = new System.Drawing.Size(112, 17);
            this.rad2008Native.TabIndex = 2;
            this.rad2008Native.Text = "2008 Native Client";
            this.rad2008Native.UseVisualStyleBackColor = true;
            this.rad2008Native.CheckedChanged += new System.EventHandler(this.rad2008Native_CheckedChanged);
            // 
            // Driver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 143);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "Driver";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Driver";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rad2008Native;
        private System.Windows.Forms.RadioButton rad2005Native;
        private System.Windows.Forms.RadioButton rad2000ODBC;
    }
}
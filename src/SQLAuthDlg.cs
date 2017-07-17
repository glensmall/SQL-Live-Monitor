using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLMonitor
{
    public partial class SQLAuthDlg : Form
    {
        public string UID, PASS, WUSER, WPASS;
        public bool UseAlt;

        public SQLAuthDlg()
        {
            InitializeComponent();
            txtSQLPass.Text = PASS;
            txtSQLUser.Text = UID;
            UseAlt = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntOK_Click(object sender, EventArgs e)
        {
            UID = txtSQLUser.Text;
            PASS = txtSQLPass.Text;

            if (chkUseAlt.Checked == true)
            {
                WUSER = txtWinUser.Text;
                WPASS = txtWinPass.Text;
                UseAlt = true;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        // Will enable the alternate windows credentials to be used
        private void chkUseAlt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseAlt.Checked == true)
            {
                txtWinPass.Enabled = true;
                txtWinUser.Enabled = true;
            }
            else
            {
                txtWinPass.Enabled = false;
                txtWinUser.Enabled = false;
            }
        }
    }
}

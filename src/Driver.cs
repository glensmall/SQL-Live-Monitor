using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLMonitor
{
    public partial class Driver : Form
    {
        public string theDriver;

        public Driver()
        {
            InitializeComponent();

            // use the default 2000 ODBC driver by default
            // should be fine for all connections as basic
            theDriver = "Driver={SQL Server}";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // update the required variables

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Do nothing here
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Select the 2000 ODBC Driver
        private void rad2000ODBC_CheckedChanged(object sender, EventArgs e)
        {
            if (rad2000ODBC.Checked == true)
            {
                theDriver = "Driver={SQL Server}";

                rad2005Native.Checked = false;
                rad2008Native.Checked = false;
            }
        }

        // Select the 2005 Driver
        private void rad2005Native_CheckedChanged(object sender, EventArgs e)
        {
            if (rad2005Native.Checked == true)
            {
                theDriver = "Driver={SQL Native Client}";

                rad2000ODBC.Checked = false;
                rad2008Native.Checked = false;
            }
        }

        // Select teh 2008 Driver
        private void rad2008Native_CheckedChanged(object sender, EventArgs e)
        {
            if (rad2008Native.Checked == true)
            {
                theDriver = "Driver={SQL Server Native Client 10.0}";

                rad2000ODBC.Checked = false;
                rad2005Native.Checked = false;
            }
        }
    }
}

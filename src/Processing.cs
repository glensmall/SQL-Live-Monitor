using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLMonitor
{
    public partial class Processing : Form
    {
        public Processing()
        {
            InitializeComponent();
            progProcessing.Value = 1;
        }

        private void Processing_Load(object sender, EventArgs e)
        {

        }

        // Just up the value a bit
        public void IncBarPos()
        {
            if (progProcessing.Value >= 89)
            {
                progProcessing.Value = 1;
            }
            else
            {
                progProcessing.Value += 10;
            }
        }
    }
}

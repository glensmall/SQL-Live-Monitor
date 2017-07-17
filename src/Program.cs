using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Security.Principal;

namespace SQLMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);

            if (!p.IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show("This application needs to be run as Administrator (UAC Elevated)");
            }
            else
            {
                Application.Run(new Form1());
            }           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortAnimations
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
            Application.Run(new FormMain());
            //System.Diagnostics.Process.Start("file:///C:/Users/SOFRAN%20ROMULUS/source/repos/SortAnimations/SortAnimations/bin/Debug/Help.html");
        }
    }
}

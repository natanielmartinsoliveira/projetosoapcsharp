using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Alerta
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach (string arg in args)
            {
                if (!(arg.Contains(@"\") || arg.Contains("/")))
                    //MessageBox.Show(arg.ToString());
                    Application.Run(new Form1(arg.ToString()));
            }
        }
    }
}

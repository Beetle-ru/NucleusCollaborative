using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EventsPlayer
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
            bool autostart = false;


            if (args.Length > 0 && bool.TryParse(args[0], out autostart))
            {
                Application.Run(new MainForm(autostart));
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}

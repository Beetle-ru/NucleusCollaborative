using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CoreTester
{
    static class Program
    {
        public static CTMainForm MainForm;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new CTMainForm();
            Application.Run(MainForm);
        }
    }
}

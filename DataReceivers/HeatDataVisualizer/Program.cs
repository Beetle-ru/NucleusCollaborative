using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HeatDataVisualizer
{
    static class Program
    {
        public static VisMain MainForm;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new VisMain();
            Application.Run(MainForm);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarbonVisualizer
{
    static class Program
    {
        public static Graph MainWindow;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow = new Graph();
            Application.Run(MainWindow);
            //Application.Run(new Graph());
        }
    }
}

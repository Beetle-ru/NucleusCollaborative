using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarbonVisualizer {
    internal static class Program {
        public static Graph MainWindow;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow = new Graph();
            Application.Run(MainWindow);
            //Application.Run(new Graph());
        }
    }
}
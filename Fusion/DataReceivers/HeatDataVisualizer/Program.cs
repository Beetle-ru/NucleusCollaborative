using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HeatDataVisualizer {
    internal static class Program {
        public static VisMain MainForm;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new VisMain();
            Application.Run(MainForm);
        }
    }
}
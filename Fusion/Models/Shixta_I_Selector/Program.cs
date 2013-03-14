using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Shixta_I_Selector
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
            ConvSelector cs = new ConvSelector();
            System.Threading.Thread.Sleep(1000);
            cs.mixCalculator1.Init();
            cs.mixCalculator2.Init();
            cs.mixCalculator3.Init();
            Application.Run(cs);
        }
    }
}

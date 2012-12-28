using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HeatCharge;
using Implements;

namespace AlgorithmsUI
{
    static class Program
    {
        public static ConnectionProvider.Client MainGate;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Logger l = new Logger("AlgorithmsUI"))
            {
                //try
                //{
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
                    var formId = Convert.ToUInt32(mainConf.AppSettings.Settings["FormToOpen"].Value);
                    MainGate = new ConnectionProvider.Client(new CoreListener());
                    MainGate.Subscribe();
                //CoreListener.Init();
                    switch (formId)
                    {
                        case 0:
                            Application.Run(new Form1());
                            break;
                        case 1:
                            Application.Run(new MixtureInitial());
                            break;
                    }
                //}
                //catch (Exception e)
                //{
                //    l.err("Exception <{0}> -- execution terminated", e.ToString());

                //}
                //catch
                //{
                //    l.err("Unknown exception -- execution terminated");
                //}
            }
        }
    }
}

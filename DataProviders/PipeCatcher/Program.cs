using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using Implements;
using CommonTypes;
using Converter;

namespace PipeCatcher
{
    internal static class Program
    {
        public static ConnectionProvider.Client CoreGate;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var l = new Logger("PipeCatcher"))
            {
                try
                {
                    var o = new FlexEvent("dummy"); /// нужно чтобы сборка загрузилась
                    CoreGate = new ConnectionProvider.Client();
                    var conf = ConfigurationManager.OpenExeConfiguration("");
                    var settings = conf.AppSettings.Settings;
                    var dbrlist = new List<DBReader>();
                    foreach (KeyValueConfigurationElement pipe in settings)
                    {
                        if (!pipe.Key.StartsWith("pipe")) continue;
                        foreach (KeyValueConfigurationElement kvel in settings)
                        {
                            if (kvel.Key.StartsWith("connectionString"))
                            {
                                dbrlist.Add(new DBReader(kvel.Value, pipe.Value));
                            }
                        }
                    }
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new Catcher(dbrlist));
                }
                catch (Exception e)
                {
                    l.err("PipeCatcher.Main exception {0}", e);
                }
            }
        }
    }
}
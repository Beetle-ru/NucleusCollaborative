using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Reflection;
using Core.Exceptions;
using CommonTypes;
using Implements;

namespace Core {
    internal class Program {
        private static List<BaseEvent> s_Events = new List<BaseEvent>();
        public static List<string> AllowIPs = new List<string>();

        private static void Main(string[] args) {
            InstantLogger.writeLogConsole = false;
            //InstantLogger.writeLogFile = true;
            try {
                var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

                Core.Instance.LoadModule(mainConf.AppSettings.Settings["Module"].Value);
                AllowIPsParce(mainConf.AppSettings.Settings["AllowIPs"].Value);
                Core.Instance.Start(int.Parse(mainConf.AppSettings.Settings["Port"].Value),
                                    int.Parse(mainConf.AppSettings.Settings["APIPort"].Value));
                //Implements.InstantLogger.log("Нажмите <ENTER> для выхода.", "Ядро запущено. [ThreadPoolCore multiple persession]", Implements.InstantLogger.TypeMessage.important);
                Console.WriteLine("{0}\nНажмите <ENTER> для выхода.",
                                  "Ядро запущено. [ThreadPoolCore multiple persession]");
                Console.ReadLine();
                Core.Instance.Stop();
            }
            catch (ModuleLoadException ex) {
                Console.WriteLine("Ядро не запущено: {0}", ex.ToString());
                Console.WriteLine("Нажмите <ENTER> для выхода.");
                Console.ReadLine();
            }
            catch (NotImplementedException ex) {
                Console.WriteLine("Ядро не запущено: {0}", ex.ToString());
                Console.WriteLine("Нажмите <ENTER> для выхода.");
                Console.ReadLine();
            }
        }

        private static void AllowIPsParce(string str) {
            const char separator = ';';
            AllowIPs = str.Split(separator).ToList();
        }
    }
}
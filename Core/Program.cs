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

namespace Core
{
    class Program
    {
        static List<BaseEvent> s_Events = new List<BaseEvent>();
        
        static void Main(string[] args)
        {
            try
            {

                var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

                Core.Instance.LoadModule(mainConf.AppSettings.Settings["Module"].Value);
                Core.Instance.Start(int.Parse(mainConf.AppSettings.Settings["Port"].Value), int.Parse(mainConf.AppSettings.Settings["APIPort"].Value));
                Implements.InstantLogger.log("Нажмите <ENTER> для выхода.", "Ядро запущено. [ThreadPoolCore multiple persession]", Implements.InstantLogger.TypeMessage.important);
                Console.ReadLine();
                Core.Instance.Stop();
            }
            catch (ModuleLoadException ex)
            {
                Console.WriteLine("Ядро не запущено: {0}", ex.ToString());
                Console.WriteLine("Нажмите <ENTER> для выхода.");
                Console.ReadLine();
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine("Ядро не запущено: {0}", ex.ToString());
                Console.WriteLine("Нажмите <ENTER> для выхода.");
                Console.ReadLine();
            }
        }
    }
}

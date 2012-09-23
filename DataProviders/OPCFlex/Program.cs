using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CommonTypes;
using System.Configuration;

namespace OPCFlex
{
    class Program
    {

        public static Configuration MainConf;
        public static Type FlexEventType;
        public static ConnectionProvider.Client MainGate;

        static void Main(string[] args)
        {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            var a = Assembly.LoadFrom(MainConf.AppSettings.Settings["Module"].Value);
            var EventsList = BaseEvent.GetEvents();

            var flexEventIsFound = false;
            foreach (var evtType in EventsList)
            {
                if (evtType.Name == "FlexEvent")
                {
                    FlexEventType = evtType;
                    flexEventIsFound = true;
                }
            }
            if (!flexEventIsFound)
            {
                Console.WriteLine("FlexEvent is not found in the assembly");
                return;
            }

            MainGate = new ConnectionProvider.Client(new CoreListener(FlexEventType));
            MainGate.Subscribe();
            
            Console.ReadLine();
        }
    }
}

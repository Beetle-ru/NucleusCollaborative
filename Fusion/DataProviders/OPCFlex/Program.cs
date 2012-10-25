using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CommonTypes;
using System.Configuration;
using Converter;

namespace OPCFlex
{
    class Program
    {

        public static Configuration MainConf;
        public static Type FlexEventType;
        public static ConnectionProvider.Client MainGate;
        public static string Destination;
        public static string CfgPath;

        static void Main(string[] args)
        {

            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            Destination = MainConf.AppSettings.Settings["OPCDestination"].Value;
            CfgPath = MainConf.AppSettings.Settings["CfgPath"].Value;

            MainGate = new ConnectionProvider.Client(new CoreListener());
            MainGate.Subscribe();


            LoaderCSV descriptionLoader = new LoaderCSV(Destination);
            CartridgeElement ce = new CartridgeElement();
            var descriptions = descriptionLoader.LoadAndGet(CfgPath);

            foreach (var description in descriptions)
            {
                ce.Add(description);
                Console.WriteLine(description.ToString());
                Console.WriteLine();
            }
            Console.WriteLine("OPCFlex is running, press enter to exit");
            Console.ReadLine();
        }
    }
}

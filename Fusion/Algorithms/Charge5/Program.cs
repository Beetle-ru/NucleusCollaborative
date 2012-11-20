using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;

namespace Charge5
{
    class Program
    {
        public static Client MainGate;
        public static Configuration MainConf;
        public static char Separator;
        static void Main(string[] args)
        {
            Init();
            Console.WriteLine("Charge5 is running, press enter to exit");
            Console.ReadLine();
        }
        static void Init()
        {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            Separator = MainConf.AppSettings.Settings["separator"].Value.ToArray()[0];
            //MatrixT.FileName = MainConf.AppSettings.Settings["matrixT"].Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;

namespace OGDecarbonaterFine
{
    class Program
    {
        public static Client MainGate;
        public const char Separator = ';';
        public static Configuration MainConf;

        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

            Console.WriteLine("Press Enter for exit\n");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;

namespace OGDecarbonaterFine
{
    class Program
    {
        public static Client MainGate;
        public const char Separator = ';';
        public static Configuration MainConf;
        public static int ConverterNumber;

        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

            ConverterNumber = Convertion.StrToInt32(ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["converterNumber"].Value);
            Iterator.CSVHimFilePath = ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["CSVHimFilePath"].Value;
            Iterator.MatrixFileName = ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["MatrixFileName"].Value;

            Iterator.Init();

            Console.WriteLine("Press Enter for exit\n");
            Console.ReadLine();
        }
    }
}

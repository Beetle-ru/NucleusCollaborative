using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implements;
using System.Configuration;

namespace CorrectionCT
{
    class Program
    {
        public static CSVTableParser MatrixT;
        public static CSVTableParser MatrixC;
        public static Configuration MainConf;
        public static char Separator;
        public static ConnectionProvider.Client MainGate;
        public static Estimates Data;
        static void Main(string[] args)
        {
            Init();
            Console.WriteLine("CorrectionCT is running, press enter to exit");
            Console.ReadLine();
        }
        static void Init()
        {
            MatrixT = new CSVTableParser();
            MatrixC = new CSVTableParser();
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            Data = new Estimates();

            Separator = MainConf.AppSettings.Settings["separator"].Value.ToArray()[0];
            MatrixT.FileName = MainConf.AppSettings.Settings["matrixT"].Value;
            MatrixT.Separator = Separator;
            
            MatrixT.Description.Add(new ColumnPath() {ColumnName = "CMin", ColumnType = typeof(double)});
            MatrixT.Description.Add(new ColumnPath() { ColumnName = "CMax", ColumnType = typeof(double) });
            MatrixT.Description.Add(new ColumnPath() { ColumnName = "OxygenOnHeating", ColumnType = typeof(int) });
            MatrixT.Description.Add(new ColumnPath() { ColumnName = "Heating", ColumnType = typeof(int) });
            MatrixT.Load();

            MatrixC.FileName = MainConf.AppSettings.Settings["matrixC"].Value;
            MatrixC.Separator = Separator;
            
            MatrixC.Description.Add(new ColumnPath() { ColumnName = "CMin", ColumnType = typeof(double) });
            MatrixC.Description.Add(new ColumnPath() { ColumnName = "CMax", ColumnType = typeof(double) });
            MatrixC.Description.Add(new ColumnPath() { ColumnName = "OxygenOnCarbon", ColumnType = typeof(int) });
            MatrixC.Load();

            MainGate = new ConnectionProvider.Client(new Listener());
            MainGate.Subscribe();
        }
        public static void Reset()
        {
            Data = new Estimates();
        }
    }
}

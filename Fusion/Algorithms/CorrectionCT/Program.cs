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
        static void Main(string[] args)
        {
            Init();
        }
        static void Init()
        {
            MatrixT = new CSVTableParser();
            MatrixC = new CSVTableParser();
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

            Separator = MainConf.AppSettings.Settings["separator"].Value.ToArray()[0];
            MatrixT.FileName = MainConf.AppSettings.Settings["matrixT"].Value;
            MatrixT.Separator = Separator;
            
            MatrixT.Description.Add(new ColumnPath() {ColumnName = "CMin", ColumnType = typeof(double)});
            MatrixT.Description.Add(new ColumnPath() { ColumnName = "CMax", ColumnType = typeof(double) });
            MatrixT.Description.Add(new ColumnPath() { ColumnName = "OxygenOnHeating", ColumnType = typeof(int) });
            MatrixT.Description.Add(new ColumnPath() { ColumnName = "Heating", ColumnType = typeof(int) });

            MatrixC.FileName = MainConf.AppSettings.Settings["matrixC"].Value;
            MatrixC.Separator = Separator;
            
            MatrixC.Description.Add(new ColumnPath() { ColumnName = "CMin", ColumnType = typeof(double) });
            MatrixC.Description.Add(new ColumnPath() { ColumnName = "CMax", ColumnType = typeof(double) });
            MatrixC.Description.Add(new ColumnPath() { ColumnName = "OxygenOnCarbon", ColumnType = typeof(int) });
        }
    }
}

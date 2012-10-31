﻿using System;
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
        public static int CalcT(CSVTableParser matrixT, Estimates data)
        {
            using (var l = new Logger("CalcT"))
            {
                if (
                    (data.CurrentT == 0) &&
                    (data.TargetT == 0) &&
                    (data.CurrentC == 0)
                    )
                {
                    return 0;
                }
                foreach (var row in matrixT.Rows)
                {
                    if ((double) (row.Cell["CMin"]) <= data.CurrentC && data.CurrentC < (double) (row.Cell["CMax"]))
                    {
                        l.msg("T item found --- CMin {0}; CMax {1}", row.Cell["CMin"], row.Cell["CMax"]);

                        var differenceT = data.TargetT - data.CurrentT;
                        if (differenceT > 0)
                        {
                            var oxygenOnHeating = (int) (row.Cell["OxygenOnHeating"]);
                            var heating = (int) (row.Cell["Heating"]);
                            double correctionOxy = (oxygenOnHeating/heating)*differenceT;
                            l.msg("Correction Oxygen T = {0}", correctionOxy);
                            return (int)Math.Round(correctionOxy);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            return 0;
        }
        public static int CalcC(CSVTableParser matrixC, Estimates data)
        {
            using (var l = new Logger("CalcC"))
            {
                if (
                    (data.CurrentC == 0) &&
                    (data.TargetC == 0) 
                    )
                {
                    return 0;
                }
                foreach (var row in matrixC.Rows)
                {
                    if ((double)(row.Cell["CMin"]) <= data.CurrentC && data.CurrentC < (double)(row.Cell["CMax"]))
                    {
                        l.msg("C item found --- CMin {0}; CMax {1}", row.Cell["CMin"], row.Cell["CMax"]);

                        var differenceC = data.TargetC - data.CurrentC;
                        if (differenceC < 0)
                        {
                            differenceC = Math.Abs(differenceC);
                            var oxygenOnCarbon = (int)(row.Cell["OxygenOnCarbon"]);
                            const double carbonConsumption = 0.01;
                            double correctionOxy = (oxygenOnCarbon / carbonConsumption) * differenceC;
                            l.msg("Correction Oxygen C = {0}", correctionOxy);
                            return (int)Math.Round(correctionOxy);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            return 0;
        }
        public static void Iterator()
        {
            var correctionOxyT = CalcT(MatrixT, Data);
            var correctionOxyC = CalcC(MatrixC, Data);
            if (correctionOxyT != 0 && correctionOxyC != 0)
            {
                var fex = new ConnectionProvider.FlexHelper("CorrectionCT.RecommendBalanceBlow");
                fex.AddArg("CorrectionOxygenT", correctionOxyT); // int
                fex.AddArg("CorrectionOxygenC", correctionOxyC); // int
                fex.AddArg("CurrentC", Data.CurrentC); // double
                fex.AddArg("TargetC", Data.TargetC); // double
                fex.AddArg("CurrentT", Data.CurrentT); // int
                fex.AddArg("TargetT", Data.TargetT); // int

                fex.Fire(Program.MainGate);

                InstantLogger.msg(fex.evt.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;
using Implements;
using System.Configuration;
using System.Timers;

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
        public static double CurrentCalcCarbone;
        public static double FixedCalcCarbone;
        public static bool IsFiered;
        public static Guid SidB;
        public static bool AutomaticStop;
        public static int CurrentOxygen;
        public static int CorrectionOxyT;
        public static int CorrectionOxyC;
        public static int EndBlowingOxygen;
        public static bool BlowStopSignalPushed;
        public static Timer WaitSublanceData;
        public static int MeteringWaitTimeUVM = 30;
        public static int MeteringWaitTimeManual = 30;
        public static int LanceMode;
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

            var o = new FlexEvent();
            MainGate = new ConnectionProvider.Client(new Listener());
            MainGate.Subscribe();

            WaitSublanceData = new Timer();
            WaitSublanceData.Elapsed += new ElapsedEventHandler(SublanceDataLost);

            Reset();
        }
        public static void Reset()
        {
            Data = new Estimates();
            IsFiered = false;
            //SidB = Guid.NewGuid();
            SidB = new Guid();
            AutomaticStop = false;
            CurrentOxygen = 0;
            CorrectionOxyT = 0;
            CorrectionOxyC = 0;
            EndBlowingOxygen = int.MaxValue;
            BlowStopSignalPushed = false;
            StopBlowFlagRelease();
            EndMeteringAccept();
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
            CorrectionOxyT = CalcT(MatrixT, Data);
            //CorrectionOxyC = CalcC(MatrixC, Data);
            CorrectionOxyC = -1; // != 0
            //EndBlowingOxygen = CorrectionOxyT; // додувать по температуре
            if (IsFiered)
            {
                InstantLogger.msg("End blowing oxygen: {0} || Current Oxygen: {1} || End: {2}", EndBlowingOxygen, CurrentOxygen, ((int)(EndBlowingOxygen - CurrentOxygen)).ToString());
            }
            if (CorrectionOxyT != 0)
            {
                Console.WriteLine("CorrectionOxyT = " + CorrectionOxyT);
            }
            //if (CorrectionOxyC != 0)
            //{
            //    Console.WriteLine("CorrectionOxyC = " + CorrectionOxyC);
            //}
            if (CorrectionOxyT != 0 && CorrectionOxyC != 0 && !IsFiered)
            {
                var fex = new ConnectionProvider.FlexHelper("CorrectionCT.RecommendBalanceBlow");
                fex.AddArg("CorrectionOxygenT", CorrectionOxyT); // int
                fex.AddArg("CorrectionOxygenC", CorrectionOxyC); // int
                fex.AddArg("CurrentC", Data.CurrentC); // double
                fex.AddArg("TargetC", Data.TargetC); // double
                fex.AddArg("CurrentT", Data.CurrentT); // int
                fex.AddArg("TargetT", Data.TargetT); // int
                fex.AddArg("SId", SidB); // Guid

                fex.Fire(Program.MainGate);
               
                IsFiered = true;

                InstantLogger.msg(fex.evt.ToString());

                EndBlowingOxygen = CorrectionOxyT + CurrentOxygen; // додувать по температуре

                InstantLogger.msg("End blowing oxygen {0}", EndBlowingOxygen);
            }
            if ((CurrentOxygen > EndBlowingOxygen) && !BlowStopSignalPushed && AutomaticStop)
            {
                DoStopBlow();
            }
        }
        public static void DoStopBlow()
        {
            var fex = new ConnectionProvider.FlexHelper("OPC.ComEndBlowing");
            fex.AddArg("EndBlowingSignal",1);
            fex.Fire(Program.MainGate);
            InstantLogger.log(fex.evt.ToString());
            BlowStopSignalPushed = true;
        }
        public static void StopBlowFlagRelease()
        {
            var fex = new ConnectionProvider.FlexHelper("OPC.ComEndBlowing");
            fex.AddArg("EndBlowingSignal", 0);
            fex.Fire(Program.MainGate);
            InstantLogger.log(fex.evt.ToString());
        }
        public static void EndNowHandler()
        {
            DoStopBlow();
            //EndMeteringAccept();
        }
        public static void EndMeteringAccept()
        {
            var fex = new ConnectionProvider.FlexHelper("CorrectionCT.EndMeteringAccept");
            fex.Fire(Program.MainGate);
            InstantLogger.log(fex.evt.ToString());
        }
        public static void SublanceDataLost(object source, ElapsedEventArgs e)
        {
            WaitSublanceData.Enabled = false;
            var fex = new ConnectionProvider.FlexHelper("CorrectionCT.SublanceDataLost");
            fex.Fire(Program.MainGate);
            InstantLogger.log(fex.evt.ToString());
        }
    }
}

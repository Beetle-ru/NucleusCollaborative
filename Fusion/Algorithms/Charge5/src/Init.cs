using System;
using System.Linq;
using System.Text;
using System.Timers;
using Charge5Classes;
using ConnectionProvider;
using Converter;
using Implements;
using System.IO;

namespace Charge5 {
    internal partial class Program {
        private static void Init() {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            Separator = MainConf.AppSettings.Settings["separator"].Value.ToArray()[0];
            StorePath = MainConf.AppSettings.Settings["StorePath"].Value;
            ConverterNumber = Int32.Parse(MainConf.AppSettings.Settings["converterNumber"].Value);

            DefaultPattern = MainConf.AppSettings.Settings["DefPattrn"].Value;

            InitTbl = new CSVTableParser();

            TablePaths = ScanStore(StorePath);
            try {
                Tables = LoadTables(DefaultPattern, ref InitTbl);
                InstantLogger.msg("default pattern \"{0}\" loaded", DefaultPattern);
            }
            catch (Exception) {
                InstantLogger.err("can't load pattern \"{0}\",\nTry load pattern \"default\"", DefaultPattern);
                Tables = LoadTables("default", ref InitTbl);
            }

            if (Tables == null) InstantLogger.err("default pattern not loaded");

            CalcModeIsAutomatic = false;

            Saver = new DataSaver();

            IterateTimer.Elapsed += new ElapsedEventHandler(IterateTimeOut);
            IterateTimer.Enabled = true;

            Reset();

            //SaveTables("new", InitTbl, Tables);

            //////////////////////////////////
            //CSVTP_FlexEventConverter.AppName = "Charge5";
            //var tableFlex = CSVTP_FlexEventConverter.PackToFlex("newToFlex", InitTbl, Tables);
            //var name = "";
            //CSVTP_FlexEventConverter.UnpackFromFlex(tableFlex, ref InitTbl, ref Tables, ref name);
            //Console.WriteLine("Pare: {0}", name);
            //SaveTables("newFromFlex", InitTbl, Tables);
        }

        public static void Reset() {
            AutoInData = new InData();
            AutoInData.SiHi = 0;
            AutoInData.THi = 0;

            //m_autoInDataPrevious = new InData();
            //m_autoInDataPrevious.SteelType = -1;
            //m_autoInDataPrevious.THi = -1;
            //m_autoInDataPrevious.SiHi = -1;
            //m_autoInDataPrevious.MSc = -1;
            //m_autoInDataPrevious.MHi = -1;

            Saver.Reset();

            IsRefrashData = false;
        }
    }
}
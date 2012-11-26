using System;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;
using System.IO;

namespace Charge5
{
    internal partial class Program
    {
        static void Init()
        {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            Separator = MainConf.AppSettings.Settings["separator"].Value.ToArray()[0];
            StorePath = MainConf.AppSettings.Settings["StorePath"].Value;

            InitTbl = new CSVTableParser();

            TablePaths = ScanStore(StorePath);
            Tables = LoadTables("default", ref InitTbl);
            //SaveTables("new", InitTbl, Tables);

            //////////////////////////////////
            //CSVTP_FlexEventConverter.AppName = "Charge5";
            //var tableFlex = CSVTP_FlexEventConverter.PackToFlex("newToFlex", InitTbl, Tables);
            //var name = "";
            //CSVTP_FlexEventConverter.UnpackFromFlex(tableFlex, ref InitTbl, ref Tables, ref name);
            //Console.WriteLine("Pare: {0}", name);
            //SaveTables("newFromFlex", InitTbl, Tables);

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CommonTypes;
using System.Configuration;
using Converter;
using OPC.Common;
using OPC.Data;

namespace OPCFlex
{
    internal class Program
    {
        public static Configuration MainConf;
        public static Type FlexEventType;
        public static ConnectionProvider.Client MainGate;
        public static string Destination;
        public static string CfgPath;
        public static CartridgeElement ce = new CartridgeElement();

        public static readonly List<OPCItemDef> OpcItemDefs_ = new List<OPCItemDef>();
        public static OpcServer OpcServer_;
        public static OpcGroup OpcGroup_;
        public static OPCItemResult[] OpcItemResults_;

        private static void Main(string[] args)
        {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            Destination = MainConf.AppSettings.Settings["OPCDestination"].Value;
            CfgPath = MainConf.AppSettings.Settings["CfgPath"].Value;
            var reqUpdateRateMs = Convert.ToInt32(MainConf.AppSettings.Settings["OPCReqUpdateRate"].Value);

            MainGate = new ConnectionProvider.Client(new CoreListener());
            MainGate.Subscribe();

            var descriptionLoader = new LoaderCSV(Destination);
            var descriptions = descriptionLoader.LoadAndGet(CfgPath);

            OpcServer_ = new OpcServer();
            OpcServer_.Connect(MainConf.AppSettings.Settings["OPCServerProgID"].Value);
            OpcGroup_ = OpcServer_.AddGroup(Destination + "-flex-events", false, reqUpdateRateMs);
            OpcGroup_.DataChanged += OnDataChange;

            var hClient = 0;
            foreach (var d in descriptions)
            {
                ce.Add(d);
                foreach (var item in d.Arguments)
                {
                    OpcItemDefs_.Add(new OPCItemDef(Convert.ToString(item.Value), true, ++hClient, VarEnum.VT_EMPTY));
                }
                Console.WriteLine(d.ToString());
                Console.WriteLine();
            }
            int addCount = 0;
            while (!OpcGroup_.AddItems(OpcItemDefs_.ToArray(), out OpcItemResults_))
            {
                if (++addCount > 1) throw new InvalidDataException("!!!AddItems failed");
                for (var i = OpcItemResults_.Count(); i > 0; i--)
                {
                    if (HRESULTS.Failed(OpcItemResults_[i - 1].Error))
                    {
                        OpcItemDefs_.RemoveAt(i - 1);
                    }
                }
            }
            List<int> opcIR = OpcItemResults_.Select(ir => ir.HandleServer).ToList();
            OpcGroup_.Active = true;
            Console.WriteLine("OPCFlex is running, press enter to exit");
            Console.ReadLine();
            int[] aE;
            OpcGroup_.DataChanged -= OnDataChange;
            //OpcGroup_.RemoveItems(opcIR.ToArray(), out aE);
            OpcGroup_.Remove(false);
            OpcServer_.Disconnect();
            Console.WriteLine("Bye!");
        }
        private static void OnDataChange(object sender, DataChangeEventArgs e)
        {
            Console.WriteLine("=========== OnDataChange");
            foreach (var s in e.sts)
            {
                Console.WriteLine("cHandle = {0} val = {1} qual = {2}", s.HandleClient, s.DataValue, s.Quality);
            }
        }


    }
}
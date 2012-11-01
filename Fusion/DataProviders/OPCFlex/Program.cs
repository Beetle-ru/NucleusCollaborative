using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CommonTypes;
using System.Configuration;
using ConnectionProvider;
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
        public static List<FlexEvent> descriptions = new List<FlexEvent>();

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
            descriptions = descriptionLoader.LoadAndGet(CfgPath);

            OpcServer_ = new OpcServer();
            OpcServer_.Connect(MainConf.AppSettings.Settings["OPCServerProgID"].Value);
            OpcGroup_ = OpcServer_.AddGroup(Destination + "-flex-events", false, reqUpdateRateMs);
            OpcGroup_.DataChanged += OnDataChange;

            var hClient = 0;
            for (int dix = 0; dix < descriptions.Count; dix++)
            {
                var d = descriptions[dix];
                foreach (var item in d.Arguments)
                {
                    OpcItemDefs_.Add(new OPCItemDef(((Element) item.Value).opcItemID, true, ++hClient, VarEnum.VT_EMPTY));
                    ((Element) item.Value).cHandle = hClient;
                }
            }
            int[] aE;
            int addCount = 0;
            while (!OpcGroup_.AddItems(OpcItemDefs_.ToArray(), out OpcItemResults_))
            {
                //if (++addCount > 1) throw new InvalidDataException("!!!AddItems failed");
                for (var i = 0; i < OpcItemResults_.Count(); i++)
                {
                    if (HRESULTS.Failed(OpcItemResults_[i].Error))
                    {
                        OpcItemDefs_.RemoveAt(i);
                        break;
                    }
                }
                OpcGroup_.RemoveItems(OpcItemResults_.Select(ir => ir.HandleServer).ToArray(), out aE);
            }
            int k = 0;
            for (int j = 0; j < OpcItemDefs_.Count(); j++)
            {
                SetServerHandle(OpcItemDefs_[j].HandleClient, OpcItemResults_[k++].HandleServer);
            }
            for (int dix = 0; dix < descriptions.Count; dix++)
            {
                Console.WriteLine(descriptions[dix]);
            }
            OpcGroup_.Active = true;
            Console.WriteLine("OPCFlex is running, press enter to exit");
            Console.ReadLine();
            OpcGroup_.DataChanged -= OnDataChange;
            OpcGroup_.RemoveItems(OpcItemResults_.Select(ir => ir.HandleServer).ToArray(), out aE);
            OpcGroup_.Remove(false);
            OpcServer_.Disconnect();
            Console.WriteLine("Bye!");
        }
        private static void OnDataChange(object sender, DataChangeEventArgs e)
        {
            var sb = new StringBuilder("=========== OnDataChange ");
            foreach (var s in e.sts)
            {
                //Console.WriteLine("cHandle = {0} val = {1} qual = {2}", s.HandleClient, s.DataValue, s.Quality);
                sb.AppendFormat("{0};", s.HandleClient);
                SetValue(s.HandleClient, s.DataValue);
            }
            Console.WriteLine(sb);
            foreach (var d in descriptions)
            {
                if ((d.Flags & FlexEventFlag.FlexEventOpcNotification) != 0)
                {
                    var fex = new FlexHelper(d.Operation);
                    fex.evt.Flags = d.Flags;
                    foreach (var a in d.Arguments)
                    {
                        var v = ((Element)a.Value).val;
                        Console.WriteLine(v.GetType().ToString());

                        if (v is string) Console.WriteLine("&&&&&&&");
                        fex.AddArg(a.Key, v);
                    }
                    fex.Fire(MainGate);
                    d.Flags ^= FlexEventFlag.FlexEventOpcNotification;
                }
            }
        }
        private static void SetServerHandle(int CH, int SH)
        {
            for (int dix = 0; dix < descriptions.Count; dix++)
            {
                var d = descriptions[dix];
                foreach (var item in d.Arguments)
                {
                    if (((Element) item.Value).cHandle == CH)
                    {
                        ((Element) item.Value).sHandle = SH;
                        return;
                    }
                }
            }
        }
        private static void SetValue(int CH, object NewVal)
        {
            for (int dix = 0; dix < descriptions.Count; dix++)
            {
                var d = descriptions[dix];
                foreach (var item in d.Arguments)
                {
                    if (((Element)item.Value).cHandle == CH)
                    {
                        ((Element)item.Value).val = NewVal;
                        d.Flags |= FlexEventFlag.FlexEventOpcNotification;
                        return;
                    }
                }
            }
        }

    }
}
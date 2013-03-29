using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;
using Oracle.DataAccess.Client;

namespace DBFlex
{
    class Program
    {
        public const string ArgEventName = "@EventName";
        public const string ArgCommandName = "@Command";
        public const string ArgCountName = "@Count";
        public const string ArgErrorCodeName = "@ErrorCode";
        public const string ArgErrorStringName = "@ErrorString";
        public const string IndexFileName = "Index";


        public static string ConnectionStr;
        public static string CfgMainDir;
        public static Configuration MainConf;
        public static Client MainGate;
        public static CfgLoader Cfg;
        static void Main(string[] args)
        {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            CfgMainDir = ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["CfgMainDir"].Value;
            ConnectionStr = ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["ConnectionString"].Value;

            Cfg = new CfgLoader(CfgMainDir);

            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            Console.WriteLine("Press Enter for exit");
            Console.ReadLine();
        }

        static public void Job(FlexEvent flx) {
            var cfgResult = Cfg.ReadCfg(flx);
            if (cfgResult.ErrorCode != CfgLoader.Result.Es.S_ERROR) {
                var req = new Requester(ConnectionStr);
                req.SQLRequestAsync(cfgResult.SQLStr, flx, ResponceGenerator);
            }
            else {
                var command = flx.Arguments.ContainsKey(ArgCommandName) ? (string)flx.Arguments[ArgCommandName] : "";
                var fex = CreateRespFex(flx);
                fex.AddArg(ArgCommandName, command);
                fex.AddArg(ArgErrorCodeName, cfgResult.ErrorCode.ToString());
                fex.AddArg(ArgErrorStringName, cfgResult.ErrorStr);
                fex.Fire(MainGate);
                InstantLogger.msg(fex.evt.ToString());
            }
        }
        static public void ResponceGenerator(FlexEvent flx, Requester.Result result) {
            var command = flx.Arguments.ContainsKey(ArgCommandName) ? (string)flx.Arguments[ArgCommandName] : "";
            var fex = CreateRespFex(flx);
            var count = 0;
            foreach (var collumn in result.ResultData)
            {
                fex.AddComplexArg(collumn.Key,collumn.Value);

                if (count == 0) {
                    foreach (var row in collumn.Value) {
                        count++;
                    }
                }
            }
            foreach (var argument in flx.Arguments) {
                if (!argument.Key.StartsWith("@")) {
                    fex.AddArg(String.Format("${0}",argument.Key), argument.Value);
                }
            }
            fex.AddArg(ArgCountName,count);
            fex.AddArg(ArgCommandName, command);
            fex.AddArg(ArgErrorCodeName, result.ErrorCode.ToString());
            fex.AddArg(ArgErrorStringName, result.ErrorStr);
            fex.Fire(MainGate);
            InstantLogger.msg(fex.evt.ToString());
        }

        static public FlexHelper CreateRespFex(FlexEvent flx) {
            var operation = flx.Arguments.ContainsKey(ArgEventName) ? (string)flx.Arguments[ArgEventName] : "DBF.default.responce";
            var fex = new FlexHelper(operation);
            return fex;
        }
    }
}

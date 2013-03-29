using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
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


        public static string ConnectionStr = "DATA SOURCE=192.168.0.53/KPBOF;PASSWORD=bof7jfa4;USER ID=BOF";
        public static string CfgMainDir = "SQLFlexDescriptions";
        public static CfgLoader Cfg;
        static void Main(string[] args)
        {
            Cfg = new CfgLoader(CfgMainDir);

            var flx = new FlexEvent("Req");
            flx.Arguments.Add(ArgEventName, "SQL.Test");
            flx.Arguments.Add(ArgCommandName, "TestCommand1");
            flx.Arguments.Add("material", "'DOLMIT'");

            var flx1 = new FlexEvent("Req");
            flx1.Arguments.Add(ArgEventName, "SQL.Test");
            flx1.Arguments.Add(ArgCommandName, "TestCommand1");
            flx1.Arguments.Add("material", "'LIME'");

            var flx2 = new FlexEvent("Req");
            flx2.Arguments.Add(ArgEventName, "SQL.Test");
            flx2.Arguments.Add(ArgCommandName, "TestCommand1");
            flx2.Arguments.Add("material", "'KOKS'");


            Job(flx);
            Job(flx1);
            Job(flx2);
            //for (int i = 0; i < 10; i++) {
            //    Job(flx);
            //    Console.WriteLine("### " + i + " ###");
            //}
            Console.ReadLine();
        }

        static public void Job(FlexEvent flx) {
            var cfgResult = Cfg.ReadCfg(flx);
            if (cfgResult.ErrorCode != CfgLoader.Result.Es.S_Error) {
                var req = new Requester(ConnectionStr);
                req.SQLRequestAsync(cfgResult.SQLStr, flx, ResponceGenerator);
            }
            else {
                var command = flx.Arguments.ContainsKey(ArgCommandName) ? (string)flx.Arguments[ArgCommandName] : "";
                var fex = CreateRespFex(flx);
                fex.AddArg(ArgCommandName, command);
                fex.AddArg(ArgErrorCodeName, cfgResult.ErrorCode.ToString());
                fex.AddArg(ArgErrorStringName, cfgResult.ErrorStr);
                Console.WriteLine(fex.evt);
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
            Console.WriteLine(fex.evt);
        }

        static public FlexHelper CreateRespFex(FlexEvent flx) {
            var operation = flx.Arguments.ContainsKey(ArgEventName) ? (string)flx.Arguments[ArgEventName] : "DBF.default.responce";
            var fex = new FlexHelper(operation);
            return fex;
        }
    }
}

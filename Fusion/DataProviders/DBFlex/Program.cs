using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Converter;
using Oracle.DataAccess.Client;

namespace DBFlex
{
    class Program
    {
        public static string ConnectionStr = "DATA SOURCE=KPBOF;PASSWORD=bof7jfa4;USER ID=BOF";
        static void Main(string[] args)
        {
            var cfg = new CfgLoader("SQLFlexDescriptions");
            var flx = new FlexEvent("Req");
            flx.Arguments.Add("@EventName", "SQL.Test");
            flx.Arguments.Add("@Command", "TestCommand1");
            flx.Arguments.Add("material", "'DOLMIT'");
            var cfgResult = cfg.ReadCfg(flx);

            Console.WriteLine("CFG Load");
            Console.WriteLine(cfgResult.ErrorCode);
            Console.WriteLine(cfgResult.ErrorStr);

            //Console.WriteLine("Status = {0}\n\nError = \n{1}\n\nSQL = \n{2}",res.ErrorCode, res.ErrorStr, res.SQLStr);

            var connectionString = "DATA SOURCE=KPBOF;PASSWORD=bof7jfa4;USER ID=BOF";
            //var sqlstr = "SELECT ELEMENT.NAME, ELEMENT.VALUE FROM ADDITION, ELEMENT WHERE ADDITION.ID = ELEMENT.SID AND ADDITION.NAME = 'DOLMIT'";
            var sqlstr = cfgResult.SQLStr;

            var req = new Requester(ConnectionStr);
            var sqlRes = req.SQLRequest(sqlstr);
            Console.WriteLine("SQL Request");
            Console.WriteLine(sqlRes.ErrorCode);
            Console.WriteLine(sqlRes.ErrorStr);

            foreach (var collumn in sqlRes.ResultData) {
                foreach (var row in collumn.Value) {
                    Console.WriteLine("{0} = {1}; type - {2}", collumn.Key, row, row.GetType());
                }
            }

            Console.ReadLine();
        }
        
    }
}

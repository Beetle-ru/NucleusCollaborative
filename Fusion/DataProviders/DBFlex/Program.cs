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
        static void Main(string[] args)
        {
            var cfg = new CfgLoader("SQLFlexDescriptions");
            var flx = new FlexEvent("Req");
            flx.Arguments.Add("@EventName","SQL.Test");
            flx.Arguments.Add("@Command", "TestCommand1");
            var res = cfg.ReadCfg(flx);

            Console.WriteLine("Status = {0}\n\nError = \n{1}\n\nSQL = \n{2}",res.ErrorCode, res.ErrorStr, res.SQLStr);

            Console.ReadLine();
        }
        
    }
}

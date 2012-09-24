using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPC
{
   
    class Program
    {
        static void Main(string[] args)
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            Client opcClient = new Client(
                mainConf.AppSettings.Settings["OPCServerName"].Value,
                mainConf.AppSettings.Settings["OPCConfigPath"].Value);
            opcClient.Connect();

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using PingLib;
using System.IO;
namespace Pinger
{
    class Program
    {
        public static List<NetMonitor> NetMonitorList; 
        public static string OutFileName;
        public const char Separator = ';';
        public static Configuration MainConf;
        public static object StreamFileLocker = new object();

        static void Main(string[] args)
        {
            var dir = "PingerArch";
            OutFileName = dir + "\\PingerOut.csv";
            Directory.CreateDirectory(dir);
            WriteFile(TableFormatter(DateTime.Now.ToString(), "", "Pinger run"));

            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

            NetMonitorList = new List<NetMonitor>();

            var timeOut = Int32.Parse(MainConf.AppSettings.Settings["TimeOut"].Value);

            var ipAddresses = MainConf.AppSettings.Settings["Address"].Value.Split(Separator);
            if (ipAddresses.Any())
            {
                foreach (var ipAddress in ipAddresses)
                {
                    var nm = new NetMonitor();
                    nm.IPAddress = ipAddress;
                    nm.NetStatusChange = NetStatusChange;
                    nm.Timeout = timeOut;
                    NetMonitorList.Add(nm);
                }
            }
            else
            {
                return;
            }
            Console.WriteLine("Press Enter for exit");
            Console.ReadLine();
            WriteFile(TableFormatter(DateTime.Now.ToString(), "", "Pinger stopped"));
        }
        static public void NetStatusChange(bool netOnline, string ipAddress)
        {
            var date = DateTime.Now.ToString();
            if (netOnline)
            {
                Console.WriteLine("***  NetOnline  *** => {0}", ipAddress);
                WriteFile(TableFormatter(date, ipAddress, "+++ NetOnline"));
            }
            else
            {
                Console.WriteLine("###  NetOffline ### => {0}", ipAddress);
                WriteFile(TableFormatter(date, ipAddress, "--- NetOffline"));
            }
        }

        static public void WriteFile(string msg)
        {
            try
            {
                lock (StreamFileLocker)
                {
                    StreamWriter oStreamWriterutFile;
                    if (File.Exists(OutFileName))
                    {
                        oStreamWriterutFile = File.AppendText(OutFileName);
                    }
                    else
                    {
                        oStreamWriterutFile = File.CreateText(OutFileName);
                        var header = TableFormatter("Time", "Address", "Status");
                        oStreamWriterutFile.WriteLine(header);
                    }

                    oStreamWriterutFile.WriteLine(msg);
                    oStreamWriterutFile.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static public string TableFormatter(string collumn1, string collumn2, string collumn3)
        {
            return String.Format("{1}{0}{2}{0}{3}", Separator, collumn1, collumn2, collumn3);
        }
    }
}

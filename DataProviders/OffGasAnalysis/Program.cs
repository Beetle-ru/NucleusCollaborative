using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Globalization;
using System.Threading;
using System.ServiceModel;
using System.Diagnostics;

namespace OffGasAnalysis
{
    class Program
    {
        public static void Main()
        {
            CultureInfo curCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo newCulture = new CultureInfo(curCulture.Name);
            newCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCulture;

            var granatServers = Process.GetProcessesByName("~GIP5DDE");
            foreach (var granat in granatServers)
            {
                granat.Kill();
            }

            Thread.Sleep(1000);

            GasManager gm = new GasManager();
            gm.Start();

            ProcessStartInfo ps = new ProcessStartInfo("C:\\GS\\Program\\~GIP5DDE.exe");
            ps.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(ps);

            Console.ReadLine();

            granatServers = Process.GetProcessesByName("~GIP5DDE");
            foreach (var granat in granatServers)
            {
                granat.Kill();
            }

            //FileStream fs = new FileStream("..\\..\\..\\..\\T3", FileMode.Open);

            //byte[] buf = new byte[fs.Length];
            //fs.Read(buf, 0, (int)fs.Length);
            //GasData gd = new GasData(buf);
            //Console.Write(gd.ToString());

            //Console.WriteLine(gd.AR);
            //Console.WriteLine(gd.CO);
            //Console.WriteLine(gd.CO2);
            //Console.WriteLine(gd.H2);
            //Console.WriteLine(gd.N2);
            //Console.WriteLine(gd.O2);

            //Console.ReadLine();
        }
    }
}

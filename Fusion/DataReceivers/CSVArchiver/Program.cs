using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Converter;
using Implements;
using Timer = System.Timers.Timer;

namespace CSVArchiver
{
    class Program
    {
        public static List<SecData> SDList;
        public static SecDataSmooth SDS;
        public static string Dir = "CSVArchives";
        public static string Path = Dir + @"\" + ArchNameGenerate("SecData");
        private static Timer m_timer;
        private static Thread receiver_thread;
        static ConnectionProvider.Client m_listenGate;
        public static RollingAverage OxygenRate;
        static void Main(string[] args)
        {
            Init();
            m_timer = new Timer(1000);
            m_timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            m_timer.Enabled = true;
            receiver_thread = new Thread(Receiver);
            receiver_thread.Start();
            //SDList.Add(new SecData());
            //SaverData(SDList);
            Console.ReadLine();
        }

        public static void Init()
        {
            System.IO.Directory.CreateDirectory(Dir);
            SDList = new List<SecData>();
            SDS = new SecDataSmooth();
            OxygenRate = new RollingAverage(300);
            
        }

        public static void SaverData(List<SecData> sdList, long IDHeat = 0)
        {
            Path = Dir + @"\" + ArchNameGenerate('[' + IDHeat.ToString() + ']');

            var strings = new string[sdList.Count+1];

            strings[0] = sdList[0].GetHeader();

            for (int item = 0; item < sdList.Count; item++)
            {
                strings[item+1] = sdList[item].ToString();
            }

            try
            {
                File.WriteAllLines(Path, strings);
            }
            catch (Exception e)
            {
                InstantLogger.err("Cannot write the file: {0}, call exeption: {1}", Path, e.ToString());
                return;
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            const int smoothTime = 1; //sec
            const int oxySmoothTime = 10; //sec
            var smoothOxy = OxygenRate.Average(oxySmoothTime);
            if ((smoothOxy > 0) || (smoothOxy != smoothOxy))
            {
                SDList.Add(SDS.GetSecData(smoothTime));
                InstantLogger.msg(SDList.Last().ToString());
            }
            else
            {
                Console.Write(".");
            }
            
        }

        static void Receiver(object state)
        {
            var o = new HeatChangeEvent();
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();

            InstantLogger.log("receiver", "Started", InstantLogger.TypeMessage.important);
        }

        public static string ArchNameGenerate(string subname)
        {
            string timeLine = DateTime.Now.ToString();
            timeLine = timeLine.Replace(':', '_');
            timeLine = timeLine.Replace('.', '_');
            timeLine = timeLine + subname + ".csv";
            return timeLine;
        }
    }
}

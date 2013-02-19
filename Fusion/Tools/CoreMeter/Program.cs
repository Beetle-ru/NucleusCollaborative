using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using ConnectionProvider;
using Converter;
using Implements;

namespace CoreMeter
{
    class Program
    {
        public static Client MainGate;
        public const char Separator = ';';
        public static Timer FireTimer = new Timer(100);
        public static Timer HourTimer = new Timer(1000 * 60 * 60);
        //public static Timer HourTimer = new Timer(1000);
        public static List<FlexEvent> FlexList;
        public static List<HourResult> ResultList;
        public static string Dir = "CoreMeterArch\\";
        public static string ArchPath = Dir + ArchNameGenerate("res");
        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            //var fex = new FlexHelper("ccc");
            //fex.Fire(MainGate);

            Directory.CreateDirectory(Dir);

            FlexList = new List<FlexEvent>();
            ResultList = new List<HourResult> {new HourResult()};

            FireTimer.Elapsed += new ElapsedEventHandler(FireTimerHandler);
            FireTimer.Enabled = true;

            HourTimer.Elapsed += new ElapsedEventHandler(HourTimerHandler);
            HourTimer.Enabled = true;

            Console.WriteLine("Press Enter for exit\n");
            Console.ReadLine();
        }

        public static void HourTimerHandler(object source, ElapsedEventArgs e)
        {
            FireTimer.Enabled = false;
            lock (ResultList)
            {
                System.Threading.Thread.Sleep(1000);
                var last = ResultList.Count - 1;
                ResultList[last].LostEvents = ResultList[last].FieredEvents - ResultList[last].ReceivedEvents;
                SaveMatrix(ArchPath);
                FlexList = new List<FlexEvent>();
                ResultList.Add(new HourResult());
                Console.Write("#\n");
            }
            FireTimer.Enabled = true;
        }

        public static void FireTimerHandler(object source, ElapsedEventArgs e)
        {
            var fex = new FlexHelper("CoreMeteringEvent");
            fex.AddArg("SendTime", DateTime.Now);
            FlexList.Add(fex.evt);
            fex.Fire(MainGate);
            var last = ResultList.Count - 1;
            ResultList[last].FieredEvents++;
            //Console.Write(".");
            //SaveMatrix(ArchPath);
        }

        public static Int64 GetMs(DateTime time)
        {
            return time.Millisecond + time.Second * 1000 + time.Minute * 1000 * 60 * time.Hour * 1000 * 60 * 60;
        }

        public static void SaveMatrix(string path)
        {
            using (var l = new Logger("SaveMatrix"))
            {
                string[] strings = new string[ResultList.Count + 1];

                strings[0] = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}",
                                                     Separator,
                                                     "BeginTime",
                                                     "FieredEvents",
                                                     "ReceivedEvents",
                                                     "LostEvents",
                                                     "MaxDelayMs",
                                                     "AverageDelayMs",
                                                     "TotalDelayMs"
                        );

                for (int dataCnt = 0; dataCnt < ResultList.Count; dataCnt++)
                {
                    strings[dataCnt + 1] = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}",
                                                     Separator,
                                                     ResultList[dataCnt].Time,
                                                     ResultList[dataCnt].FieredEvents,
                                                     ResultList[dataCnt].ReceivedEvents,
                                                     ResultList[dataCnt].LostEvents,
                                                     ResultList[dataCnt].MaxDelayMs,
                                                     ResultList[dataCnt].AverageDelayMs,
                                                     ResultList[dataCnt].TotalDelayMs
                        );
                }
                try
                {
                    File.WriteAllLines(path, strings);
                }
                catch (Exception e)
                {
                    l.err("Cannot write the file: {0}, call exeption: {1}", path, e.ToString());
                    return;
                    //throw;
                }
            }
        }
        public static string ArchNameGenerate(string subname)
        {
            var dt = DateTime.Now;
            string timeLine = String.Format("Y{0}M{1}D{2}H{3}m{4}S{5}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            timeLine = timeLine + subname + ".csv";
            return timeLine;
        }
    }
}

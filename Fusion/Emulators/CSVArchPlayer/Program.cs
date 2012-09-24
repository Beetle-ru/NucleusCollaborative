using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Timers;
using ConnectionProvider;
using Converter;
using Implements;

namespace CSVArchPlayer
{
    class Program
    {
        public static char Separator = ';';
        private static Timer m_timer;
        private static int m_position;
        public static List<HeatData> HDataList;
        public static ConnectionProvider.Client MainGate;
        private static double m_totalO2;

        static void Main(string[] args)
        {
            Settings sttngs = ParceArgs(args);
            if (sttngs != null)
            {
                Console.WriteLine("ok");
                HDataList = LoadHd(sttngs.File.ElementAt(0).Value);
                if (HDataList != null)
                {
                    m_position = 0;
                    MainGate = new Client();
                    m_timer = new Timer(1000);
                    m_timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    m_timer.Enabled = true;
                }
                else
                {
                    return;
                }
            //for (int i = 0; i < hDataList.Count; i++)
                //{
                //    Console.WriteLine(hDataList[i].HeightLance);
                    
                //}
            }
            else
            {
                return;
            }
            Console.WriteLine("press any key for exit");
            Console.ReadLine();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("{0} | {1}\t| {2}\t| {3}\t| {4}\t| {5}\t| {6}\t| {7}",
                HDataList[m_position].HeightLance,
                HDataList[m_position].RateO2,
                HDataList[m_position].H2,
                HDataList[m_position].O2,
                HDataList[m_position].CO,
                HDataList[m_position].CO2,
                HDataList[m_position].N2,
                HDataList[m_position].Ar
               );
            var le = new LanceEvent();
            le.LanceHeight = HDataList[m_position].HeightLance;
            le.O2Flow = HDataList[m_position].RateO2;
            m_totalO2 += HDataList[m_position].RateO2/60;
            le.O2TotalVol = (int)m_totalO2;

            var offGA = new OffGasAnalysisEvent();
            offGA.H2 = HDataList[m_position].H2;
            offGA.O2 = HDataList[m_position].O2;
            offGA.CO = HDataList[m_position].CO;
            offGA.CO2 = HDataList[m_position].CO2;
            offGA.N2 = HDataList[m_position].N2;
            offGA.Ar = HDataList[m_position].Ar;

            var offG = new OffGasEvent();
            offG.OffGasFlow = HDataList[m_position].VOffGas;

            var bE = new BlowingEvent();
            bE.O2TotalVol = (int)m_totalO2;

            MainGate.PushEvent(le);
            MainGate.PushEvent(offGA);
            MainGate.PushEvent(offG);
            MainGate.PushEvent(bE);

            if (m_position < HDataList.Count)
            {
                m_position++;
            }
            else
            {
                m_timer.Enabled = false;
            }
        }
        private static List<HeatData> LoadHd(string fileName)
        {
            var heatDataList = new List<HeatData>();
            string[] strings;
            try
            {
                strings = File.ReadAllLines(fileName);
            }
            catch
            {
                strings = new string[0];
                Console.WriteLine("Cannot read the file: {0}", fileName);
                return null;
            }
            try
            {
                int itemCounter = 0;
                for (int strCnt = 1; strCnt < strings.Count(); strCnt++)
                {
                    string[] values = strings[strCnt].Split(Separator);
                    heatDataList.Add(new HeatData());
                    heatDataList[itemCounter].DTime = Convertion.StrToDateTime(values[0]);
                    heatDataList[itemCounter].HeightLance = Convertion.StrToInt32(values[1]);
                    heatDataList[itemCounter].RateO2 = Convertion.StrToDouble(values[2]);
                    heatDataList[itemCounter].H2 = Convertion.StrToDouble(values[3]);
                    heatDataList[itemCounter].O2 = Convertion.StrToDouble(values[4]);
                    heatDataList[itemCounter].CO = Convertion.StrToDouble(values[5]);
                    heatDataList[itemCounter].CO2 = Convertion.StrToDouble(values[6]);
                    heatDataList[itemCounter].N2 = Convertion.StrToDouble(values[7]);
                    heatDataList[itemCounter].Ar = Convertion.StrToDouble(values[8]);
                    heatDataList[itemCounter].VOffGas = Convertion.StrToDouble(values[9]);
                    itemCounter++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot parce the file: {0}, bad format call exeption: \n{1}", fileName, e.ToString());
                return null;
            }
            return heatDataList;
        }
        private static Settings ParceArgs(string[] args)
        {
            var sttngs = new Settings();
            var propIsFind = false;
            try
            {
                for (int i = 0; i < args.Count(); i++)
                {
                    if (sttngs.File.ContainsKey(args[i]))
                    {
                        sttngs.File[args[i]] = args[i + 1];
                        propIsFind = true;
                    }
                }
            }
            catch (Exception)
            {
                propIsFind = false;
            }
           
            if (!propIsFind)
            {
                PrintHelp();
                return null;
            }
            return sttngs;
        }
        private static void PrintHelp()
        {
            string str = "";
            str += "-----------------[help]---------------\n";
            str += "CSVArchPlayer -f [fileName.csv]\n";
            str += "--------------------------------------";
            Console.WriteLine(str);
        }
    }

    class Settings
    {
        public Dictionary<string, string> File;
        public Settings()
        {
            File = new Dictionary<string, string>();
            File.Add("-f","");
        }
    }
    
}

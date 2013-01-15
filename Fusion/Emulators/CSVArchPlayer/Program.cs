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
        private static bool m_vPathIsOutput;
        private static VPathData m_vPathDataLast;
        private static Int64 m_heatNumber;
        private static bool m_sublanceCIsPushed;
       

        static void Main(string[] args)
        {
            Settings sttngs = ParceArgs(args);
            if (sttngs != null)
            {
                Console.WriteLine("ok");
                HDataList = LoadHd(sttngs.File.ElementAt(0).Value);
                var filePathSplt = sttngs.File.ElementAt(0).Value.Split('\\');
                m_heatNumber = ReadHeatNumber(filePathSplt[filePathSplt.Count() - 1]);

                Console.WriteLine("HeatNumber -- {0}", m_heatNumber);
                
                

                if (HDataList != null)
                {
                    m_position = 0;
                    MainGate = new Client();

                    System.Threading.Thread.Sleep(6000); // Ждем открытия ворот

                    MainGate.PushEvent(new HeatChangeEvent() { HeatNumber = m_heatNumber });

                    m_vPathDataLast = new VPathData();
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
            m_vPathIsOutput = !VPathDataIsEqual(HDataList[m_position].Bunkers, m_vPathDataLast);
            if (m_vPathIsOutput)
            {
                Console.WriteLine(
                    "{0:00000} > {1:000} | {2:0000.0} | {3:00.0} | {4:00.0} | {5:00.0} | {6:00.0} | {7:00.0} | {8:00.0} " +
                    "| {9:0000} | {10:0000} | {11:0000} | {12:0000} | {13:0000} | {14:0000} | {15:0000} | {16:0000}",
                    m_totalO2,
                    HDataList[m_position].HeightLance,
                    HDataList[m_position].RateO2,
                    HDataList[m_position].H2,
                    HDataList[m_position].O2,
                    HDataList[m_position].CO,
                    HDataList[m_position].CO2,
                    HDataList[m_position].N2,
                    HDataList[m_position].Ar,
                    HDataList[m_position].Bunkers.RB5,
                    HDataList[m_position].Bunkers.RB6,
                    HDataList[m_position].Bunkers.RB7,
                    HDataList[m_position].Bunkers.RB8,
                    HDataList[m_position].Bunkers.RB9,
                    HDataList[m_position].Bunkers.RB10,
                    HDataList[m_position].Bunkers.RB11,
                    HDataList[m_position].Bunkers.RB12
                );
                m_vPathDataLast = HDataList[m_position].Bunkers;
            }
            else
            {
                Console.WriteLine(
                    "{0:00000} > {1:000} | {2:0000.0} | {3:00.0} | {4:00.0} | {5:00.0} | {6:00.0} | {7:00.0} | {8:00.0}",
                    m_totalO2,
                    HDataList[m_position].HeightLance,
                    HDataList[m_position].RateO2,
                    HDataList[m_position].H2,
                    HDataList[m_position].O2,
                    HDataList[m_position].CO,
                    HDataList[m_position].CO2,
                    HDataList[m_position].N2,
                    HDataList[m_position].Ar
                );
            }
            var le = new LanceEvent();
            le.LanceHeight = HDataList[m_position].HeightLance;
            le.O2Flow = HDataList[m_position].RateO2;
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
            bE.BlowingFlag = 1;

            var vate = new visAdditionTotalEvent();
            vate.RB5TotalWeight = HDataList[m_position].Bunkers.RB5;
            vate.RB6TotalWeight = HDataList[m_position].Bunkers.RB6;
            vate.RB7TotalWeight = HDataList[m_position].Bunkers.RB7;
            vate.RB8TotalWeight = HDataList[m_position].Bunkers.RB8;
            vate.RB9TotalWeight = HDataList[m_position].Bunkers.RB9;
            vate.RB10TotalWeight = HDataList[m_position].Bunkers.RB10;
            vate.RB11TotalWeight = HDataList[m_position].Bunkers.RB11;
            vate.RB12TotalWeight = HDataList[m_position].Bunkers.RB12;

            if ((HDataList[m_position].SublanceC > 0) && !m_sublanceCIsPushed)
            {
                Int64 reminder = 0;
                Int64 res = Math.DivRem(m_heatNumber, 10000, out reminder);
                Int64 longHN = res * 100000 + reminder;
                MainGate.PushEvent(new visSpectrluksEvent() { C = HDataList[m_position].SublanceC, HeatNumber = longHN});
                Console.WriteLine("specroluks push Heat = {0} ", longHN);
                MainGate.PushEvent(new SublanceCEvent() { C = HDataList[m_position].SublanceC });
                m_sublanceCIsPushed = true;
                Console.WriteLine("Carbone pushed C = {0}", HDataList[m_position].SublanceC);
            }

            MainGate.PushEvent(le);
            MainGate.PushEvent(offGA);
            MainGate.PushEvent(offG);
            MainGate.PushEvent(bE);

            m_totalO2 += HDataList[m_position].RateO2 * 0.01666666666666666666666666666667;

            if (m_vPathIsOutput) MainGate.PushEvent(vate);

            //Console.WriteLine("m_position -- {0}; HDataList.Count -- {1}", m_position, HDataList.Count);
            if (m_position < HDataList.Count - 1)
            {
                m_position++;
            }
            else
            {
                Console.WriteLine("Exit 0");
                System.Environment.Exit(0);
                m_timer.Enabled = false;
            }
        }
        private static Int64 ReadHeatNumber(string fileName)
        {
            Int64 res = 0;
            var splitedFN = fileName.Split('[');
            if (splitedFN.Any())
            {
                var endFN = splitedFN[splitedFN.Count() - 1].Split(']');
                if (endFN.Any())
                {
                    try
                    {
                        res = Int64.Parse(endFN[0]);
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            return res;
        }

        private static List<HeatData> LoadHd(string fileName)
        {
            var heatDataList = new List<HeatData>();
            m_vPathIsOutput = false;
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
                    heatDataList[itemCounter].SublanceC = Convertion.StrToDouble(values[12]);
                    if (values.Count() >= 28)
                    {
                        heatDataList[itemCounter].Bunkers.RB5 = Convertion.StrToDouble(values[21]);
                        heatDataList[itemCounter].Bunkers.RB6 = Convertion.StrToDouble(values[22]);
                        heatDataList[itemCounter].Bunkers.RB7 = Convertion.StrToDouble(values[23]);
                        heatDataList[itemCounter].Bunkers.RB8 = Convertion.StrToDouble(values[24]);
                        heatDataList[itemCounter].Bunkers.RB9 = Convertion.StrToDouble(values[25]);
                        heatDataList[itemCounter].Bunkers.RB10 = Convertion.StrToDouble(values[26]);
                        heatDataList[itemCounter].Bunkers.RB11 = Convertion.StrToDouble(values[27]);
                        heatDataList[itemCounter].Bunkers.RB12 = Convertion.StrToDouble(values[28]);
                        //m_vPathIsOutput = true;
                    }
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
        private static bool VPathDataIsEqual(VPathData vpd1, VPathData vpd2)
        {
            bool res = 
                (vpd1.RB5 != vpd2.RB5) || 
                (vpd1.RB6 != vpd2.RB6) || 
                (vpd1.RB7 != vpd2.RB7) || 
                (vpd1.RB8 != vpd2.RB8) || 
                (vpd1.RB9 != vpd2.RB9) || 
                (vpd1.RB10 != vpd2.RB10) || 
                (vpd1.RB11 != vpd2.RB11) || 
                (vpd1.RB12 != vpd2.RB12);
            return !res;
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

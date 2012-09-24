using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using HeatCharge;
using Implements;

namespace NeuralProcessorC
{
    class Program
    {
        public static Client PushGate;
        private static Client m_listenGate;
        public static List<MFCMDataFull> MatrixStateDataFull;
        public static List<MFCMDataFull> MatrixStateDataFullTotal = new List<MFCMDataFull>();
        public static Dictionary<Int64, MFCMDataFull> WaitCarbonDic = new Dictionary<Int64, MFCMDataFull>(); // очередь ожидания углерода
        public const string PathArch = @"Archives";
        public static string ArchFileName = PathArch + @"\" + ArchNameGenerate("res");
        public static string Path;
        public static char Separator;
        public static int ConverterNumber;
        
        static void Main(string[] args)
        {
            Directory.CreateDirectory(PathArch);
            Path = ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["matrix"].Value;
            try
            {
                Separator = ((string)ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["separator"].Value).ToCharArray()[0];
                ConverterNumber = Convertion.StrToInt32(
                        (string)ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["converterNumber"].Value);
            }
            catch (Exception e)
            {
                InstantLogger.err("Bad config called error: {0}", e.ToString());
                throw e;
            }

            LoadMatrix(Path, Separator, out MatrixStateDataFull);
            
            CIterator.Init();


            var o = new HeatChangeEvent();
            PushGate = new Client();
            m_listenGate = new Client(new Listener());
            m_listenGate.Subscribe();

            Console.WriteLine("Carbone processor is running, press enter to exit");
            Console.ReadLine();

        }
        public static void LoadMatrix(string path, char separator, out List<MFCMDataFull> matrixStateData)
        {
            using (Logger l = new Logger("LoadMatrix"))
            {
                matrixStateData = new List<MFCMDataFull>();
                string[] strings;
                try
                {
                    strings = File.ReadAllLines(path);
                }
                catch
                {
                    strings = new string[0];
                    l.err("Cannot read the file: {0}", path);
                    return;
                }

                try
                {
                    for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                    {
                        string[] values = strings[strCnt].Split(separator);
                        matrixStateData.Add(new MFCMDataFull()
                        {
                            IdHeat = Convertion.StrToInt32(values[0]),
                            NumberHeat = Convertion.StrToInt64(values[1]),
                            CarbonMonoxideVolumePercent = Convertion.StrToDouble(values[2]),
                            CarbonOxideVolumePercent = Convertion.StrToDouble(values[3]),
                            HeightLanceCentimeters = Convertion.StrToInt32(values[4]),
                            OxygenVolumeRate = Convertion.StrToDouble(values[5]),
                            SteelCarbonPercent = Convertion.StrToDouble(values[6]),
                            SteelCarbonCalculationPercent = Convertion.StrToDouble(values[7])
                        });
                    }
                }
                catch (Exception e)
                {
                    l.err("Cannot read the file: {0}, bad format call exeption: {1}", path, e.ToString());
                    //return;
                    throw e;
                }
            }
        }
        public static void SaveMatrix(string path, char separator, List<MFCMDataFull> matrixStateDataFull)
        {
            using (Logger l = new Logger("SaveMatrix"))
            {
                string[] strings = new string[matrixStateDataFull.Count];
                for (int dataCnt = 0; dataCnt < matrixStateDataFull.Count; dataCnt++)
                {
                    strings[dataCnt] = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                                                     separator,
                                                     matrixStateDataFull[dataCnt].IdHeat,
                                                     matrixStateDataFull[dataCnt].NumberHeat,
                                                     matrixStateDataFull[dataCnt].CarbonMonoxideVolumePercent,
                                                     matrixStateDataFull[dataCnt].CarbonOxideVolumePercent,
                                                     matrixStateDataFull[dataCnt].HeightLanceCentimeters,
                                                     matrixStateDataFull[dataCnt].OxygenVolumeRate,
                                                     matrixStateDataFull[dataCnt].SteelCarbonPercent,
                                                     matrixStateDataFull[dataCnt].SteelCarbonCalculationPercent
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
            string timeLine = DateTime.Now.ToString();
            timeLine = timeLine.Replace(':', '_');
            timeLine = timeLine.Replace('.', '_');
            timeLine = timeLine + subname + ".csv";
            return timeLine;
        }

        public static List<MFCMData> MFCMDataGenerate(List<MFCMDataFull> inMfcmDataFull )
        {
            var outMFCMData = new List<MFCMData>();
            for (int i = 0; i < inMfcmDataFull.Count; i++)
            {
                outMFCMData.Add(new MFCMData());
                outMFCMData[i].CarbonMonoxideVolumePercent = inMfcmDataFull[i].CarbonMonoxideVolumePercent;
                outMFCMData[i].CarbonOxideVolumePercent = inMfcmDataFull[i].CarbonOxideVolumePercent;
                outMFCMData[i].HeightLanceCentimeters = inMfcmDataFull[i].HeightLanceCentimeters;
                outMFCMData[i].OxygenVolumeRate = inMfcmDataFull[i].OxygenVolumeRate;
                outMFCMData[i].SteelCarbonPercent = inMfcmDataFull[i].SteelCarbonPercent;
            }
            return outMFCMData;
        }

        
    }
}

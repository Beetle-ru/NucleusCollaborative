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

namespace CPlusProcessor
{
    class Program
    {
        public static Client MainGate;
        public const char Separator = ';';
        public static Configuration MainConf;
        public static string MatrixPath;
        public static string MatrixTotalDir;
        public static string MatrixTotalPath;
        public static double COMax;
        public static double COMin;

        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

            MatrixPath = MainConf.AppSettings.Settings["MatrixPath"].Value;
            MatrixTotalDir = MainConf.AppSettings.Settings["MatrixTotalDir"].Value;
            Directory.CreateDirectory(MatrixTotalDir);
            MatrixTotalPath = MatrixTotalDir + "\\" + ArchNameGenerate("TOTAL");
            COMax = Double.Parse(MainConf.AppSettings.Settings["COMax"].Value);
            COMin = Double.Parse(MainConf.AppSettings.Settings["COMin"].Value);

            Iterator.Init();

            Console.WriteLine("Press Enter for exit\n");
            Console.ReadLine();
        }

        public static void LoadMatrix(string path, out List<MFCPData> matrixStateData)
        {
            using (Logger l = new Logger("LoadMatrix"))
            {
                matrixStateData = new List<MFCPData>();
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
                        string[] values = strings[strCnt].Split(Separator);
                        matrixStateData.Add(new MFCPData()
                        {
                            HeatNumber = Convertion.StrToInt64(values[0]),
                            TimeFromX = Convertion.StrToInt32(values[1]),
                            CarbonOxideIVP = Convertion.StrToDouble(values[2]),
                            SteelCarbonPercent = Convertion.StrToDouble(values[3]),
                            SteelCarbonPercentCalculated = Convertion.StrToDouble(values[4]),
                            HightQualityHeat = Convertion.StrToBool(values[5])
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
        public static void SaveMatrix(string path, List<MFCPData> matrixStateDataFull)
        {
            using (Logger l = new Logger("SaveMatrix"))
            {
                string[] strings = new string[matrixStateDataFull.Count];
                for (int dataCnt = 0; dataCnt < matrixStateDataFull.Count; dataCnt++)
                {
                    strings[dataCnt] = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                                                     Separator,
                                                     matrixStateDataFull[dataCnt].HeatNumber,
                                                     matrixStateDataFull[dataCnt].TimeFromX,
                                                     matrixStateDataFull[dataCnt].CarbonOxideIVP,
                                                     matrixStateDataFull[dataCnt].SteelCarbonPercent,
                                                     matrixStateDataFull[dataCnt].SteelCarbonPercentCalculated,
                                                     matrixStateDataFull[dataCnt].HightQualityHeat
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
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using ConnectionProvider;
using Converter;
using System.Configuration;
using HeatCharge;
using Implements;

namespace SMFCarbon {
    internal class Program {
        public static Client PushGate;
        private static Client m_listenGate;
        public static Dictionary<int, Matrix> MatrixStateDataFull = new Dictionary<int, Matrix>();
        public static List<MFCMDataFull> MatrixStateDataFullTotal = new List<MFCMDataFull>();

        public static Dictionary<Int64, MFCMDataFull> WaitCarbonDic = new Dictionary<Int64, MFCMDataFull>();
                                                      // очередь ожидания углерода

        public const string PathArch = @"Archives";
        public static string ArchFileName = PathArch + @"\" + ArchNameGenerate("res");
        //public static string ArchFileName = PathArch + @"\" + ArchNameGenerate("res");
        public static Dictionary<int, string> ModelsPathDic = new Dictionary<int, string>();
        public static char Separator;
        public static int ConverterNumber;

        private static void Main(string[] args) {
            Directory.CreateDirectory(PathArch);

            try {
                Separator =
                    ((string) ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["separator"].Value).
                        ToCharArray()[0];
                ConverterNumber = Convertion.StrToInt32(
                    (string) ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["converterNumber"].Value);
            }
            catch (Exception e) {
                InstantLogger.err("Bad config called error: {0}", e.ToString());
                throw e;
            }


            AnyMatryxLoader();
            CIterator.Init();
            CIterator.IterateTimer.Elapsed += new ElapsedEventHandler(CIterator.IterateTimeOut);
            CIterator.IterateTimer.Enabled = true;
            //CIterator.DataCurrentHeat.MatrixStateData = MFCMDataGenerate(MatrixStateDataFull);

            var o = new HeatChangeEvent();
            PushGate = new Client();
            m_listenGate = new Client(new Listener());
            m_listenGate.Subscribe();

            Console.WriteLine("Carbone processor is running, press enter to exit");
            Console.ReadLine();
        }

        public static void LoadMatrix(string path, char separator, out List<MFCMDataFull> matrixStateData) {
            using (Logger l = new Logger("LoadMatrix")) {
                matrixStateData = new List<MFCMDataFull>();
                string[] strings;
                try {
                    strings = File.ReadAllLines(path);
                }
                catch {
                    strings = new string[0];
                    l.err("Cannot read the file: {0}", path);
                    return;
                }

                try {
                    for (int strCnt = 0; strCnt < strings.Count(); strCnt++) {
                        string[] values = strings[strCnt].Split(separator);
                        matrixStateData.Add(new MFCMDataFull() {
                                                                   IdHeat = Convertion.StrToInt32(values[0]),
                                                                   NumberHeat = Convertion.StrToInt64(values[1]),
                                                                   CarbonMonoxideVolumePercent =
                                                                       Convertion.StrToDouble(values[2]),
                                                                   CarbonOxideVolumePercent =
                                                                       Convertion.StrToDouble(values[3]),
                                                                   HeightLanceCentimeters =
                                                                       Convertion.StrToInt32(values[4]),
                                                                   OxygenVolumeRate = Convertion.StrToDouble(values[5]),
                                                                   SteelCarbonPercent =
                                                                       Convertion.StrToDouble(values[6]),
                                                                   SteelCarbonCalculationPercent =
                                                                       Convertion.StrToDouble(values[7])
                                                               });
                    }
                }
                catch (Exception e) {
                    l.err("Cannot read the file: {0}, bad format call exeption: {1}", path, e.ToString());
                    //return;
                    throw e;
                }
            }
        }

        public static void SaveMatrix(string path, char separator, List<MFCMDataFull> matrixStateDataFull) {
            using (Logger l = new Logger("SaveMatrix")) {
                string[] strings = new string[matrixStateDataFull.Count];
                for (int dataCnt = 0; dataCnt < matrixStateDataFull.Count; dataCnt++) {
                    strings[dataCnt] = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",
                                                     separator,
                                                     matrixStateDataFull[dataCnt].IdHeat,
                                                     matrixStateDataFull[dataCnt].NumberHeat,
                                                     matrixStateDataFull[dataCnt].CarbonMonoxideVolumePercent,
                                                     matrixStateDataFull[dataCnt].CarbonOxideVolumePercent,
                                                     matrixStateDataFull[dataCnt].HeightLanceCentimeters,
                                                     matrixStateDataFull[dataCnt].OxygenVolumeRate,
                                                     matrixStateDataFull[dataCnt].SteelCarbonPercent,
                                                     matrixStateDataFull[dataCnt].SteelCarbonCalculationPercent,
                                                     matrixStateDataFull[dataCnt].MFMEquationId
                        );
                }
                try {
                    File.WriteAllLines(path, strings);
                }
                catch (Exception e) {
                    l.err("Cannot write the file: {0}, call exeption: {1}", path, e.ToString());
                    return;
                    //throw;
                }
            }
        }

        public static string ArchNameGenerate(string subname) {
            string timeLine = DateTime.Now.ToString();
            timeLine = timeLine.Replace(':', '_');
            timeLine = timeLine.Replace('.', '_');
            timeLine = timeLine + subname + ".csv";
            return timeLine;
        }

        public static List<MFCMData> MFCMDataGenerate(List<MFCMDataFull> inMfcmDataFull) {
            var outMFCMData = new List<MFCMData>();
            for (int i = 0; i < inMfcmDataFull.Count; i++) {
                outMFCMData.Add(new MFCMData());
                outMFCMData[i].CarbonMonoxideVolumePercent = inMfcmDataFull[i].CarbonMonoxideVolumePercent;
                outMFCMData[i].CarbonOxideVolumePercent = inMfcmDataFull[i].CarbonOxideVolumePercent;
                outMFCMData[i].HeightLanceCentimeters = inMfcmDataFull[i].HeightLanceCentimeters;
                outMFCMData[i].OxygenVolumeRate = inMfcmDataFull[i].OxygenVolumeRate;
                outMFCMData[i].SteelCarbonPercent = inMfcmDataFull[i].SteelCarbonPercent;
            }
            return outMFCMData;
        }

        private static void AnyMatryxLoader() {
            const int mfmEquations = 2;
            for (int mfmEquation = 0; mfmEquation < mfmEquations; mfmEquation++) {
                ModelsPathDic.Add(mfmEquation,
                                  ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings[
                                      String.Format("matrix_{0}", mfmEquation)].Value);
                List<MFCMDataFull> loadedMatrixFull;
                LoadMatrix(ModelsPathDic[mfmEquation], Separator, out loadedMatrixFull);
                MatrixStateDataFull.Add(mfmEquation, new Matrix() {MatrixList = loadedMatrixFull});
            }
        }
    }
}
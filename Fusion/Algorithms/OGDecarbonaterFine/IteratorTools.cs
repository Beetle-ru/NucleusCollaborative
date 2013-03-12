using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Converter;
using Implements;

namespace OGDecarbonaterFine
{
    internal static partial class Iterator
    {

        public static void Init()
        {
            Directory.CreateDirectory(ArchDir);
            Reset();

            HimMaterials = new XimTable();
            HimMaterials.LoadFromCSV(CSVHimFilePath);

            Program.MainGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(BoundNameMaterialsEvent).Name });

            IterateTimer.Elapsed += new ElapsedEventHandler(IterateTimeOut);
            IterateTimer.Enabled = true;

            LoadMatrix(MatrixFileName);
            QueueWaitCarbon = new List<MFOGDFData>();
        }

        public static void Reset()
        {
            Receiver = new HeatDataReceiver(PeriodSec);
            CurrentState = new RecalculateData();
            InputDataBuffer = new List<InputData>();
            MaterialsZeroLevel = new SupportMaterials();
            ArchFileName = String.Format("{0}\\{1}", ArchDir, ArchNameGenerate(""));
            Console.WriteLine("Reset");
        }

        public static void ArchFileGen()
        {
            ArchFileName = String.Format("{0}\\{1}", ArchDir, ArchNameGenerate(CurrentState.HeatNumber.ToString()));
            WriteFile(CurrentState.GetHeaderLine(), ArchFileName);
        }

        public static void PutInputDataIntoTheBuffer()
        {
            var data = new InputData();
            data.Ar = Receiver.GetAr();
            data.CO = Receiver.GetCO();
            data.CO2 = Receiver.GetCO2();
            data.H2 = Receiver.GetH2();
            data.N2 = Receiver.GetN2();
            data.O2 = Receiver.GetO2();
            data.OffGasDecompression = Receiver.GetOffGasDecompression();
            data.OffGasT = Receiver.GetOffGasT();
            data.OffGasV = Receiver.GetOffGasV();
            data.LanceHeight = Receiver.GetLanceHeight();
            data.QO2 = Receiver.QO2;
            data.QO2I = Receiver.QO2I;

            InputDataBuffer.Add(data);
        }

        public static void SyncInputData()
        {
            if (InputDataBuffer.Any() && (InputDataBuffer.Count > CurrentState.OffGasTransportDelay))
            {
                var currentSecond = InputDataBuffer.Count - 1;
                var delayedSecond = currentSecond - CurrentState.OffGasTransportDelay;
                CurrentState.Ar = InputDataBuffer[currentSecond].Ar;
                CurrentState.CO = InputDataBuffer[currentSecond].CO;
                CurrentState.CO2 = InputDataBuffer[currentSecond].CO2;
                CurrentState.H2 = InputDataBuffer[currentSecond].H2;
                CurrentState.N2 = InputDataBuffer[currentSecond].N2;
                CurrentState.O2 = InputDataBuffer[currentSecond].O2;

                CurrentState.OffGasV = InputDataBuffer[delayedSecond].OffGasV;
                CurrentState.OffGasT = InputDataBuffer[delayedSecond].OffGasT;
                CurrentState.OffGasDecompression = InputDataBuffer[delayedSecond].OffGasDecompression;
                CurrentState.LanceHeight = InputDataBuffer[delayedSecond].LanceHeight;
                CurrentState.QO2 = InputDataBuffer[delayedSecond].QO2;
                CurrentState.QO2I = InputDataBuffer[delayedSecond].QO2I;
            }
        }

        public static void SyncPushInputData()
        {
            PutInputDataIntoTheBuffer();
            SyncInputData();
        }

        static public void PushCarbon(double carbon)
        {
            const double tresholdCarbon = 0.00;
            carbon = carbon < tresholdCarbon ? tresholdCarbon : carbon; // ограничение на углерод

            var fex = new ConnectionProvider.FlexHelper("OGDecarbonaterFine.Result");
            fex.AddArg("C", carbon);
            fex.Fire(Program.MainGate);
        }

        static public void WriteFile(string msg, string outFileName)
        {
            try
            {
                StreamWriter oStreamWriterutFile;
                if (File.Exists(outFileName))
                {
                    oStreamWriterutFile = File.AppendText(outFileName);
                }
                else
                {
                    oStreamWriterutFile = File.CreateText(outFileName);
                }

                oStreamWriterutFile.WriteLine(msg);
                oStreamWriterutFile.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static string ArchNameGenerate(string subname)
        {
            string timeLine = DateTime.Now.ToString();
            timeLine = timeLine.Replace(':', '_');
            timeLine = timeLine.Replace('.', '_');
            timeLine = timeLine + "_" + subname + ".csv";
            return timeLine;
        }

        public static void LoadMatrix(string fileName)
        {
            using (Logger l = new Logger("LoadMatrix"))
            {
                Matrix = new List<MFOGDFData>();
                string[] strings;
                try
                {
                    strings = File.ReadAllLines(fileName);
                }
                catch
                {
                    strings = new string[0];
                    l.err("Cannot read the file: {0}", fileName);
                    return;
                }

                try
                {
                    for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                    {
                        string[] values = strings[strCnt].Split(Separator);
                        Matrix.Add(new MFOGDFData()
                        {
                            HeatNumber =  Convertion.StrToInt64(values[0]),
                            DeltaK =      Convertion.StrToDouble(values[1]),
                            DeltaCarbon = Convertion.StrToDouble(values[2]),
                            MFe =         Convertion.StrToDouble(values[3]),
                            MCarbonCalc = Convertion.StrToDouble(values[4]),
                            MCarbonReal = Convertion.StrToDouble(values[5]),
                            PCarbonCalc = Convertion.StrToDouble(values[6]),
                            PCarbonReal = Convertion.StrToDouble(values[7]),
                        });
                    }
                }
                catch (Exception e)
                {
                    l.err("Cannot read the file: {0}, bad format call exeption: {1}", fileName, e.ToString());
                    //return;
                    throw e;
                }
            }
        }

        public static void SaveMatrix(string fileName)
        {
            using (Logger l = new Logger("SaveMatrix"))
            {
                string[] strings = new string[Matrix.Count];
                for (int dataCnt = 0; dataCnt < Matrix.Count; dataCnt++)
                {
                    strings[dataCnt] = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                                                     Separator,
                                                     Matrix[dataCnt].HeatNumber,
                                                     Matrix[dataCnt].DeltaK,
                                                     Matrix[dataCnt].DeltaCarbon,
                                                     Matrix[dataCnt].MFe,
                                                     Matrix[dataCnt].MCarbonCalc,
                                                     Matrix[dataCnt].MCarbonReal,
                                                     Matrix[dataCnt].PCarbonCalc,
                                                     Matrix[dataCnt].PCarbonReal
                                                    );
                }
                try
                {
                    File.WriteAllLines(fileName, strings);
                }
                catch (Exception e)
                {
                    l.err("Cannot write the file: {0}, call exeption: {1}", fileName, e.ToString());
                    return;
                    //throw;
                }
            }
        }

        public static void VerifyFixAndEnqueue()
        {
            const int lanceFixPositionTreshold = 330;
            const int oxigenTreshold = 16500;

            var fix = (!CurrentState.DataFinishFixed) &&
                      (CurrentState.QO2I > oxigenTreshold) &&
                      (CurrentState.LanceHeight >= lanceFixPositionTreshold);

            if (fix)
            {
                CurrentState.DataFinishFixed = true;
                
                var item = new MFOGDFData();
                item.HeatNumber = CurrentState.HeatNumber;
                item.MCarbonCalc = CurrentState.FixPointCarbonResult;
                item.PCarbonCalc = CurrentState.FixPointPC;
                item.MFe = CurrentState.CurrentMF;
                item.DeltaK = CurrentState.FixPointDeltaK;
                
                QueueWaitCarbon.Add(item);
            }
        }

        public static void FindAndDequeue(double carbonReal, Int64 HeatNumber)
        {
            for (int i = 0; i < QueueWaitCarbon.Count; i++)
            {
                if (QueueWaitCarbon[i].HeatNumber == HeatNumber)
                {
                    QueueWaitCarbon[i].PCarbonReal = carbonReal;
                    QueueWaitCarbon[i].MCarbonReal = (carbonReal * QueueWaitCarbon[i].MFe)/100;
                    QueueWaitCarbon[i].DeltaCarbon = QueueWaitCarbon[i].MCarbonCalc - QueueWaitCarbon[i].MCarbonReal;

                    VerifyAndAddItemMatrix(QueueWaitCarbon[i]);
                    QueueWaitCarbon.RemoveAt(i);
                }
            }
            if (QueueWaitCarbon.Count > 1000) QueueWaitCarbon.Clear(); // на всякиц случай защита от переполнения
        }

        public static void VerifyAndAddItemMatrix(MFOGDFData item)
        {
            if (Matrix.Count > MatrixLength) Matrix.RemoveAt(0);
            Matrix.Add(item);
            SaveMatrix(MatrixFileName);
        }

    }
}
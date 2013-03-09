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

    }
}
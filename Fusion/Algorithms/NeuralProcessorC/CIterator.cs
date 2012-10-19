using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Converter;
using HeatCharge;
using Implements;

namespace NeuralProcessorC
{
    internal static class CIterator
    {
        public const int PeriodSec = 15; // время сглаживания
        private static Stopwatch m_sw;
        public static Timer IterateTimer;
        private static int m_maxDownLancePosition;
        private static RollingAverage m_smoothSecondLancePosition; // средние за секунду для определения скорости
        private static double m_previosSecondLancePosition; // предыдущее положение фурмы для определения скорости
        private static double m_lanceSpeed;
        public static bool FirstHeating = true;
        private static bool m_noFixData;
        public static double TotalCarbonMass { set; get; }
        public static double RemainCarbonMass { set; get; }
        public static double RemainCarbonPercent { set; get; }
        public static bool TotalCarbonMassCalculated { set; get; }
        public static bool GasCarbonMassFinished { set; get; }
        public static MFCMDataFull CurrentHeatResult { set; get; }
        public static HeatData DataCurrentHeat { set; get; }
        public static HeatDataSmoother DataSmoothCurrent { set; get; }

        public static void Init()
        {
            m_sw = new Stopwatch();
            CurrentHeatResult = new MFCMDataFull();
            DataCurrentHeat = new HeatData();
            DataCurrentHeat.MatrixStateData = Program.MFCMDataGenerate(Program.MatrixStateDataFull); //!!
            Decarbonater.ComplexNMCTrain(DataCurrentHeat.MatrixStateData); //!!
            DataSmoothCurrent = new HeatDataSmoother(100);
            m_smoothSecondLancePosition = new RollingAverage();
            TotalCarbonMass = 0.0;
            RemainCarbonMass = 0.0;
            RemainCarbonPercent = 0.0;
            m_maxDownLancePosition = 10000;
            m_lanceSpeed = 0.0;
            TotalCarbonMassCalculated = false;
            GasCarbonMassFinished = false;
            m_noFixData = true;
        }

        public static void StartHeating()
        {
            Init();
            FirstHeating = false;
        }

        public static void Iterate(HeatData heatData)
        {
            using (var l = new Logger("Iterate"))
            {
                var calculatedCarboneEvent = new CalculatedCarboneEvent();

                var currentStateData = new MFCMData
                                           {
                                               CarbonMonoxideVolumePercent = heatData.CarbonMonoxideVolumePercent,
                                               CarbonOxideVolumePercent = heatData.CarbonOxideVolumePercent,
                                               HeightLanceCentimeters = heatData.HeightLanceCentimeters,
                                               OxygenVolumeRate = heatData.OxygenVolumeRate
                                           };
                //RemainCarbonPercent = Decarbonater.ComplexNMCProcess(heatData.MatrixStateData, currentStateData);
                RemainCarbonPercent = Decarbonater.ComplexNMCProcess(currentStateData);

                if (MomentFixDataForMFactorModel(heatData.OxygenVolumeCurrent,heatData.OxygenVolumeTotal, heatData.CarbonMonoxideVolumePercent, heatData.CarbonOxideVolumePercent))
                    // фиксируем для обучения
                {
                    if (m_noFixData)
                    {
                        CurrentHeatResult.OxygenVolumeRate = heatData.OxygenVolumeRate;
                        CurrentHeatResult.SteelCarbonCalculationPercent = RemainCarbonPercent;
                        CurrentHeatResult.CarbonMonoxideVolumePercent = heatData.CarbonMonoxideVolumePercent;
                        CurrentHeatResult.CarbonOxideVolumePercent = heatData.CarbonOxideVolumePercent;
                        CurrentHeatResult.HeightLanceCentimeters = heatData.HeightLanceCentimeters;
                        EnqueueWaitC(CurrentHeatResult); // ставим в очередь ожидания углерода
                        //Program.PushGate.PushEvent(new FixDataMfactorModelEvent());
                        m_noFixData = false;
                    }
                }


                calculatedCarboneEvent.model = "Complex neural Model";
                calculatedCarboneEvent.CarbonePercent = RemainCarbonPercent;
                calculatedCarboneEvent.CarboneMass = RemainCarbonMass;
                var fex = new ConnectionProvider.FlexHelper("NeuralProcessorC.Calc");
                fex.AddArg("TypeNeural", "8x8 non linear");
                fex.AddArg("C", RemainCarbonPercent);
                fex.Fire(Program.PushGate);
                l.msg("fired carbon:\n{0}",fex.evt.ToString());
                //Program.PushGate.PushEvent(calculatedCarboneEvent);
                //Program.PushGate.PushEvent(new CalculatedCarboneEvent());
            }
        }

        public static void HardFixData(MFCMDataFull currentHeatResult)
        {
            if (VerificateDataHF(currentHeatResult))
            {
                Program.MatrixStateDataFull.RemoveAt(0);
                Program.MatrixStateDataFull.Add(currentHeatResult);
                DataCurrentHeat.MatrixStateData = Program.MFCMDataGenerate(Program.MatrixStateDataFull);
                Decarbonater.ComplexNMCTrain(DataCurrentHeat.MatrixStateData);
            }
            Program.MatrixStateDataFullTotal.Add(currentHeatResult);

            for (int iD = 0; iD < Program.MatrixStateDataFull.Count; iD++)
            {
                Program.MatrixStateDataFull[iD].IdHeat = iD;
            }
            Program.SaveMatrix(Program.Path, Program.Separator, Program.MatrixStateDataFull);
            Program.SaveMatrix(Program.ArchFileName, Program.Separator, Program.MatrixStateDataFullTotal);
            //StartHeating();
        }
        static public bool VerificateDataHF(MFCMDataFull currentHeatResult)
        {
            var result = false;
            const double minCarbonPercent = 0.03;
            const double maxCarbonPercent = 0.1;
            const double minOxygenVolumeRate = 600;
            const double maxOxygenVolumeRate = 1400;
            const double maxHeightLance = 230;
            const double maxCarbonMonoxideVolumePercent = 30;
            const double maxCarbonOxideVolumePercent = 30;
            result = (currentHeatResult.SteelCarbonCalculationPercent != 0) &&
                     (currentHeatResult.SteelCarbonPercent > minCarbonPercent) &&
                     (currentHeatResult.SteelCarbonPercent < maxCarbonPercent) &&
                     (currentHeatResult.OxygenVolumeRate > minOxygenVolumeRate) &&
                     (currentHeatResult.OxygenVolumeRate < maxOxygenVolumeRate) &&
                     (currentHeatResult.HeightLanceCentimeters <= maxHeightLance) &&
                     (currentHeatResult.CarbonMonoxideVolumePercent <= maxCarbonMonoxideVolumePercent) &&
                     (currentHeatResult.CarbonOxideVolumePercent <= maxCarbonOxideVolumePercent);
            return result;
        }
        public static void EnqueueWaitC(MFCMDataFull currentHeatResult)
        {
            long numberHeat = currentHeatResult.NumberHeat;
            if (numberHeat <= 0) return;

            if (!Program.WaitCarbonDic.ContainsKey(numberHeat))
            {
                currentHeatResult.SteelCarbonPercent = 0; // на всякий обнуляем
                Program.WaitCarbonDic.Add(numberHeat, currentHeatResult);
            }
            else return;
        }

        public static void AddCarbonToQueue(Int64 heatNumber, Double carbonValue)
        {
            if (Program.WaitCarbonDic.ContainsKey(heatNumber))
            {
                Program.WaitCarbonDic[heatNumber].SteelCarbonPercent = carbonValue;
                CompleteQueueWC();
            }
            else
            {
                InstantLogger.log("HeatNumber {0} in the WaitCarbonDic dictionary not found", heatNumber.ToString());
            }
        }

        public static void CompleteQueueWC()
        {
            int i = 0;
            while (i < Program.WaitCarbonDic.Count)
            {
                if (Program.WaitCarbonDic.ElementAt(i).Value.SteelCarbonPercent > 0)
                {
                    long key = Program.WaitCarbonDic.ElementAt(i).Key;
                    HardFixData(Program.WaitCarbonDic[key]);
                    Program.WaitCarbonDic.Remove(key);
                }
                else
                {
                    i++;
                }
            }
            const int maxLength = 1000;
            if (Program.WaitCarbonDic.Count > maxLength)
                // если по каким-то причинам очередь слишком разрослась, то безжалостно ее прибиваем
            {
                Program.WaitCarbonDic.Clear();
                InstantLogger.err("WaitCarbonDic too grown");
            }
        }

        private static void SetMaxDownLancePosition(int currenLancePosition)
        {
            if (currenLancePosition > 0)
            {
                if (m_maxDownLancePosition > currenLancePosition)
                {
                    m_maxDownLancePosition = currenLancePosition;
                }
            }
        }

        public static void CalculateLanceSpeed(int lancePosition)
        {
            SetMaxDownLancePosition(lancePosition);
            m_smoothSecondLancePosition.Add(lancePosition);
            double smoothSecondLancePosition = m_smoothSecondLancePosition.Average(1);
            double speed = smoothSecondLancePosition - m_previosSecondLancePosition;
            //speed = Math.Abs(speed);
            m_previosSecondLancePosition = smoothSecondLancePosition;
            m_lanceSpeed = speed; // + up , - down
        }

        public static bool MomentFixDataForMFactorModel(double oxygenVolumeCurrent, double oxygenVolumeTotal, double carbonMonoxide, double carbonOxide)
        {
            const int maxDownPosition = 255;
            const int minDownPosition = 190;
            const int lanceSpeed = 5; // + up , - down
            const double carbonMonoxideTreshol = 30.0; //%
            const double carbonOxideTreshol = 5.0; //%
            //InstantLogger.msg("speed >>> " + m_lanceSpeed.ToString() + " ||| moment >>> " + (m_lanceSpeed > lanceSpeed).ToString());
            if (((oxygenVolumeCurrent/oxygenVolumeTotal*100) > 80) &&
                (m_maxDownLancePosition < maxDownPosition) &&
                (carbonMonoxideTreshol > carbonMonoxide) &&
                (carbonOxideTreshol < carbonOxide)
                ) // && (m_maxDownLancePosition > minDownPosition))
            {
                return (m_lanceSpeed > lanceSpeed);
            }
            return false;
        }

        private static double GetCarbonPercent(
            double carbonMass,
            double ironMass,
            double ironCarbonPercent,
            double scrapMass,
            double scrapCarbonPercent
            )
        {
            double ferumMass = (ironMass - (ironMass*ironCarbonPercent*0.01)) +
                               (scrapMass - (scrapMass*scrapCarbonPercent*0.01));
            if (ferumMass > 0.0)
            {
                return carbonMass/ferumMass*100;
            }
            else
            {
                return -1.01;
            }
        }

        private static bool VerifyGasCarbonFinished(
            double oxygenVolumeTotal,
            double oxygenVolumeCurrent,
            double totalCarbonMass,
            double remainCarbonMass,
            double carbonMonoxideVolumePercent,
            double carbonMonoxideVolumePercentPrevious,
            double carbonOxideVolumePercent,
            double carbonOxideVolumePercentPrevious
            )
        {
            if (oxygenVolumeTotal <= 0)
            {
                return false;
            }

            if (totalCarbonMass <= 0)
            {
                return false;
            }

            return (
                       ((oxygenVolumeCurrent/oxygenVolumeTotal*100) > 80) &&
                       //((totalCarbonMass - remainCarbonMass) > (totalCarbonMass * 0.9)) &&
                       ((carbonMonoxideVolumePercent - carbonMonoxideVolumePercentPrevious) < 0) &&
                       ((carbonOxideVolumePercent - carbonOxideVolumePercentPrevious) > 0)
                   );
        }
    }

    internal class HeatData
    {
        public HeatData()
        {
            IronMass = 300000.1133; //!!!!!!!!!!!!!!!!!
            IronCarbonPercent = 4.1133; //!!!!!!!!!!!!!!!!!!!!!!!
            ScrapMass = 150000.0; //!!!!!!!!!!!!!!!!!!!!!                     
            //ScrapMass = 0.0;
            ScrapCarbonPercent = 0.21;
            SteelCarbonPercent = 0.04;
            CarbonMonoxideVolumePercent = 0.0;
            CarbonOxideVolumePercent = 0.0;
            OffgasVolumeRate = 0.0;
            DeltaT = 1.0;
            Kgasan = 0.6357;
            HeightLanceCentimeters = 0;
            OxygenVolumeRate = 0.0;
            OxygenVolumeCurrent = 0.0;
            OxygenVolumeTotal = 20000;
            MatrixStateData = new List<MFCMData>();
        }

        public double IronMass { set; get; }
        public double IronCarbonPercent { set; get; }
        public double ScrapMass { set; get; }
        public double ScrapCarbonPercent { set; get; }
        public double SteelCarbonPercent { set; get; }
        public double CarbonMonoxideVolumePercent { set; get; }
        public double CarbonMonoxideVolumePercentPrevious { set; get; }
        public double CarbonOxideVolumePercent { set; get; }
        public double CarbonOxideVolumePercentPrevious { set; get; }
        public double OffgasVolumeRate { set; get; }
        public double DeltaT { set; get; }
        public double Kgasan { set; get; }
        public Int32 HeightLanceCentimeters { set; get; }
        public double OxygenVolumeRate { set; get; }
        public double OxygenVolumeCurrent { set; get; }
        public double OxygenVolumeTotal { set; get; }
        public List<MFCMData> MatrixStateData { set; get; }
    }

    internal class HeatDataSmoother
    {
        public HeatDataSmoother(int lengthBuff = 50)
        {
            CarbonMonoxideVolumePercent = new RollingAverage(lengthBuff);
            CarbonMonoxideVolumePercentPrevious = new RollingAverage(lengthBuff);
            CarbonOxideVolumePercent = new RollingAverage(lengthBuff);
            CarbonOxideVolumePercentPrevious = new RollingAverage(lengthBuff);
            OffgasVolumeRate = new RollingAverage(lengthBuff);
            HeightLanceCentimeters = new RollingAverage(lengthBuff);
            OxygenVolumeRate = new RollingAverage(lengthBuff);
            OxygenVolumeCurrent = new RollingAverage(lengthBuff);
        }

        public RollingAverage CarbonMonoxideVolumePercent { set; get; }
        public RollingAverage CarbonMonoxideVolumePercentPrevious { set; get; }
        public RollingAverage CarbonOxideVolumePercent { set; get; }
        public RollingAverage CarbonOxideVolumePercentPrevious { set; get; }
        public RollingAverage OffgasVolumeRate { set; get; }
        public RollingAverage HeightLanceCentimeters { set; get; }
        public RollingAverage OxygenVolumeRate { set; get; }
        public RollingAverage OxygenVolumeCurrent { set; get; }

        public HeatData GetHeatData(HeatData hd, int intervalSec)
        {
            if (hd == null) throw new ArgumentNullException("hd");
            hd.CarbonMonoxideVolumePercent = CarbonMonoxideVolumePercent.Average(intervalSec);
            hd.CarbonMonoxideVolumePercentPrevious = CarbonMonoxideVolumePercentPrevious.Average(intervalSec);
            hd.CarbonOxideVolumePercent = CarbonOxideVolumePercent.Average(intervalSec);
            hd.CarbonOxideVolumePercentPrevious = CarbonOxideVolumePercentPrevious.Average(intervalSec);
            hd.OffgasVolumeRate = OffgasVolumeRate.Average(intervalSec);
            hd.HeightLanceCentimeters = (int) Math.Round(HeightLanceCentimeters.Average(intervalSec), 0);
            hd.OxygenVolumeRate = OxygenVolumeRate.Average(intervalSec);
            hd.OxygenVolumeCurrent = OxygenVolumeCurrent.Average(intervalSec);
            return hd;
        }
    }
}
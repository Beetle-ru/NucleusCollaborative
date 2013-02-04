using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Converter;
using HeatCharge;
using System.Diagnostics;
using Implements;

namespace SMFCarbon
{
    static class CIterator
    {
        private static Stopwatch m_sw;
        public const int PeriodSec = 15; // время сглаживания
        public const int IntervalSec = 1; // время сглаживания
        public static Timer IterateTimer = new Timer(IntervalSec * 1000);
        public static double TotalCarbonMass                    { set; get; }
        public static double RemainCarbonMass                   { set; get; }
        public static double RemainCarbonPercent                { set; get; }
        public static bool TotalCarbonMassCalculated            { set; get; }
        public static bool GasCarbonMassFinished                { set; get; }
        public static MFCMDataFull CurrentHeatResult            { set; get; }
        public static HeatData DataCurrentHeat                  { set; get; }
        public static HeatDataSmoother DataSmoothCurrent        { set; get; }
        private static int m_maxDownLancePosition;
        private static RollingAverage m_smoothSecondLancePosition; // средние за секунду для определения скорости
        private static double m_previosSecondLancePosition; // предыдущее положение фурмы для определения скорости
        private static double m_lanceSpeed;
        public static bool FirstHeating = true;
        private static int m_currentMatrix;
        private static bool m_dataIsFixed;
        public static bool ModelIsStarted;

        static public void Init()
        {
            m_sw = new Stopwatch();
            CurrentHeatResult = new MFCMDataFull();
            DataCurrentHeat = new HeatData();
            DataSmoothCurrent = new HeatDataSmoother(100);
            m_smoothSecondLancePosition = new RollingAverage();
            TotalCarbonMass = 0.0;
            RemainCarbonMass = 0.0;
            RemainCarbonPercent = 0.0;
            m_maxDownLancePosition = 10000;
            m_lanceSpeed = 0.0;
            TotalCarbonMassCalculated = false;
            GasCarbonMassFinished = false;
            m_dataIsFixed = false;
            ModelIsStarted = false;
        }
        static public void Reset()
        {
            Init();
            FirstHeating = false;
        }
        static public void Iterate(HeatData heatData)
        {
            using (var l = new Logger("Iterate"))
            {
                if (ModelIsStarted)
                {
                    var currentStateData = new MFCMData
                                               {
                                                   CarbonMonoxideVolumePercent = heatData.CarbonMonoxideVolumePercent,
                                                   CarbonOxideVolumePercent = heatData.CarbonOxideVolumePercent,
                                                   HeightLanceCentimeters = heatData.HeightLanceCentimeters,
                                                   OxygenVolumeRate = heatData.OxygenVolumeRate
                                               };

                    m_currentMatrix = MFMChooser(heatData);
                    var matrixStateData = Program.MFCMDataGenerate(Program.MatrixStateDataFull[m_currentMatrix].MatrixList);
                    var CMCarbon = Decarbonater.MultiFactorCarbonMass(matrixStateData, currentStateData);
                    //if (CMCarbon < RemainCarbonPercent) RemainCarbonPercent = CMCarbon;
                    RemainCarbonPercent = CMCarbon;

                    var fex2 = new ConnectionProvider.FlexHelper("SMFCarbon.Result");
                    fex2.AddArg("C", RemainCarbonPercent);
                    fex2.Fire(Program.PushGate);
                    
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.WriteLine("                                                   ");
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.WriteLine("Carbon = " + RemainCarbonPercent + "%");

                    if (MomentFixDataForMFactorModel(heatData.CarbonMonoxideVolumePercent, heatData.CarbonOxideVolumePercent)) // фиксируем для обучения
                    {
                        if (!m_dataIsFixed)
                        {
                            CurrentHeatResult.OxygenVolumeRate = heatData.OxygenVolumeRate;
                            CurrentHeatResult.SteelCarbonCalculationPercent = RemainCarbonPercent;
                            CurrentHeatResult.CarbonMonoxideVolumePercent = heatData.CarbonMonoxideVolumePercent;
                            CurrentHeatResult.CarbonOxideVolumePercent = heatData.CarbonOxideVolumePercent;
                            CurrentHeatResult.HeightLanceCentimeters = heatData.HeightLanceCentimeters;
                            CurrentHeatResult.MFMEquationId = m_currentMatrix; // фиксируем матрицу по которой учим
                            EnqueueWaitC(CurrentHeatResult); // ставим в очередь ожидания углерода
                            
                            var fex = new ConnectionProvider.FlexHelper("SMFCarbon.DataFix");
                            fex.AddArg("C", RemainCarbonPercent);
                            fex.Fire(Program.PushGate); 
                            l.msg(fex.evt + "\n");

                            m_dataIsFixed = true;
                        }
                    }


                }
                else
                {
                    ModelIsStarted = ModelVerifiForStart(
                            heatData.OxygenVolumeTotal,
                            heatData.OxygenVolumeCurrent,
                            heatData.CarbonMonoxideVolumePercent,
                            heatData.CarbonMonoxideVolumePercentPrevious,
                            heatData.CarbonOxideVolumePercent,
                            heatData.CarbonOxideVolumePercentPrevious
                            );
                    if (ModelIsStarted)
                    {
                        var fex = new ConnectionProvider.FlexHelper("SMFCarbon.ModelIsStarted");
                        fex.Fire(Program.PushGate);
                        l.msg(fex.evt + "\n");
                    }
                    Console.Write(".");
                }
            }
        }

        static public void HardFixData(MFCMDataFull currentHeatResult)
        {
            int matrixId = currentHeatResult.MFMEquationId;
            if (VerificateDataHF(currentHeatResult))
            {
                Program.MatrixStateDataFull[matrixId].MatrixList.RemoveAt(0);
                Program.MatrixStateDataFull[matrixId].MatrixList.Add(currentHeatResult);
            }
            
            Program.MatrixStateDataFullTotal.Add(currentHeatResult);

            for (int iD = 0; iD < Program.MatrixStateDataFull[matrixId].MatrixList.Count; iD++)
            {
                Program.MatrixStateDataFull[matrixId].MatrixList[iD].IdHeat = iD;
            }
            Program.SaveMatrix(Program.ModelsPathDic[matrixId], Program.Separator, Program.MatrixStateDataFull[matrixId].MatrixList);
            Program.SaveMatrix(Program.ArchFileName, Program.Separator, Program.MatrixStateDataFullTotal);
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

        static public void EnqueueWaitC(MFCMDataFull currentHeatResult)
        {
            var numberHeat = currentHeatResult.NumberHeat;
            if (numberHeat <= 0) return;

            if (!Program.WaitCarbonDic.ContainsKey(numberHeat))
            {
                currentHeatResult.SteelCarbonPercent = 0; // на всякий обнуляем
                Program.WaitCarbonDic.Add(numberHeat, currentHeatResult);
            }
            else return;
        }

        static public void AddCarbonToQueue(Int64 heatNumber, Double carbonValue)
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
                if(Program.WaitCarbonDic.ElementAt(i).Value.SteelCarbonPercent > 0)
                {
                    var key = Program.WaitCarbonDic.ElementAt(i).Key;
                    HardFixData(Program.WaitCarbonDic[key]);
                    Program.WaitCarbonDic.Remove(key);
                }
                else
                {
                    i++;
                }
            }
            const int maxLength = 1000;
            if(Program.WaitCarbonDic.Count > maxLength) // если по каким-то причинам очередь слишком разрослась, то безжалостно ее прибиваем
            {
                Program.WaitCarbonDic.Clear();
                InstantLogger.err("WaitCarbonDic too grown");
            }
        }

        static private void SetMaxDownLancePosition(int currenLancePosition)
        {
            if (currenLancePosition > 0)
            {
                if (m_maxDownLancePosition > currenLancePosition)
                {
                    m_maxDownLancePosition = currenLancePosition;
                }
            }
        }
        static public void CalculateLanceSpeed(int lancePosition)
        {
            SetMaxDownLancePosition(lancePosition);
            m_smoothSecondLancePosition.Add((double)lancePosition);
            double smoothSecondLancePosition = m_smoothSecondLancePosition.Average(1);
            double speed = smoothSecondLancePosition - m_previosSecondLancePosition;
            //speed = Math.Abs(speed);
            m_previosSecondLancePosition = smoothSecondLancePosition;
            m_lanceSpeed = speed; // + up , - down
        }
        static public bool MomentFixDataForMFactorModel( double carbonMonoxide, double carbonOxide)
        {
            const int maxDownPosition = 255;
            const int minDownPosition = 190;
            const int lanceSpeed = 5; // + up , - down
            const double carbonMonoxideTreshol = 30.0; //%
            const double carbonOxideTreshol = 5.0; //%
            //InstantLogger.msg("speed >>> " + m_lanceSpeed.ToString() + " ||| moment >>> " + (m_lanceSpeed > lanceSpeed).ToString());
            if ((m_maxDownLancePosition < maxDownPosition) && 
                (carbonMonoxideTreshol > carbonMonoxide) &&
                (carbonOxideTreshol < carbonOxide)
                )// && (m_maxDownLancePosition > minDownPosition))
            {
                return (m_lanceSpeed > lanceSpeed);
            }
            return false;
        }

        static private bool ModelVerifiForStart(
            double oxygenVolumeTotal,
            double oxygenVolumeCurrent,
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
            return (
                       ((oxygenVolumeCurrent/oxygenVolumeTotal*100) > 80) &&
                       ((carbonMonoxideVolumePercent - carbonMonoxideVolumePercentPrevious) < 0) &&
                       ((carbonOxideVolumePercent - carbonOxideVolumePercentPrevious) > 0)
                   );
        }

        static private int MFMChooser(HeatData hd)
        {
            return (hd.CarbonOxideVolumePercent > hd.CarbonOxideVolumePercentPrevious) ? 0 : 1;
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            Iterate(DataCurrentHeat);
        }
    }

    class HeatData
    {
        public double IronMass                                  { set; get; }
        public double IronCarbonPercent                         { set; get; }
        public double ScrapMass                                 { set; get; }
        public double ScrapCarbonPercent                        { set; get; } 
        public double SteelCarbonPercent                        { set; get; } 
        public double CarbonMonoxideVolumePercent               { set; get; }
        public double CarbonMonoxideVolumePercentPrevious       { set; get; }
        public double CarbonOxideVolumePercent                  { set; get; }
        public double CarbonOxideVolumePercentPrevious          { set; get; }
        public double OffgasVolumeRate                          { set; get; }
        public double DeltaT                                    { set; get; } 
        public double Kgasan                                    { set; get; } 
        public Int32 HeightLanceCentimeters                     { set; get; }       
        public double OxygenVolumeRate                          { set; get; }
        public double OxygenVolumeCurrent                       { set; get; }
        public double OxygenVolumeTotal                         { set; get; }
        //public List<MFCMData> MatrixStateData                   { set; get; }

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
            //MatrixStateData = new List<MFCMData>();                  
        }
    }
    class HeatDataSmoother
    {
        public RollingAverage CarbonMonoxideVolumePercent { set; get; }
        public RollingAverage CarbonMonoxideVolumePercentPrevious { set; get; }
        public RollingAverage CarbonOxideVolumePercent { set; get; }
        public RollingAverage CarbonOxideVolumePercentPrevious { set; get; }
        public RollingAverage OffgasVolumeRate { set; get; }
        public RollingAverage HeightLanceCentimeters { set; get; }
        public RollingAverage OxygenVolumeRate { set; get; }
        public RollingAverage OxygenVolumeCurrent { set; get; }
        
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

        public HeatData GetHeatData(HeatData hd, int intervalSec)
        {
            if (hd == null) throw new ArgumentNullException("hd");
            hd.CarbonMonoxideVolumePercent = CarbonMonoxideVolumePercent.Average(intervalSec);
            hd.CarbonMonoxideVolumePercentPrevious = CarbonMonoxideVolumePercentPrevious.Average(intervalSec);
            hd.CarbonOxideVolumePercent = CarbonOxideVolumePercent.Average(intervalSec);
            hd.CarbonOxideVolumePercentPrevious = CarbonOxideVolumePercentPrevious.Average(intervalSec);
            hd.OffgasVolumeRate = OffgasVolumeRate.Average(intervalSec);
            hd.HeightLanceCentimeters = (int)Math.Round(HeightLanceCentimeters.Average(intervalSec), 0);
            hd.OxygenVolumeRate = OxygenVolumeRate.Average(intervalSec);
            hd.OxygenVolumeCurrent = OxygenVolumeCurrent.Average(intervalSec);
            return hd;
        }
    }
}

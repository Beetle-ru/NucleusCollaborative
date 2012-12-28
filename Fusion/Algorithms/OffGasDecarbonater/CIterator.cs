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

namespace OffGasDecarbonater
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
        public static HeatData DataCurrentHeat                  { set; get; }
        public static HeatDataSmoother DataSmoothCurrent        { set; get; }
        private static int m_maxDownLancePosition;
        private static RollingAverage m_smoothSecondLancePosition; // средние за секунду для определения скорости
        private static double m_previosSecondLancePosition; // предыдущее положение фурмы для определения скорости
        private static double m_lanceSpeed;
        public static bool FirstHeating = true;
        private static int m_currentMatrix;
        private static bool m_noFixData;

        public static long HeatNumber;

        static public void Init()
        {
            m_sw = new Stopwatch();
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
            m_noFixData = true;
        }
        static public void StartHeating()
        {
            Init();
            FirstHeating = false;
        }
        static public void Iterate(HeatData heatData)
        {
            using (var l = new Logger("Iterate"))
            {
                var calculatedCarboneEvent = new CalculatedCarboneEvent();
                if (!TotalCarbonMassCalculated)
                {
                    if (
                        (heatData.IronMass > 0) &&
                        (heatData.IronCarbonPercent > 0) &&
                        (heatData.ScrapMass > 0) &&
                        (heatData.ScrapCarbonPercent > 0) &&
                        (heatData.SteelCarbonPercent > 0)
                        )
                    {


                        TotalCarbonMass = Decarbonater.HeatCarbonMass(
                            heatData.IronMass,
                            heatData.IronCarbonPercent,
                            heatData.ScrapMass,
                            heatData.ScrapCarbonPercent,
                            heatData.SteelCarbonPercent
                            );
                        RemainCarbonMass = TotalCarbonMass;
                        RemainCarbonPercent = GetCarbonPercent(RemainCarbonMass, heatData.IronMass,
                                                               heatData.IronCarbonPercent,
                                                               heatData.ScrapMass, heatData.ScrapCarbonPercent);
                        if (TotalCarbonMass > 0 && heatData.OxygenVolumeRate > 0)
                        {
                            TotalCarbonMassCalculated = true;
                            l.msg("##### [TotalCarbonMassCalculated: {0}][RemainCarbonPercent]", TotalCarbonMass,
                                  RemainCarbonPercent);
                        }
                        else
                        {
                            l.msg("HeatCarbonMass returned bad result: {0}", TotalCarbonMass);
                        }
                    }
                    else
                    {
                        l.err("bad data for HeatCarbonMass [IronMass: {0}][IronCarbonPercent: {1}][ScrapMass: {2}][ScrapCarbonPercent: {3}][SteelCarbonPercent: {4}]",
                            heatData.IronMass,
                            heatData.IronCarbonPercent,
                            heatData.ScrapMass,
                            heatData.ScrapCarbonPercent,
                            heatData.SteelCarbonPercent
                            );
                    }

                }
                else// if (!GasCarbonMassFinished)
                {
                    heatData.DeltaT = m_sw.ElapsedMilliseconds*0.001;
                    m_sw.Restart();

                    if (
                        (heatData.CarbonMonoxideVolumePercent > 0) &&
                        (heatData.OffgasVolumeRate > 0) &&
                        (heatData.DeltaT > 0) &&
                        (heatData.Kgasan > 0)
                        )
                    {
                        double GCMResult = Decarbonater.GasanCarbonMass(
                            heatData.CarbonMonoxideVolumePercent,
                            heatData.OffgasVolumeRate,
                            heatData.DeltaT,
                            heatData.Kgasan
                            );
                        if (GCMResult >= 0)
                        {
                            if (heatData.OxygenVolumeRate > 0)
                            {
                                RemainCarbonMass -= GCMResult;//////////////////////////////
                            }
                        }
                        else
                        {
                            l.err("GasanCarbonMass return bad result: {0}", GCMResult);
                        }
                        if (
                            (RemainCarbonMass > 0) &&
                            (heatData.IronMass > 0) &&
                            (heatData.IronCarbonPercent > 0) &&
                            (heatData.ScrapMass > 0) &&
                            (heatData.ScrapCarbonPercent > 0)
                        )
                        {
                            RemainCarbonPercent = GetCarbonPercent(
                                RemainCarbonMass,
                                heatData.IronMass,
                                heatData.IronCarbonPercent,
                                heatData.ScrapMass,
                                heatData.ScrapCarbonPercent
                            );
                        }
                        else
                        {
                            l.err("bad data for GetCarbonPercent [RemainCarbonMass: {0}][IronMass: {1}][IronCarbonPercent: {2}][ScrapMass: {3}][ScrapCarbonPercent: {4}]",
                                RemainCarbonMass,
                                heatData.IronMass,
                                heatData.IronCarbonPercent,
                                heatData.ScrapMass,
                                heatData.ScrapCarbonPercent
                            );
                        }
                    }
                    else
                    {
                        l.err("bad data for GasanCarbonMass [CarbonMonoxideVolumePercent: {0}][OffgasVolumeRate: {1}][DeltaT: {2}][Kgasan: {3}]",
                            heatData.CarbonMonoxideVolumePercent,
                            heatData.OffgasVolumeRate,
                            heatData.DeltaT,
                            heatData.Kgasan
                            );
                    }
                }

                var fex2 = new ConnectionProvider.FlexHelper("OffGasDecarbonater.Result");
                fex2.AddArg("C", RemainCarbonPercent);
                fex2.Fire(Program.PushGate);
                //////////////////////////////////////////////////////////////////////
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

        static private double GetCarbonPercent(
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
        static private bool VerifyGasCarbonFinished(
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

            //return false; // типа выключатель многофакторной модели

            return (
                       ((oxygenVolumeCurrent/oxygenVolumeTotal*100) > 80) &&
                       //((totalCarbonMass - remainCarbonMass) > (totalCarbonMass * 0.9)) &&
                       ((carbonMonoxideVolumePercent - carbonMonoxideVolumePercentPrevious) < 0) &&
                       ((carbonOxideVolumePercent - carbonOxideVolumePercentPrevious) > 0)
                   );
        }

        static private int MFMChooser(HeatData hd)
        {
            //return (hd.CarbonMonoxideVolumePercent < hd.CarbonMonoxideVolumePercentPrevious) ? 0 : 1;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using HeatCharge;
using Implements;

namespace CPlusProcessor
{
    static class Iterator
    {
        private static List<MFCPData> m_matrix;
        private static List<MFCPData> m_matrixTotal;
        public static MFCPData CurrentState;
        public static double IntegralCO;

        public static HeatDataSmoother HDSmoother;
        public const int PeriodSec = 15; // время сглаживания
        public const int IntervalSec = 1; // интервал сглаживания
        public static Timer IterateTimer = new Timer(IntervalSec * 1000);
        public static Dictionary<Int64, MFCPData> WaitCarbonDic; // очередь ожидания углерода

        public static bool ModelIsStarted;
        private static bool m_dataIsFixed;
        private static bool m_dataIsEnqueue;

        public static void Init()
        {
            m_matrixTotal = new List<MFCPData>();
            Program.LoadMatrix(Program.MatrixPath, out m_matrix);
            Reset();

            IterateTimer.Elapsed += new ElapsedEventHandler(IterateTimeOut);
            IterateTimer.Enabled = true;

            WaitCarbonDic = new Dictionary<Int64, MFCPData>();
        }

        public static void Reset()
        {
            CurrentState = new MFCPData();
            HDSmoother = new HeatDataSmoother();

            ModelIsStarted = false;
            m_dataIsFixed = false;
            m_dataIsEnqueue = false;
            Console.WriteLine("Reset");
            IntegralCO = 0;
        }

        public static void Iterate()
        {
            if (ModelIsStarted)
            {
                if (m_dataIsFixed)
                {
                    if (!m_dataIsEnqueue)
                    {
                        CurrentState.SteelCarbonPercentCalculated = Decarbonater.MFactorCarbonPlus(m_matrix, CurrentState);
                        EnqueueWaitC();
                        m_dataIsEnqueue = true;

                        //var fex = new ConnectionProvider.FlexHelper("CPlusProcessor.DataFix");
                        //fex.Fire(Program.MainGate);
                        FireFixEvent();
                        //Console.WriteLine(fex.evt + "\n");
                    }
                }
                else
                {
                    CurrentState.CarbonOxideIVP += HDSmoother.CO2.Average(PeriodSec);
                    CurrentState.TimeFromX += IntervalSec;
                    CurrentState.SteelCarbonPercentCalculated = Decarbonater.MFactorCarbonPlus(m_matrix, CurrentState);

                    PushCarbon(CurrentState.SteelCarbonPercentCalculated); // fire flex
                    
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.WriteLine("                                                   ");
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.WriteLine("Carbone = " + CurrentState.SteelCarbonPercentCalculated + "%");
                    m_dataIsFixed = ModelVerifiForFix();
                }
            }
            else
            {
                ModelIsStarted = ModelVerifiForStart();
                if (ModelIsStarted)
                {
                    var fex = new ConnectionProvider.FlexHelper("CPlusProcessor.ModelIsStarted");
                    fex.Fire(Program.MainGate);
                    Console.WriteLine(fex.evt + "\n");
                }
            }
            
        }


        static public void FireFixEvent()
        {
            var fex = new ConnectionProvider.FlexHelper("CPlusProcessor.DataFix");
            fex.Fire(Program.MainGate);
            Console.WriteLine(fex.evt + "\n");
        }

        static public void PushCarbon(double carbon)
        {
            var fex = new ConnectionProvider.FlexHelper("CPlusProcessor.Result");
            fex.AddArg("C", carbon);
            fex.Fire(Program.MainGate);
        }
        static public void EnqueueWaitC()
        {
            var numberHeat = CurrentState.HeatNumber;
            if (numberHeat <= 0) return;

            if (!WaitCarbonDic.ContainsKey(numberHeat))
            {
                CurrentState.SteelCarbonPercent = 0; // на всякий обнуляем
                WaitCarbonDic.Add(numberHeat, CurrentState);
            }
            else return;
        }

        static public void AddCarbonToQueue(Int64 heatNumber, Double carbonValue)
        {
            if (WaitCarbonDic.ContainsKey(heatNumber))
            {
                WaitCarbonDic[heatNumber].SteelCarbonPercent = carbonValue;
                CompleteQueueWC();
            }
            else
            {
                InstantLogger.log("HeatNumber {0} in the WaitCarbonDic dictionary not found\n", heatNumber.ToString());
            }
        }

        public static void CompleteQueueWC()
        {
            int i = 0;
            while (i < WaitCarbonDic.Count)
            {
                if (WaitCarbonDic.ElementAt(i).Value.SteelCarbonPercent > 0)
                {
                    var key = WaitCarbonDic.ElementAt(i).Key;
                    HardFixData(WaitCarbonDic[key]);
                    WaitCarbonDic.Remove(key);
                }
                else
                {
                    i++;
                }
            }
            const int maxLength = 1000;
            if (WaitCarbonDic.Count > maxLength) // если по каким-то причинам очередь слишком разрослась, то безжалостно ее прибиваем
            {
                WaitCarbonDic.Clear();
                InstantLogger.err("WaitCarbonDic too grown\n");
            }
        }

        static public void HardFixData(MFCPData hDataResult)
        {
            if (VerifiDataForSave(hDataResult))
            {
                m_matrix.RemoveAt(0);
                m_matrix.Add(hDataResult);
            }

            m_matrixTotal.Add(hDataResult);

            Program.SaveMatrix(Program.MatrixPath, m_matrix);
            Program.SaveMatrix(Program.MatrixTotalPath, m_matrixTotal);
        }

        static public bool VerifiDataForSave(MFCPData currentHeatResult)
        {
            const double minCarbonPercent = 0.03;
            const double maxCarbonPercent = 0.1;
            const double maxHeightLance = 230;
            const double maxCarbonMonoxideVolumePercent = 30;
            const double maxCarbonOxideVolumePercent = 30;
            return   (currentHeatResult.SteelCarbonPercentCalculated != 0) &&
                     (currentHeatResult.SteelCarbonPercent > minCarbonPercent) &&
                     (currentHeatResult.SteelCarbonPercent < maxCarbonPercent);
        }

        private static bool ModelVerifiForStart()
        {
            const double oxygenTreshol = 16000;
            return (!ModelIsStarted) && 
                   (HDSmoother.Oxygen.Average(PeriodSec) >= oxygenTreshol) &&
                   (HDSmoother.CO2.Average(PeriodSec) >= HDSmoother.CO.Average(PeriodSec));
        }

        static public bool ModelVerifiForFix()
        {
            const int maxDownPosition = 255;
            const int minDownPosition = 190;
            const int lanceSpeed = 5; // + up , - down
            const double carbonMonoxideTreshol = 30.0; //%
            const double carbonOxideTreshol = 5.0; //%

            //InstantLogger.msg("integral CO {1} > {0} > {2}", IntegralCO, Program.COMax, Program.COMin);

            return (!m_dataIsFixed) &&
                   (HDSmoother.LanceHeigth.Average(PeriodSec) < maxDownPosition) && 
                   (HDSmoother.LanceHeigth.Average(PeriodSec) > minDownPosition) &&
                   (HDSmoother.CO.Average(PeriodSec) < carbonMonoxideTreshol) &&
                   (HDSmoother.CO2.Average(PeriodSec) > carbonOxideTreshol) &&
                   ((HDSmoother.LanceHeigth.Average(PeriodSec) - HDSmoother.LanceHeigthPrevious.Average(PeriodSec)) > lanceSpeed) &&
                   (IntegralCO > Program.COMin) && // проверка на интегральный CO
                   (IntegralCO < Program.COMax);
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            Iterate();
            Console.Write(".");
        }

    }

    class HeatData
    {
        public double CO;
        public double COPrevious;
        public double CO2;
        public int LanceHeigth;
        public int LanceHeigthPrevious;
        public int Oxygen;

        public HeatData()
        {
            CO = 0.0;
            COPrevious = 0.0;
            CO2 = 0.0;
            LanceHeigth = 0;
            LanceHeigthPrevious = 0;
            Oxygen = 0;
        }
    }

    class HeatDataSmoother
    {
        public RollingAverage CO;
        //public RollingAverage COPrevious;
        public RollingAverage CO2;
        public RollingAverage LanceHeigth;
        public RollingAverage LanceHeigthPrevious;
        public RollingAverage Oxygen;


        public HeatDataSmoother(int lengthBuff = 50)
        {
            CO = new RollingAverage(lengthBuff);
           // COPrevious = new RollingAverage(lengthBuff);
            CO2 = new RollingAverage(lengthBuff);
            LanceHeigth = new RollingAverage(lengthBuff);
            LanceHeigthPrevious = new RollingAverage(lengthBuff);
            Oxygen = new RollingAverage(lengthBuff);
        }

        public HeatData GetHeatData(HeatData hd, int intervalSec)
        {
            if (hd == null) throw new ArgumentNullException("hd");

            hd.CO = CO.Average(intervalSec);
            //hd.COPrevious = COPrevious.Average(intervalSec);
            hd.CO2 = CO2.Average(intervalSec);
            hd.LanceHeigth = (int)Math.Round(LanceHeigth.Average(intervalSec));
            hd.LanceHeigthPrevious = (int)Math.Round(LanceHeigthPrevious.Average(intervalSec));
            hd.Oxygen = (int)Math.Round(Oxygen.Average(intervalSec));

            return hd;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ConnectionProvider;
using HeatCharge;
using Implements;

namespace UniversalCPlus {
    internal static class Iterator {
        private static List<MFUCPData> m_matrix;
        private static List<MFUCPData> m_matrixTotal;
        public static MFUCPData CurrentState;
        public static double IntegralCO;
        public static double IntegralCO2;
        public static double OffGasV;

        public static HeatDataSmoother HDSmoother;
        public const int PeriodSec = 3; // время сглаживания
        public const int IntervalSec = 1; // интервал расчетов
        public static Timer IterateTimer = new Timer(IntervalSec*1000);
        public static Dictionary<long, MFUCPData> WaitCarbonDic; // очередь ожидания углерода

        public static bool ModelIsStarted;
        private static bool m_dataIsFixed;
        private static bool m_dataIsEnqueue;

        private static bool m_isBadInitBlowinByCO;
        private static bool m_isBlowingUpliftLance;

        private static double m_lastCarbon;
        private static double m_previousCarbon;

        public static bool HeatIsStarted;

        public static void Init() {
            m_matrixTotal = new List<MFUCPData>();
            Program.LoadMatrix(Program.MatrixPath, out m_matrix);
            Reset();

            IterateTimer.Elapsed += new ElapsedEventHandler(IterateTimeOut);
            IterateTimer.Enabled = true;

            WaitCarbonDic = new Dictionary<long, MFUCPData>();
        }

        public static void Reset() {
            CurrentState = new MFUCPData();
            HDSmoother = new HeatDataSmoother();

            ModelIsStarted = false;
            m_dataIsFixed = false;
            m_dataIsEnqueue = false;
            m_isBadInitBlowinByCO = false;
            m_isBlowingUpliftLance = false;
            Console.WriteLine("Reset");
            IntegralCO = 0;
            IntegralCO2 = 0;
            OffGasV = 320001;
            m_lastCarbon = 0;
            m_previousCarbon = Double.MaxValue;
            HeatIsStarted = false;
        }

        public static void Iterate() {
            if (ModelIsStarted) {
                if (m_dataIsFixed) {
                    if (!m_dataIsEnqueue) {
                        CurrentState.SteelCarbonPercentCalculated = Decarbonater.MFactorUniversalCarbonPlus(m_matrix,
                                                                                                   CurrentState);
                        CurrentState.SteelCarbonPercentCalculated =
                            CarbonClipper(CurrentState.SteelCarbonPercentCalculated);
                        if (VerifyForEnqueueWaitC()) EnqueueWaitC(); // ставим в очередь если плавка нормальная
                        m_dataIsEnqueue = true;
                        FireCurrentCarbon(CurrentState.SteelCarbonPercentCalculated);
                        FireFixEvent(CurrentState.SteelCarbonPercentCalculated);
                    }
                }
                else {
                    var co2 = HDSmoother.CO2.Average(PeriodSec);
                    var co = HDSmoother.CO.Average(PeriodSec);
                    CurrentState.CarbonVP = co*0.43 + co2 * 0.27;
                    CurrentState.CarbonIVP += CurrentState.CarbonVP;
                    CurrentState.TimeFromX += IntervalSec;
                    CurrentState.SteelCarbonPercentCalculated = Decarbonater.MFactorUniversalCarbonPlus(m_matrix, CurrentState);

                    //if (!m_dataIsEnqueue) m_lastCarbon = CurrentState.SteelCarbonPercentCalculated;
                    CurrentState.SteelCarbonPercentCalculated = CarbonClipper(CurrentState.SteelCarbonPercentCalculated);
                    FireCurrentCarbon(CurrentState.SteelCarbonPercentCalculated); // fire flex

                    //Console.WriteLine("Carbone = " + CurrentState.SteelCarbonPercentCalculated + "%");
                    m_dataIsFixed = ModelVerifiForFix();
                }
            }
            else {
                ModelIsStarted = ModelVerifiForStart();

                if (ModelIsStarted) {
                    FireCurrentCarbon(0.095);
                    var fex = new FlexHelper("UniversalCPlus.ModelIsStarted");
                    fex.Fire(Program.MainGate);
                    InstantLogger.msg(fex.evt + "\n");
                }
            }

            if (HDSmoother.HeatIsStarted)
                CurrentState.HightQualityHeat = HightQualityHeatVerify();
        }

        public static double CarbonClipper(double carbon) {
            var res = 0.0;
            if (carbon > m_previousCarbon)
                res = m_previousCarbon;
            else {
                res = carbon;
                m_previousCarbon = carbon;
            }
            return res;
        }

        public static void FireFixEvent(double carbon) {
            var fex = new FlexHelper("UniversalCPlus.DataFix");
            fex.AddArg("C", carbon);
            fex.Fire(Program.MainGate);
            InstantLogger.msg(fex.evt + "\n");
        }

        public static void FireCurrentCarbon(double carbon) {
            const double tresholdCarbon = 0.03;
            carbon = carbon < tresholdCarbon ? tresholdCarbon : carbon; // ограничение на углерод

            var fex = new FlexHelper("UniversalCPlus.Result");
            fex.AddArg("C", carbon);
            fex.Fire(Program.MainGate);
            InstantLogger.msg("carbon = {0}", carbon);
        }

        public static void EnqueueWaitC() {
            var numberHeat = CurrentState.HeatNumber;
            if (numberHeat <= 0) return;

            if (!WaitCarbonDic.ContainsKey(numberHeat)) {
                CurrentState.SteelCarbonPercent = 0; // на всякий обнуляем
                WaitCarbonDic.Add(numberHeat, CurrentState);
            }
            else return;
        }

        public static void AddCarbonToQueue(Int64 heatNumber, Double carbonValue) {
            if (WaitCarbonDic.ContainsKey(heatNumber)) {
                WaitCarbonDic[heatNumber].SteelCarbonPercent = carbonValue;
                CompleteQueueWC();
            }
            else
                InstantLogger.log("HeatNumber {0} in the WaitCarbonDic dictionary not found\n", heatNumber.ToString());
        }

        public static void CompleteQueueWC() {
            int i = 0;
            while (i < WaitCarbonDic.Count) {
                if (WaitCarbonDic.ElementAt(i).Value.SteelCarbonPercent > 0) {
                    var key = WaitCarbonDic.ElementAt(i).Key;
                    HardFixData(WaitCarbonDic[key]);
                    WaitCarbonDic.Remove(key);
                }
                else
                    i++;
            }
            const int maxLength = 10000;
            if (WaitCarbonDic.Count > maxLength)
                // если по каким-то причинам очередь слишком разрослась, то безжалостно ее прибиваем
            {
                WaitCarbonDic.Clear();
                InstantLogger.err("WaitCarbonDic too grown\n");
            }
        }

        public static void HardFixData(MFUCPData hDataResult) {
            if (VerifiDataForSave(hDataResult)) {
                //m_matrix.RemoveAt(0);
                //m_matrix.Add(hDataResult);
                //m_matrix.Insert();
                const double epsilon = 0.005;
                var isFoundInMatrix = false;
                for (int i = 0; i < m_matrix.Count; i++) {
                    if (Math.Abs(m_matrix[i].SteelCarbonPercent - hDataResult.SteelCarbonPercent) < epsilon) {
                        m_matrix.RemoveAt(i);
                        m_matrix.Insert(i, hDataResult);
                        isFoundInMatrix = true;
                        break;
                    }
                }
                if (isFoundInMatrix)
                    InstantLogger.log("hDataResult.SteelCarbonPercent is found in m_matrix and replased");
                else
                    InstantLogger.err("hDataResult.SteelCarbonPercent = {0} not found in m_matrix",
                                      hDataResult.SteelCarbonPercent);
            }
            else
                hDataResult.HightQualityHeat = false;

            m_matrixTotal.Add(hDataResult);

            Program.SaveMatrix(Program.MatrixPath, m_matrix);
            Program.SaveMatrix(Program.MatrixTotalPath, m_matrixTotal);
        }

        public static bool VerifiDataForSave(MFUCPData currentHeatResult) {
            const double minCarbonPercent = 0.03;
            const double maxCarbonPercent = 0.12;

            return (currentHeatResult.SteelCarbonPercentCalculated != 0) &&
                   (currentHeatResult.SteelCarbonPercent > minCarbonPercent) &&
                   (currentHeatResult.SteelCarbonPercent < maxCarbonPercent) &&
                   (currentHeatResult.HightQualityHeat);
        }

        private static bool VerifyForEnqueueWaitC() {
            const double minIcoIco2Ratio = 1.6;

            var it4 = (!((IntegralCO/IntegralCO2) < minIcoIco2Ratio));
                // 4. Плавки, выполненные с полным дожиганием «СО»
            if (!it4)
                Console.WriteLine("Bad blowing item 4.: (!(({0} / {1}) < {2}))\n", IntegralCO, IntegralCO2,
                                  minIcoIco2Ratio);
            //InstantLogger.msg("(!((IntegralCO[{0}] / IntegralCO2[{1}])[{2}] < minIcoIco2Ratio[{3}])) = {4}\n(IntegralCO[{0}] > Program.COMin[{5}]) = {6}\n (IntegralCO[{0}] < Program.COMax[{7}]) = {8}",
            //IntegralCO, 
            //IntegralCO2, 
            //(IntegralCO / IntegralCO2),
            //minIcoIco2Ratio, 
            //it4, 
            //Program.COMin, 
            //(IntegralCO > Program.COMin), 
            //Program.COMax, 
            //(IntegralCO < Program.COMax)
            //);
            return it4 &&
                   false && // выключено
                   (IntegralCO > Program.COMin) && // проверка на интегральный CO
                   (IntegralCO < Program.COMax);
        }

        private static bool ModelVerifiForStart() {
            const double oxygenTreshol = 16000;
            return (!ModelIsStarted) &&
                   (HDSmoother.Oxygen >= oxygenTreshol) &&
                   (HDSmoother.CO2.Average(PeriodSec) >= HDSmoother.CO.Average(PeriodSec));
        }

        public static bool ModelVerifiForFix() {
            const int lanceFixPositionTreshold = 330;
            const int oxigenTreshold = 16000;

            return (!m_dataIsFixed) &&
                   (HDSmoother.Oxygen > oxigenTreshold) &&
                   (HDSmoother.LanceHeigth >= lanceFixPositionTreshold);
                // 6.	 Технологические данные плавок “matrix” приведены в таблице 1. 
        }

        public static bool HightQualityHeatVerify() {
            const double initCOTreshold = 1;
            const double minOffGasV = 320000;
            const double maxOffGasV = 420000;
            const double minOxiIgnition = 2000; //начало периода зажигания
            const double maxOxiIgnition = 7000; // окончание периода зажигания

            const double minOxiLanceDown = 2000; // начало периода нижнего положения фурмы
            const double maxOxiLanceDown = 16000; // конец периода нижнего положения фурмы
            const double maxLanceHeigth = 500;
            if (HDSmoother.Oxygen > minOxiIgnition && HDSmoother.Oxygen < maxOxiIgnition)
                // 2. Содержание «СО» в отходящих газах по данным газоанализатора (зажигание плавки).
            {
                if (HDSmoother.CO.Average(PeriodSec) < initCOTreshold) {
                    m_isBadInitBlowinByCO = true;
                    InstantLogger.err("Bad blowing item 2.: {0} < {1}\n CurrentOxygen -- {2}\n",
                                      HDSmoother.CO.Average(PeriodSec), initCOTreshold, HDSmoother.Oxygen);
                }
            }

            if (OffGasV < minOffGasV && OffGasV > maxOffGasV) // 5. Плавки с искажениями по величине отходящих газов
            {
                m_isBadInitBlowinByCO = true;
                InstantLogger.err("Bad blowing item 5.: {2} < {0} < {1}\n", minOffGasV, minOffGasV, maxOffGasV);
            }

            if (HDSmoother.Oxygen > minOxiLanceDown && HDSmoother.Oxygen < maxOxiLanceDown) // проверка на слив шлака
            {
                if (HDSmoother.LanceHeigth >= maxLanceHeigth) {
                    m_isBlowingUpliftLance = true;
                    InstantLogger.err("Bad blowing Lance heigth {0} > {1}\n CurrentOxygen -- {2}\n",
                                      HDSmoother.LanceHeigth, maxLanceHeigth, HDSmoother.Oxygen);
                }
            }
            return !m_isBadInitBlowinByCO && !m_isBlowingUpliftLance;
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e) {
            Iterate();
            Console.Write(".");
        }
    }

    internal class HeatDataSmoother {
        public RollingAverage CO;
        public RollingAverage CO2;

        public double LanceHeigth;
        public double LanceHeigthPrevious;
        public double Oxygen;
        public bool HeatIsStarted;

        public HeatDataSmoother(int lengthBuff = 50) {
            CO = new RollingAverage(lengthBuff);
            CO2 = new RollingAverage(lengthBuff);

            LanceHeigth = 0;
            LanceHeigthPrevious = 0;
            Oxygen = 0.0;
            HeatIsStarted = false;
        }
    }
}
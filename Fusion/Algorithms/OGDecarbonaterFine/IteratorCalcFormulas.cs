using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    internal static partial class Iterator
    {
        #region 1.1 Уточнение расхода отходящих газов
        /// <summary>
        /// Расчет "измеренного" давления в газоходе
        /// </summary>
        static void CalcPlue()
        {
            const double k1 = 0.3030303030; // 1/3.3
            const double k2 = 2500;
            CurrentState.PFlue = (HDSmoother.GetOffGasDecompression() + k2) * k1;
        }

        /// <summary>
        /// Расчет абсолютного давления в газоходе по формуле 1
        /// </summary>
        static void CalcPa1()
        {
            const double k1 = 3.3;
            const double k2 = 2500;
            const double k3 = 10200;
            CurrentState.Pa1 = (CurrentState.PFlue * k1 - k2) + k3;
        }

        /// <summary>
        /// Расчет абсолютного давления в газоходе по формуле 2
        /// </summary>
        static void CalcPa2()
        {
            const double k1 = 3.3;
            const double k2 = 2500;
            const double k3 = 10200;
            const double k4 = 1000;
            CurrentState.Pa2 = (k4 - CurrentState.PFlue * k1 - k2) + k3;
        }

        /// <summary>
        /// Расчет поправочного коэффициента к расходу
        /// </summary>
        static void CalcK1()
        {
            CurrentState.K1 = Math.Sqrt(CurrentState.Pa1 / CurrentState.Pa2);
        }

        /// <summary>
        /// Расчет объемного расхода м3/час
        /// </summary>
        static void CalcQ1()
        {
            CurrentState.Q1 = CurrentState.K1 * HDSmoother.GetOffGasV();
        }

        /// <summary>
        /// Расчет абсолютного давление среды
        /// </summary>
        static void CalcPa()
        {
            const double k1 = 10200;
            const double k2 = 0.0001; // 1 / 10000
            CurrentState.Pa = (HDSmoother.GetOffGasDecompression() + k1)*k2;
        }
        #endregion

        #region 1.2. Уточнение расхода от учета насыщенных паров воды

        /// <summary>
        /// Расчет доли насыщенных паров воды в отходящих газах
        /// </summary>
        static void CalcSH2O()
        {
            const double k1 = 0.000184;
            const double k2 = 0.012366;
            const double k3 = 0.277243;
            CurrentState.SH2O = k1 * Math.Pow(HDSmoother.GetOffGasT(), 2) - k2 + k3;
        }

        /// <summary>
        /// Расчет объемного расхода м3/час с учетом доли насыщенных паров воды в отходящих газах
        /// </summary>
        static void CalcQ2()
        {
            const double k1 = 1;
            CurrentState.Q2 = (k1 - CurrentState.SH2O) * CurrentState.Q1;
        }
        #endregion

        #region 1.3. Уточнение расхода газов от отклонений плотности газов относительно принятых в измерениях

        /// <summary>
        /// Расчет текущей плотности отходящих газов в стандартных условиях (кг/м3)
        /// </summary>
        static void CalcOffgasDensity()
        {
            const double k1 = 100;
            const double k2 = 0.01;

            //плотности газов при 1 атм, 20 град
            const double DO2 = 1.5337;
            const double DH2 = 0.0965;
            const double DCO = 1.3416;
            const double DCO2 = 2.1216;
            const double DN2 = 1.3421;
            const double DAr = 0.8413;
            const double DH2O = 0.5980;

            //текущие концентрации газов
            var O2 = HDSmoother.GetO2();
            var H2 = HDSmoother.GetH2();
            var CO = HDSmoother.GetCO();
            var CO2 = HDSmoother.GetCO2();
            var N2 = HDSmoother.GetN2();
            var Ar = HDSmoother.GetAr();
            var H2O = CurrentState.SH2O;
            var _H2O = 1 - H2O;


            // необходим учет задержки газоанализатора
            CurrentState.OffgasDensity = ((DO2*O2*_H2O) + 
                                          (DH2*H2*_H2O) + 
                                          (DCO*CO*_H2O) + 
                                          (DCO2*CO2*_H2O) +
                                          (DN2*N2*_H2O) + 
                                          (DAr*Ar*_H2O) + 
                                          (DH2O*H2O*k1)
                                         )*k2;
        }
        #endregion
    }
}
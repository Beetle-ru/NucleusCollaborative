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
            CurrentState.PFlue = (CurrentState.OffGasDecompression + k2) * k1;
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
            CurrentState.Q1 = CurrentState.K1 * CurrentState.OffGasV;
        }

        /// <summary>
        /// Расчет абсолютного давление среды
        /// </summary>
        static void CalcPa()
        {
            const double k1 = 10200;
            const double k2 = 0.0001; // 1 / 10000
            CurrentState.Pa = (CurrentState.OffGasDecompression + k1) * k2;
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
            CurrentState.SH2O = k1 * Math.Pow(CurrentState.OffGasT, 2) - k2 * CurrentState.OffGasT + k3;
            //CurrentState.SH2O = k1 * CurrentState.OffGasT * CurrentState.OffGasT - k2 * CurrentState.OffGasT + k3;
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
            var O2 = CurrentState.O2;
            var H2 = CurrentState.H2;
            var CO = CurrentState.CO;
            var CO2 = CurrentState.CO2;
            var N2 = CurrentState.N2;
            var Ar = CurrentState.Ar;
            var H2O = CurrentState.SH2O;
            var _H2O = 1 - H2O;

            CurrentState.OffgasDensity = ((DO2*O2*_H2O)   + 
                                          (DH2*H2*_H2O)   + 
                                          (DCO*CO*_H2O)   + 
                                          (DCO2*CO2*_H2O) +
                                          (DN2*N2*_H2O)   + 
                                          (DAr*Ar*_H2O)   + 
                                          (DH2O*H2O*k1)
                                         )*k2;
        }

        /// <summary>
        /// Расчет поправочного коэффициента на плотность
        /// </summary>
        static void CalcDensity()
        {
            const double k1 = 1.245;

            CurrentState.KDensity = Math.Sqrt(k1/CurrentState.OffgasDensity);
        }

        /// <summary>
        /// Расчет окончательного объемного расхода
        /// </summary>
        static void CalcQ3()
        {
            CurrentState.Q3 = CurrentState.KDensity*CurrentState.Q2;
        }
        #endregion

        #region 2 Расчет массы уноса углерода
        
        /// <summary>
        /// Расчет текущего и накопленного уноса углерода от CO
        /// </summary>
        static void CalcMIco()
        {
            const double k1 = 0.01; // 1/100
            const double k2 = 0.0002777778; // 1/3600
            const double k3 = 0.0821;
            const double k4 = 273.15;
            const double k5 = 12;
            
            var Pa = CurrentState.Pa;
            var CO = CurrentState.CO;
            var q3 = CurrentState.Q3;
            var T = CurrentState.OffGasT;
            
            CurrentState.Mco = (Pa * CO * k1 * q3 * k2) / (k3 * (k4 + T)) * k5;
            
            CurrentState.MIco += CurrentState.Mco;
        }

        /// <summary>
        /// Расчет текущего и накопленного уноса углерода от CO2
        /// </summary>
        static void CalcMIco2()
        {
            const double k1 = 0.01; // 1/100
            const double k2 = 0.0002777778; // 1/3600
            const double k3 = 0.0821;
            const double k4 = 273.15;
            const double k5 = 12;
            
            var Pa = CurrentState.Pa;
            var CO2 = CurrentState.CO2;
            var q3 = CurrentState.Q3;
            var T = CurrentState.OffGasT;
            
            CurrentState.Mco2 = (Pa * CO2 * k1 * q3 * k2) / (k3 * (k4 + T)) * k5;
            
            CurrentState.MIco2 += CurrentState.Mco2;
        }

        /// <summary>
        /// Расчет суммарного текущего и накопленного уноса углерода от CO и CO2
        /// </summary>
        static void CalcMI()
        {
            CurrentState.M = CurrentState.Mco + CurrentState.Mco2;

            CurrentState.MI = CurrentState.MIco + CurrentState.MIco2;
        }
        #endregion

        #region 3.1 Расчет содержания углерода в металлошихте

        /// <summary>
        /// Расчет массы углерода в чугуне
        /// </summary>
        static void CalcMCHi()
        {
            const double k1 = 0.01; // 1/100

            CurrentState.MCHi = CurrentState.MHi * CurrentState.PCHi * k1;
        }

        /// <summary>
        /// Расчет массы углерода в скрапе
        /// </summary>
        static void CalcMCSc()
        {
            const double k1 = 0.01; // 1/100

            CurrentState.MCSc = CurrentState.MSc * CurrentState.PCSc * k1;
        }

        /// <summary>
        /// Расчет массы углерода в металлошихте
        /// </summary>
        static void CalcMCMetall()
        {
            CurrentState.MCMetall = CurrentState.MCHi + CurrentState.MCSc;
        }

        #endregion

        #region 3.2 Расчет углерода в сыпучих материалах

        /// <summary>
        /// Расчет массы углерода в сыпучих
        /// </summary>
        static void CalcMCsp()
        {
            const double k1 = 0.01; // 1/100
            const double k2 = 100; // 
            const double MMCO2 = 44; // молекулярная масса СО2
            const double MMC = 12; // молекулярная масса С

            CurrentState.MCsp = 0; // т.к. у нас данныый "нарастающим итогом" то нужно обнулять

            foreach (var materialData in CurrentState.Materials.MaterialList)
            {
                var name = materialData.CodeName;
                var wgh = materialData.TotalWeight;
                var pmpp = HimMaterials.GetHimValue(name, "PMPP");
                var c = HimMaterials.GetHimValue(name, "C");
                var co2 = HimMaterials.GetHimValue(name, "CO2");

                CurrentState.MCsp += wgh * (pmpp / (k2 * MMCO2)) * MMC + wgh * c * k1 + wgh * (co2 / (k2 * MMCO2)) * MMC;
            }
        }
        #endregion

        #region 4. Расчет остатка углерода в конвертере

        /// <summary>
        /// Расчет массы углерода в конвертере
        /// </summary>
        static void CalcDeltaMC()
        {

            CurrentState.DeltaMC = CurrentState.MCMetall - CurrentState.MI - CurrentState.MCsp;
            CurrentState.DeltaMC1 = CurrentState.MCMetall - CurrentState.MI;
            CurrentState.CurrentMC = CurrentState.MCMetall + CurrentState.MCsp - CurrentState.MI;
        }

        /// <summary>
        /// Расчет текущей массы железа в конвертере
        /// </summary>
        static void CalcCurrentMF()
        {
            CurrentState.CurrentMF = (CurrentState.MHi + CurrentState.MSc) - CurrentState.MCMetall;
        }

        /// <summary>
        /// Расчет текущего процента углерода в конвертере
        /// </summary>
        static void CalcCurrentPC()
        {
            CurrentState.CurrentPC = CurrentState.CurrentMC/CurrentState.CurrentMF*100;
        }
        #endregion


        #region ПЕРЕСЧЕТ ВСЕГО
        /// <summary>
        /// Полный пересчет
        /// </summary>
        static void CalcAll()
        {
            CalcPlue();
            CalcPa1();
            CalcPa2();
            CalcK1();
            CalcQ1();
            CalcPa();
            CalcSH2O();
            CalcQ2();
            CalcOffgasDensity();
            CalcDensity();
            CalcQ3();
            CalcMIco();
            CalcMIco2();
            CalcMI();
            CalcMCHi();
            CalcMCSc();
            CalcMCMetall();
            CalcMCsp();
            CalcDeltaMC();
            CalcCurrentMF();
            CalcCurrentPC();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGDecarbonaterFine
{
    public class RecalculateData : InputData
    {
        public Int64 HeatNumber;
        public int OffGasTransportDelay;

        /// <summary>
        /// Давление в газоходе
        /// </summary>
        public double PFlue;

        /// <summary>
        /// Абсолютное давление в газоходе по формуле 1
        /// </summary>
        public double Pa1;

        /// <summary>
        /// Абсолютное давление в газоходе по формуле 2
        /// </summary>
        public double Pa2;

        /// <summary>
        /// Поправочный коэффициент к расходу
        /// </summary>
        public double K1;

        /// <summary>
        /// Объемный расход
        /// </summary>
        public double Q1;

        /// <summary>
        /// Абсолютное давление среды
        /// </summary>
        public double Pa;

        /// <summary>
        /// Доля насыщенных паров воды в отходящих газах
        /// </summary>
        public double SH2O;

        /// <summary>
        /// Объемный расход с учетом доли насыщенных паров воды в отходящих газах
        /// </summary>
        public double Q2;

        /// <summary>
        /// Текущая плотность отходящих газов в стандартных условиях
        /// </summary>
        public double OffgasDensity;

        /// <summary>
        /// Поправочный коэффициент на плотность #
        /// </summary>
        public double KDensity;

        /// <summary>
        /// Объемный расход окончательный
        /// </summary>
        public double Q3;

        /// <summary>
        /// Текущий унос углерода от CO
        /// </summary>
        public double Mco;

        /// <summary>
        /// Накопленный унос углерода от CO
        /// </summary>
        public double MIco;

        /// <summary>
        /// Текущий унос углерода от CO2
        /// </summary>
        public double Mco2;

        /// <summary>
        /// Накопленный унос углерода от CO2
        /// </summary>
        public double MIco2;

        /// <summary>
        /// Суммарный текущий унос углерода
        /// </summary>
        public double M;
       
        /// <summary>
        /// Суммарный накопленный унос углерода
        /// </summary>
        public double MI;

        /// <summary>
        /// Масса чугуна в кг
        /// </summary>
        public double MHi;

        /// <summary>
        /// Процент углерода в чугуне
        /// </summary>
        public double PCHi;

        /// <summary>
        /// Масса углерода в чугуне  в кг
        /// </summary>
        public double MCHi;

        /// <summary>
        /// Масса скрапа в кг
        /// </summary>
        public double MSc;

        /// <summary>
        /// Процент углерода в скрапе
        /// </summary>
        public double PCSc;

        /// <summary>
        /// Масса углерода в скрапе в кг
        /// </summary>
        public double MCSc;

        /// <summary>
        /// Масса углерода в металлошихте
        /// </summary>
        public double MCMetall;

        /// <summary>
        /// Материалы отданные в конвертер
        /// </summary>
        public SupportMaterials Materials;

        /// <summary>
        /// Общаяя масса углерода в сыпучих
        /// </summary>
        public double MCsp;

        /// <summary>
        /// Общаяя масса углерода в конвертере
        /// </summary>
        public double DeltaMC;

        /// <summary>
        /// (для корректировки) ???
        /// </summary>
        public double DeltaMC1;

        /// <summary>
        /// Текущая масса углерода в конверторе ++
        /// </summary>
        public double CurrentMC;

        public RecalculateData()
        {
            HeatNumber = 0;
            OffGasTransportDelay = 25;
            PFlue = 0.0;
            Pa1 = 0.0;
            Pa2 = 0.0;
            K1 = 0.0;
            Q1 = 0.0;
            Pa = 0.0;
            SH2O = 0.0;
            Q2 = 0.0;
            OffgasDensity = 0.0;
            KDensity = 0.0;
            Q3 = 0.0;
            Mco = 0.0;
            MIco = 0.0;
            Mco2 = 0.0;
            MIco2 = 0.0;
            M = 0.0;
            MI = 0.0;
            MHi = 300000;
            PCHi = 4.5;
            MCHi = 0;
            MSc = 110000;
            PCSc = 0.2;
            MCSc = 0;
            MCMetall = 0;
            Materials = new SupportMaterials();
            MCsp = 0;
            DeltaMC = 0;
            DeltaMC1 = 0;
            CurrentMC = 0;
        }
    }
}

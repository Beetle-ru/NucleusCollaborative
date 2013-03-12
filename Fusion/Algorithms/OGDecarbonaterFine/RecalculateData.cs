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
        
        public bool DataFinishFixed;

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

        /// <summary>
        /// Текущая масса железа в конверторе ++
        /// </summary>
        public double CurrentMF;

        /// <summary>
        /// Текущий процент углерода в конверторе ++
        /// </summary>
        public double CurrentPC;

        /// <summary>
        /// Максимальное значение уноса углерода по CO
        /// </summary>
        public double MaxMCo;

        /// <summary>
        /// Значение CO2 за предыдущую секунду для анализа скорости роста концентрации CO2
        /// </summary>
        public double PreviousCO2;

        /// <summary>
        /// Значение CO2 в реперной точке
        /// </summary>
        public double FixPointCO2;

        /// <summary>
        /// Значение накопленного кислорода в реперной точке
        /// </summary>
        public double FixPointQO2I;

        /// <summary>
        /// Значение унесенного углерода с учетом сыпучих в реперной точке
        /// </summary>
        public double FixPointMICsp;

        /// <summary>
        /// Условная удельная масса углерода не 1 м3 O2 в реперной точке
        /// </summary>
        public double FixPointKCMetall;

        /// <summary>
        /// Условная удельная масса углерода унесенная отходящими газами в реперной точке
        /// </summary>
        public double FixPointKCOffGas;

        /// <summary>
        /// Поправка к массе углерода в реперной точке в реперной точке
        /// </summary>
        public double FixPointDeltaMC;

        /// <summary>
        /// Дельта K в реперной точке
        /// </summary>
        public double FixPointDeltaK;

        /// <summary>
        /// Уточненный результатирующих углерод в кг
        /// </summary>
        public double FixPointCarbonResult;

        /// <summary>
        /// Уточненный углерод в процентах ++
        /// </summary>
        public double FixPointPC;

        public RecalculateData()
        {
            HeatNumber = 0;
            OffGasTransportDelay = 25;
            
            DataFinishFixed = false;

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
            MCSc = 0.0;
            MCMetall = 0.0;
            Materials = new SupportMaterials();
            MCsp = 0.0;
            DeltaMC = 0.0;
            DeltaMC1 = 0.0;
            CurrentMF = 0.0;
            CurrentMC = 0.0;
            CurrentPC = 0.0;
            MaxMCo = 0.0;
            PreviousCO2 = 0.0;
            FixPointCO2 = 0.0;
            FixPointQO2I = 0.0;
            FixPointMICsp = 0.0;
            FixPointKCMetall = 0.0;
            FixPointKCOffGas = 0.0;
            FixPointDeltaMC = 0.0;
            FixPointDeltaK = 0.0;
            FixPointCarbonResult = 0.0;
            FixPointPC = 0.0;
        }

        public string GetDataLine()
        {
            const char sep = ';';
            var str = "";
            str += String.Format("{0}", DateTime.Now);
            str += String.Format("{0}{1}", sep, LanceHeight);
            str += String.Format("{0}{1}", sep, Ar);
            str += String.Format("{0}{1}", sep, CO);
            str += String.Format("{0}{1}", sep, CO2);
            str += String.Format("{0}{1}", sep, O2);
            str += String.Format("{0}{1}", sep, N2);
            str += String.Format("{0}{1}", sep, H2);
            str += String.Format("{0}{1}", sep, OffGasTransportDelay);
            str += String.Format("{0}{1}", sep, OffGasV);
            str += String.Format("{0}{1}", sep, OffGasT);
            str += String.Format("{0}{1}", sep, OffGasDecompression);
            str += String.Format("{0}{1}", sep, PFlue);
            str += String.Format("{0}{1}", sep, Pa1);
            str += String.Format("{0}{1}", sep, Pa2);
            str += String.Format("{0}{1}", sep, K1);
            str += String.Format("{0}{1}", sep, Q1);
            str += String.Format("{0}{1}", sep, Pa);
            str += String.Format("{0}{1}", sep, SH2O);
            str += String.Format("{0}{1}", sep, Q2);
            str += String.Format("{0}{1}", sep, OffgasDensity);
            str += String.Format("{0}{1}", sep, KDensity);
            str += String.Format("{0}{1}", sep, Q3);
            str += String.Format("{0}{1}", sep, Mco);
            str += String.Format("{0}{1}", sep, MIco);
            str += String.Format("{0}{1}", sep, Mco2);
            str += String.Format("{0}{1}", sep, MIco2);
            str += String.Format("{0}{1}", sep, M);
            str += String.Format("{0}{1}", sep, MI);
            str += String.Format("{0}{1}", sep, MHi);
            str += String.Format("{0}{1}", sep, PCHi);
            str += String.Format("{0}{1}", sep, MCHi);
            str += String.Format("{0}{1}", sep, MSc);
            str += String.Format("{0}{1}", sep, PCSc);
            str += String.Format("{0}{1}", sep, MCSc);
            str += String.Format("{0}{1}", sep, MCMetall);

            str = Materials.MaterialList.Aggregate(str, (current, materialData) => current + String.Format("{0}{1}", sep, materialData.TotalWeight));

            str += String.Format("{0}{1}", sep, MCsp);
            str += String.Format("{0}{1}", sep, DeltaMC);
            str += String.Format("{0}{1}", sep, DeltaMC1);
            str += String.Format("{0}{1}", sep, CurrentMC);
            str += String.Format("{0}{1}", sep, CurrentMF);
            str += String.Format("{0}{1}", sep, CurrentPC);
            // реперная точка
            str += String.Format("{0}{1}", sep, MaxMCo);
            str += String.Format("{0}{1}", sep, PreviousCO2);
            str += String.Format("{0}{1}", sep, FixPointCO2);
            str += String.Format("{0}{1}", sep, FixPointQO2I);
            str += String.Format("{0}{1}", sep, FixPointMICsp);
            str += String.Format("{0}{1}", sep, FixPointKCMetall);
            str += String.Format("{0}{1}", sep, FixPointKCOffGas);
            str += String.Format("{0}{1}", sep, FixPointDeltaMC);
            str += String.Format("{0}{1}", sep, FixPointDeltaK);
            str += String.Format("{0}{1}", sep, FixPointCarbonResult);
            str += String.Format("{0}{1}", sep, FixPointPC);

            return str;
        }

        public string GetHeaderLine()
        {
            const char sep = ';';
            var str = "";
            str += String.Format("{0}", DateTime.Now);
            str += String.Format("{0}{1}", sep, "LanceHeight");
            str += String.Format("{0}{1}", sep, "Ar");
            str += String.Format("{0}{1}", sep, "CO");
            str += String.Format("{0}{1}", sep, "CO2");
            str += String.Format("{0}{1}", sep, "O2");
            str += String.Format("{0}{1}", sep, "N2");
            str += String.Format("{0}{1}", sep, "H2");
            str += String.Format("{0}{1}", sep, "OffGasTransportDelay");
            str += String.Format("{0}{1}", sep, "OffGasV");
            str += String.Format("{0}{1}", sep, "OffGasT");
            str += String.Format("{0}{1}", sep, "OffGasDecompression");
            str += String.Format("{0}{1}", sep, "PFlue");
            str += String.Format("{0}{1}", sep, "Pa1");
            str += String.Format("{0}{1}", sep, "Pa2");
            str += String.Format("{0}{1}", sep, "K1");
            str += String.Format("{0}{1}", sep, "Q1");
            str += String.Format("{0}{1}", sep, "Pa");
            str += String.Format("{0}{1}", sep, "SH2O");
            str += String.Format("{0}{1}", sep, "Q2");
            str += String.Format("{0}{1}", sep, "OffgasDensity");
            str += String.Format("{0}{1}", sep, "KDensity");
            str += String.Format("{0}{1}", sep, "Q3");
            str += String.Format("{0}{1}", sep, "Mco");
            str += String.Format("{0}{1}", sep, "MIco");
            str += String.Format("{0}{1}", sep, "Mco2");
            str += String.Format("{0}{1}", sep, "MIco2");
            str += String.Format("{0}{1}", sep, "M");
            str += String.Format("{0}{1}", sep, "MI");
            str += String.Format("{0}{1}", sep, "MHi");
            str += String.Format("{0}{1}", sep, "PCHi");
            str += String.Format("{0}{1}", sep, "MCHi");
            str += String.Format("{0}{1}", sep, "MSc");
            str += String.Format("{0}{1}", sep, "PCSc");
            str += String.Format("{0}{1}", sep, "MCSc");
            str += String.Format("{0}{1}", sep, "MCMetall");

            str = Materials.MaterialList.Aggregate(str, (current, materialData) => current + String.Format("{0}{1}", sep, materialData.CodeName));

            str += String.Format("{0}{1}", sep, "MCsp");
            str += String.Format("{0}{1}", sep, "DeltaMC");
            str += String.Format("{0}{1}", sep, "DeltaMC1");
            str += String.Format("{0}{1}", sep, "CurrentMC");
            str += String.Format("{0}{1}", sep, "CurrentMF");
            str += String.Format("{0}{1}", sep, "CurrentPC");
            // реперная точка
            str += String.Format("{0}{1}", sep, "PreviousMCo");
            str += String.Format("{0}{1}", sep, "PreviousCO2");
            str += String.Format("{0}{1}", sep, "FixPointCO2");
            str += String.Format("{0}{1}", sep, "FixPointQO2I");
            str += String.Format("{0}{1}", sep, "FixPointMICsp");
            str += String.Format("{0}{1}", sep, "FixPointKCMetall");
            str += String.Format("{0}{1}", sep, "FixPointKCOffGas");
            str += String.Format("{0}{1}", sep, "FixPointDeltaMC");
            str += String.Format("{0}{1}", sep, "FixPointDeltaK");
            str += String.Format("{0}{1}", sep, "FixPointCarbonResult");
            str += String.Format("{0}{1}", sep, "FixPointPC");

            return str;
        }
    }
}

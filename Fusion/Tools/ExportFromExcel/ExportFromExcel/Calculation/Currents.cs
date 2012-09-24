using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator
{
    public class Currents
    {

        /// <summary>
        /// Текущая масса жидкой ванны в печи, кг
        /// </summary>
        public double mStL { get; set; }
        public double mStLPrev { get; set; }
        
        /// <summary>
        /// Текущая температура системы металл-шлак, К
        /// </summary>
        public double Tav { get; set; }

        public double ECPh { get; set; }
        public double EScrS { get; set; }

        #region Металл

        /// <summary>
        /// Масса металла в печи, кг
        /// </summary>
        public double mSt { get; set; }

        /// <summary>
        /// Концентрация углерода в металле, % масс
        /// </summary>
        public double CC { get; set; }

        /// <summary>
        /// Концентрация марганца в металле, % масс
        /// </summary>
        public double CMn { get; set; }

        /// <summary>
        /// Концентрация кремния в металле, % масс
        /// </summary>
        public double CSi { get; set; }

        /// <summary>
        ///Концентрация кислорода в металле, % масс
        /// </summary>
        public double CO { get; set; }

        /// <summary>
        /// Текущая масса углерода в металле, кг
        /// </summary>
        public double mC { get; set; }
        
        /// <summary>
        /// Текущая масса железа в металле, кг
        /// </summary>
        public double mFe { get; set; }
        
        /// <summary>
        /// Текущая масса марганца в металле, кг
        /// </summary>
        public double mMn { get; set; }
        
        /// <summary>
        /// Текущая масса кремния в металле, кг
        /// </summary>
        public double mSi { get; set; }
        
        /// <summary>
        /// Текущая масса кислорода в металле, кг
        /// </summary>
        public double mO { get; set; }
        
        #endregion

        #region Шлак

        /// <summary>
        /// Масса шлака в печи, кг
        /// </summary>
        public double mSl { get; set; }

        /// <summary>
        /// Концентрация FeO в шлаке, % масс
        /// </summary>
        public double CFeO { get; set; }

        /// <summary>
        /// Концентрация MnO в шлаке, % масс
        /// </summary>
        public double CMnO { get; set; }

        /// <summary>
        /// Концентрация SiO2 в шлаке, % масс
        /// </summary>
        public double CSiO2 { get; set; }

        /// <summary>
        /// Концентрация CaO в шлаке, % масс
        /// </summary>
        public double CCaO { get; set; }

        /// <summary>
        /// Текущая масса FeO в шлаке, кг
        /// </summary>
        public double mFeO { get; set; }
        
        /// <summary>
        /// Текущая масса MnO в шлаке, кг
        /// </summary>
        public double mMnO { get; set; }
        
        /// <summary>
        /// Текущая масса SiO2 в шлаке, кг
        /// </summary>
        public double mSiO2 { get; set; }
        
        /// <summary>
        /// Текущая масса CaO в шлаке, кг
        /// </summary>
        public double mCaO { get; set; }
       
        #endregion
        
        #region Расходы

        /// <summary>
        /// Текущий расход доломита, кг
        /// </summary>
        public double CnsDlmt { get; set; }
        
        /// <summary>
        /// Текущий расход кокса, кг
        /// </summary>
        public double CnsCk { get; set; }
        
        /// <summary>
        /// Текущий расход извести, кг
        /// </summary>
        public double CnsLm { get; set; }
        
        /// <summary>
        /// Текущий расход электроэнергии, МВт·ч
        /// </summary>
        public double CnsEE { get; set; }
        
        /// <summary>
        /// Текущий расход кислорода через фурмы, м3
        /// </summary>
        public double CnsO2L { get; set; }
        
        /// <summary>
        /// Текущий расход кислорода через горелки, м3
        /// </summary>
        public double CnsO2B { get; set; }
        
        /// <summary>
        /// Текущий расход природного газа, м3
        /// </summary>
        public double CnsCH4 { get; set; }
        
        /// <summary>
        /// Текущий расход лома, кг
        /// </summary>
        public double CnsScr { get; set; }
        
        /// <summary>
        /// Текущий расход жидкого чугуна, кг
        /// </summary>
        public double CnsHI { get; set; }
       
        /// <summary>
        /// Текущий расход порошка углерода, кг
        /// </summary>
        public double CnsCP { get; set; }
        
        #endregion

    }
}

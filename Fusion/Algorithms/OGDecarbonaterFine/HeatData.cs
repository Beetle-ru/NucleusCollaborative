using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGDecarbonaterFine
{
    public class HeatData
    {
        public Int64 HeatNumber;

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
        /// Поправочный коэффициент
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

        public HeatData()
        {
            HeatNumber = 0;
            PFlue = 0.0;
            Pa1 = 0.0;
            Pa2 = 0.0;
            K1 = 0.0;
            Q1 = 0.0;
            Pa = 0.0;
            SH2O = 0.0;
            Q2 = 0.0;
            OffgasDensity = 0.0;
        }
    }
}

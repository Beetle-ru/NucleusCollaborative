using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charge5
{
    class InData
    {
        /// <summary>
        /// Тип стали 0 -- 7
        /// </summary>
        public int SteelType;

        /// <summary>
        /// Кремний в чугуне
        /// </summary>
        public double SiHi;

        /// <summary>
        /// Температура чугуна
        /// </summary>
        public int THi;

        /// <summary>
        /// Масса чугуна, если 0 то считать выключенным
        /// </summary>
        public int MHi;

        /// <summary>
        /// Масса лома, если 0 то считать выключенным
        /// </summary>
        public int MSc;

        /// <summary>
        /// Последующая обработка на УВС
        /// </summary>
        public bool IsProcessingUVS;
    }
}

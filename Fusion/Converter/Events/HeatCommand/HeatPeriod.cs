using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Converter
{
    /// <summary>
    /// Период плавки
    /// </summary>
    public class HeatPeriod
    {
        /// <summary>
        /// Наименование периода
        /// </summary>
        public string PeriodName { get; set; }
        /// <summary>
        /// Номер периода
        /// </summary>
        public int PeriodNumber { get; set; }
        /// <summary>
        /// Коллекция этапов плавки
        /// </summary>
        public List<HeatPhase> HeatPhases;
    }
}

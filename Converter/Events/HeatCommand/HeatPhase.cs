using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Converter
{
    /// <summary>
    /// Этап плавки
    /// </summary>
    public class HeatPhase
    {
        /// <summary>
        /// Наименование этапа
        /// </summary>
        public string PhaseName { get; set; }
        /// <summary>
        /// Номер этапа
        /// </summary>
        public int PhaseNumber { get; set; }
        /// <summary>
        /// Признак обязательности этапа
        /// </summary>
        public int PhaseIsNeed { get; set; }
        /// <summary>
        /// Коллекция шагов в этапе плавки
        /// </summary>
        public List<HeatStep> Steps;
    }
}

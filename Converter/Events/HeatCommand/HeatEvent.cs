using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CommonTypes;


namespace Converter
{
    /// <summary>
    /// Плавка
    /// </summary>
    public class HeatEvent : BaseEvent
    {
        /// <summary>
        /// Коллекция периодов плавки
        /// </summary>
        public List<HeatPeriod> HeatPeriods;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    /// <summary>
    /// Treated heat related data.
    /// Accessible via Data.MINP class.
    /// </summary>
    public class Heat
    {
        public Guid HeatID { get; set; }
        public string HeatNumber { get; set; }
        public int? SimulationNumber { get; set; }
        public bool Charged { get; set; }
        public int? HotMetalTemperature { get; set; }
        public int? ScrapTemperature { get; set; }
        public int? CalculatedO2Amount_Nm3 { get; set; }
        public int? FinalTemperature { get; set; }
    }
}

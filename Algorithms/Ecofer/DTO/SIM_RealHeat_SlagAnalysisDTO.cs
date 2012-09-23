// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:14
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class SIM_RealHeat_SlagAnalysisDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid SIM_RealHeatID { get; set; }

        public DateTime SampleTime { get; set; }

        public Double CaO { get; set; }

        public Double SiO2 { get; set; }

        public Double FeO { get; set; }

        public Double MgO { get; set; }

        public Double Al2O3 { get; set; }

        public Double S { get; set; }

        public Double P2O5 { get; set; }

        public SIM_RealHeatDTO SIM_RealHeat { get; set; }
    }
}

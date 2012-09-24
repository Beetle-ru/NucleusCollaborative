// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:11
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class SIM_RealHeat_SteelAnalysisDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid SIM_RealHeatID { get; set; }

        public DateTime SampleTime { get; set; }

        public Double C { get; set; }

        public Double Si { get; set; }

        public Double Mn { get; set; }

        public Double P { get; set; }

        public Double S { get; set; }

        public Double Cr { get; set; }

        public Double Ni { get; set; }

        public Double Cu { get; set; }

        public Double Al { get; set; }

        public Double N { get; set; }

        public Double Mo { get; set; }

        public SIM_RealHeatDTO SIM_RealHeat { get; set; }
    }
}

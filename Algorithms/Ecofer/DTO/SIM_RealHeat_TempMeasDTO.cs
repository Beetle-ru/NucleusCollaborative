// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:12
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class SIM_RealHeat_TempMeasDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid SIM_RealHeatID { get; set; }

        public DateTime TimeProcessed { get; set; }

        public Nullable<Double> Carbon_p { get; set; }

        public Int32 Temperature { get; set; }

        public Nullable<Int32> Temperature2 { get; set; }

        public SIM_RealHeatDTO SIM_RealHeat { get; set; }
    }
}

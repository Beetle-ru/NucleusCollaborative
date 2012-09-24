// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:12
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_TempMeasDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Note { get; set; }

        public Guid MINP_HeatID { get; set; }

        public DateTime TimeProcessed { get; set; }

        public Nullable<Int32> Temperature { get; set; }

        public Nullable<Double> Oxygen_ppm { get; set; }

        public Nullable<Double> Hydrogen_ppm { get; set; }

        public Nullable<Double> Nitrogen_ppm { get; set; }

        public Nullable<Double> Aluminum_p { get; set; }

        public Nullable<Double> Carbon_p { get; set; }

        public MINP_HeatDTO MINP_Heat { get; set; }
    }
}

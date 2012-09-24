// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:14
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L1L2_TempMeasDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Int32 ConverterNo { get; set; }

        public DateTime TimeProcessed { get; set; }

        public Boolean Considered { get; set; }

        public Nullable<Int32> Temperature { get; set; }

        public Nullable<Double> Oxygen_ppm { get; set; }

        public Nullable<Double> Hydrogen_ppm { get; set; }

        public Nullable<Double> Nitrogen_ppm { get; set; }

        public Nullable<Double> Aluminum_p { get; set; }

        public Nullable<Double> Carbon_p { get; set; }
    }
}

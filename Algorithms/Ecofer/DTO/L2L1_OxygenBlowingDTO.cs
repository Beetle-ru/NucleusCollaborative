// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:06
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L2L1_OxygenBlowingDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Int32 ConverterNo { get; set; }

        public Boolean Considered { get; set; }

        public Int32 OxygenFlow_Nm3_min { get; set; }

        public Int32 LanceDistance_mm { get; set; }

        public String HeatNumber { get; set; }
    }
}

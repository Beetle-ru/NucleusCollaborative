// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:59
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_ProcessPatternOxygenDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MINP_ProcessPatternID { get; set; }

        public Int32 Index { get; set; }

        public Nullable<Int32> OxygenAmount_m3 { get; set; }

        public Int32 OxygenFlow_Nm3_min { get; set; }

        public Int32 LanceDistance_mm { get; set; }

        public Boolean Correction { get; set; }

        public MINP_ProcessPatternDTO MINP_ProcessPattern { get; set; }
    }
}

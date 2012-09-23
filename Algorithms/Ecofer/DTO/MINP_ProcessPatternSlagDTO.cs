// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:00
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_ProcessPatternSlagDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MINP_ProcessPatternID { get; set; }

        public Int32 Index { get; set; }

        public Boolean Prepairing { get; set; }

        public Nullable<Int32> OxygenAmount_m3 { get; set; }

        public Nullable<Int32> Lime_p { get; set; }

        public Nullable<Int32> Dolomit_p { get; set; }

        public Nullable<Int32> Fom_p { get; set; }

        public Nullable<Int32> Coke_p { get; set; }

        public Nullable<Int32> CaCO3_p { get; set; }

        public MINP_ProcessPatternDTO MINP_ProcessPattern { get; set; }
    }
}

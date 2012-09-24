// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:58
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_ProcessPatternSlagDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MINP_GD_ProcessPatternID { get; set; }

        public Int32 Index { get; set; }

        public Boolean Prepairing { get; set; }

        public Nullable<Int32> OxygenAmount_m3 { get; set; }

        public Nullable<Int32> Lime_p { get; set; }

        public Nullable<Int32> Dolomit_p { get; set; }

        public Nullable<Int32> Fom_p { get; set; }

        public Nullable<Int32> Coke_p { get; set; }

        public Nullable<Int32> CaCO3_p { get; set; }

        public MINP_GD_ProcessPatternDTO MINP_GD_ProcessPattern { get; set; }
    }
}

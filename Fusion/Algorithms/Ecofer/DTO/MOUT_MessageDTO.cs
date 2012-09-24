// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:53
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MOUT_MessageDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Boolean Considered { get; set; }

        public Nullable<Guid> MINP_Heat { get; set; }

        public Int32 Type { get; set; }

        public String Message { get; set; }

        public MINP_HeatDTO MINP_Heat1 { get; set; }
    }
}

// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:50
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_MatAddDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Note { get; set; }

        public Guid MINP_HeatID { get; set; }

        public Nullable<Guid> MINP_GD_MaterialID { get; set; }

        public DateTime TimeProcessed { get; set; }

        public String Code { get; set; }

        public String ShortCode { get; set; }

        public Int32 Amount_kg { get; set; }

        public MINP_GD_MaterialDTO MINP_GD_Material { get; set; }

        public MINP_HeatDTO MINP_Heat { get; set; }
    }
}

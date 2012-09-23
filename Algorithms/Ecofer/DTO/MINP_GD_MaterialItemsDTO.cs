// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:49
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_MaterialItemsDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MINP_GD_MaterialID { get; set; }

        public Guid MINP_GD_MaterialElementID { get; set; }

        public Double Amount_p { get; set; }

        public MINP_GD_MaterialDTO MINP_GD_Material { get; set; }

        public MINP_GD_MaterialElementDTO MINP_GD_MaterialElement { get; set; }
    }
}

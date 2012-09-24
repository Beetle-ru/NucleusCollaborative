// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:43
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L3L2_MaterialItemsDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid L3L2_MaterialID { get; set; }

        public Int32 ElementIndex { get; set; }

        public Double Amount_p { get; set; }

        public L3L2_MaterialDTO L3L2_Material { get; set; }
    }
}

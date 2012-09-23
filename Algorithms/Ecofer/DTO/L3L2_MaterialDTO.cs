// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:42
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L3L2_MaterialDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Code { get; set; }

        public String ShortCode { get; set; }

        public String Description { get; set; }

        public Boolean Available { get; set; }

        public List<L3L2_MaterialItemsDTO> L3L2_MaterialItems { get; set; }
    }
}

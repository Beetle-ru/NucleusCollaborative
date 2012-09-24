// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:46
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_BunkerMaterialDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MINP_GD_MaterialID { get; set; }

        public Int32 ConverterNo { get; set; }

        public String BunkerNo { get; set; }

        public Nullable<Int32> Amount { get; set; }

        public Nullable<Boolean> Available { get; set; }

        public MINP_GD_MaterialDTO MINP_GD_Material { get; set; }
    }
}

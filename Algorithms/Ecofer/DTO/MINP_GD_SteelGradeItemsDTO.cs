// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:50
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_SteelGradeItemsDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MINP_GD_SteelGradeID { get; set; }

        public Guid MINP_GD_MaterialElementID { get; set; }

        public Nullable<Double> Min { get; set; }

        public Double Aim { get; set; }

        public Nullable<Double> Max { get; set; }

        public MINP_GD_MaterialElementDTO MINP_GD_MaterialElement { get; set; }

        public MINP_GD_SteelGradeDTO MINP_GD_SteelGrade { get; set; }
    }
}

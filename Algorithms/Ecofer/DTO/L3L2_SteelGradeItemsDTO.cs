// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:44
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L3L2_SteelGradeItemsDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid L3L2_SteelGradeID { get; set; }

        public Int32 ElementIndex { get; set; }

        public Nullable<Double> Min { get; set; }

        public Double Aim { get; set; }

        public Nullable<Double> Max { get; set; }

        public L3L2_SteelGradeDTO L3L2_SteelGrade { get; set; }
    }
}

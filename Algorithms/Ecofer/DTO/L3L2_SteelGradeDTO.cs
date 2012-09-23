// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:43
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L3L2_SteelGradeDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public Nullable<Int32> FinalTemperature { get; set; }

        public List<L3L2_SteelGradeItemsDTO> L3L2_SteelGradeItems { get; set; }
    }
}

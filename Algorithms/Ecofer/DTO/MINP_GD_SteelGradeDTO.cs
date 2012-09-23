// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:49
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_SteelGradeDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public Nullable<Int32> FinalTemperature { get; set; }

        public List<MINP_GD_SteelGradeItemsDTO> MINP_GD_SteelGradeItems { get; set; }

        public List<MINP_GD_ProcessPatternDTO> MINP_GD_ProcessPatterns { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimDatas { get; set; }

        public List<MINP_ProcessPatternDTO> MINP_ProcessPatterns { get; set; }

        public List<MINP_HeatDTO> MINP_Heats { get; set; }
    }
}

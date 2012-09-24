// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:57
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_ProcessPatternDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Note { get; set; }

        public String Name { get; set; }

        public Int32 Variant { get; set; }

        public Nullable<Guid> MINP_GD_SteelGradeID { get; set; }

        public MINP_GD_SteelGradeDTO MINP_GD_SteelGrade { get; set; }

        public List<MINP_GD_ProcessPatternOxygenDTO> MINP_GD_ProcessPatternOxygens { get; set; }

        public List<MINP_GD_ProcessPatternSlagDTO> MINP_GD_ProcessPatternSlags { get; set; }

        public List<MINP_HeatDTO> MINP_Heats { get; set; }
    }
}

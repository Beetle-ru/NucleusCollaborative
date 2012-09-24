// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:17
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class SIM_RealHeat_MatAddDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid SIM_RealHeatID { get; set; }

        public DateTime TimeProcessed { get; set; }

        public String Code { get; set; }

        public Int32 Amount_kg { get; set; }

        public SIM_RealHeatDTO SIM_RealHeat { get; set; }
    }
}

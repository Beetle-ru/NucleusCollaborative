// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:56
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MOUT_TemperatureTimeDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Boolean Considered { get; set; }

        public Guid MINP_HeatID { get; set; }

        public Int32 PhaseNumber { get; set; }

        public Int32 Temperature { get; set; }

        public Nullable<Int32> EndTemperature { get; set; }

        public Nullable<DateTime> EndTime { get; set; }

        public MINP_HeatDTO MINP_Heat { get; set; }
    }
}

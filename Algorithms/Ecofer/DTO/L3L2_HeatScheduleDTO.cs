// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:03
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L3L2_HeatScheduleDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Nullable<Int32> HeatID { get; set; }

        public String HeatNumber { get; set; }

        public String SteelGrade { get; set; }

        public Nullable<Int32> ConverterNo { get; set; }

        public Nullable<DateTime> StartTime { get; set; }

        public Nullable<DateTime> EndTime { get; set; }

        public Nullable<Int32> HotMetal_t { get; set; }

        public Nullable<Int32> Scrap_t { get; set; }

        public Nullable<Int32> Total_t { get; set; }

        public Nullable<Int32> HotMetal_Temperature { get; set; }

        public Nullable<Int32> Scrap_Temperature { get; set; }
    }
}

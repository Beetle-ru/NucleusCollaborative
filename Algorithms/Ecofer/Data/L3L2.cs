using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public static class L3L2
    {
        static L3L2()
        {
            Clear();
        }

        public static List<DTO.L3L2_HeatScheduleDTO> L3L2_HeatSchedule { get; set; }
        public static List<DTO.L3L2_SteelGradeDTO> L3L2_SteelGrade { get; set; }

        private static void Clear()
        {
            L3L2_HeatSchedule = new List<DTO.L3L2_HeatScheduleDTO>();
            L3L2_SteelGrade = new List<DTO.L3L2_SteelGradeDTO>();
        }
    }
}

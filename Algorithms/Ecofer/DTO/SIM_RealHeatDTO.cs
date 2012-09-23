// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:09
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class SIM_RealHeatDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String HeatNumber { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public String SteelGrade { get; set; }

        public DateTime ChargingTime_HotMetal { get; set; }

        public Int32 Temperature_HotMetal { get; set; }

        public DateTime ChargingTime_Scrap { get; set; }

        public Int32 ChargedScrap_kg { get; set; }

        public Int32 ChargedTotal_t { get; set; }

        public DateTime TappingStart { get; set; }

        public DateTime TappingEnd { get; set; }

        public Int32 O2Consumption_Main_m3 { get; set; }

        public Nullable<Int32> O2Consumption_Correction_m3 { get; set; }

        public Double HotMetal_C { get; set; }

        public Double HotMetal_Si { get; set; }

        public Double HotMetal_Mn { get; set; }

        public Double HotMetal_P { get; set; }

        public Double HotMetal_S { get; set; }

        public Double Requested_C_p { get; set; }

        public Int32 Requested_Temperature { get; set; }

        public Nullable<Int32> FOM_kg { get; set; }

        public Nullable<Int32> CaO_kg { get; set; }

        public Nullable<Int32> CaCO3_kg { get; set; }

        public Nullable<Int32> Dolomit_kg { get; set; }

        public List<SIM_RealHeat_SteelAnalysisDTO> SIM_RealHeat_SteelAnalysis { get; set; }

        public List<SIM_RealHeat_TempMeasDTO> SIM_RealHeat_TempMeas { get; set; }

        public List<SIM_RealHeat_SlagAnalysisDTO> SIM_RealHeat_SlagAnalysis { get; set; }

        public List<SIM_RealHeat_CyclicDTO> SIM_RealHeat_Cyclics { get; set; }

        public List<SIM_RealHeat_MatAddDTO> SIM_RealHeat_MatAdds { get; set; }
    }
}

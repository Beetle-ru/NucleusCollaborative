// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:05
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_HeatDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Note { get; set; }

        public Nullable<Int32> HeatID { get; set; }

        public String HeatNumber { get; set; }

        public Int32 ConverterNo { get; set; }

        public Nullable<Int32> SimulationNumber { get; set; }

        public DateTime StartTime { get; set; }

        public Nullable<DateTime> EndTime { get; set; }

        public Nullable<Int32> SteelHeight_mm { get; set; }

        public Guid MINP_GD_SteelGradeID { get; set; }

        public Int32 FinalTemperature { get; set; }

        public Guid MINP_GD_ProcessPatternID { get; set; }

        public Boolean Charged { get; set; }

        public Nullable<Int32> CalculatedOxygenAmount_Nm3 { get; set; }

        public MINP_GD_ProcessPatternDTO MINP_GD_ProcessPattern { get; set; }

        public MINP_GD_SteelGradeDTO MINP_GD_SteelGrade { get; set; }

        public List<MINP_MatAddDTO> MINP_MatAdds { get; set; }

        public List<MINP_ModelParametersDTO> MINP_ModelParameters { get; set; }

        public List<MINP_PhaseChangeDTO> MINP_PhaseChanges { get; set; }

        public List<MINP_ProcessPatternDTO> MINP_ProcessPatterns { get; set; }

        public List<MOUT_AlloyRecipeDTO> MOUT_AlloyRecipes { get; set; }

        public List<MOUT_MessageDTO> MOUT_Messages { get; set; }

        public List<MOUT_OxygenBlowingDTO> MOUT_OxygenBlowings { get; set; }

        public List<MOUT_SteelAnalysisDTO> MOUT_SteelAnalysis { get; set; }

        public List<MOUT_TemperatureTimeDTO> MOUT_TemperatureTimes { get; set; }

        public List<MINP_CyclicDTO> MINP_Cyclics { get; set; }

        public List<MOUT_SlagAnalysisDTO> MOUT_SlagAnalysis { get; set; }

        public List<MINP_TempMeasDTO> MINP_TempMeas { get; set; }
    }
}

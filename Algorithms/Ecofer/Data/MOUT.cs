using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public static class MOUT
    {
        static MOUT()
        {
            ClearHeatData();
        }
        public static void ClearHeatData()
        {
            MOUT_AlloyingRecipe = null;
            MOUT_TemperatureTime = new List<DTO.MOUT_TemperatureTimeDTO>();
            MOUT_SteelAnalysis = new List<DTO.MOUT_SteelAnalysisDTO>();
            MOUT_SlagAnalysis = new List<DTO.MOUT_SlagAnalysisDTO>();
        }

        public static DTO.MOUT_AlloyRecipeDTO MOUT_AlloyingRecipe { get; set; }
        public static List<DTO.MOUT_TemperatureTimeDTO> MOUT_TemperatureTime { get; set; }
        public static List<DTO.MOUT_SteelAnalysisDTO> MOUT_SteelAnalysis { get; set; }
        public static List<DTO.MOUT_SlagAnalysisDTO> MOUT_SlagAnalysis { get; set; }

    }
}

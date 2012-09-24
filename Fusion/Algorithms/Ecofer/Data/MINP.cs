using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Data
{
    public static class MINP
    {
        static MINP()
        {
            Heat = new DTO.MINP_HeatDTO();
            ClearHeatData();
        }

        public static DTO.MINP_HeatDTO Heat { get; set; }
        public static DTO.MINP_HeatAimDataDTO HeatAimData { get; set; }
        public static Phases Phases { get; set; }
        public static List<Graph.O2RequestItem> O2Request { get; set; }
        public static List<DTO.MINP_CyclicDTO> MINP_Cyclic { get; set; }
        public static List<DTO.MINP_MatAddDTO> MINP_MatAdds { get; set; }
        public static List<DTO.MINP_TempMeasDTO> MINP_TempMeas { get; set; }
        public static List<DTO.MINP_ModelParametersDTO> MINP_ModelParameters { get; set; }
        public static DTO.MINP_ProcessPatternDTO MINP_ProcessPattern { get; set; }

        public static Dictionary<int, DTO.MINP_GD_MaterialElementDTO> MINP_GD_MaterialElements { get; set; }
        public static Dictionary<Common.Enumerations.MINP_GD_Material_ModelMaterial, DTO.MINP_GD_MaterialDTO> MINP_GD_ModelMaterials { get; set; }

        public static void ClearHeatData()
        {
            Heat.ID = Guid.Empty;
            Heat.HeatNumber = null;
            Phases = null;

            O2Request = new List<Graph.O2RequestItem>();
            MINP_Cyclic = new List<DTO.MINP_CyclicDTO>();
            MINP_MatAdds = new List<DTO.MINP_MatAddDTO>();
            MINP_TempMeas = new List<DTO.MINP_TempMeasDTO>();
            MINP_ModelParameters = new List<DTO.MINP_ModelParametersDTO>();
            MINP_ProcessPattern = null;
        }

        public static float Sum(float[] aArray, int aFromIndex, int aToIndex)
        {
            float lResult = 0;

            for (int i = aFromIndex; i <= aToIndex; i++)
            {
                lResult += aArray[i] / ConversionVector(i);
            }

            return lResult;
        }
        public static float FP(DTO.MINP_GD_MaterialDTO aMaterial, int aIndex)
        {
            if (aMaterial == null) return 0;
            DTO.MINP_GD_MaterialItemsDTO lMaterialItem = aMaterial.MINP_GD_MaterialItems.SingleOrDefault(aR => aR.MINP_GD_MaterialElement.Index == aIndex);
            return lMaterialItem == null ? 0 : (float)lMaterialItem.Amount_p;
        }
        public static float ConversionVector(int aIndex)
        {
            return (float)MINP.MINP_GD_MaterialElements[aIndex].Vector;
        }
        public static float Mm(int aIndex)
        {
            return (float)MINP.MINP_GD_MaterialElements[aIndex].Mm;
        }
        public static float O2_Stechio(int aIndex)
        {
            return (float)MINP.MINP_GD_MaterialElements[aIndex].O2;
        }
        public static float E_Ox1(int aIndex)
        {
            return (float)MINP.MINP_GD_MaterialElements[aIndex].E_Ox1;
        }
        public static float E_Ox2(int aIndex)
        {
            return (float)MINP.MINP_GD_MaterialElements[aIndex].E_Ox2;
        }
        public static float Eta_Ox1(int aIndex)
        {
            return (float)MINP.MINP_GD_MaterialElements[aIndex].Eta_Ox1;
        }
        public static float Eta_Ox2(int aIndex)
        {
            return (float)MINP.MINP_GD_MaterialElements[aIndex].Eta_Ox2;
        }
    }
}

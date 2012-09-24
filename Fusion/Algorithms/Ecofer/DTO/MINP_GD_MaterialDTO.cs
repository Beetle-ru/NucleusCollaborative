// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:47
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_GD_MaterialDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public String Code { get; set; }

        public String ShortCode { get; set; }

        public String Description { get; set; }

        public Boolean Available { get; set; }

        public Nullable<Int32> ModelMaterial { get; set; }

        public Double AlloyingYield_p { get; set; }

        public Int32 Price { get; set; }

        public List<MINP_GD_BunkerMaterialDTO> MINP_GD_BunkerMaterials { get; set; }

        public List<MINP_GD_MaterialItemsDTO> MINP_GD_MaterialItems { get; set; }

        public List<MINP_MatAddDTO> MINP_MatAdds { get; set; }

        public List<MOUT_AlloyRecipeItemsDTO> MOUT_AlloyRecipeItems { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimDatas { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData1s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData2s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData3s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData4s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData5s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData6s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData7s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData8s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData9s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData10s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData11s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData12s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData13s { get; set; }

        public List<MINP_HeatAimDataDTO> MINP_HeatAimData14s { get; set; }
    }
}

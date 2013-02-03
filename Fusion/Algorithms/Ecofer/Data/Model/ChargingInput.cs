using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data.Model
{
    public class ChargingInput
    {
        public ChargingInput()
        {
            HotMetals = new DTO.MINP_GD_MaterialDTO[Common.Global.HOTMETAL_COUNT];
            HotMetals_t = new int[Common.Global.HOTMETAL_COUNT];
            Scraps = new DTO.MINP_GD_MaterialDTO[Common.Global.SCRAPYARDS_COUNT];
            Scraps_t = new int[Common.Global.SCRAPYARDS_COUNT];
        }

        // temperatures
        public int Final_Temperature;
        public int? HotMetal_Temperature;
        public int? Scrap_Temperature;

        // materials
        public DTO.MINP_GD_MaterialDTO[] HotMetals;
        public int[] HotMetals_t;
        
        public DTO.MINP_GD_MaterialDTO[] Scraps;
        public int[] Scraps_t;
        
        public double Basicity;
        public int FeO_p;
        public int MgO_p;
        public DTO.MINP_GD_MaterialDTO Lime;
        public DTO.MINP_GD_MaterialDTO S1;
        public DTO.MINP_GD_MaterialDTO S2;
        public DTO.MINP_GD_MaterialDTO FOM;
        public DTO.MINP_GD_MaterialDTO Dolomite;
        public DTO.MINP_GD_MaterialDTO Coke;
        public int Lime_kg;
        public int S1_kg;
        public int S2_kg;
        public int FOM_kg;
        public int Dolomite_kg;
        public int Coke_kg;

        public DTO.MINP_GD_MaterialDTO Odprasky;
        public DTO.MINP_GD_MaterialDTO StrStr;
        public DTO.MINP_GD_MaterialDTO Steel;
    }
}

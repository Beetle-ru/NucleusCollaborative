using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    public class DynamicInput
    {
        public List<DTO.MINP_MatAddDTO> ChargedMaterials;
        public Nullable<int> HotMetal_Temperature;
        public int? Scrap_Temperature;

        public List<PhaseItem> OxygenBlowingPhases;
    }
}

// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:53
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MOUT_AlloyRecipeItemsDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MOUT_AlloyRecipeID { get; set; }

        public Guid MINP_GD_MaterialID { get; set; }

        public Nullable<Int32> Amount_kg { get; set; }

        public MINP_GD_MaterialDTO MINP_GD_Material { get; set; }

        public MOUT_AlloyRecipeDTO MOUT_AlloyRecipe { get; set; }
    }
}

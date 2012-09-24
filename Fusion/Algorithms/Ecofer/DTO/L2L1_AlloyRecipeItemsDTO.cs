// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:13
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L2L1_AlloyRecipeItemsDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid L2L1_AlloyRecipeID { get; set; }

        public String BunkerNo { get; set; }

        public String Code { get; set; }

        public String ShortCode { get; set; }

        public Int32 Amount_kg { get; set; }

        public L2L1_AlloyRecipeDTO L2L1_AlloyRecipe { get; set; }
    }
}

// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:13
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L2L1_AlloyRecipeDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Int32 ConverterNo { get; set; }

        public Boolean Considered { get; set; }

        public String HeatNumber { get; set; }

        public Int32 RecipeNo { get; set; }

        public List<L2L1_AlloyRecipeItemsDTO> L2L1_AlloyRecipeItems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.EventArgs
{
    public class CommunicationL2L1AlloyingRecipeEventArgs : System.EventArgs
    {
        public DTO.MOUT_AlloyRecipeDTO AlloyingRecipe;

        /// <summary>
        /// Initializes a new instance of the CommunicationL2L1AlloyingRecipeEventArgs class.
        /// </summary>
        /// <param name="alloyingRecipe"></param>
        public CommunicationL2L1AlloyingRecipeEventArgs(DTO.MOUT_AlloyRecipeDTO alloyingRecipe)
        {
            AlloyingRecipe = alloyingRecipe;
        }
    }
}

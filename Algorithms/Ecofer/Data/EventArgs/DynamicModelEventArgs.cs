using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.EventArgs
{
    public class DynamicModelCorrectionO2AmountEventArgs : System.EventArgs
    {
        public int CorrectionO2Amount_Nm3;

        /// <summary>
        /// Initializes a new instance of the SimulationEventArgs class.
        /// </summary>
        /// <param name="temperature"></param>
        public DynamicModelCorrectionO2AmountEventArgs(int aCorrectionO2Amount_Nm3)
        {
            CorrectionO2Amount_Nm3 = aCorrectionO2Amount_Nm3;
        }
    }
}

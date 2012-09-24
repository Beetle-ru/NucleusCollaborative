using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.EventArgs
{
    public class SimulationTempMeasEventArgs : System.EventArgs
    {
        public int? Temperature;
        public float? Carbon_p;

        public SimulationTempMeasEventArgs()
        {
            Temperature = null;
            Carbon_p = null;
        }
        /// <summary>
        /// Initializes a new instance of the SimulationEventArgs class.
        /// </summary>
        /// <param name="temperature"></param>
        public SimulationTempMeasEventArgs(int? aTemperature, float? aCarbon)
        {
            Temperature = aTemperature;
            Carbon_p = aCarbon;
        }
    }
}

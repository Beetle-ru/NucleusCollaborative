using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.EventArgs
{
    public class CurrentPhaseChangedEventArgs : System.EventArgs
    {
        public PhaseItem PreviousPhase;
        public PhaseItem CurrentPhase;
        public PhaseItem CurrentVisiblePhase;

        /// <summary>
        /// Initializes a new instance of the CurrentPhaseChangedEventArgs class.
        /// </summary>
        /// <param name="aPreviousPhase"></param>
        /// <param name="aCurrentPhase"></param>
        public CurrentPhaseChangedEventArgs(PhaseItem aPreviousPhase, PhaseItem aCurrentPhase, PhaseItem aCurrentVisiblePhase)
        {
            PreviousPhase = aPreviousPhase;
            CurrentPhase = aCurrentPhase;
            CurrentVisiblePhase = aCurrentVisiblePhase;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    /// <summary>
    /// Represents treatment duration in realtime or simulation process.
    /// </summary>
    public class Clock
    {
        public static Clock Current;

        public Clock()
        {
            Current = this;
            mSimulation = false;
        }
        public Clock(int aSimulationDeltaT_s)
        {
            Current = this;
            mSimulation = true;
            mSimulationDeltaT_s = aSimulationDeltaT_s;
            mSimulationSteps = 0;
            ResetStartTime();
        }

        public void ResetStartTime()
        {
            StartTime = DateTime.Now;
        }
        public void IncSimulationStep()
        {
            mSimulationSteps++;
        }

        private bool mSimulation;
        private int mSimulationDeltaT_s;
        private int mSimulationSteps;

        public DateTime StartTime { get; set; }
        public TimeSpan Duration
        {
            get
            {
                if (mSimulation)
                {
                    return StartTime.AddSeconds(mSimulationSteps * mSimulationDeltaT_s) - StartTime;
                }
                else
                {
                    return DateTime.Now - StartTime;
                }
            }
        }
        public DateTime ActualTime
        {
            get
            {
                return StartTime + Duration;
            }
        }
    }
}

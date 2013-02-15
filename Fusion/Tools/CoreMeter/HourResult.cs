using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreMeter
{
    public class HourResult
    {
        public DateTime Time = DateTime.Now;
        public int FieredEvents;
        public int ReceivedEvents;
        public double MaxDelayMs;
        public double AverageDelayMs;
        public int LostEvents;
        public double TotalDelayMs;
    }
}

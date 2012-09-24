using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;


namespace HeatInfo
{
    public class HotMetalTorpedo
    {
        public int Number { get; set; }
        public DateTime ChargeTime { get; set; }
        public int Weight { get; set; }
        public HotMetalAnalysys Analysys { get; set; }

        public HotMetalTorpedo()
        {
            Analysys = new HotMetalAnalysys();
        }
    }
}

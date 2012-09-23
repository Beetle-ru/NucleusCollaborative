using System;

namespace Converter
{
    [Serializable]
    public class HotMetalAnalysys
    {
        public DateTime Time { get; set; }
        public int ProbeNumber { get; set; }
        public int? TorpedoNumber { get; set; }
        public int? LadleNumber { get; set; }
        public double C { get; set; }
        public double Si { get; set; }
        public double Mn { get; set; }
        public double P { get; set; }
        public double S { get; set; }
        public double Ti { get; set; }
    }
}

using System;

namespace CommonTypes.Classes
{
    [Serializable]
    public class SlagAnalysys
    {
        public DateTime Time { get; set; }
        public int ProbeNumber { get; set; }
        public double CaO { get; set; }
        public double SiO2 { get; set; }
        public double FeO { get; set; }
        public double MgO { get; set; }
        public double MnO { get; set; }
        public double S { get; set; }
        public double Al2O3 { get; set; }
        public double P2O5 { get; set; }
    }
}
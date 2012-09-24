using System;

namespace Converter
{
    [Serializable]
    public class Sublance
    {
        public DateTime StartDate { get; set; }
        public int Temperature { get; set; }
        public int Oxigen { set; get; }
        public double C { set; get; }
    }
}

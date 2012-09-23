using System;
using Esms;

namespace EsmsFusionProtocol
{
    public class HeatCommon
    {
        public int HeatNumber { get; set; }
        public DateTime HeatStart { get; set; }
        public DateTime HeatEnd { get; set; }
        public DateTime PrecedingHeatEnd{ get; set; }
        public int NextHeatNumber { get; set; }
        public DateTime NextHeatStart { get; set; }
        public DateTime NextHeatEnd { get; set; }
        public int PreviousHeatNumber { get; set; }
        public DateTime PreviousHeatStart { get; set; }
        public DateTime PreviousHeatEnd { get; set; }
        public int HeatId { get; set; }
        public int ShpNumber { get; set; }
        public DateTime HeatStartDB { get; set; }
        public DateTime HeatEndtDB { get; set; }
    }
}

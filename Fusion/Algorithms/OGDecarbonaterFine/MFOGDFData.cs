using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGDecarbonaterFine {
    public class MFOGDFData {
        public Int64 HeatNumber;
        public double DeltaK;
        public double DeltaCarbon;
        public double MFe; // масса железа
        public double MCarbonCalc; // dmctk - уточненный углерод в килограммах
        public double MCarbonReal; // реальный углерод в килограммах
        public double PCarbonCalc; // dmctk - уточненный углерод в процентах
        public double PCarbonReal; // реальный углерод в процентах
    }
}
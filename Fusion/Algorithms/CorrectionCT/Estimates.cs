using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorrectionCT {
    internal class Estimates {
        public int CurrentT;
        public double CurrentC;
        public int TargetT;
        public double TargetC;

        public Estimates() {
            CurrentT = 0;
            CurrentC = 0.0;
            TargetT = 0;
            TargetC = 0.0;
        }
    }
}
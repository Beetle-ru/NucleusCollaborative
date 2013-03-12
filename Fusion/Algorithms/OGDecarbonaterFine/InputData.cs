using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGDecarbonaterFine {
    public class InputData {
        public double CO;
        public double CO2;
        public double H2;
        public double O2;
        public double N2;
        public double Ar;
        public double OffGasT;
        public double OffGasV;
        public double OffGasDecompression;
        public int LanceHeight;
        public double QO2;
        public int QO2I;

        public InputData() {
            CO = 0.0;
            CO = 0.02;
            H2 = 0.0;
            O2 = 0.0;
            N2 = 0.0;
            Ar = 0.0;
            OffGasT = 0.0;
            OffGasV = 0.0;
            OffGasDecompression = 0.0;
            LanceHeight = 0;
            QO2 = 0.0;
            QO2I = 0;
        }
    }
}
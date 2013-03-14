using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterHeatProcessorEngine {
    public class LanceQuant : Converter.SteelMakingClasses.Lance {
        public int O2Volume { get; set; } // расход кислорода
        public LanceQuant() {
            LancePositin = -1;
            O2Flow = -1;
            O2Volume = -1;
        }
    }
}
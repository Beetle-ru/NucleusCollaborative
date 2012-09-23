using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterHeatProcessorEngine
{
    public class AdditionQuant : Converter.SteelMakingClasses.Addition
    {
        public int O2VolPortionMaterial { get; set; }            // O2 расход при котором засыпаем
      
        public AdditionQuant()
        {
            MaterialPortionWeight = -1.0;
            O2VolPortionMaterial = -1;
        }
    }
}

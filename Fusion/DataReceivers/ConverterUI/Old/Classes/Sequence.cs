using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterUI
{
    [Serializable]
    public class Sequence
    {
        public const int NumbersOfStageIntervals = 20;

        public Sequence() 
        {
            Values = new double[NumbersOfStageIntervals];
        }

        public string Name;

        public double[] Values;
    }
}

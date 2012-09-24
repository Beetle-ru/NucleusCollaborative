using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterUI
{
    [Serializable]
    public class HeatStage
    {
        public const int NumbersOfStageIntervals = 20;
        public const int NumbersOfStageSequencies = 15;
        public double O2Volume = 0;

        public HeatStage()
        {
            Percentages = new double[NumbersOfStageIntervals];
            Sequencies = new Sequence[NumbersOfStageSequencies];
            for (int i = 0; i < NumbersOfStageSequencies; i++)
            {
                Sequencies[i] = new Sequence();
            }
            Sequencies[0].Name = "Интенсивность";
            Sequencies[1].Name = "Положение Фурмы";
        }

        public string Name;

        public Sequence[] Sequencies;
        public double[] Percentages;


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterUI
{
    [Serializable]
    public class HeatScript
    {
        public HeatScript() 
        {
            Stages = new HeatStage[4];
            for (int i = 0; i < 4; i++)
            {
                Stages[i] = new HeatStage();
            }
        }

        public string Name;

        public HeatStage[] Stages; 
    }
}

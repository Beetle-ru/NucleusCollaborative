using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeatCharge;

namespace CarboneProcessor
{
    class MFCMDataFull : MFCMData
    {
        public Int32 IdHeat { set; get; }
        public Int64 NumberHeat { set; get; }
        public double SteelCarbonCalculationPercent { set; get; }
        
        public MFCMDataFull()
        {
            IdHeat = -1;
            NumberHeat = -1;
            SteelCarbonCalculationPercent = 0;
        }
    }
}

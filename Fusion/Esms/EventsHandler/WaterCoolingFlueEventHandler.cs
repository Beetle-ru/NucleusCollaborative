using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(WaterCoolingFlueEvent _event)
        {
            try
            {
                _Module.Heat.WaterCoolingFlueHistory.Add(_event);
            }
            catch { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(WaterCoolingMineEvent _event)
        {
            try
            {
                _Module.Heat.WaterCoolingMineHistory.Add(_event);
            }
            catch { }
        }
    }
}
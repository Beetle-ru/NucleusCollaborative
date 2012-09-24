using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(WaterCoolingPanelEvent _event)
        {
            try
            {
               _Module.Heat.WaterCoolingPanelHistory.Add(_event);
            }
            catch { }
        }
    }
}
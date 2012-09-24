using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(EnergyEvent _event)
        {
            try
            {
                _Module.Heat.EnergyHistory.Add(_event);
            }
            catch { }
        }
    }
}
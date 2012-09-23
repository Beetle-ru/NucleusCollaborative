using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(CoalInjectionEvent _event)
        {
            try
            {
                _Module.Heat.CoalInjectionHistory.Add(_event);
            }
            catch { }
        }
    }
}
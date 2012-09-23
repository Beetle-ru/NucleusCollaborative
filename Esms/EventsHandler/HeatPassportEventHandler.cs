using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(HeatPassportEvent _event)
        {
            try
            {
                _Module.Heat.HeatPassportHistory.Add(_event);
            }
            catch { }
        }
    }
}
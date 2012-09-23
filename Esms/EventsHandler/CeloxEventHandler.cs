using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(CeloxEvent _event)
        {
            try
            {
                _Module.Heat.CeloxHistory.Add(_event);
            }
            catch { }
        }
    }
}
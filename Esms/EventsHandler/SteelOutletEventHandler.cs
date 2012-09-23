using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(SteelOutletEvent _event)
        {
            try
            {
                _Module.Heat.SteelOutletHistory.Add(_event);
            }
            catch { }
        }
    }
}
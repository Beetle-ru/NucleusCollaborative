using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(PouringHotMetalEvent _event)
        {
            try
            {
                _Module.Heat.PouringHotMetalHistory.Add(_event);
            }
            catch { }
        }
    }
}
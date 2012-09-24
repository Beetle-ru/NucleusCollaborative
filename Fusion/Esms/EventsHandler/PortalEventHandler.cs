using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(PortalEvent _event)
        {
            try
            {
                _Module.Heat.PortalHistory.Add(_event);
            }
            catch { }
        }
    }
}
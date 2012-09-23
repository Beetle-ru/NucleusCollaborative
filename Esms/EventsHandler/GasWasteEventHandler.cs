using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(GasWasteEvent _event)
        {
            try
            {
                _Module.Heat.GasWasteHistory.Add(_event);
            }
            catch { }
        }
    }
}
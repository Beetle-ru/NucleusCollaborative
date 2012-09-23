using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(CapMineEvent _event)
        {
            try
            {
                _Module.Heat.CapMineHistory.Add(_event);
            }
            catch { }
        }
    }
}
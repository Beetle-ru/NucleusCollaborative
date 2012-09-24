using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(SchieberEvent _event)
        {
            try
            {
                _Module.Heat.SchieberHistory.Add(_event);
            }
            catch { }
        }
    }
}
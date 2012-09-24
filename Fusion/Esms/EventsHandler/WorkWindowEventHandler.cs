using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(WorkWindowEvent _event)
        {
            try
            {
                _Module.Heat.WorkWindowHistory.Add(_event);
            }
            catch { }
        }
    }
}
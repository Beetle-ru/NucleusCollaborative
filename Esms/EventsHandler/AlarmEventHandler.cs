using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(AlarmEvent _event)
        {
            try
            {
                _Module.Heat.AlarmHistory.Add(_event);
            }
            catch { }
        }
    }
}
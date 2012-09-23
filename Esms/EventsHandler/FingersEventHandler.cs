using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(FingersEvent _event)
        {
            try
            {
                _Module.Heat.FingersHistory.Add(_event);
            }
            catch { }
        }
    }
}
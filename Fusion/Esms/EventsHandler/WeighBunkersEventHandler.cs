using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(WeighBunkersEvent _event)
        {
            try
            {
                _Module.Heat.WeighBunkersHistory.Add(_event);
            }
            catch { }
        }
    }
}
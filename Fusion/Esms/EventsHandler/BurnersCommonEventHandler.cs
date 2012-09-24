using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(BurnersCommonEvent _event)
        {
            try
            {
                _Module.Heat.BurnersCommonHistory.Add(_event);
            }
            catch { }
        }
    }
}
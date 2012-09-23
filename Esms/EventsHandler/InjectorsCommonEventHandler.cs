using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(InjectorsCommonEvent _event)
        {
            try
            {
                _Module.Heat.InjectorsCommonHistory.Add(_event);
            }
            catch { }
        }
    }
}
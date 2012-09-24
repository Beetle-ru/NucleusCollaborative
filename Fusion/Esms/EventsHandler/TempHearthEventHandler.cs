using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(TempHearthEvent _event)
        {
            try
            {
                _Module.Heat.TempHearthHistory.Add(_event);
            }
            catch { }
        }
    }
}
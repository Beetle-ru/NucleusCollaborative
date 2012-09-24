using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(MaterialsFurnaceEvent _event)
        {
            try
            {
                _Module.Heat.MaterialsFurnaceHistory.Add(_event);
            }
            catch { }
        }
    }
}
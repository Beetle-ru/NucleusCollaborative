using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(SteelCarEvent _event)
        {
            try
            {
                _Module.Heat.SteelCarHistory.Add(_event);
            }
            catch { }
        }
    }
}
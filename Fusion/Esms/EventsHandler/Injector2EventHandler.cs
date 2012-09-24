using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(Injector2Event _event)
        {
            try
            {
                _Module.Heat.Injector2History.Add(_event);
            }
            catch { }
        }
    }
}
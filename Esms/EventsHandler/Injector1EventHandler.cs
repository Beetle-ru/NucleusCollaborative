using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(Injector1Event _event)
        {
            try
            {
                _Module.Heat.Injector1History.Add(_event);
            }
            catch { }
        }
    }
}
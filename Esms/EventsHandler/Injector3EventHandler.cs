using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(Injector3Event _event)
        {
            try
            {
                _Module.Heat.Injector3History.Add(_event);
            }
            catch { }
        }
    }
}
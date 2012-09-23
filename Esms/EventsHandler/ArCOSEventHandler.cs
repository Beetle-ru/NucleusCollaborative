using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(ArCOSEvent _event)
        {
            try
            {
                _Module.Heat.ArCOSHistory.Add(_event);
            }
            catch { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(LanceCrestEvent _event)
        {
            try
            {
                _Module.Heat.LanceCrestHistory.Add(_event);
            }
            catch { }
        }
    }
}
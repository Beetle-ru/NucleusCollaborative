using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(ElectrodesEvent _event)
        {
            try
            {
                _Module.Heat.ElectrodesHistory.Add(_event);
            }
            catch { }
        }
    }
}
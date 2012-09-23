using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(MineEvent _event)
        {
            try
            {
                _Module.Heat.MineHistory.Add(_event);
            }
            catch { }
        }
    }
}
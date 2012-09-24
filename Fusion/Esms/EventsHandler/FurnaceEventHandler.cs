using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(FurnaceEvent _event)
        {
            try
            {
                _Module.Heat.FurnaceHistory.Add(_event);
            }
            catch { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(SuctionValveEvent _event)
        {
            try
            {
                _Module.Heat.SuctionValveHistory.Add(_event);
            }
            catch { }
        }
    }
}
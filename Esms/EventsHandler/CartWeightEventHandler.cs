using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(CartWeightEvent _event)
        {
            try
            {
                _Module.Heat.CartWeightHistory.Add(_event);
            }
            catch { }
        }
    }
}
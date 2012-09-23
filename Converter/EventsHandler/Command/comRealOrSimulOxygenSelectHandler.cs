using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comRealOrSimulOxygenSelectEvent _event)
        {
            
            try
            {
                this._Module._Heat.comRealOrSimulOxygenSelectHistory.Add(_event);
            }
            catch { }
        }

    }
}

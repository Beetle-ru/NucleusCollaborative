using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(cntO2FlowRateEvent _event)
        {
            
            try
            {
                this._Module._Heat.cntO2FlowRateHistory.Add(_event);
            }
            catch { }
        }

    }
}

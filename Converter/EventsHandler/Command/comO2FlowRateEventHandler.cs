using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comO2FlowRateEvent _event)
        {
            
            try
            {
                this._Module._Heat.comO2FlowRateHistory.Add(_event);
            }
            catch { }
        }

    }
}

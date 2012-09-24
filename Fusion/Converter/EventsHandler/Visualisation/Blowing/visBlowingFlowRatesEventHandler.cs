using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visBlowingFlowRatesEvent _event)
        {
            try
            {
                this._Module._Heat.visBlowingFlowRatesHistory.Add(_event);
            }
            catch { }
        }

    }
}

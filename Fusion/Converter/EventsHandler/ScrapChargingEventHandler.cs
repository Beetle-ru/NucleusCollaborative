using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ScrapChargingEvent _event)
        {
            try
            {
                this._Module._Heat.ScrapChargingHistory.Add(_event);
            }
            catch { }

        }
    }
}

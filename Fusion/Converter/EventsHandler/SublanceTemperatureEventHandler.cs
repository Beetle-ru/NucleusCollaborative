using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(SublanceTemperatureEvent _event)
        {
            try
            {
                this._Module._Heat.SublanceTemperatureHistory.Add( _event);
                this._Module._Heat.Actual.Temperature = _event.SublanceTemperature;
            }
            catch { }

        }
    }
}

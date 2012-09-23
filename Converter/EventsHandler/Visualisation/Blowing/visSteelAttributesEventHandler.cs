using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visSteelAttributesEvent _event)
        {
            
            try
            {
                this._Module._Heat.visSteelAttributesHistory.Add(_event);
                this._Module._Heat.Grade = _event.Grade;
                this._Module._Heat.Planned.Temperature = _event.PlannedTemperature;
            }
            catch { }
        }

    }
}

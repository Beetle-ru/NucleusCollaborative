using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(TappingEvent _event)
        {
            try
            {
                this._Module._Heat.TappingHistory.Add(_event);
            }
            catch { }

        }
    }
}

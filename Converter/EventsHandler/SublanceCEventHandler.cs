using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(SublanceCEvent _event)
        {
            try
            {
                this._Module._Heat.SublanceCHistory.Add(_event);
                this._Module._Heat.Actual.Carbon = _event.C;
            }
            catch { }

        }
    }
}

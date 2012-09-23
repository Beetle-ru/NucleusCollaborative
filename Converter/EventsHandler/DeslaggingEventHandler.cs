using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(DeslaggingEvent _event)
        {
            try
            {
                this._Module._Heat.DeslaggingHistory.Add( _event);
            }
            catch { }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(TorkretingEvent _event)
        {
            try
            {
                this._Module._Heat.TorkretingHistory.Add( _event);
            }
            catch { }

        }
    }
}

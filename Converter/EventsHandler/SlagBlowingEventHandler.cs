using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(SlagBlowingEvent _event)
        {
            try
            {
                this._Module._Heat.SlagBlowingHistory.Add( _event);
            }
            catch { }

        }
    }
}

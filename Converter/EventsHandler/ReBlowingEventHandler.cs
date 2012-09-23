using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ReBlowingEvent _event)
        {
            try
            {
                this._Module._Heat.ReBlowingHistory.Add( _event);
            }
            catch { }

        }
    }
}

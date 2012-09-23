using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(SublanceStartEvent _event)
        {
            try
            {
                this._Module._Heat.SublanceStartHistory.Add( _event);
            }
            catch { }

        }
    }
}

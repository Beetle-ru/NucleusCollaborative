using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(IgnitionEvent _event)
        {
            try
            {
                this._Module._Heat.IgnitionHistory.Add( _event);
            }
            catch { }

        }
    }
}

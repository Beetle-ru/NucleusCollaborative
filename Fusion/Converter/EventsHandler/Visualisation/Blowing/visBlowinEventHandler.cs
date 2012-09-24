using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visBlowingEvent _event)
        {
            
            try
            {
                this._Module._Heat.visBlowingHistory.Add(_event);
            }
            catch { }
        }

    }
}

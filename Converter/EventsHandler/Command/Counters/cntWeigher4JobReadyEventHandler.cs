using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(cntWeigher4JobReadyEvent _event)
        {
            
            try
            {
                this._Module._Heat.cntWeigher4JobReadyHistory.Add(_event);
            }
            catch { }
        }

    }
}

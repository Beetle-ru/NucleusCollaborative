using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(cntWeigher6JobReadyEvent _event)
        {
            
            try
            {
                this._Module._Heat.cntWeigher6JobReadyHistory.Add(_event);
            }
            catch { }
        }

    }
}

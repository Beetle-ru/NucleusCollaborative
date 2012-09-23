using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(cntAdditionsEvent _event)
        {
            
            try
            {
                this._Module._Heat.cntAdditionsHistory.Add(_event);
            }
            catch { }
        }

    }
}

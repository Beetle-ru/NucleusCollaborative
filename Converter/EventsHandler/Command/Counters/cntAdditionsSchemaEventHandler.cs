using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(cntAdditionsSchemaEvent _event)
        {
            
            try
            {
                this._Module._Heat.cntAdditionsSchemaHistory.Add(_event);
            }
            catch { }
        }

    }
}

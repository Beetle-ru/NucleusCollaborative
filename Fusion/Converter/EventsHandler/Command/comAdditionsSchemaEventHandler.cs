using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comAdditionsSchemaEvent _event)
        {
            
            try
            {
                this._Module._Heat.comAdditionsSchemaHistory.Add(_event);
            }
            catch { }
        }

    }
}

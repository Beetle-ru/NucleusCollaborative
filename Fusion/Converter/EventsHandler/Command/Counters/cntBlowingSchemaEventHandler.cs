using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(cntBlowingSchemaEvent _event)
        {
            
            try
            {
                this._Module._Heat.cntBlowingSchemaHistory.Add(_event);
            }
            catch { }
        }

    }
}

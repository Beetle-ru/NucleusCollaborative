using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comBlowingSchemaEvent _event)
        {
            
            try
            {
                this._Module._Heat.comBlowingSchemaHistory.Add(_event);
            }
            catch { }
        }

    }
}

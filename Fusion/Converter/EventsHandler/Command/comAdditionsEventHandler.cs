using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comAdditionsEvent _event)
        {
            
            try
            {
                this._Module._Heat.comAdditionsHistory.Add(_event);
            }
            catch { }
        }

    }
}

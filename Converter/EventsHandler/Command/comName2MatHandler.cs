using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ComName2MatEvent _event)
        {
            
            try
            {
                this._Module._Heat.comName2MatHistory.Add(_event);
            }
            catch { }
        }

    }
}

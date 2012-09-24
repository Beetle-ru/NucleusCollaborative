using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(OPCDirectReadEvent _event)
        {
            try
            {
                this._Module._Heat.OPCDirectReadHistory.Add(_event);
            }
            catch { }

        }
    }
}

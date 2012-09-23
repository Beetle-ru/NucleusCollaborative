using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visIndustrialBunkersEvent _event)
        {
            try
            {
                this._Module._Heat.visIndustrialBunkersHistory.Add(_event);
            }
            catch { }
        }

    }
}

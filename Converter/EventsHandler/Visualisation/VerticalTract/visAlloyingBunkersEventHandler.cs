using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visAlloyingBunkersEvent _event)
        {
            try
            {
                this._Module._Heat.visAlloyingBunkersHistory.Add(_event);
            }
            catch { }
        }

    }
}

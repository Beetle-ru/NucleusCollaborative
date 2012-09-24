using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(OffGasEvent _event)
        {
            try
            {
                this._Module._Heat.OffGasHistory.Add(_event);
            }
            catch { }

        }
    }
}

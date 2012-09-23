using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(WeighersStateEvent _event)
        {
            try
            {
                this._Module._Heat.WeighersStateHistory.Add(_event);
            }
            catch { }
        }

    }
}

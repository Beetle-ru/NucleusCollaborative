using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    public partial class ConverterEventsHandler
    {
        public void Process(BlowingInterruptEvent _event)
        {
            try
            {
                this._Module._Heat.BlowingInterruptHistory.Add(_event);
            }
            catch { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ModeVerticalPathEvent _event)
        {
            try
            {
                this._Module._Heat.ModeVerticalPathHistory.Add(_event);
            }
            catch { }

        }
    }
}

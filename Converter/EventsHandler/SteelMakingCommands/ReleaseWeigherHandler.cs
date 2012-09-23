using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ReleaseWeigherEvent _event)
        {
            
            try
            {
                this._Module._Heat.ReleaseWeigherHistory.Add(_event);
            }
            catch { }
        }

    }
}

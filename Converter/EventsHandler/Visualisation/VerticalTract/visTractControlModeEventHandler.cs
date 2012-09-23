using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visTractControlModeEvent _event)
        {
            
            try
            {
                this._Module._Heat.visTractControlModeHistory.Add(_event);
            }
            catch { }
        }

    }
}

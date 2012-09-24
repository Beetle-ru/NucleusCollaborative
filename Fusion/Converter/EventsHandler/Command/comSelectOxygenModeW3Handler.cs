using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comSelectOxygenModeW3Event _event)
        {
            
            try
            {
                this._Module._Heat.comSelectOxygenModeW3History.Add(_event);
            }
            catch { }
        }

    }
}

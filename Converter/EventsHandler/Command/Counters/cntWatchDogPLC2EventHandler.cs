using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(cntWatchDogPLC2Event _event)
        {
            
            try
            {
                this._Module._Heat.cntWatchDogPLC2History.Add(_event);
            }
            catch { }
        }

    }
}

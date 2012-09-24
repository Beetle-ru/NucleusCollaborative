using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visSublanceEvent _event)
        {
            
            try
            {
                this._Module._Heat.visSublanceHistory.Add(_event);
                
            }
            catch { }
        }

    }
}

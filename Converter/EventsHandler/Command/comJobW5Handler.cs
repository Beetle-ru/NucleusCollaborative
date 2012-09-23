using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comJobW5Event _event)
        {
            
            try
            {
                this._Module._Heat.comJobW5History.Add(_event);
            }
            catch { }
        }

    }
}

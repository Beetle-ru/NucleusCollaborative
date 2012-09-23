using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comJobW7Event _event)
        {
            
            try
            {
                this._Module._Heat.comJobW7History.Add(_event);
            }
            catch { }
        }

    }
}

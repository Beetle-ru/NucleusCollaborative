using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comJobW4Event _event)
        {
            
            try
            {
                this._Module._Heat.comJobW4History.Add(_event);
            }
            catch { }
        }

    }
}

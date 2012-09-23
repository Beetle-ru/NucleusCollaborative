using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ModeLanceEvent _event)
        {
            try
            {
                this._Module._Heat.ModeLanceHistory.Add( _event);
            }
            catch { }

        }
    }
}

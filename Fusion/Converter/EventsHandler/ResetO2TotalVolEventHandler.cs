using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ResetO2TotalVolEvent _event)
        {
            try
            {
                this._Module._Heat.ResetO2TotalVolHistory.Add( _event);
            }
            catch { }

        }
    }
}

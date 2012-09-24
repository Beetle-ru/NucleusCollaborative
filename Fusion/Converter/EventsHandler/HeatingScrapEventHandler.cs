using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(HeatingScrapEvent _event)
        {
            try
            {
                this._Module._Heat.HeatingScrapHistory.Add( _event);
            }
            catch { }
        }
    }
}

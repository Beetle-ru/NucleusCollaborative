using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ScrapEvent _event)
        {
            try
            {
                this._Module._Heat.ScrapHistory.Add( _event);
            }
            catch { }

        }
    }
}

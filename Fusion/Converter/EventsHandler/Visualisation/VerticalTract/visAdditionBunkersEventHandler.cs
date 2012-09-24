using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visAdditionBunkersEvent _event)
        {
            try
            {
                this._Module._Heat.visAdditionBunkersHistory.Add(_event);
            }
            catch { }
        }

    }
}

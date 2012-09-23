using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comOxigenW5SimilatorEvent _event)
        {
            
            try
            {
                this._Module._Heat.comOxigenW5SimilatorHistory.Add(_event);
            }
            catch { }
        }

    }
}

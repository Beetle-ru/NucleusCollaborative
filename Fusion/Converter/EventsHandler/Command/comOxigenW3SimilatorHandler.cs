using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comOxigenW3SimilatorEvent _event)
        {
            
            try
            {
                this._Module._Heat.comOxigenW3SimilatorHistory.Add(_event);
            }
            catch { }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ComName1MatEvent _event)
        {
            
            try
            {
                this._Module._Heat.comName1MatHistory.Add(_event);
            }
            catch { }
        }

    }
}

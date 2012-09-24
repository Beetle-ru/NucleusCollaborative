using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visAlloyingBunker3AEvent _event)
        {
            try
            {
                this._Module._Heat.visAlloyingBunker3AHistory.Add(_event);
            }
            catch { }
        }

    }
}

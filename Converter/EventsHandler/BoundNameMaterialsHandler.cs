using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(BoundNameMaterialsEvent _event)
        {
            
            try
            {
                this._Module._Heat.BoundNameMaterialsHistory.Add(_event);
            }
            catch { }
        }

    }
}

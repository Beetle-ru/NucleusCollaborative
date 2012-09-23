using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(BoilerWaterCoolingEvent _event)
        {
            try
            {
                this._Module._Heat.BoilerWaterCoolingHistory.Add(_event);
            }
            catch {}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(MixerAnalysisEvent _event)
        {
            try 
            {
                this._Module._Heat.MixerAnalysisHistory.Add(_event);
            }
            catch { }

        }
    }
}

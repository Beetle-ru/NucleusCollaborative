﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(OffGasAnalysisEvent _event)
        {
            try
            {
                this._Module._Heat.OffGasAnalysisHistory.Add( _event);
            }
            catch { }

        }
    }
}

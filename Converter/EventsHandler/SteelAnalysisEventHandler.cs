﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(SteelAnalysisEvent _event)
        {
            try
            {
                this._Module._Heat.SteelAnalysisHistory.Add( _event);
            }
            catch { }

        }
    }
}

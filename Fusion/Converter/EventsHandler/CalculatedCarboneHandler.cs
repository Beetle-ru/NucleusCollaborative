﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(CalculatedCarboneEvent _event)
        {
            try
            {
                this._Module._Heat.CalculatedCarboneHistory.Add(_event);
            }
            catch { }

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comOxigenSimilatorEvent _event)
        {
            
            try
            {
                this._Module._Heat.comOxigenSimilatorHistory.Add(_event);
            }
            catch { }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(comOxigenW6SimilatorEvent _event)
        {
            
            try
            {
                this._Module._Heat.comOxigenW6SimilatorHistory.Add(_event);
            }
            catch { }
        }

    }
}

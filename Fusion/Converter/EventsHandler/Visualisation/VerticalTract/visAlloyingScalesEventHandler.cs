﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visAlloyingScalesEvent _event)
        {
            try
            {
                this._Module._Heat.visAlloyingScalesHistory.Add(_event);
            }
            catch { }
        }

    }
}

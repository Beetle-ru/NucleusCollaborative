﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(FixDataMfactorModelEvent _event)
        {
            try
            {
                this._Module._Heat.FixDataMfactorModelHistory.Add(_event);
            }
            catch { }

        }
    }
}

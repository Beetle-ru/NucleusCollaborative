﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visCalcinatingFurnacesEvent _event)
        {
            
            try
            {
                this._Module._Heat.visCalcinatingFurnacesHistory.Add(_event);
            }
            catch { }
        }

    }
}

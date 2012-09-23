using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(ConverterAngleEvent _event)
        {
            try
            {
                this._Module._Heat.ConverterAngleHistory.Add( _event);
            }
            catch { }

            //throw new NotImplementedException();
        }
    }
}

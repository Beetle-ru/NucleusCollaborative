using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(HeatChangeEvent _event)
        {

            //this.Converter1Heat.HeatChangeEvent.
            if (this._Module._Heat.Number != _event.HeatNumber)
            {
                this._Module._Heat = new Heat();
                this._Module._Heat.Number = _event.HeatNumber;
                this._Module._Heat.AggregateNumber = this._Module.ConverterNumber;
                this._Module._Heat.StartDate = _event.Time;
            }
        }
    }
}

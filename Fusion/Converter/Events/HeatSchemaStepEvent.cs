using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Converter
{
    [Serializable]
    public class HeatSchemaStepEvent : ConverterBaseEvent
    {
        [DataMember]
        public int Step { set; get; }            //шаг

        public HeatSchemaStepEvent()
        {
            Step = 0;

        }
    }
}

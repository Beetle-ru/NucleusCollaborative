using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;
using CommonTypes;

namespace ConverterVisio
{
    [Serializable]
    [DataContract]
    class SpeedChangeEvent: BaseEvent
    {
        [DataMember]
        public int Speed { set; get; } 
    }
}

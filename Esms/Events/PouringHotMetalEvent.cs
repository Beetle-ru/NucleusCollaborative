using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Core;

namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "PouringHotMetalEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class PouringHotMetalEvent : EsmsBaseEvent
    {
        //161	Идет заливка
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 0)]
        public bool IsPouring { set; get; }       
    }
}

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
    [DBGroup(UnitNumber = 2)]
    public class ScrapLoadEvent : EsmsBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        public int Id { get; set; }
        [DataMember]
        [DBPoint(IsStored = true)]
        public int TaskNumber { get; set; } // 1 - завалка, 2 - подвалка вроде :)
        [DataMember]
        [DBPoint(IsStored = true)]
        public int TankNumber { get; set; } // номер бадьи
        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 40)]
        public string ScrapName { get; set; }
        [DataMember]
        [DBPoint(IsStored = true)]
        public float Weight { get; set; } // Вес завалки/подвалки в тоннах, с точностью до 2-го знака после запятой;
        [DataMember]
        [DBPoint(IsStored = true)]
        public int ChargeNumber { get; set; } // номер горелки
    }
}

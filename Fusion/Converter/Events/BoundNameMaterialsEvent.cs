using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    // Привязка названий материалов к бункерам от 1 уровня					

    // PLC:	PLC x.2	(x=номер конвертера)	

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Strings")]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class BoundNameMaterialsEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING134,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker5MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING140,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker6MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING146,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker7MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING152,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker8MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING158,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker9MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING164,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker10MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING170,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker11MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB2,STRING176,6", IsWritable = false, Encoding = "x-cp1251")]
        public string Bunker12MaterialName { set; get; }
    }
}
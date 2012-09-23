using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    // 1003	Заданные добавки в главной продувке						

    // PLC:	PLC x.2	(x=номер конвертера)	
    // Событие			
    // После подтверждения предварительного расчета		

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
    public class comAdditionsEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB1,STRING272,6", Encoding = "x-cp1251")]
        [PLCPoint(Location = "DB1,STRING272,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker1MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING278,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker2MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING284,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker3MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING290,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker4MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING296,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker5MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING302,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker6MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING308,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker7MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING314,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker8MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING320,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker9MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING326,6", IsWritable = true, Encoding = "x-cp1251")]
        public string Bunker10MaterialName { set; get; }

    }
}
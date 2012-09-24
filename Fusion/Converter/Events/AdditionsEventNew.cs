using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.2						
    // Von:	PLC x.2	(x=номер конвертера)
    // Данные по добавочным материалам
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class AdditionsEventNew : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL2")]
        public double Weight1 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL6")]
        public double Weight2 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL10")]
        public double Weight3 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL14")]
        public double Weight4 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL18")]
        public double Weight5 { set; get; }


        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL22")]
        public double Weight6 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL26")]
        public double Weight7 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL30")]
        public double Weight8 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL34")]
        public double Weight9 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL38")]
        public double Weight10 { set; get; }

        [DataMember]
        [DBPoint(IsStored = true , MaxSize=15)]
        [PLCPoint(Location = "DB1,STRING272,6", Encoding = "x-cp1251")]
        public string Bunker1MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING278,6", Encoding = "x-cp1251")]
        public string Bunker2MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING284,6", Encoding = "x-cp1251")]
        public string Bunker3MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING290,6", Encoding = "x-cp1251")]
        public string Bunker4MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING296,6", Encoding = "x-cp1251")]
        public string Bunker5MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING302,6", Encoding = "x-cp1251")]
        public string Bunker6MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING308,6", Encoding = "x-cp1251")]
        public string Bunker7MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING314,6", Encoding = "x-cp1251")]
        public string Bunker8MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING320,6", Encoding = "x-cp1251")]
        public string Bunker9MaterialName { set; get; }

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB1,STRING326,6", Encoding = "x-cp1251")]
        public string Bunker10MaterialName { set; get; }

        //[DataMember]
        //[DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB2,STRING170,6", Encoding = "x-cp1251")]
        //public string Bunker11MaterialName { set; get; }

        //[DataMember]
        //[DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB2,STRING176,6", Encoding = "x-cp1251")]
        //public string Bunker12MaterialName { set; get; }

        //[DataMember]
        //[DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB2,STRING182,6", Encoding = "x-cp1251")]
        //public string Bunker13MaterialName { set; get; }

        //[DataMember]
        //[DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB2,STRING188,6", Encoding = "x-cp1251")]
        //public string Bunker14MaterialName { set; get; }

        //[DataMember]
        //[DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB2,STRING194,6", Encoding = "x-cp1251")]
        //public string Bunker15MaterialName { set; get; }

        //[DataMember]
        //[DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB2,STRING200,6", Encoding = "x-cp1251")]
        //public string Bunker16MaterialName { set; get; }

        //[DataMember]
        //[DBPoint(IsStored = true, MaxSize = 15)]
        //[PLCPoint(Location = "DB2,STRING206,6", Encoding = "x-cp1251")]
        //public string Bunker17MaterialName { set; get; }

        
        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes { set; get; }
    }
}

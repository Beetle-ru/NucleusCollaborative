using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;

namespace Converter
{
    // Данные с PLC 0.1	
    // Фактические данные по скрапу
    // Имя группы ACT_TOSCRAP
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Common")]
    [PLCGroup(Location = "PLC01", Destination = "Converter1", FilterPropertyName = "ConverterNumber", FilterPropertyValue = "1")]
    [PLCGroup(Location = "PLC01", Destination = "Converter2", FilterPropertyName = "ConverterNumber", FilterPropertyValue = "2")]
    [PLCGroup(Location = "PLC01", Destination = "Converter3", FilterPropertyName = "ConverterNumber", FilterPropertyValue = "3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class ScrapEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W46")]
        public int BucketNumber { set; get; }                      // № скрапного совка               # ACT_SCRBUCKETID

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W48")]
        public int ConverterNumber { set; get; }                   // № конвертера                    # ACT_SCRKONVNR

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W50")]
        public int ScrapType1 { set; get; }                        // фактический вид скрапа 1        # ACT_SCRMATID1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W52")]
        public int ScrapType2 { set; get; }                        // фактический вид скрапа 2        # ACT_SCRMATID2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W54")]
        public int ScrapType3 { set; get; }                        // фактический вид скрапа 3        # ACT_SCRMATID3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W56")]
        public int ScrapType4 { set; get; }                        // фактический вид скрапа 4        # ACT_SCRMATID4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W58")]
        public int ScrapType5 { set; get; }                        // фактический вид скрапа 5        # ACT_SCRMATID5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W60")]
        public int ScrapType6 { set; get; }                        // фактический вид скрапа 6        # ACT_SCRMATID6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W62")]
        public int ScrapType7 { set; get; }                        // фактический вид скрапа 7        # ACT_SCRMATID7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W64")]
        public int ScrapType8 { set; get; }                        // фактический вид скрапа 8        # ACT_SCRMATID8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D66")]
        public int Weight1 { set; get; }                             // факт.вес-вес1                   # ACT_SCRWGT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D70")]
        public int Weight2 { set; get; }                             // факт.вес-вес2                   # ACT_SCRWGT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D74")]
        public int Weight3 { set; get; }                             // факт.вес-вес3                   # ACT_SCRWGT3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D78")]
        public int Weight4 { set; get; }                             // факт.вес-вес4                   # ACT_SCRWGT4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D82")]
        public int Weight5 { set; get; }                             // факт.вес-вес5                   # ACT_SCRWGT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D86")]
        public int Weight6 { set; get; }                             // факт.вес-вес6                   # ACT_SCRWGT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D90")]
        public int Weight7 { set; get; }                             // факт.вес-вес7                   # ACT_SCRWGT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D94")]
        public int Weight8 { set; get; }                             // факт.вес-вес8                   # ACT_SCRWGT8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D98")]
        public int TotalWeight { set; get; }                         // скрап общий факт.вес            # ACT_SCRWGTTOTAL

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes1 { set; get; }

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes2 { set; get; }

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes3 { set; get; }

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes4 { set; get; }

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes5 { set; get; }

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes6 { set; get; }

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes7 { set; get; }

        [DBPoint(IsStored = false)]
        public Dictionary<string, double> ChemestryAttributes8 { set; get; }
    }
}

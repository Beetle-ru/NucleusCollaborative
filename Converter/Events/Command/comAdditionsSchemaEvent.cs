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
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class comAdditionsSchemaEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL4")]
        public double Material1Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 1       # SP_CX_ADDMAINP1WGT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL16")]
        public double Material2Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 2       # SP_CX_ADDMAINP1WGT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL28")]
        public double Material3Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 3       # SP_CX_ADDMAINP1WGT3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL40")]
        public double Material4Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 4       # SP_CX_ADDMAINP1WGT4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL52")]
        public double Material5Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 5       # SP_CX_ADDMAINP1WGT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL64")]
        public double Material6Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 6       # SP_CX_ADDMAINP1WGT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL76")]
        public double Material7Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 7       # SP_CX_ADDMAINP1WGT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL88")]
        public double Material8Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 8       # SP_CX_ADDMAINP1WGT8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL100")]
        public double Material9Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 9       # SP_CX_ADDMAINP1WGT9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL112")]
        public double Material10Portion1Weight { set; get; }                        // Заданный вес,порция  1 материал 10     # SP_CX_ADDMAINP1WGT10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL8")]
        public double Material1Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 1       # SP_CX_ADDMAINP2WGT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL20")]
        public double Material2Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 2       # SP_CX_ADDMAINP2WGT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL32")]
        public double Material3Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 3       # SP_CX_ADDMAINP2WGT3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL44")]
        public double Material4Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 4       # SP_CX_ADDMAINP2WGT4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL56")]
        public double Material5Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 5       # SP_CX_ADDMAINP2WGT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL68")]
        public double Material6Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 6       # SP_CX_ADDMAINP2WGT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL80")]
        public double Material7Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 7       # SP_CX_ADDMAINP2WGT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL92")]
        public double Material8Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 8       # SP_CX_ADDMAINP2WGT8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL104")]
        public double Material9Portion2Weight { set; get; }                         // Заданный вес,порция  2 материал 9       # SP_CX_ADDMAINP2WGT9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL116")]
        public double Material10Portion2Weight { set; get; }                        // Заданный вес,порция  2 материал 10      # SP_CX_ADDMAINP2WGT10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL12")]
        public double Material1Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 1       # SP_CX_ADDMAINP3WGT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL24")]
        public double Material2Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 2       # SP_CX_ADDMAINP3WGT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL36")]
        public double Material3Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 3       # SP_CX_ADDMAINP3WGT3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL48")]
        public double Material4Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 4       # SP_CX_ADDMAINP3WGT4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL60")]
        public double Material5Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 5       # SP_CX_ADDMAINP3WGT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL72")]
        public double Material6Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 6       # SP_CX_ADDMAINP3WGT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL84")]
        public double Material7Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 7       # SP_CX_ADDMAINP3WGT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL96")]
        public double Material8Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 8       # SP_CX_ADDMAINP3WGT8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL108")]
        public double Material9Portion3Weight { set; get; }                         // Заданный вес,порция  3 материал 9       # SP_CX_ADDMAINP3WGT9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL120")]
        public double Material10Portion3Weight { set; get; }                        // Заданный вес,порция  3 материал 10      # SP_CX_ADDMAINP3WGT10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT124")]
        public int O2VolPortion1Material1 { set; get; }                             // O2 расход при порции  1 материал 1      # SP_CX_ADDSTEPP1MAT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT130")]
        public int O2VolPortion1Material2 { set; get; }                             // O2 расход при порции  1 материал 2      # SP_CX_ADDSTEPP1MAT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT136")]
        public int O2VolPortion1Material3 { set; get; }                             // O2 расход при порции  1 материал 3      # SP_CX_ADDSTEPP1MAT3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT142")]
        public int O2VolPortion1Material4 { set; get; }                             // O2 расход при порции  1 материал 4      # SP_CX_ADDSTEPP1MAT4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT148")]
        public int O2VolPortion1Material5 { set; get; }                             // O2 расход при порции  1 материал 5      # SP_CX_ADDSTEPP1MAT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT154")]
        public int O2VolPortion1Material6 { set; get; }                             // O2 расход при порции  1 материал 6      # SP_CX_ADDSTEPP1MAT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT160")]
        public int O2VolPortion1Material7 { set; get; }                             // O2 расход при порции  1 материал 7      # SP_CX_ADDSTEPP1MAT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT166")]
        public int O2VolPortion1Material8 { set; get; }                             // O2 расход при порции  1 материал 8      # SP_CX_ADDSTEPP1MAT8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT172")]
        public int O2VolPortion1Material9 { set; get; }                             // O2 расход при порции  1 материал 9      # SP_CX_ADDSTEPP1MAT9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT178")]
        public int O2VolPortion1Material10 { set; get; }                            // O2 расход при порции  1 материал 10     # SP_CX_ADDSTEPP1MAT10


        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT126")]
        public int O2VolPortion2Material1 { set; get; }                             // O2 расход при порции  2 материал 1      # SP_CX_ADDSTEPP2MAT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT132")]
        public int O2VolPortion2Material2 { set; get; }                             // O2 расход при порции  2 материал 2      # SP_CX_ADDSTEPP2MAT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT138")]
        public int O2VolPortion2Material3 { set; get; }                             // O2 расход при порции  2 материал 3      # SP_CX_ADDSTEPP2MAT3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT144")]
        public int O2VolPortion2Material4 { set; get; }                             // O2 расход при порции  2 материал 4      # SP_CX_ADDSTEPP2MAT4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT150")]
        public int O2VolPortion2Material5 { set; get; }                             // O2 расход при порции  2 материал 5      # SP_CX_ADDSTEPP2MAT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT156")]
        public int O2VolPortion2Material6 { set; get; }                             // O2 расход при порции  2 материал 6      # SP_CX_ADDSTEPP2MAT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT162")]
        public int O2VolPortion2Material7 { set; get; }                             // O2 расход при порции  2 материал 7      # SP_CX_ADDSTEPP2MAT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT168")]
        public int O2VolPortion2Material8 { set; get; }                             // O2 расход при порции  2 материал 8      # SP_CX_ADDSTEPP2MAT8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT174")]
        public int O2VolPortion2Material9 { set; get; }                             // O2 расход при порции  2 материал 9      # SP_CX_ADDSTEPP2MAT9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT180")]
        public int O2VolPortion2Material10 { set; get; }                            // O2 расход при порции  2 материал 10     # SP_CX_ADDSTEPP2MAT10


        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT128")]
        public int O2VolPortion3Material1 { set; get; }                             // O2 расход при порции  3 материал 1      # SP_CX_ADDSTEPP3MAT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT134")]
        public int O2VolPortion3Material2 { set; get; }                             // O2 расход при порции  3 материал 2      # SP_CX_ADDSTEPP3MAT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT140")]
        public int O2VolPortion3Material3 { set; get; }                             // O2 расход при порции  3 материал 3      # SP_CX_ADDSTEPP3MAT3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT146")]
        public int O2VolPortion3Material4 { set; get; }                             // O2 расход при порции  3 материал 4      # SP_CX_ADDSTEPP3MAT4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT152")]
        public int O2VolPortion3Material5 { set; get; }                             // O2 расход при порции  3 материал 5      # SP_CX_ADDSTEPP3MAT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT158")]
        public int O2VolPortion3Material6 { set; get; }                             // O2 расход при порции  3 материал 6      # SP_CX_ADDSTEPP3MAT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT164")]
        public int O2VolPortion3Material7 { set; get; }                             // O2 расход при порции  3 материал 7      # SP_CX_ADDSTEPP3MAT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT170")]
        public int O2VolPortion3Material8 { set; get; }                             // O2 расход при порции  3 материал 8      # SP_CX_ADDSTEPP3MAT8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT176")]
        public int O2VolPortion3Material9 { set; get; }                             // O2 расход при порции  3 материал 9      # SP_CX_ADDSTEPP3MAT9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT182")]
        public int O2VolPortion3Material10 { set; get; }                            // O2 расход при порции  3 материал 10     # SP_CX_ADDSTEPP3MAT10


        public comAdditionsSchemaEvent()
        {
            Material1Portion1Weight = -1;
            Material2Portion1Weight = -1;
            Material3Portion1Weight = -1;
            Material4Portion1Weight = -1;
            Material5Portion1Weight = -1;
            Material6Portion1Weight = -1;
            Material7Portion1Weight = -1;
            Material8Portion1Weight = -1;
            Material9Portion1Weight = -1;
            Material10Portion1Weight = -1;

            Material1Portion2Weight = -1;
            Material2Portion2Weight = -1;
            Material3Portion2Weight = -1;
            Material4Portion2Weight = -1;
            Material5Portion2Weight = -1;
            Material6Portion2Weight = -1;
            Material7Portion2Weight = -1;
            Material8Portion2Weight = -1;
            Material9Portion2Weight = -1;
            Material10Portion2Weight = -1;

            Material1Portion3Weight = -1;
            Material2Portion3Weight = -1;
            Material3Portion3Weight = -1;
            Material4Portion3Weight = -1;
            Material5Portion3Weight = -1;
            Material6Portion3Weight = -1;
            Material7Portion3Weight = -1;
            Material8Portion3Weight = -1;
            Material9Portion3Weight = -1;
            Material10Portion3Weight = -1;

            O2VolPortion1Material1 = -1;
            O2VolPortion1Material2 = -1;
            O2VolPortion1Material3 = -1;
            O2VolPortion1Material4 = -1;
            O2VolPortion1Material5 = -1;
            O2VolPortion1Material6 = -1;
            O2VolPortion1Material7 = -1;
            O2VolPortion1Material8 = -1;
            O2VolPortion1Material9 = -1;
            O2VolPortion1Material10 = -1;

            O2VolPortion2Material1 = -1;
            O2VolPortion2Material2 = -1;
            O2VolPortion2Material3 = -1;
            O2VolPortion2Material4 = -1;
            O2VolPortion2Material5 = -1;
            O2VolPortion2Material6 = -1;
            O2VolPortion2Material7 = -1;
            O2VolPortion2Material8 = -1;
            O2VolPortion2Material9 = -1;
            O2VolPortion2Material10 = -1;

            O2VolPortion3Material1 = -1;
            O2VolPortion3Material2 = -1;
            O2VolPortion3Material3 = -1;
            O2VolPortion3Material4 = -1;
            O2VolPortion3Material5 = -1;
            O2VolPortion3Material6 = -1;
            O2VolPortion3Material7 = -1;
            O2VolPortion3Material8 = -1;
            O2VolPortion3Material9 = -1;
            O2VolPortion3Material10 = -1;

        }

    }
}
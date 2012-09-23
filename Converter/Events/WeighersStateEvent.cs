using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    // Состояние весов			

    // PLC:	PLC x.2	(x=номер конвертера)	
    // Событие			

    /// <summary>
    /// Состояния весов.
    /// Пустые -   WeigherLoadFree = True,  WeigherUnLoadFree = False, WeigherEmpty = True;
    /// Заняты -   WeigherLoadFree = True,  WeigherUnLoadFree = True,  WeigherEmpty = False;
    /// Загрузка - WeigherLoadFree = False, WeigherUnLoadFree = False, WeigherEmpty = False;
    /// Выгрузка - WeigherLoadFree = False, WeigherUnLoadFree = False, WeigherEmpty = True;
    /// </summary>
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class WeighersStateEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W218", IsWritable = false)]
        public int Weigher3LoadFree { set; get; }                                            // # ACT_CX_WAAGE_LOADFREE3
       
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W220", IsWritable = false)]
        public int Weigher4LoadFree { set; get; }                                            // # ACT_CX_WAAGE_LOADFREE4
     
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W222", IsWritable = false)]
        public int Weigher5LoadFree { set; get; }                                            // # ACT_CX_WAAGE_LOADFREE5
     
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W224", IsWritable = false)]
        public int Weigher6LoadFree { set; get; }                                            // # ACT_CX_WAAGE_LOADFREE6
      
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W226", IsWritable = false)]
        public int Weigher7LoadFree { set; get; }                                            // # ACT_CX_WAAGE_LOADFREE7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W228", IsWritable = false)]
        public int Weigher3UnLoadFree { set; get; }                                            // # ACT_CX_WAAGE_UNLOADFREE3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W230", IsWritable = false)]
        public int Weigher4UnLoadFree { set; get; }                                            // # ACT_CX_WAAGE_UNLOADFREE4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W232", IsWritable = false)]
        public int Weigher5UnLoadFree { set; get; }                                            // # ACT_CX_WAAGE_UNLOADFREE5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W234", IsWritable = false)]
        public int Weigher6UnLoadFree { set; get; }                                            // # ACT_CX_WAAGE_UNLOADFREE6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W236", IsWritable = false)]
        public int Weigher7UnLoadFree { set; get; }                                            // # ACT_CX_WAAGE_UNLOADFREE7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W238", IsWritable = false)]
        public int Weigher3Empty { set; get; }                                            // # ACT_CX_WAAGE_EMPTY3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W240", IsWritable = false)]
        public int Weigher4Empty { set; get; }                                            // # ACT_CX_WAAGE_EMPTY4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W242", IsWritable = false)]
        public int Weigher5Empty { set; get; }                                            // # ACT_CX_WAAGE_EMPTY5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W244", IsWritable = false)]
        public int Weigher6Empty { set; get; }                                            // # ACT_CX_WAAGE_EMPTY6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W246", IsWritable = false)]
        public int Weigher7Empty { set; get; }                                            // # ACT_CX_WAAGE_EMPTY7

    }
}
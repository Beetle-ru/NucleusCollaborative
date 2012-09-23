using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//ѕодача к печи/ковшу
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "SubmissionEvent2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class SubmissionEvent : EsmsBaseEvent
    {
        //75  онвейер 6 ¬4 2009 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 0)]
        public bool Conveyor6 ¬4n2009Work { set; get; }        //IX65.1
        //76 Ўибер и поворотный клапан в положении Ђк печиї
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 1)]
        public bool SchieberPositionFurnace { set; get; }        //IX122.0
        //77 Ўибер и поворотный клапан в положении Ђк ковшуї
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 2)]
        public bool SchieberPositionBucket { set; get; }        //IX122.3
        //80 «агрузка материалов из бункеров 1-17Е4-17 старт
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 3)]
        public bool LoadMaterials1t17n4t17Start { set; get; }        //DB14.DBX4.0
        //81 «агрузка материалов из бункеров 1-16Е10-16 старт
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 4)]
        public bool LoadMaterials1t16n10t16Start { set; get; }        //DB14.DBX4.1
        //Pезерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 5)]
        public bool Reserv1 { set; get; }	
        //Pезерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 6)]
        public bool Reserv2 { set; get; }	
        //Pезерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 7)]
        public bool Reserv3 { set; get; }	
        //Pезерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 0)]
        public bool Reserv4 { set; get; }	
        //Pезерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 1)]
        public bool Reserv5 { set; get; }	
        //Pезерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 2)]
        public bool Reserv6 { set; get; }	
        //Pезерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 3)]
        public bool Reserv7 { set; get; }	
        //Pезерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 4)]
        public bool Reserv8 { set; get; }
        //Pезерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 5)]
        public bool Reserv9 { set; get; }	
        //Pезерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 6)]
        public bool Reserv10 { set; get; }	
        //Pезерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 7)]
        public bool Reserv11 { set; get; }	

        
        //78 «агрузка материалов из бункеров 1-17Е4-17, выбор режима (0 Ц в печь, 1 Ц в ковш)
        [DataMember]
        [PLCPoint(Location = "DB550,INT330")]
        public int LoadMaterials1t17n4t17 { set; get; }        //DB14.DBW0
        //79 «агрузка материалов из бункеров 1-16Е10-16, выбор режима (0 Ц телега, 1 Ц в печь, 2 Ц в ковш, 3 - аварийна€)
        [DataMember]
        [PLCPoint(Location = "DB550,INT332")]
        public int LoadMaterials1t16n10t16 { set; get; }        //DB14.DBW2
        
        
        
        
    }
}  

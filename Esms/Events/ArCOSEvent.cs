using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//ArCOS
namespace Esms
{          
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "ArCOSEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class ArCOSEvent : EsmsBaseEvent
    {
        //34 ��� ���� 1, ArCOS, �� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL232")]
        public float AmperagePhase1 { get; set; }        //DB189.DBD58
        //35 ��� ���� 2, ArCOS, �� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL236")]
        public float AmperagePhase2 { get; set; }        //DB189.DBD62
        //36 ��� ���� 3, ArCOS, �� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL240")]
        public float AmperagePhase3 { get; set; }        //DB189.DBD66
        //37 �������� ���� 1, ArCOS, ��� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL244")]
        public float ImpedancePhase1 { get; set; }        //DB189.DBD130
        //38 �������� ���� 2, ArCOS, ��� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL248")]
        public float ImpedancePhase2 { get; set; }        //DB189.DBD134
        //39 �������� ���� 3, ArCOS, ��� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL252")]
        public float ImpedancePhase3 { get; set; }        //DB189.DBD138
        //40 �������� �������, ArCOS, ��� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL256")]
        public float ActiveEnergy { get; set; }        //DB189.DBD178
        //41 ���������� �������, ArCOS, ���� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL260")]
        public float ReactiveEnergy { get; set; }        //DB189.DBD194
        //42 ����������� �������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL264")]
        public float PowerFactor { get; set; }        //DB189.DBD214
        //43 ������� ������ � ArCOS 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT268")]
        public int CurrentCurve { set; get; }        //DB189.DBW358
        //44 ������ 1 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL270")]
        public float Reserve1 { get; set; } 
        //45 ������ 2 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL274")]
        public float Reserve2 { get; set; } 
        //46 ������ 3 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL278")]
        public float Reserve3 { get; set; } 
        //47 ������ 4 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL282")]
        public float Reserve4 { get; set; }
        //48 ������ 5 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL286")]
        public float Reserve5 { get; set; } 
        //49 ������ 6 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL290")]
        public float Reserve6 { get; set; } 
        //50 ������ 7 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL294")]
        public float Reserve7 { get; set; } 
        //51 ������ 8 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL298")]
        public float Reserve8 { get; set; } 
        //52 ������ 9 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL302")]
        public float Reserve9 { get; set; } 
        //53 ������ 10 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL306")]
        public float Reserve10 { get; set; } 
    }
}    

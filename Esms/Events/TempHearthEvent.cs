using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//����������� ������
namespace Esms
{    
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "TempHearthEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class TempHearthEvent : EsmsBaseEvent
    {
        //26 ����������� ������ 1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL200")]
        public float TempHearth1 { get; set; }        //DB824.DBD440
        //27 ����������� ������ 2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL204")]
        public float TempHearth2 { get; set; }        //DB824.DBD444
        //28 ����������� ������ 3 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL208")]
        public float TempHearth3 { get; set; }        //DB824.DBD448
        //29 ����������� ������ 4 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL212")]
        public float TempHearth4 { get; set; }        //DB824.DBD452
        //30 ����������� ������ 5 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL216")]
        public float TempHearth5 { get; set; }        //DB824.DBD456
        //31 ����������� ������ 6 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL220")]
        public float TempHearth6 { get; set; }        //DB824.DBD460
        //32 ����������� ������ 7 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL224")]
        public float TempHearth7 { get; set; }        //DB824.DBD464
        //33 ����������� ������ 8 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL228")]
        public float TempHearth8 { get; set; }        //DB824.DBD468
    }
}    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

 //������� � �������
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "LevelBunkerEvent2", Location = "PLC5", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class LevelBunkerEvent : EsmsBaseEvent
    {
        //1 ������� � ������� 1-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL0")]
        public float Bunker1t16 { get; set; }        //DB6.DBD0
        //2 ������� � ������� 2-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL4")]
        public float Bunker2t16 { get; set; }        //DB6.DBD4
        //3 ������� � ������� 3-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL8")]
        public float Bunker3t16 { get; set; }        //DB6.DBD8
        //4 ������� � ������� 4-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL12")]
        public float Bunker4t16 { get; set; }        //DB6.DBD12
        //5 ������� � ������� 5-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL16")]
        public float Bunker5t16 { get; set; }        //DB6.DBD16
        //6 ������� � ������� 6-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL20")]
        public float Bunker6t16 { get; set; }        //DB6.DBD20
        //7 ������� � ������� 7-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL24")]
        public float Bunker7t16 { get; set; }        //DB6.DBD24
        //8 ������� � ������� 8-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL28")]
        public float Bunker8t16 { get; set; }        //DB6.DBD28
        //9 ������� � ������� 9-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL32")]
        public float Bunker9t16 { get; set; }        //DB6.DBD32
        //10 ������� � ������� 10-16 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL36")]
        public float Bunker10t16 { get; set; }        //DB6.DBD36
        //11 ������� � ������� 1-17 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL40")]
        public float Bunker1t17 { get; set; }        //DB6.DBD40
        //12 ������� � ������� 2-17 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL44")]
        public float Bunker2t17 { get; set; }        //DB6.DBD44
        //13 ������� � ������� 3-17 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL48")]
        public float Bunker3t17 { get; set; }        //DB6.DBD48
        //14 ������� � ������� 4-17 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL52")]
        public float Bunker4t17 { get; set; }        //DB6.DBD52
    }
 }
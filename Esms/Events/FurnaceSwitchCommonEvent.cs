using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������ ����������� ����� �������
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FurnaceSwitchCommonEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FurnaceSwitchCommonEvent : EsmsBaseEvent
    {
        //129 ������ ����������� ����� � ��������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 0)]
        public bool Ready  { set; get; }        //DB822.DBX768.0
        //130 ������ ����������� ������ 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 1)]
        public bool Failure { set; get; }        //DB822.DBX768.1
        //131 ������ ����������� ������ 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 2)]
        public bool Error  { set; get; }        //DB822.DBX768.2
        //132 ������ ����������� ������� (���� �������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 3)]
        public bool Close  { set; get; }        //DB822.DBX768.3
        //133 ������ ����������� ��������� (���� ���������) 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 4)]
        public bool Open  { set; get; }        //DB822.DBX768.4
        //134 ������ ������ ����������� �1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 5)]
        public bool Switch1 { set; get; }        //DB822.DBX754.2
        //135 ������ ������ ����������� �2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 6)]
        public bool Switch2 { set; get; }        //DB822.DBX761.2
        //136 ������ 1 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 7)]
        public bool Reserve1 { get; set; } 
        //137 ������ 2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 0)]
        public bool Reserve2 { get; set; } 
        //138 ������ 3 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 1)]
        public bool Reserve3 { get; set; } 
        //139 ������ 4 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 2)]
        public bool Reserve4 { get; set; } 
        //140 ������ 5 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 3)]
        public bool Reserve5 { get; set; } 
        //141 ������ 6 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 4)]
        public bool Reserve6 { get; set; } 
        //142 ������ 7 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 5)]
        public bool Reserve7 { get; set; } 
        //143 ������ 8 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 6)]
        public bool Reserve8 { get; set; } 
        //144 ������ 9 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 7)]
        public bool Reserve9 { get; set; } 

    }
}
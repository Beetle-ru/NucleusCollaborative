using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//�������� �����
namespace Esms
{    
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "LanceCrestEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class LanceCrestEvent : EsmsBaseEvent
    {
        //145 ����� ����� ������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 0)]
        public bool Up { get; set; }         //DB822.DBX464.5
        //146 ����� ����� � ������� ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 1)]
        public bool PositionInjection { get; set; }         //DB822.DBX463.6
        //147 ����� ����� �����
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 2)]
        public bool Down { get; set; }         //DB822.DBX464.4
        //148 ������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 3)]
        public bool Reserve1 { get; set; }
        //149 ������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 4)]
        public bool Reserve2 { get; set; }
        //150 ������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 5)]
        public bool Reserve3 { get; set; }
        //151 ������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 6)]
        public bool Reserve4 { get; set; }
        //152 ������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 7)]
        public bool Reserve5 { get; set; }
        //153 ������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 0)]
        public bool Reserve6{ get; set; }
        //154 ������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 1)]
        public bool Reserve7 { get; set; }
        //155 ������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 2)]
        public bool Reserve8 { get; set; }
        //156 ������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 3)]
        public bool Reserve9 { get; set; }
        //157 ������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 4)]
        public bool Reserve10 { get; set; }
        //158 ������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 5)]
        public bool Reserve11 { get; set; }
        //159 ������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 6)]
        public bool Reserve12 { get; set; }
        //160 ������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 7)]
        public bool Reserve13 { get; set; }  
        //78 �������� �����, ���������, ��
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL406")]
        public float Position { get; set; }        //DB824.DBD400
        //79 �������� �����, ������� ������ ���������, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL410")]
        public float CurrentOxygenFlow { get; set; }        //DB824.DBD404
        //80 ����������� ����������� ���� �� ����� �������� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL414")]
        public float TempWaterInput { get; set; }        //DB824.DBD408
        //81 ����������� ����������� ���� �� ������ �������� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL418")]
        public float TempOtputWater { get; set; }        //DB824.DBD412
        //82 �������� �����, ������� ������� ���������, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL422")]
        public float SetpointOxygenFlow { get; set; }        //DB826.DBD868
        //83 �������� �����, ��������� ������ � ������ ������, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL426")]
        public float TotalOxygenFlow { get; set; }        //DB824.DBD364
        //84 ������������ ������ ��������� �����, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT430")]
        public int StatusControlValveOxygen { set; get; }        //DB822.DBW438
        //85 ������ ��������� ������� ��������� �����
        [DataMember]
        [PLCPoint(Location = "DB550,INT432")]
        public int StatusShutOffValveOxygen { set; get; }        //DB822.DBW440
        //86 ������������ ������ ��������� �����, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT434")]
        public int StatusControlVentilOxygen { set; get; }        //DB822.DBW442
        //87 ����� ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT436")]
        public int StatusLanceCrest { set; get; }        //DB822.DBW444
        //88 ����� �������� ��������� ����� �����, �������
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL438")]
        public float InjectionOxygenTime { get; set; } 
        //89 ������ 14  
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL442")]
        public float Reserve14 { get; set; } 
        //90 ������ 15  
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL446")]
        public float Reserve15 { get; set; } 
        //91 ������ 16  
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL450")]
        public float Reserve16 { get; set; } 
    }
}    

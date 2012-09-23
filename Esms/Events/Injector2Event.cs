using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
 
//�������� 2
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Injector2Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Injector2Event : EsmsBaseEvent
    {
        //128 ���� ������ ���� ��������, �������� 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 0)]
        public bool TestGasLeak { set; get; }        //DB902.DBX504.6
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 1)]
        public bool Reserv1 { set; get; }     
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 2)]
        public bool Reserv2 { set; get; }         
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 3)]
        public bool Reserv3 { set; get; }         
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 4)]
        public bool Reserv4 { set; get; }         
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 5)]
        public bool Reserv5 { set; get; }         
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 6)]
        public bool Reserv6 { set; get; }         
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 7)]
        public bool Reserv7 { set; get; }         
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 0)]
        public bool Reserv8 { set; get; }         
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 1)]
        public bool Reserv9 { set; get; }         
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 2)]
        public bool Reserv10 { set; get; }         
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 3)]
        public bool Reserv11 { set; get; }         
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 4)]
        public bool Reserv12 { set; get; }         
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 5)]
        public bool Reserv13 { set; get; }         
        //������ 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 6)]
        public bool Reserv14 { set; get; } 
        //������ 15
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 7)]
        public bool Reserv15 { set; get; }
        //104 ������� ������ ����, �������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL482")]
        public float CurrentGasFlow { get; set; }        //DB904.DBD1328
        //105 ������� ������� �� ������� ���������� ����, �������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL486")]
        public float SetpointGasFlow { get; set; }        //DB904.DBD1248
        //106 ������ ���� � ������ ������, �������� 2, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL490")]
        public float GasFlow { get; set; }        //DB904.DBD1344
        //107 ������� ������ ��������� (����� �������), �������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL494")]
        public float CurrentOxygenFlowBurner { get; set; }        //DB904.DBD1320
        //108 ������� ������� �� ������� ��������� (����� �������), �������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL498")]
        public float SetpointOxygenFlowBurner { get; set; }        //DB904.DBD1240
        //109 ������ ��������� � ������ ������ (����� �������), �������� 2, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL502")]
        public float OxygenFlowBurner { get; set; }        //DB904.DBD1336
        //110 ������� ������ ��������� (����� ���������), �������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL506")]
        public float CurrentOxygenFlowInjector { get; set; }        //DB904.DBD1324
        //111 ������� ������� �� ������� ��������� (����� ���������), �������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL510")]
        public float SetpointOxygenFlowIngector { get; set; }        //DB904.DBD1244
        //112 ������ ��������� � ������ ������ (����� ���������), �������� 2, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL514")]
        public float OxygenFlowIngector { get; set; }        //DB904.DBD1340
        //113 ����������� ���/�������� (����-�)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL518")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD1232
        //114 ��������� ��������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT522")]
        public int Programm { set; get; }        //DB901.DBW42
        //115 ������ ��������� 2
        [DataMember]
        [PLCPoint(Location = "DB550,INT524")]
        public int Status { set; get; }        //DB902.DBW490
        //116 �������� ������ 1 ����, �������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT526")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW478
        //117 �������� ������ 2 ����, �������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT528")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW480
        //118 ��������� ������������� ������� ����, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL530")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD1308
        //119 ������������ ������ ����, �������� 2, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT534")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW624
        //120 ������������ ������ ����, �������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT536")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW488
        //121 ��������� ������������� ������� ��������� �� ������� �������� 2, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL538")]
        public float PositionControlValveOxygenBurning { get; set; }        //DB904.DBD1300
        //122 ������������ ������ ��������� �� �������, �������� 2, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT542")]
        public int StatusControlVentilOxygenBurning { set; get; }        //DB902.DBW634
        //123 ������������ ������ ��������� �� �������, �������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT544")]
        public int StatusControlValveOxygenBurning { set; get; }        //DB902.DBW484
        //124 ��������� ������������� ������� ������ ��������� �� �������-�, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL546")]
        public float PositionControlValveOxygenRefining { get; set; }        //DB904.DBD1304
        //125 ������������ ������ ��������� �� �������������, �������� 2, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT550")]
        public int StatusControlVentilOxygenRefining { set; get; }        //DB902.DBW636
        //126 ������������ ������ ��������� �� �������������, �������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT552")]
        public int StatusControlValveOxygenRefining { set; get; }        //DB902.DBW486
        //127 ��������� ������ ��������� ��������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT554")]
        public int BypassValveOxygen { set; get; }        //DB902.DBW492
        //����� ������ �� ������ � ������ ������� � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL556")]
        public float BurnerWorkTime { set; get; }	
        //����� ������ �� ������ � ������ ����� (���.) � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL560")]
        public float LanceWorkTime1 { set; get; }	
        //����� ������ �� ������ � ������ ����� (���.) � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL564")]
        public float LanceWorkTime2 { set; get; }	
        //������ 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL568")]
        public float Reserv16 { set; get; }  	
        //������ 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL572")]
        public float Reserv17 { set; get; }
        
    }
}    

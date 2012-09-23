using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//�������� 1
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Injector1Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Injector1Event : EsmsBaseEvent
    {
        //93 ���� ������ ���� ��������, �������� 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 0)]
        public bool TestGasLeak { set; get; }        //DB902.DBX472.6
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 1)]
        public bool Reserv1 { set; get; }     
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 2)]
        public bool Reserv2 { set; get; }         
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 3)]
        public bool Reserv3 { set; get; }         
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 4)]
        public bool Reserv4 { set; get; }         
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 5)]
        public bool Reserv5 { set; get; }         
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 6)]
        public bool Reserv6 { set; get; }         
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 7)]
        public bool Reserv7 { set; get; }         
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 0)]
        public bool Reserv8 { set; get; }         
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 1)]
        public bool Reserv9 { set; get; }         
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 2)]
        public bool Reserv10 { set; get; }         
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 3)]
        public bool Reserv11 { set; get; }         
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 4)]
        public bool Reserv12 { set; get; }         
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 5)]
        public bool Reserv13 { set; get; }         
        //������ 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 6)]
        public bool Reserv14 { set; get; } 
        //������ 15
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 7)]
        public bool Reserv15 { set; get; }         
        //79 ������� ������ ����, �������� 1, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL388")]
        public float CurrentGasFlow { get; set; }        //DB904.DBD1188
        //80 ������� ������� �� ������� ���������� ����, �������� 1, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL392")]
        public float SetpointGasFlow { get; set; }        //DB904.DBD1108
        //81 ������ ���� � ������ ������, �������� 1, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL396")]
        public float GasFlow { get; set; }        //DB904.DBD1204
        //82 ������� ������ ��������� (����� �������), �������� 1, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL400")]
        public float CurrentOxygenFlowBurner { get; set; }        //DB904.DBD1180
        //83 ������� ������� �� ������� ��������� (����� �������), �������� 1, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL404")]
        public float SetpointOxygenFlowBurner { get; set; }        //DB904.DBD1100
        //84 ������ ��������� � ������ ������ (����� �������), �������� 1, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL408")]
        public float OxygenFlowBurner { get; set; }        //DB904.DBD1196
        //85 ������� ������ ��������� (����� ���������), �������� 1, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL412")]
        public float CurrentOxygenFlowInjector { get; set; }        //DB904.DBD1184
        //86 ������� ������� �� ������� ��������� (����� ���������), �������� 1, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL416")]
        public float SetpointOxygenFlowIngector { get; set; }        //DB904.DBD1104
        //87 ������ ��������� � ������ ������ (����� ���������), �������� 1, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL420")]
        public float OxygenFlowIngector { get; set; }        //DB904.DBD1200
        //88 ����������� ���/�������� (����-�)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL424")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD1092
        //89 ��������� ��������� 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT428")]
        public int Programm { set; get; }        //DB901.DBW40
        //90 ������ ��������� 1
        [DataMember]
        [PLCPoint(Location = "DB550,INT430")]
        public int Status { set; get; }        //DB902.DBW458
        //91 �������� ������ 1 ����, �������� 1, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT432")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW446
        //92 �������� ������ 2 ����, �������� 1, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT434")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW448
        //94 ��������� ������������� ������� ����, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL436")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD1168
        //95 ������������ ������ ����, �������� 1, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT440")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW622
        //96 ������������ ������ ����, �������� 1, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT442")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW456
        //97 ��������� ������������� ������� ��������� �� ������� �������� 1, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL444")]
        public float PositionControlValveOxygenBurning { get; set; }        //DB904.DBD1160
        //98 ������������ ������ ��������� �� �������, �������� 1, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT448")]
        public int StatusControlVentilOxygenBurning { set; get; }        //DB902.DBW630
        //99 ������������ ������ ��������� �� �������, �������� 1, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT450")]
        public int StatusControlValveOxygenBurning { set; get; }        //DB902.DBW452
        //100 ��������� ������������� ������� ������ ��������� �� �������������, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL452")]
        public float PositionControlValveOxygenRefining { get; set; }        //DB904.DBD1164
        //101 ������������ ������ ��������� �� �������������, �������� 1, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT456")]
        public int StatusControlVentilOxygenRefining { set; get; }        //DB902.DBW632
        //102 ������������ ������ ��������� �� �������������, �������� 1, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT458")]
        public int StatusControlValveOxygenRefining { set; get; }        //DB902.DBW454
        //103 ��������� ������ ��������� ��������� 1, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT460")]
        public int BypassValveOxygen { set; get; }        //DB902.DBW450
        
        //����� ������ �� ������ � ������ ������� � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL462")]
        public float BurnerWorkTime { set; get; }	
        //����� ������ �� ������ � ������ ����� (���.) � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL466")]
        public float LanceWorkTime1 { set; get; }	
        //����� ������ �� ������ � ������ ����� (���.) � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL470")]
        public float LanceWorkTime2 { set; get; }	
        //������ 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL474")]
        public float Reserv16 { set; get; }  	
        //������ 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL478")]
        public float Reserv17 { set; get; }  	

    }
}    

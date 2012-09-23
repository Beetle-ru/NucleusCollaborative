using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
 
//�������� 3
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Injector3Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Injector3Event : EsmsBaseEvent
    {
        //146 ���� ������ ���� ��������, �������� 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 0)]
        public bool TestGasLeak { set; get; }        //DB902.DBX536.6
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 1)]
        public bool Reserv1 { set; get; }     
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 2)]
        public bool Reserv2 { set; get; }         
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 3)]
        public bool Reserv3 { set; get; }         
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 4)]
        public bool Reserv4 { set; get; }         
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 5)]
        public bool Reserv5 { set; get; }         
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 6)]
        public bool Reserv6 { set; get; }         
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 7)]
        public bool Reserv7 { set; get; }         
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 0)]
        public bool Reserv8 { set; get; }         
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 1)]
        public bool Reserv9 { set; get; }         
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 2)]
        public bool Reserv10 { set; get; }         
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 3)]
        public bool Reserv11 { set; get; }         
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 4)]
        public bool Reserv12 { set; get; }         
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 5)]
        public bool Reserv13 { set; get; }         
        //������ 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 6)]
        public bool Reserv14 { set; get; } 
        //������ 15
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 7)]
        public bool Reserv15 { set; get; }
        //129 ������� ������ ����, �������� 3, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL576")]
        public float CurrentGasFlow { get; set; }        //DB904.DBD1468
        //130 ������� ������� �� ������� ���������� ����, �������� 3, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL580")]
        public float SetpointGasFlow { get; set; }        //DB904.DBD1388
        //131 ������ ���� � ������ ������, �������� 3, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL584")]
        public float GasFlow { get; set; }        //DB904.DBD1484
        //132 ������� ������ ��������� (����� �������), �������� 3, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL588")]
        public float CurrentOxygenFlowBurner { get; set; }        //DB904.DBD1460
        //133 ������� ������� �� ������� ��������� (����� �������), �������� 3, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL592")]
        public float SetpointOxygenFlowBurner { get; set; }        //DB904.DBD1380
        //134 ������ ��������� � ������ ������ (����� �������), �������� 3, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL596")]
        public float OxygenFlowBurner { get; set; }        //DB904.DBD1476
        //135 ������� ������ ��������� (����� ���������), �������� 3, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL600")]
        public float CurrentOxygenFlowInjector { get; set; }        //DB904.DBD1464
        //136 ������� ������� �� ������� ��������� (����� ���������), �������� 3, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL604")]
        public float SetpointOxygenFlowIngector { get; set; }        //DB904.DBD1384
        //137 ������ ��������� � ������ ������ (����� ���������), �������� 3, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL608")]
        public float OxygenFlowIngector { get; set; }        //DB904.DBD1480
        //138 ����������� ���/�������� (����-�)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL612")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD1372
        //139 ��������� ��������� 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT616")]
        public int Programm { set; get; }        //DB901.DBW44
        //140 ������ ��������� 3
        [DataMember]
        [PLCPoint(Location = "DB550,INT618")]
        public int Status { set; get; }        //DB902.DBW522
        //141 �������� ������ 1 ����, �������� 3, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT620")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW510
        //142 �������� ������ 2 ����, �������� 3, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT622")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW512
        //143 ��������� ������������� ������� ����, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL624")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD1448
        //144 ������������ ������ ����, �������� 3, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT628")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW626
        //145 ������������ ������ ����, �������� 3, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT630")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW520
        //147 ��������� ������������� ������� ��������� �� ������� �������� 3, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL632")]
        public float PositionControlValveOxygenBurning { get; set; }        //DB904.DBD1440
        //148 ������������ ������ ��������� �� �������, �������� 3, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT636")]
        public int StatusControlVentilOxygenBurning { set; get; }        //DB902.DBW638
        //149 ������������ ������ ��������� �� �������, �������� 3, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT638")]
        public int StatusControlValveOxygenBurning { set; get; }        //DB902.DBW516
        //150 ��������� ������������� ������� ������ ��������� �� ���������-�, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL640")]
        public float PositionControlValveOxygenRefining { get; set; }        //DB904.DBD1444
        //151 ������������ ������ ��������� �� �������������, �������� 3, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT644")]
        public int StatusControlVentilOxygenRefining { set; get; }        //DB902.DBW640
        //152 ������������ ������ ��������� �� �������������, �������� 3, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT646")]
        public int StatusControlValveOxygenRefining { set; get; }        //DB902.DBW518
        //153 ��������� ������ ��������� ��������� 3, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT648")]
        public int BypassValveOxygen { set; get; }        //DB902.DBW514
        //����� ������ �� ������ � ������ ������� � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL650")]
        public float BurnerWorkTime { set; get; }	
        //����� ������ �� ������ � ������ ����� (���.) � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL654")]
        public float LanceWorkTime1 { set; get; }	
        //����� ������ �� ������ � ������ ����� (���.) � ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL658")]
        public float LanceWorkTime2 { set; get; }	
        //������ 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL662")]
        public float Reserv16 { set; get; }  	
        //������ 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL666")]
        public float Reserv17 { set; get; }
    }
}    

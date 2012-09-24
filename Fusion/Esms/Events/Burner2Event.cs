using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//�������2
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Burner2Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Burner2Event : EsmsBaseEvent
    {
        //29 �������� ������� ������� �� �������� ����� ������� 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 0)]
        public bool PressureAirBlowing { set; get; }        //DB902.DBX310.0
        //32 ���� ������ ���� ��������, ������� 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 1)]
        public bool TestGasLeak { set; get; }        //DB902.DBX310.6
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }     
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }         
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }         
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }         
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }         
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }         
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 0)]
        public bool Reserv7 { set; get; }         
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 1)]
        public bool Reserv8 { set; get; }         
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 2)]
        public bool Reserv9 { set; get; }         
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 3)]
        public bool Reserv10 { set; get; }         
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 4)]
        public bool Reserv11 { set; get; }         
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 5)]
        public bool Reserv12 { set; get; }         
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 6)]
        public bool Reserv13 { set; get; }         
        //������ 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 7)]
        public bool Reserv14 { set; get; }         
        //20 ������� ������ ���������� ����, ������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL172")]
        public float CurrentNaturalGasFlow { get; set; }        //DB904.DBD444
        //21 ������� ������� �� ������� ���������� ����, ������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL176")]
        public float SetpointNaturalGasFlow { get; set; }        //DB904.DBD364
        //22 ������ ���������� ���� � ������ ������, ������� 2, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL180")]
        public float NaturalGasFlow { get; set; }        //DB904.DBD464
        //23 ������� ������ ���������, ������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL184")]
        public float CurrentOxygenFlow { get; set; }        //DB904.DBD440
        //24 ������� ������� �� ������� ���������, ������� 2, �3/�
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL188")]
        public float SetpointOxygenFlow { get; set; }        //DB904.DBD360
        //25 ������ ��������� � ������ ������, ������� 2, �3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL192")]
        public float OxygenFlow  { get; set; }        //DB904.DBD460
        //26 ����������� ���/�������� (����-�)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL196")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD352
        //27 ��������� ������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT200")]
        public int Program { set; get; }        //DB901.DBW22
        //28 ������ ������� 2
        [DataMember]
        [PLCPoint(Location = "DB550,INT202")]
        public int Status { set; get; }        //DB902.DBW296
        //30 �������� ������ 1 ����, ������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT204")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW284
        //31 �������� ������ 2 ����, ������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT206")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW286
        //33 ��������� ������������� ������� ������ ����, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL208")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD424
        //34 ������������ ������ ����, ������� 2, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT212")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW608
        //35 ������������ ������ ����, ������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT214")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW292
        //36 ��������� ������������� ������� ������ ���������, ������� 2, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL216")]
        public float PositionControlValveOxygen { get; set; }        //DB904.DBD420
        //37 ������������ ������ ���������, ������� 2, ������ ������������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,INT220")]
        public int StatusControlVentilOxygen { set; get; }        //DB902.DBW616
        //38 ������������ ������ ���������, ������� 2, ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT222")]
        public int StatusControlValveOxygen { set; get; }        //DB902.DBW290
        //����� ������ ������� �� ������ � �������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL224")]
        public float BurnerWorkTime { get; set; }
        //������ 15
        [DataMember]
        [PLCPoint(Location = "DB550,REAL228")]
        public float Reserv15 { get; set; }	
        //������ 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL232")]
        public float Reserv16 { get; set; }	
        //������ 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL236")]
        public float Reserv17 { get; set; }	
        //������ 18
        [DataMember]
        [PLCPoint(Location = "DB550,REAL240")]
        public float Reserv18 { get; set; }	
    }
}    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������� 1
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Burner1Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Burner1Event : EsmsBaseEvent
    {
        //10 �������� ������� ������� �� �������� ����� ������� 1 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool PressureAirBlowing { set; get; }         //DB902.DBX278.0
        //13 ���� ������ ���� ��������, ������� 1 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool TestGasLeak { set; get; }         //DB902.DBX278.6
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }     
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }         
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }         
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }         
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }         
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }         
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool Reserv7 { set; get; }         
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool Reserv8 { set; get; }         
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool Reserv9 { set; get; }         
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool Reserv10 { set; get; }         
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Reserv11 { set; get; }         
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool Reserv12 { set; get; }         
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool Reserv13 { set; get; }         
        //������ 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool Reserv14 { set; get; }         
        //1 ������� ������ ���������� ����, ������� 1, �3/� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL100")]
        public float CurrentNaturalGasFlow { get; set; }        //DB904.DBD304
        //2 ������� ������� �� ������� ���������� ����, ������� 1, �3/� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL104")]
        public float SetpointNaturalGasFlow { get; set; }        //DB904.DBD224
        //3 ������ ���������� ���� � ������ ������, ������� 1, �3 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL108")]
        public float NaturalGasFlow { get; set; }        //DB904.DBD324
        //4 ������� ������ ���������, ������� 1, �3/� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL112")]
        public float CurrentOxygenFlow { get; set; }        //DB904.DBD300
        //5 ������� ������� �� ������� ���������, ������� 1, �3/� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL116")]
        public float SetpointOxygenFlow { get; set; }        //DB904.DBD220
        //6 ������ ��������� � ������ ������, ������� 1, �3 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL120")]
        public float OxygenFlow { get; set; }        //DB904.DBD320
        //7 ����������� ���/�������� (����-�) 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL124")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD212
        //8 ��������� ������� 1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT128")]
        public int Program { set; get; }        //DB901.DBW20
        //9 ������ ������� 1 
        [DataMember]
        [PLCPoint(Location = "DB550,INT130")]
        public int Status { set; get; }        //DB902.DBW264
        //11 �������� ������ 1 ����, ������� 1, ������ 
        [DataMember]
        [PLCPoint(Location = "DB550,INT132")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW252
        //12 �������� ������ 2 ����, ������� 1, ������ 
        [DataMember]
        [PLCPoint(Location = "DB550,INT134")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW254
        //14 ��������� ������������� ������� ������ ����, % 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL136")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD284
        //15 ������������ ������ ����, ������� 1, ������ ������������ ������� 
        [DataMember]
        [PLCPoint(Location = "DB550,INT140")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW606
        //16 ������������ ������ ����, ������� 1, ������ 
        [DataMember]
        [PLCPoint(Location = "DB550,INT142")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW260
        //17 ��������� ������������� ������� ������ ���������, ������� 1, % 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL144")]
        public float PositionControlValveOxygen { get; set; }        //DB904.DBD280
        //18 ������������ ������ ���������, ������� 1, ������ ������������ ������� 
        [DataMember]
        [PLCPoint(Location = "DB550,INT148")]
        public int StatusControlVentilOxygen { set; get; }        //DB902.DBW614
        //19 ������������ ������ ���������, ������� 1, ������ 
        [DataMember]
        [PLCPoint(Location = "DB550,INT150")]
        public int StatusControlValveOxygen { set; get; }        //DB902.DBW258
        //����� ������ ������� �� ������ � �������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL152")]
        public float BurnerWorkTime { get; set; }
        //������ 15
        [DataMember]
        [PLCPoint(Location = "DB550,REAL156")]
        public float Reserv15 { get; set; }	
        //������ 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL160")]
        public float Reserv16 { get; set; }	
        //������ 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL164")]
        public float Reserv17 { get; set; }	
        //������ 18
        [DataMember]
        [PLCPoint(Location = "DB550,REAL168")]
        public float Reserv18 { get; set; }	

    }
}    

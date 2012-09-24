using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������������ ���� ������, �.�. ��� ���������� ������������ ������������.
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "CoalInjectionEvent2", Location = "PLC4", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class CoalInjectionEvent : EsmsBaseEvent
    {
        //1 ������ Y9 ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool ValveY9Open { set; get; }        //DB183.DBX8.5
        //2 ������ Y8 ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool ValveY8Open { set; get; }        //DB183.DBX8.4
        //5 ������ Y5.2 ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool ValveY5p2Open{ set; get; }        //DB183.DBX7.5
        //6 ������ Y5.1 ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool ValveY5p1Open { set; get; }        //DB183.DBX7.4
        //7 ���������������� ������ (Y5, �������� � ������) �������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool ValveY5IsOn { set; get; }        //DB183.DBX5.4
        //8 ��������� 1 ������� (������ ����� �� �������������� �������)
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool Engine1IsOn { set; get; }        //DB183.DBX8.0
        //9 ������� � ������������� ������� ����-�
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 6)]
        public float LevelBunkerMax { get; set; }        //DB183.DBX7.1
        //10 ������� � ������������� ������� �������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool LevelBunkerAvg { set; get; }        //DB183.DBX7.3
        //11 ������� � ������������� ������� ���-�
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool LevelBunkerMin { set; get; }        //DB183.DBX7.2
        //12 ���������� ������ �������������� ������� (Y2) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool ValveY2Open { set; get; }        //DB183.DBX4.1
        //13 ���������� ������ �������������� ������� (Y2) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool ValveY2Close { set; get; }        //DB183.DBX4.2
        //14 ���������� ������ �������������� ������� (Y2) ������ ���������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool ValveY2BeepOpen { set; get; }        //DB184.DBX18.0
        //15 ��������� 2 ������� (������ ����� �� �������������� �������)
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Engine2IsOn { set; get; }        //DB183.DBX8.2
        //16 ���������� ������ UNIDOS (Y3) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool ValveY3UNIDOSOpen { set; get; }        //DB183.DBX4.3
        //17 ���������� ������ UNIDOS (Y3) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool ValveY3UNIDOSClose { set; get; }        //DB183.DBX4.4
        //18 ���������� ������ UNIDOS (Y3)������ ���������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool ValveY3UNIDOSBeepOpen { set; get; }        //DB184.DBX18.2
        //19 ������ ������������ (Y6) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 0)]
        public bool ValveY6Open { set; get; }        //DB183.DBX5.5
        //20 �������� � ������ ������ 0
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 1)]
        public bool PressureChamberLargeZero { set; get; }        //DB183.DBX5.6
        //21 ������� � UNIDOS ������������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 2)]
        public bool LevelUNIDOSMax { set; get; }        //DB183.DBX6.2
        //24 ������ ������� �������������� ������� (Y1) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 3)]
        public bool ValveY1Open { set; get; }        //DB183.DBX7.0
        //25 ������ ������� UNIDOS (Y1) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 4)]
        public bool ValveY1UNIDOSOpen { set; get; }        //DB183.DBX5.7
        //26 ������ �������� ������ ����� 2 (Y21) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 5)]
        public bool ValveY21Open { set; get; }        //DB183.DBX6.1
        //27 ������ �������� ����� 2 ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 6)]
        public bool ValveBlowingLine2Open { set; get; }        //DB183.DBX7.7
        //28 ������ �������� ������ ����� 1 (Y11) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 7)]
        public bool ValveY11Open { set; get; }        //DB183.DBX6.0
        //29 ������ �������� ����� 1 ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 0)]
        public bool ValveBlowingLine1Open { set; get; }        //DB183.DBX7.6
        //30 ������ ������ ��������� �� UNIDOS, ����� 2 (Y22) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 1)]
        public bool ValveY22UNIDOSOpen { set; get; }        //DB183.DBX5.2
        //31 ������ ������ ��������� �� UNIDOS, ����� 2 (Y22) ������
        [DataMember]
         [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 2)]
        public bool ValveY22UNIDOSClose { set; get; }        //DB183.DBX5.3
        //32 ������ ������ ��������� �� UNIDOS, ����� 2 (Y22) ������� ���������
        [DataMember]
         [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 3)]
        public bool ValveY22UNIDOSBeepOpen { set; get; }        //DB184.DBX18.6
        //33 ������ ������ ��������� �� UNIDOS, ����� 2 (Y12) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 4)]
        public bool ValveY12UNIDOSOpen{ set; get; }        //DB183.DBX4.7
        //34 ������ ������ ��������� �� UNIDOS, ����� 2 (Y12) ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 5)]
        public bool ValveY12UNIDOSClose  { set; get; }        //DB183.DBX5.0
        //35 ������ ������ ��������� �� UNIDOS, ����� 2 (Y12) ������� ���������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 6)]
        public bool ValveY12UNIDOSBeepOpen { set; get; }        //DB184.DBX18.4
        //38 �������������� ���������� ����������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 7)]
        public bool AutoControl { set; get; }        //DB183.DBX6.5
        //39 ������ ���������� ����������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 0)]
        public bool ManualControl { set; get; }        //DB183.DBX6.4
        //40 ���������� ��������� � ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 1)]
        public bool ReadyToGo { set; get; }        //DB183.DBX6.6
        //41 ��������� � ��������� ������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 2)]
        public bool TotalError { set; get; }        //DB183.DBX6.7
        //45 ������� ����� 1
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 3)]
        public bool SelectLine1 { set; get; }        //DB183.DBX74.4
        //46 ������� ����� 2
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 4)]
        public bool SelectLine2 { set; get; }        //DB183.DBX74.5
        //47 ���������� ��������
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 5)]
        public bool FillingWorks { set; get; }        //DB183.DBX9.0
        //48 �������� �����
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 6)]
        public bool BlowingStart { set; get; }        //DB183.DBX74.0
        //49 �������� ����
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 7)]
        public bool BlowingStop { set; get; }        //DB183.DBX74.1
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 0)]
        public bool Reserv1 { set; get; }
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 1)]
        public bool Reserv2 { set; get; }
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 2)]
        public bool Reserv3 { set; get; }
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 3)]
        public bool Reserv4{ set; get; }
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 4)]
        public bool Reserv5{ set; get; }
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 5)]
        public bool Reserv6{ set; get; }
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 6)]
        public bool Reserv7{ set; get; }
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 7)]
        public bool Reserv8{ set; get; }
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 0)]
        public bool Reserv9 { set; get; }
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 1)]
        public bool Reserv10 { set; get; }
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 2)]
        public bool Reserv11 { set; get; }
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 3)]
        public bool Reserv12 { set; get; }
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 4)]
        public bool Reserv13 { set; get; }
        //������ 14
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 5)]
        public bool Reserv14 { set; get; }
        //������ 15
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 6)]
        public bool Reserv15 { set; get; }
        //������ 16
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 7)]
        public bool Reserv16 { set; get; }
        //������ 17
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 0)]
        public bool Reserv17 { set; get; }
        //������ 18
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 1)]
        public bool Reserv18 { set; get; }
        //������ 19
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 2)]
        public bool Reserv19 { set; get; }
        //������ 20
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 3)]
        public bool Reserv20 { set; get; }
        //������ 21
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 4)]
        public bool Reserv21 { set; get; }
        //������ 22
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 5)]
        public bool Reserv22 { set; get; }
        //������ 23
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 6)]
        public bool Reserv23 { set; get; }
        //������ 24
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 7)]
        public bool Reserv24 { set; get; }
        //������ 25
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 0)]
        public bool Reserv25 { set; get; }
        //������ 26
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 1)]
        public bool Reserv26 { set; get; }
        //������ 27
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 2)]
        public bool Reserv27 { set; get; }
        //������ 28
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 3)]
        public bool Reserv28 { set; get; }
        //������ 29
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 4)]
        public bool Reserv29 { set; get; }
        //������ 30
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 5)]
        public bool Reserv30 { set; get; }
        //������ 31
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 6)]
        public bool Reserv31 { set; get; }
        //������ 32
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 7)]
        public bool Reserv32 { set; get; }
        //������ 33
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 0)]
        public bool Reserv33 { set; get; }
        //������ 34
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 1)]
        public bool Reserv34 { set; get; }
        //������ 35
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 2)]
        public bool Reserv35 { set; get; }
        //������ 36
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 3)]
        public bool Reserv36 { set; get; }
        //������ 37
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 4)]
        public bool Reserv37 { set; get; }
        //������ 38
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 5)]
        public bool Reserv38 { set; get; }
        //������ 39
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 6)]
        public bool Reserv39 { set; get; }
        //������ 40
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 7)]
        public bool Reserv40 { set; get; }
        //3 ������� �������� � ������
        [DataMember]
        [PLCPoint(Location = "DB80,REAL10")]
        public float CurrentPressureChamber { get; set; }        //DB183.DBD10
        //4 ������� �������� � ������
        [DataMember]
        [PLCPoint(Location = "DB80,REAL14")]
        public float SetpiontPressureChamber { get; set; }        //DB183.DBD14
        //22 ��� ��������� � UNIDOS ������
        [DataMember]
        [PLCPoint(Location = "DB80,REAL18")]
        public float WeightUNIDOSBrutto { get; set; }        //DB183.DBD18
        //23 ��� �����, �������� � 00:00
        [DataMember]
        [PLCPoint(Location = "DB80,REAL22")]
        public float WeightNetto { get; set; }        //DB183.DBD22
         //36 �������� � ����� 2
        [DataMember]
        [PLCPoint(Location = "DB80,REAL26")]
        public float PressureLine2 { get; set; }        //DB183.DBD46
        //37 �������� � ����� 1
        [DataMember]
        [PLCPoint(Location = "DB80,REAL30")]
        public float PressureLine1 { get; set; }        //DB183.DBD42
        //42 ������ ����� �� ������, ��
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL34")]
        public float CokeFlow { get; set; }        //DB183.DBD26
        //43 ������������� �������� �����, ��/���
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL38")]
        public float CokeBlowing { get; set; }        //DB183.DBD30
        //44 ������� ������� �����, ��/���
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL42")]
        public float SetpiontCokeFlow { get; set; }        //DB183.DBD34
        //����������������� �������� ����� �� ������, �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL46")]
        public float CokeBlowingTime { get; set; }	
    }
}  

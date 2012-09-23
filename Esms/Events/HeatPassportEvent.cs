using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������� ������
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "HeatPassportEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class HeatPassportEvent : EsmsBaseEvent
    {
        //124 ����� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL582")]
        public float HeatNumber { get; set; }        //DB710.DBD0
        //125 ��� ������� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL586")]
        public float IronWeight { get; set; }        //DB710.DBD4
        //126 ����� ��������� ���������� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,DT590")]
        public DateTime PrecedingHeatEndTime { set; get; }        //P#DB710.DBX8.0 byte 8
        //127 ����� ������ ������ ��������������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,DT598")]
        public DateTime ElectricityStartTime { set; get; }        //P#DB710.DBX16.0 byte 8
        //128 ����� ��������� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,DT606")]
        public DateTime HeatEndTime { set; get; }        //P#DB710.DBX24.0 byte 8
        //129 ����� ������, ���
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL614")]
        public float HeatTime { get; set; }        //DB710.DBD32
        //130 ����� �������� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,DT618")]
        public DateTime CloseFingersTime { set; get; }        //P#DB710.DBX36.0 byte 8
        //131 �������������� ��� �������� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL626")]
        public float CloseFingersElectricity { get; set; }        //DB710.DBD44
        //132 ��� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL630")]
        public float ReleaseWeight { get; set; }        //DB710.DBD48
        //133 ����� ��� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL634")]
        public float UnderCurrentTime { get; set; }        //DB710.DBD52
        //134 ����� ��� ����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL638")]
        public float WithoutCurrentTime { get; set; }        //DB710.DBD56
        //135 ������ �������������� �� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL642")]
        public float HeatElectricityFlow  { get; set; }        //DB710.DBD60
        //136 ������ �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL646")]
        public float CokeFlow { get; set; }        //DB710.DBD64
        //137 ����������� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL650")]
        public float ReleaseTemp { get; set; }        //DB710.DBD68
        //138 ������������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL654")]
        public float Oxidation { get; set; }        //DB710.DBD72
        //139 ������ ���� �� ������� 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL658")]
        public float Burner1GasFlow { get; set; }        //DB710.DBD76
        //140 ������ ��������� �� ������� 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL662")]
        public float Burner1OxygenFlow { get; set; }        //DB710.DBD80
        //141 ������ ���� �� ������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL666")]
        public float Burner2GasFlow { get; set; }        //DB710.DBD84
        //142 ������ ��������� �� ������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL670")]
        public float Burner2OxygenFlow { get; set; }        //DB710.DBD88
        //143 ������ ���� �� ������� 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL674")]
        public float Burner3GasFlow { get; set; }        //DB710.DBD92
        //144 ������ ��������� �� ������� 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL678")]
        public float Burner3OxygenFlow { get; set; }        //DB710.DBD96
        //145 ������ ���� �� ������� 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL682")]
        public float Burner4GasFlow { get; set; }        //DB710.DBD100
        //146 ������ ��������� �� ������� 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL686")]
        public float Burner4OxygenFlow { get; set; }        //DB710.DBD104
        //147 ������ ���� �� ������� �� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL690")]
        public float BurnersTotalGasFlow { get; set; }        //DB710.DBD108
        //148 ������ ��������� �� ������� �� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL694")]
        public float BurnersTotalOxygenFlow { get; set; }        //DB710.DBD112
        //149 ������ ���� �������� 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL698")]
        public float Injector1GasFlow { get; set; }        //DB710.DBD116
        //150 ������ ��������� �� ������� �������� 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL702")]
        public float Injector1OxygenFlowBurning { get; set; }        //DB710.DBD120
        //151 ������ ��������� �� ����� �������� 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL706")]
        public float Injector1OxygenFlowBlowing { get; set; }        //DB710.DBD124
        //152 ������ ��������� ��������� �������� 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL710")]
        public float Injector1OxygenFlowTotal { get; set; }        //DB710.DBD128
        //153 ������ ���� �������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL714")]
        public float Injector2GasFlow { get; set; }        //DB710.DBD132
        //154 ������ ��������� �� ������� �������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL718")]
        public float Injector2OxygenFlowBurning { get; set; }        //DB710.DBD136
        //155 ������ ��������� �� ����� �������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL722")]
        public float Injector2OxygenFlowBlowing { get; set; }        //DB710.DBD140
        //156 ������ ��������� ��������� �������� 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL726")]
        public float Injector2OxygenFlowTotal { get; set; }        //DB710.DBD144
        //157 ������ ���� �������� 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL730")]
        public float Injector3GasFlow { get; set; }        //DB710.DBD148
        //158 ������ ��������� �� ������� �������� 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL734")]
        public float Injector3OxygenFlowBurning { get; set; }        //DB710.DBD152
        //159 ������ ��������� �� ����� �������� 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL738")]
        public float Injector3OxygenFlowBlowing { get; set; }        //DB710.DBD156
        //160 ������ ��������� ��������� �������� 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL742")]
        public float Injector3OxygenFlowTotal { get; set; }        //DB710.DBD160
        //161 ������ ���� �������� 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL746")]
        public float Injector4GasFlow { get; set; }        //DB710.DBD164
        //162 ������ ��������� �� ������� �������� 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL750")]
        public float Injector4OxygenFlowBurning { get; set; }        //DB710.DBD168
        //163 ������ ��������� �� ����� �������� 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL754")]
        public float Injector4OxygenFlowBlowing { get; set; }        //DB710.DBD172
        //164 ������ ��������� ��������� �������� 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL758")]
        public float Injector4OxygenFlowTotal { get; set; }        //DB710.DBD176
        //165 ������ ��������� �� ����� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL762")]
        public float LanceCrestOxygenFlow { get; set; }        //DB710.DBD180
        //166 ������ ���� �� ������� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL766")]
        public float BurnerAfterburnerGasFlow { get; set; }        //DB710.DBD184
        //167 ����� ������ ����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL770")]
        public float TotalGasFlow { get; set; }        //DB710.DBD188
        //168 ����� ������ ���������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL774")]
        public float TotalOxygenFlow { get; set; }        //DB710.DBD192
        //169 ����� ������ �� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT778")]
        public int HeatNumberCrest { set; get; }        //DB710.DBW196
        //170 ����� ������ �� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT780")]
        public int HeatNumberWall { set; get; }        //DB710.DBW198
        //171 ����� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT782")]
        public int TeamsNumber { set; get; }        //DB710.DBW200
        //172 ����� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING784,18", Encoding = "x-cp1251")]
        public string GradeSteel { set; get; }        //DB710.S202,16
        //173 ������������� ����� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT802")]
        public int GradeSteelId { set; get; }        //DB710.DBW220
        //174 ����� ����������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT804")]
        public int LadleNumber { set; get; }        //DB710.DBW222
        //175 ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT806")]
        public int Master { set; get; }        //DB710.DBW224
        //176 ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT808")]
        public int SteelMaker { set; get; }        //DB710.DBW226
        //177 ���������� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING810,12", Encoding = "x-cp1251")]
        public string StripChart { set; get; }        //DB710.S228,10
        //178 ����� ������ �� 2-� ����������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT822")]
        public int TwoElectrodesOperationTime { set; get; }        //DB710.DBW240
        //179 �������������� ��� �������� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT824")]
        public int ElectricityOpeningTub { set; get; }        //DB710.DBW242
        //180 ����������� ����������� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT826")]
        public int RatioFoamingAverage  { set; get; }        //DB710.DBW244
        //181 ������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,REAL828")]
        public float Reserve1 { get; set; } 
        //182 ������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,REAL832")]
        public float Reserve2 { get; set; }
        //183 ������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,REAL836")]
        public float Reserve3 { get; set; }
        //184 ������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,REAL840")]
        public float Reserve4 { get; set; }
    }
}    

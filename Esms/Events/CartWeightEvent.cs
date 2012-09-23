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
    [PLCGroup(Name = "CartWeightEvent2", Location = "PLC3", Destination = "ESMS2")]
    public class CartWeightEvent : EsmsBaseEvent
    {
        //70 ������������� 2170 ������� ������ ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 0)]
        public bool Vibrating2170Work { set; get; }        //�X3.1
        //71 ������� ������ ���� ����� (� ����)
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 1)]
        public bool GoingForward { set; get; }        //IX116.1
        //72 ������� ������ ���� ����� (�� ����)
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 2)]
        public bool TravelsBack { set; get; }        //IX116.2
        //73 ������� ������ � ������� ������ ���������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 3)]
        public bool FarRight { set; get; }        //DB25.DBX17.1
        //74 ������� ������ � ������� ����� ���������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 4)]
        public bool FarLeft { set; get; }        //DB25.DBX17.0
        //��� � ������� ������ ����� 3900 ��
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 5)]
        public bool Weight3900 { set; get; }        //��3.0
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 6)]
        public bool Reserv1 { set; get; }
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 7)]
        public bool Reserv2 { set; get; }
        //68 ��� � ������� ������
        [DataMember]
        [PLCPoint(Location = "DB550,REAL324")]
        public float Weight { get; set; }        //DB29.DBD60
        //69 ��������� ������� ������
        [DataMember]
        [PLCPoint(Location = "DB550,INT328")]
        public int Position { set; get; }        //DB15.DBW0
    }
}  

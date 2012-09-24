using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������ � ����/�����
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "AlarmEvent2", Location = "PLC3", Destination = "ESMS2")]
    public class AlarmEvent : EsmsBaseEvent
    {
        //������ ������������� 6�4 (������ 2201) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 0)]
        public bool Drive2201 { set; get; }        //DB9.DBX2.0
        //������ ������������� 6�4 (������ 2202) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 1)]
        public bool Drive2202 { set; get; }        //DB9.DBX2.1
        //������ ������������� 6�4 (������ 2203) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 2)]
        public bool Drive2203 { set; get; }        //DB9.DBX2.2
        //������ ������������� 6�4 (������ 2204) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 3)]
        public bool Drive2204 { set; get; }        //DB9.DBX2.3
        //������ ������������� 6�4 (������ 2205) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 4)]
        public bool Drive2205 { set; get; }        //DB9.DBX2.4
        //������ ������������� 6�4 (������ 2206) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 5)]
        public bool Drive2206 { set; get; }        //DB9.DBX2.5
        //������ ������������� 6�4 (������ 2207) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 6)]
        public bool Drive2207 { set; get; }        //DB9.DBX2.6
        //������ ������������� 6�4 (������ 2208) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 7)]
        public bool Drive2208 { set; get; }        //DB9.DBX2.7
        //������ ������������� 6�4 (������ 2209) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 0)]
        public bool Drive2209 { set; get; }        //DB9.DBX3.0
        //������ ������������� 6�4 (������ 2210) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 1)]
        public bool Drive2210 { set; get; }        //DB9.DBX3.1
        //������ �������� 6�13 (������ 2141) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 2)]
        public bool Drive2141 { set; get; }        //DB9.DBX3.2
        //������ �������� 6�13 (������ 2142) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 3)]
        public bool Drive2142 { set; get; }        //DB9.DBX3.3
        //������ �������� 6�13 (������ 2143) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 4)]
        public bool Drive2143 { set; get; }        //DB9.DBX3.4
        //������ �������� 6�13 (������ 2144) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 5)]
        public bool Drive2144 { set; get; }        //DB9.DBX3.5
        //������ �������� 6�13 (������ 2145) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 6)]
        public bool Drive2145 { set; get; }        //DB9.DBX3.6
        //������ �������� 6�13 (������ 2146) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 7)]
        public bool Drive2146 { set; get; }        //DB9.DBX3.7
        //������ �������� 6�14 (������ 2147) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 0)]
        public bool Drive2147 { set; get; }        //DB9.DBX4.0
        //������ �������� 6�14 (������ 2148) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 1)]
        public bool Drive2148 { set; get; }        //DB9.DBX4.1
        //������ �������� 6�14 (������ 2149) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 2)]
        public bool Drive2149 { set; get; }        //DB9.DBX4.2
        //������ �������� 6�14 (������ 2150) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 3)]
        public bool Drive2150 { set; get; }        //DB9.DBX4.3
        //������ �������� 6�15 (������ 2160) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 4)]
        public bool Drive2160 { set; get; }        //DB9.DBX4.4
        //������ �������� 6�16 (������ 2161) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 5)]
        public bool Drive2161 { set; get; }        //DB9.DBX4.5
        //������ ��������� 6��4 (������ 2009) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 6)]
        public bool Drive2009 { set; get; }        //DB9.DBX4.6
        //������ �������� �������� 6�5 (������ 2211) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 7)]
        public bool Drive2211 { set; get; }        //DB9.DBX4.7
        //������ �������� �������� 6�5 (������ 2212) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 0)]
        public bool Drive2212 { set; get; }        //DB9.DBX5.0
        //������ �������� �������� 6�5 (������ 2213) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 1)]
        public bool Drive2213 { set; get; }        //DB9.DBX5.1
        //������ �������� �������� 6�5 (������ 2214) ���. ������.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 2)]
        public bool Drive2214 { set; get; }        //DB9.DBX5.2
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

 //������� ��������
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "DrivesBunkersEvent2", Location = "PLC3", Destination = "ESMS2")]
    public class DrivesBunkersEvent : EsmsBaseEvent
    {
        //15 ������ 2201 ������� 1-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool Drive2201Bunker1t16 { set; get; }        //IX2.0
        //16 ������ 2202 ������� 2-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool Drive2202Bunker2t16 { set; get; }        //IX4.0
        //17 ������ 2203 ������� 3-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool Drive2203Bunker3t16{ set; get; }        //IX6.0
        //18 ������ 2204 ������� 4-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool Drive2204Bunker4t16 { set; get; }        //IX8.0
        //19 ������ 2205 ������� 5-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool Drive2205Bunker5t16 { set; get; }        //IX10.0
        //20 ������ 2206 ������� 6-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool Drive2206Bunker6t16 { set; get; }        //IX12.0
        //21 ������ 2207 ������� 7-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 6)]
        public bool Drive2207Bunker7t16 { set; get; }        //IX14.0
        //22 ������ 2208 ������� 8-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool Drive2208Bunker8t16 { set; get; }        //IX16.0
        //23 ������ 2209 ������� 9-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool Drive2209Bunker9t16 { set; get; }        //IX18.0
        //24 ������ 2210 ������� 10-16 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool Drive2210Bunker10t16 { set; get; }        //IX20.0
        //25 ������ 2211 ������� 1-17 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool Drive2211Bunker1t17 { set; get; }        //QX50.0
        //26 ������ 2212 ������� 2-17 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool Drive2212Bunker2t17 { set; get; }        //QX54.0
        //27 ������ 2213 ������� 3-17 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Drive2213Bunker3t17 { set; get; }        //QX58.0
        //28 ������ 2214 ������� 4-17 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool Drive2214Bunker4t17 { set; get; }        //QX62.0
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool Reserv1 { set; get; }        
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool Reserv2 { set; get; }        
    }
}  

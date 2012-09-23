using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������
namespace Esms
{  
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FingersEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FingersEvent : EsmsBaseEvent
    {
        //1	������ �������
		[DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool FingersOpen { get; set; }        //DB822.DBX283.3
        //2	������ ����� �������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool AlmostOpen { get; set; }        //DB822.DBX283.4
        //3	������ ����� �������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool AlmostClose { get; set; }        //DB822.DBX283.5
        //4	������ �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool FingersClose { get; set; }        //DB822.DBX283.6
        //5 ����� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool TubOpen { get; set; }
        //6 ����� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool TubClose { get; set; }
        //7 ������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 6)]
        public bool Reserve1 { get; set; }
        //8 ������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool Reserve2 { get; set; }	
        //9 ������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool Reserve3 { get; set; }	
        //10 ������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool Reserve4 { get; set; }	
        //11 ������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool Reserve5 { get; set; }	
        //12 ������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool Reserve6 { get; set; }	
        //13 ������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Reserve7 { get; set; }	
        //14 ������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool Reserve8 { get; set; }	
        //15 ������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool Reserve9 { get; set; }	
        //16 ������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool Reserve10 { get; set; }	
		//193 ������� ������� �� ������� ������� 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL880")]
        public float PositionOutput { get; set; }        //DB824.DBD156
        //194 ������� ������� �� ������� ����� 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL884")]
        public float PositionSlag { get; set; }        //DB824.DBD160

    }
}    

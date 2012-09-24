using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Охлаждающая вода на панели (кожух)
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "WaterCoolingPanelEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class WaterCoolingPanelEvent : EsmsBaseEvent
    {
        //1 Температура охлаждающей воды на входе 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL100")]
        public float TempWaterInput { get; set; }        //DB824.DBD484
        //2 Температура охлаждающей воды на выходе панели 1.1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL104")]
        public float TempWaterOutputPanel1p1 { get; set; }        //DB824.DBD488
        //3 Температура охлаждающей воды на выходе панели 1.2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL108")]
        public float TempWaterOutputPanel1p2 { get; set; }        //DB824.DBD492
        //4 Температура охлаждающей воды на выходе панели 2.1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL112")]
        public float TempWaterOutputPanel2p1 { get; set; }        //DB824.DBD496
        //5 Температура охлаждающей воды на выходе панели 2.2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL116")]
        public float TempWaterOutputPanel2p2 { get; set; }        //DB824.DBD500
        //6 Температура охлаждающей воды на выходе панели 3.1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL120")]
        public float TempWaterOutputPanel3p1 { get; set; }        //DB824.DBD504
        //7 Температура охлаждающей воды на выходе панели 3.2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL124")]
        public float TempWaterOutputPanel3p2 { get; set; }        //DB824.DBD508
        //8 Температура охлаждающей воды на выходе панели 4.1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL128")]
        public float TempWaterOutputPanel4p1 { get; set; }        //DB824.DBD512
        //9 Температура охлаждающей воды на выходе панели 4.2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL132")]
        public float TempWaterOutputPanel4p2 { get; set; }        //DB824.DBD516
        //10 Температура охлаждающей воды на выходе панели 5.1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL136")]
        public float TempWaterOutputPanel5p1 { get; set; }        //DB824.DBD520
        //11 Температура охлаждающей воды на выходе панели 5.2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL140")]
        public float TempWaterOutputPanel5p2 { get; set; }        //DB824.DBD524
        //12 Температура охлаждающей воды на выходе панели 6.1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL144")]
        public float TempWaterOutputPanel6p1 { get; set; }        //DB824.DBD528
        //13 Температура охлаждающей воды на выходе панели 6.2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL148")]
        public float TempWaterOutputPanel6p2 { get; set; }        //DB824.DBD532
        //14 Температура охлаждающей воды на выходе панели 7 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL152")]
        public float TempWaterOutputPanel7 { get; set; }        //DB824.DBD536
        //15 Температура охлаждающей воды на выходе панели 8 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL156")]
        public float TempWaterOutputPanel8 { get; set; }        //DB824.DBD540
        //16 Температура охлаждающей воды на выходе панели 9 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL160")]
        public float TempWaterOutputPanel9 { get; set; }        //DB824.DBD544
        //17 Температура охлаждающей воды на выходе панели 10.1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL164")]
        public float TempWaterOutputPanel10p1 { get; set; }        //DB824.DBD548
        //18 Температура охлаждающей воды на выходе панели 10.2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL168")]
        public float TempWaterOutputPanel10p2 { get; set; }        //DB824.DBD552
        //19 Температура охлаждающей воды на выходе панели 11 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL172")]
        public float TempWaterOutputPanel11 { get; set; }        //DB824.DBD556
        //20 Температура охлаждающей воды на выходе панели 12 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL176")]
        public float TempWaterOutputPanel12 { get; set; }        //DB824.DBD560
        //21 Температура охлаждающей воды на выходе панели 13 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL180")]
        public float TempWaterOutputPanel3 { get; set; }        //DB824.DBD564
        //22 Температура охлаждающей воды на выходе панели 14 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL184")]
        public float TempWaterOutputPanel14 { get; set; }        //DB824.DBD568
        //23 Температура охлаждающей воды на выходе кожуха (панелей) 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL188")]
        public float TempWaterOutputCover { get; set; }        //DB824.DBD572
        //24 Расход охлаждающей воды на выходе кожуха 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL192")]
        public float FlowWaterOutputCover { get; set; }        //DB824.DBD576
        //25 Температура охлаждающей воды на выходе шлакового тоннеля 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL196")]
        public float TempWaterOutputSlagTunnel { get; set; }        //DB824.DBD580
    }
}    

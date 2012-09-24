using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.1						
    // Von:	PLC x.1	(x=номер конвертера)
    // Данные по продувке	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1, DisplayName = "Данные по продувочной фурме", IsTrendGroup = true, BindingPropertyName = "LanceHistory")]
    [DBGroup(UnitNumber = 2, DisplayName = "Данные по продувочной фурме", IsTrendGroup = true, BindingPropertyName = "LanceHistory")]
    [DBGroup(UnitNumber = 3, DisplayName = "Данные по продувочной фурме", IsTrendGroup = true, BindingPropertyName = "LanceHistory")]
    public class LanceEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Oбщий расход O2, нм3", DisplayShortName = "Расход O2", IsTrendPoint = true, MinValue = 0, MaxValue = 50000)]
        [PLCPoint(Location = "DB2,W0")]
        public int O2TotalVol { set; get; }               // общий O2 расход            # ACT_CX_O2VOL_TOTAL

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Интенсивность O2, нм3/мин", DisplayShortName = "Инт. O2", IsTrendPoint = true, MinValue = 0, MaxValue = 5000)]
        [PLCPoint(Location = "DB2,REAL4")]
        public double O2Flow { set; get; }                // O2 интенсивность           # ACT_CX_O2FLOW

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Давление O2, бар", DisplayShortName = "Давл. O2", IsTrendPoint = true, MinValue = 0, MaxValue = 100)]
        [PLCPoint(Location = "DB2,REAL8")]
        public double O2Pressure { set; get; }            // O2 давление                # ACT_CX_O2PRESSURE

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Положение фурмы, см", DisplayShortName = "Пол. фурмы", IsTrendPoint = true, MinValue = 0, MaxValue = 1400)]
        [PLCPoint(Location = "DB2,W12")]
        public int LanceHeight { set; get; }              // положение фурмы              # ACT_CX_LANCEPOS

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W30")]
        public int LanceMode { set; get; }                // Режим работы управления фурмой 1=ручной, 2=автомат, 3=компьютер # ACT_CX_OPLANCE

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W32")]
        public int O2FlowMode { set; get; }               // Режим работы регулирования O2 1=ручной, 2=автомат, 3=компьютер  # ACT_CX_OPO2FLOW

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL102")]
        public double O2LeftLanceWaterInput { set; get; }    // O2 лев.фурма Q воды вход     # ACT_CX_REAL_QWASSZU_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL106")]
        public double O2LeftLanceWaterOutput { set; get; }   // O2 лев.фурма Q воды слив     # ACT_CX_REAL_QWASSAB_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL110")]
        public double O2LeftLanceWaterTempInput { set; get; }   // O2 лев.фурма T воды вход  # ACT_CX_REAL_TWASSZU_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL114")]
        public double O2LeftLanceWaterTempOutput { set; get; }   // O2 лев.фурма T воды слив # ACT_CX_REAL_TWASSAB_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W66")]
        public int O2LeftLanceLeck { set; get; }              // O2 лев.фурма течь        # ACT_CX_LECK_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W80")]
        public int O2RightLanceLeck { set; get; }              // O2 прав.фурма течь      # ACT_CX_LECK_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL68")]
        public double O2LeftLanceWaterPressure { set; get; }    // O2 лев.фурма давление воды   # ACT_CX_PWASS_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL82")]
        public double O2RightLanceWaterPressure { set; get; }    // O2 прав.фурма давление воды # ACT_CX_PWASS_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL118")]
        public double O2RightLanceWaterInput { set; get; }       // O2 прав.фурма Q воды вход      # ACT_CX_REALQWASSZU_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL122")]
        public double O2RightLanceWaterOutput { set; get; }      // O2 прав.фурма Q воды слив      # ACT_CX_REALQWASSAB_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL126")]
        public double O2RightLanceWaterTempInput { set; get; }   // O2 прав.фурма T воды вход      # ACT_CX_REALTWASSZU_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL130")]
        public double O2RightLanceWaterTempOutput { set; get; }   // O2 прав.фурма T воды слив     # ACT_CX_REALTWASSAB_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL86")]
        public double O2LeftLanceGewWeight { set; get; }       // O2 лев.фурма вес            # ACT_CX_GEWLANCE_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL90")]
        public double O2LeftLanceGewBaer { set; get; }         // O2 лев.фурма настыль        # ACT_CX_GEWBAER_L

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL94")]
        public double O2RightLanceGewWeight { set; get; }      // O2 прав.фурма вес           # ACT_CX_GEWLANCE_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL98")]
        public double O2RightLanceGewBaer { set; get; }          // O2 прав.фурма настыль       # ACT_CX_GEWBAER_R

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W36")]
        public int BathLevel { set; get; }                // уровень ванны,измеренный продувочной фурмой # ACT_CX_BATHLEVELMAN

        // !new 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,W36")]
        public int O2Summary { set; get; }               // суммарный расход кислорода # AS31/SEN_S31.O3_QO2_sum
    }
}

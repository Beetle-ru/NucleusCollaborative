using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.3 (Визиуализация)
    // Von:	PLC x.3	(x=номер конвертера)
    // Данные по расходам текущей плавки
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visBlowingFlowRatesEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,W14")]
        public int LanceNFlow { set; get; }                         // Расход азота на уплотнение фурменных окон # AS33/A3_SIGN.G3_SFN2fo

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,W16")]
        public int BoilerNFlow { set; get; }                        // Расход азота на уплотнение котла # AS33/A3_SIGN.G3_SFN2kotel

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,REAL40")]
        public double CandleGasPressure { set; get; }               // Давление природного газа на свечу # AS33/A3_SIGN.Pg

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,W26")]
        public int CandleGasFlow { set; get; }                      // Расход природного газа на свечу # AS33/A3_SIGN.G3S_SFe

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,REAL36")]
        public double CandleSteamPressure { set; get; }             // Давление пара на свечу # AS33/A3_SIGN.Pp

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,W24")]
        public int CandleSteamFlow { set; get; }                    // Расход пара на свечу # AS33/A3_SIGN.G3S_SFd

        // !new
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,BYTE5", IsBoolean = true, BitNumber = 1)]
        public bool CandleFire { set; get; }                        // Факел на свече горит "1" / не горит "0" # AS33/A3_SIGN.G3A_SFAKEL

        // !new
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool CandleFireError { set; get; }                   // Факел на свече не зажегся # AS33/A3_SIGN.A3G_SFAKELneZAG

        // !new
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,BYTE4", IsBoolean = true, BitNumber = 2)]
        public bool CandleSparkFire { set; get; }                   // Запальное устройство (камина) на свече горит "1" / не горит "0"  # AS33/A3_SIGN.G3A_SCOb1
    }
}

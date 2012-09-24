using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Enumerations
    {
        public enum M3ElementEnum : int
        {
            Si = 1,
            Mn = 2,
            P = 3,
            Al = 5,
            Cr = 7,
            V = 10,
            Ti = 11,
            Fe = 32
        }

        public enum MINP_GD_Material_ModelMaterial : int
        {
            CaO = 1,
            Dolomite = 2,
            FOM = 3,
            //CaCO3 = 4,    // replaced by SlagFormer1, 2
            Slag = 10,
            Coke = 20,
            Steel = 30,
            Odprasky = 50,
            SlagFormer1 = 6,
            SlagFormer2 = 7,
        }

        public enum MOUT_Message_Enum : int
        {
            Information_0 = 0,
            Warning_10 = 10,
            Error_20 = 20
        }

        public enum MINP_HeatAimData_State : int
        {
            Prepared_0 = 0,
            Announced_10 = 10,
            Released_20 = 20
        }

        public enum L2L1_Command : int
        {
            TemperatureMeasurement = 1,
            OxygenBlowingStart = 10,
            OxygenLanceToParkingPosition = 20,
        }

        public enum L1L2_Command : int
        {
            OxygenBlowingStarted = 10,
            OxygenBlowingStopped = 15,
            OxygenLanceInParkingPosition = 20,
        }
    }
}

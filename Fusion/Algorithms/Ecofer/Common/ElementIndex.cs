using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class ElementIndex
    {
        public static Dictionary<int, string> Items;

        static ElementIndex()
        {
            Items = new Dictionary<int, string>();
            Items.Add(0, "C");
            Items.Add(1, "Si");
            Items.Add(2, "Mn");
            Items.Add(3, "P");
            Items.Add(4, "S");
            Items.Add(5, "Al");
            Items.Add(6, "Cu");
            Items.Add(7, "Cr");
            Items.Add(8, "Mo");
            Items.Add(9, "Ni");
            Items.Add(10, "V");
            Items.Add(11, "Ti");
            Items.Add(12, "Nb");
            Items.Add(13, "Ca");
            Items.Add(14, "Co");
            Items.Add(15, "Pb");
            Items.Add(16, "W");
            Items.Add(17, "Mg");
            Items.Add(18, "Ce");
            Items.Add(19, "B");
            Items.Add(20, "As");
            Items.Add(21, "Sn");
            Items.Add(22, "Bi");
            Items.Add(23, "Sb");
            Items.Add(24, "Zn");
            Items.Add(25, "Ta");
            Items.Add(26, "Zr");
            Items.Add(27, "Se");
            Items.Add(28, "O");
            Items.Add(29, "N");
            Items.Add(30, "H");
            Items.Add(31, "La");
            Items.Add(32, "Fe");
            Items.Add(41, "AlS");
            Items.Add(42, "Ali");
            Items.Add(43, "Cai");
            Items.Add(50, "Slag_CaO");
            Items.Add(51, "Slag_SiO2");
            Items.Add(52, "Slag_Cr2O3");
            Items.Add(53, "Slag_MnO");
            Items.Add(54, "Slag_NiO");
            Items.Add(55, "Slag_P2O5");
            Items.Add(56, "Slag_S");
            Items.Add(57, "Slag_TiO2");
            Items.Add(58, "Slag_TiFeO");
            Items.Add(59, "Slag_CaF2");
            Items.Add(60, "Slag_Fe");
            Items.Add(61, "Slag_FeO");
            Items.Add(62, "Slag_Al2O3");
            Items.Add(63, "Slag_MgO");
            Items.Add(64, "Slag_CaC2");
        }

        public static int C = 0;
        public static int Si = 1;
        public static int Mn = 2;
        public static int P = 3;
        public static int S = 4;
        public static int Al = 5;
        public static int Cu = 6;
        public static int Cr = 7;
        public static int Mo = 8;
        public static int Ni = 9;
        public static int V = 10;
        public static int Ti = 11;
        public static int Nb = 12;
        public static int Ca = 13;
        public static int Co = 14;
        public static int Pb = 15;
        public static int W = 16;
        public static int Mg = 17;
        public static int Ce = 18;
        public static int B = 19;
        public static int As = 20;
        public static int Sn = 21;
        public static int Bi = 22;
        public static int Sb = 23;
        public static int Zn = 24;
        public static int Ta = 25;
        public static int Zr = 26;
        public static int Se = 27;
        public static int O = 28;
        public static int N = 29;
        public static int H = 30;
        public static int La = 31;
        public static int Fe = 32;
        public static int AlS = 41;
        public static int Ali = 42;
        public static int Cai = 43;
        public static int Slag_CaO = 50;
        public static int Slag_SiO2 = 51;
        public static int Slag_Cr2O3 = 52;
        public static int Slag_MnO = 53;
        public static int Slag_NiO = 54;
        public static int Slag_P2O5 = 55;
        public static int Slag_S = 56;
        public static int Slag_TiO2 = 57;
        public static int Slag_TiFeO = 58;
        public static int Slag_CaF2 = 59;
        public static int Slag_Fe = 60;
        public static int Slag_FeO = 61;
        public static int Slag_Al2O3 = 62;
        public static int Slag_MgO = 63;
        public static int Slag_CaC2 = 64;
        public static int SUMA = 65;
        public static int Basicity = 66;
        public static int Yield = 67;
        public static int approx_steel_yield = 68;
        public static int T = 69;
        public static int Const_H = 70;
        public static int Const_cp = 71;
        public static int Const_TH = 72;
        public static int Const_ro = 73;
    }
}

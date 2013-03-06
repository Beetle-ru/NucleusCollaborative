using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeatCharge
{
    public class FPColumns
    {
        public String Marking;
        public String Units;
        public Double Val;
        public Double Mm;
        public Double O2Stoichio;
        public Double E_ox1;
        public Double E_ox2;
        public Double Eta_ox1;
        public Double Eta_ox2;
        public Double _rcv;
        public bool isO2balanced;
        public FPColumns(string _Marking, Double _Mm = 0.0, string _Units = "%", Int32 _ConvVector = 100)
        {
            Marking = _Marking;
            Mm = _Mm;
            Units = _Units;
            _rcv = 1.0 / _ConvVector;
            Val = 0;
            Eta_ox1 = 1;
            Eta_ox2 = 1;
        }

    }
    public class FPCarrier
    {
        public FPColumns[] fp;
        private Dictionary<String, int> index = new Dictionary<string, int>();
        private void AddDef(int idx, string _Marking, Double _Mm = 0.0, string _Units = "%", Int32 _ConvVector = 100)
        {
            if (index.ContainsKey(_Marking))
            {
                throw new Exception("Duplicate FP key detected:" + _Marking + " at pos " + idx.ToString());
            }
            index.Add(_Marking, idx);
            fp[idx] = new FPColumns(_Marking, _Mm, _Units, _ConvVector);
        }
        public FPCarrier()
        {
            fp = new FPColumns[74];

            AddDef(0, "C", 12);
            fp[0].O2Stoichio = 11.2;
            fp[0].E_ox1 = 2.9;
            fp[0].E_ox2 = 6.6;
            fp[0].Eta_ox1 = 0.99;
            fp[0].Eta_ox2 = 0.45;
            fp[0].isO2balanced = true;

            AddDef(1, "Si", 28);
            fp[1].O2Stoichio = 22.4;
            fp[1].E_ox1 = 4.8;
            fp[1].isO2balanced = true;

            AddDef(2, "Mn", 55);
            fp[2].O2Stoichio = 11.2;
            fp[2].E_ox1 = 2;
            fp[2].isO2balanced = true;

            AddDef(3, "P", 31);
            fp[3].O2Stoichio = 28;
            fp[3].E_ox1 = 6.9;
            fp[3].isO2balanced = true;

            AddDef(4, "S", 32);

            AddDef(5, "Al", 27);
            fp[5].O2Stoichio = 33.6;
            fp[5].E_ox1 = 8.6;
            fp[5].isO2balanced = true;

            AddDef(6, "Cu", 63.5);

            AddDef(7, "Cr", 52);
            fp[7].O2Stoichio = 33.6;
            fp[7].E_ox1 = 3;
            fp[7].isO2balanced = true;

            AddDef(8, "Mo", 96);

            AddDef(9, "Ni", 58.7);

            AddDef(10, "V", 51);
            fp[10].O2Stoichio = 22.4;
            fp[10].isO2balanced = true;

            AddDef(11, "Ti", 47.9);
            fp[11].O2Stoichio = 22.4;
            fp[11].E_ox1 = 5.5;
            fp[11].isO2balanced = true;

            AddDef(12, "Nb", 93);
            fp[12].O2Stoichio = 33.6;

            AddDef(13, "Ca", 40);
            fp[13].O2Stoichio = 11.2;

            AddDef(14, "Co", 58.9);

            AddDef(15, "Pb", 207);

            AddDef(16, "W", 184);

            AddDef(17, "Mg", 24.3);
            fp[17].O2Stoichio = 11.2;

            AddDef(18, "Ce", 140);
            fp[18].O2Stoichio = 11.2;

            AddDef(19, "B", 10.8);
            fp[19].O2Stoichio = 33.6;

            AddDef(20, "As", 75);

            AddDef(21, "Sn", 119);

            AddDef(22, "Bi", 209);
            fp[22].O2Stoichio = 11.2;

            AddDef(23, "Sb", 122);

            AddDef(24, "Zn", 65.4);

            AddDef(25, "Ta", 181);

            AddDef(26, "Zr", 91.2);
            fp[26].O2Stoichio = 22.4;

            AddDef(27, "Se", 79);

            AddDef(28, "O", 16, "ppm", 1000000);
            fp[28].O2Stoichio = 11.2;

            AddDef(29, "N", 14, "ppm", 1000000);

            AddDef(30, "H", 1, "ppm", 1000000);

            AddDef(31, "La", 139);

            AddDef(32, "Fe", 55.8);
            fp[32].O2Stoichio = 11.2;
            fp[32].E_ox1 = 1.4; //2.7; //4.61594;
            fp[32].isO2balanced = true;

            for (var i = 33; i <= 40; i++) AddDef(i, "?" + i);

            AddDef(41, "AlS", 27);

            AddDef(42, "Ali", 27);

            AddDef(43, "Cai", 27);

            AddDef(44, "CO2", 44);

            for (var i = 45; i <= 49; i++) AddDef(i, "?" + i);

            AddDef(50, "CaO", 56);

            AddDef(51, "SiO2", 60);

            AddDef(52, "Cr2O3", 152);

            AddDef(53, "MnO", 71);

            AddDef(54, "NiO", 74.7);

            AddDef(55, "P2O5", 142);

            AddDef(56, "S+", 32);

            AddDef(57, "TiO2", 79.9);

            AddDef(58, "TiFeO", 152);

            AddDef(59, "CaF2", 78);

            AddDef(60, "Fe+", 55.8);

            AddDef(61, "FeO", 71.8);

            AddDef(62, "Al2O3", 102);

            AddDef(63, "MgO", 40.3);

            AddDef(64, "CaC2", 64);

            AddDef(65, "TOTAL", 0, "share", 1);
            fp[65].Val = 1.0;

            AddDef(66, "Basiticy", 0, "-", 1);

            AddDef(67, "Yield");

            AddDef(68, "Steel");

            AddDef(69, "T", 0, "°C", 1);

            AddDef(70, "eH", 0, "kWh/t", 1000);

            AddDef(71, "cp", 0, "kWh/t/°C", 1000);

            AddDef(72, "TeH", 0, "°C", 1);

            AddDef(73, "ro", 0, "kg/m3", 1);
        }
        public double fpNorm(int idx)
        {
            return fp[idx].Val * fp[idx]._rcv;
        }
        public double fpNorm(String skey)
        {
            if (index.ContainsKey(skey))
            {
                return fpNorm(index[skey]);
            }
            return Double.NaN;
        }
        public double fpGet(int idx)
        {
            return fp[idx].Val;
        }
        public double fpGet(String skey)
        {
            if (index.ContainsKey(skey))
            {
                return fpGet(index[skey]);
            }
            return Double.NaN;
        }
        public void fpSet(String skey, Double sval)
        {
            if (!index.ContainsKey(skey))
            {
                throw new Exception("FP key not found:" + skey);
            }
            fp[index[skey]].Val = sval;
        }
        public int name2ix(String Name)
        {
            if (!index.ContainsKey(Name))
            {
                throw new Exception("FP key not found:" + Name);
            }
            return index[Name];
        }
    }
}

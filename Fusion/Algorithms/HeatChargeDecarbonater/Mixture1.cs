using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeatCharge
{
    public class Mixture1
    {
        public enum CalcTask
        {
            CalcTaskIron,
            CalcTaskScrap,
            CalcTaskSteel
        }

        public static CalcTask s_CalcTask;
        public static FPCarrier s_Iron = new FPCarrier();
        public static FPCarrier s_Scrap = new FPCarrier();
        public static FPCarrier s_Steel = new FPCarrier();
        public static FPCarrier s_Fom = new FPCarrier();
        public static FPCarrier s_CaCO3 = new FPCarrier();
        public static FPCarrier s_Lime = new FPCarrier();
        public static FPCarrier s_Coke = new FPCarrier();
        public static FPCarrier s_LimeStone = new FPCarrier();
        public static FPCarrier s_Dust = new FPCarrier();
        public static double m_Iron, m_Scrap, m_Steel;
        public static double m_IronTask, m_ScrapTask, m_SteelTask, scaleFactor = 1.0;
        public static double t_Iron, t_Scrap, t_Steel;
        public static double basiticy, p_MgO, p_FeO;
        public static double m_Vapno, m_Dolmax, m_Fom, m_CaCO3;
        public static double step_m_Scrap, e_Iron, e_Scrap, m_SiO2, m_MnO, m_Al2O3, m_CaO, m_slag, m_MgO, m_FeO;
        public static double m_Dust, m_Fe_k_oxidaci, m_Coke, m_dolomite, m_lime;
        public static double e_Si_ox, e_Mn_ox, e_Al_ox, e_Fe_ox, e_C_ox, e_Coke, e_SlagForming, e_Common, e_Dust, e_Steel, e_Curr;
        public static double[] p_SteelAdd = new double[8];

        public static void Initialize()
        {
            m_Iron = 300;
            m_Scrap = 60;
            step_m_Scrap = 0.1;
            s_Iron.fpSet("T", t_Iron);
            s_Steel.fpSet("T", t_Steel);
            s_Steel.fpSet("TeH", 1550);
            s_Steel.fpSet("cp", 0.22);
            s_Steel.fpSet("eH", 380);
            s_Scrap.fpSet("T", t_Scrap);
            s_Fom.fpSet("T", t_Scrap);
            s_CaCO3.fpSet("T", t_Scrap);
            s_Coke.fpSet("T", t_Scrap);
            s_LimeStone.fpSet("T", t_Scrap);
        }
        private static double mass001(int i1, int i2, int i3)
        {
            double mass = m_Iron * s_Iron.fpNorm(i1) + m_Scrap * s_Scrap.fpNorm(i1);
            mass *= s_Scrap.fp[i2].Mm / (s_Scrap.fp[i1].Mm * i3);
            mass += m_Iron * s_Iron.fpNorm(i2);
            mass += m_Scrap * s_Scrap.fpNorm(i2);
            return mass;
        }
        private static double heat001(FPCarrier fp)
        {
            return (fp.fpNorm(70) / fp.fpNorm(72)) * fp.fpNorm(69); 
        }
        public static void Calculate()
        {
            Double PC = 0.05;
            e_Iron = m_Iron * heat001(s_Iron);
            Double diff = s_Iron.fpNorm(69) - s_Iron.fpNorm(72);
            if (diff > 0)
            {
                e_Iron = m_Iron * (s_Iron.fpNorm(70) + s_Iron.fpNorm(71) * diff);
            }
            e_Scrap = m_Scrap * heat001(s_Scrap);
            m_SiO2 = mass001(1, 51, 1);
            m_MnO = mass001(2, 53, 1);
            m_Al2O3 = mass001(5, 62, 2);
            m_CaO = basiticy * m_SiO2;
            m_slag = (m_CaO + m_SiO2 + m_MnO + m_Al2O3)*100/(100 - p_FeO - p_MgO);
            m_MgO = m_slag * p_MgO * 0.01;
            m_FeO = m_Iron*s_Iron.fpNorm(61) + m_Scrap*s_Scrap.fpNorm(61);
            m_Fe_k_oxidaci = (m_slag*0.01*p_MgO + m_Dust*s_Dust.fpNorm(61) - m_FeO)*s_Scrap.fp[32].Mm/s_Scrap.fp[61].Mm;
            e_Si_ox = (m_Iron * s_Iron.fpNorm(1) + m_Scrap * s_Scrap.fpNorm(1)) * s_Scrap.fp[1].E_ox1 * s_Scrap.fp[1].Eta_ox1;
            e_Mn_ox = (m_Iron * s_Iron.fpNorm(2) + m_Scrap * s_Scrap.fpNorm(2)) * s_Scrap.fp[2].E_ox1 * s_Scrap.fp[2].Eta_ox1;
            e_Al_ox = (m_Iron * s_Iron.fpNorm(5) + m_Scrap * s_Scrap.fpNorm(5)) * s_Scrap.fp[5].E_ox1 * s_Scrap.fp[5].Eta_ox1;
            e_Fe_ox = m_Fe_k_oxidaci * s_Scrap.fp[32].E_ox1 * s_Scrap.fp[32].Eta_ox1;
            e_C_ox = (m_Iron*s_Iron.fpNorm(0) + m_Scrap*s_Scrap.fpNorm(0))*
                        (s_Scrap.fp[0].E_ox1*s_Scrap.fp[0].Eta_ox1 + s_Scrap.fp[0].E_ox2*s_Scrap.fp[0].Eta_ox2);
            e_Coke = m_Coke*0.97*(s_Scrap.fp[0].E_ox1*s_Scrap.fp[0].Eta_ox1 + PC*s_Scrap.fp[0].E_ox2*s_Scrap.fp[0].Eta_ox2);
            Double d_CaCO3 = m_CaCO3*s_CaCO3.fpNorm(50)*s_CaCO3.fpNorm(67);
            Double d_Fom = m_Fom*s_Fom.fpNorm(50)*s_Fom.fpNorm(67);
            Double d_Iron = m_Iron*s_Iron.fpNorm(50);
            Double d_Scrap = m_Scrap*s_Scrap.fpNorm(50);
            m_CaO = m_CaO - d_CaCO3 - d_Fom - d_Iron - d_Scrap;
            d_CaCO3 = m_CaCO3*s_CaCO3.fpNorm(63)*s_CaCO3.fpNorm(67);
            d_Fom = m_Fom*s_Fom.fpNorm(63)*s_Fom.fpNorm(67);
            d_Iron = m_Iron*s_Iron.fpNorm(63);
            d_Scrap = m_Scrap*s_Scrap.fpNorm(63);
            m_MgO = m_MgO - d_CaCO3 - d_Fom - d_Iron - d_Scrap;
            m_dolomite = (m_CaO/s_Lime.fpNorm(50) - m_MgO/s_Lime.fpNorm(63))
                / (s_LimeStone.fpNorm(50)/s_Lime.fpNorm(50) - s_LimeStone.fpNorm(63)/s_Lime.fpNorm(63));
            m_lime = (m_CaO/s_LimeStone.fpNorm(50) - m_MgO/s_LimeStone.fpNorm(63))
                / (s_Lime.fpNorm(50)/s_LimeStone.fpNorm(50) - s_Lime.fpNorm(63)/s_LimeStone.fpNorm(63));
            m_dolomite = m_dolomite/s_LimeStone.fpNorm(67);
            m_lime = m_lime/s_Lime.fpNorm(67);
            e_SlagForming = 0;/* m_Fom * (s_Fom.fpNorm(70) / s_Fom.fpNorm(72)) * s_Fom.fpNorm(69)
                 + m_dolomite * (s_LimeStone.fpNorm(70) / s_LimeStone.fpNorm(72)) * s_LimeStone.fpNorm(69)
                 + m_lime * (s_Lime.fpNorm(70) / s_Lime.fpNorm(72)) * s_Lime.fpNorm(69)
                 + m_CaCO3 * (s_CaCO3.fpNorm(70) / s_CaCO3.fpNorm(72)) * s_CaCO3.fpNorm(69); // CaO ?*/
            
            e_Common = e_Iron + e_Scrap + e_SlagForming + e_Si_ox + e_Mn_ox + e_Al_ox + e_Fe_ox + e_C_ox + e_Coke;
        }

        public static bool Ready()
        {
            Double heatTime = 20; // min
            Double powerDissipation = 220; // kWh/min
            Double e_Accumulation = 18000; // kWh
            m_Dust = 3; // tonns
            m_Steel = 0;
            Double d_Iron = 100, d_Scrap = 100;
            int i = 0;
            while (i < 65)
            {
                d_Iron -= s_Iron.fpGet(i);
                d_Scrap -= s_Scrap.fpGet(i);
                if (i == 5) i = 50;
                else i++;
            }
            m_Steel += m_Iron * d_Iron * 0.01 + m_Scrap * d_Scrap * 0.01;
            m_Steel -= m_Fe_k_oxidaci + m_Dust * (s_Dust.fpNorm(32) + s_Dust.fpNorm(60));
            Double dt = t_Steel - s_Steel.fpNorm(72);
            e_Dust = m_Dust * (s_Dust.fpNorm(71) * dt + s_Dust.fpNorm(70)); 
            e_Steel = m_Steel*(s_Steel.fpNorm(71)* dt + s_Steel.fpNorm(70));
            e_Curr = e_Steel + (e_Accumulation + heatTime*powerDissipation) * 0.001 + e_Dust + e_SlagForming;
            double d_Entalpia = Math.Abs(e_Common - e_Curr) / e_Common;
            if (d_Entalpia > 5e-5)
            {
                m_Scrap *= e_Common / e_Curr;
                return false;
            };
            return true;
        }
        public static void PostCalc()
        {
            m_Iron *= scaleFactor;
            m_Scrap *= scaleFactor;
            m_Steel *= scaleFactor;
            m_Fom *= scaleFactor;
            m_CaCO3 *= scaleFactor;
            m_dolomite *= scaleFactor;
            m_lime *= scaleFactor;
            m_slag *= scaleFactor;
            int[] aix = new int[] { 6, 8, 9, 14, 16, 20, 21, 23 };
            for (int i = 0; i < 8; i++)
            {
                p_SteelAdd[i] = m_Iron * s_Iron.fpGet(aix[i]) + m_Scrap * s_Scrap.fpGet(aix[i]);
                p_SteelAdd[i] /= m_Steel;
            }
        }
    }
}

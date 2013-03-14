using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeatControl;

namespace HeatControl
{
    public class MixCalc
    {
        public const double m_Dust = 3; // tonns
        public enum CalcTask
        {
            CalcTaskIron,
            CalcTaskScrap,
            CalcTaskSteel
        }

        public static long calcPattern;
        public static CalcTask s_CalcTask;
        public static FPCarrier s_Iron = new FPCarrier();
        public static FPCarrier s_Scrap = new FPCarrier();
        public static FPCarrier s_Steel = new FPCarrier();
        public static FPCarrier s_Fom = new FPCarrier();
        public static FPCarrier s_DolMax = new FPCarrier();
        public static FPCarrier s_Lime = new FPCarrier();
        public static FPCarrier s_Coke = new FPCarrier();
        public static FPCarrier s_DolomS = new FPCarrier();
        public static FPCarrier s_Dust = new FPCarrier();
        public static double m_Iron, m_Scrap, m_Steel;
        public static double m_IronTask, m_ScrapTask, m_SteelTask, scaleFactor = 1.0;
        public static double t_Iron, t_Scrap, t_Steel;
        public static double basiticy, p_MgO, p_FeO;
        public static double m_Vapno, m_Dolmax, m_Fom, m_DolMax;
        public static double step_m_Scrap, e_Iron, e_Scrap, m_SiO2, m_MnO, m_Al2O3, m_CaO, m_slag, m_MgO, m_FeO;
        public static double m_Fe_k_oxidaci, m_Coke, m_DolomS, m_Lime;
        public static double e_Si_ox, e_Mn_ox, e_Al_ox, e_Fe_ox, e_C_ox, e_Coke, e_SlagForming, e_SlagFormingExtra, e_Common, e_Dust, e_Steel, e_Curr;
        public static double[] p_SteelAdd = new double[8];

        public static void Initialize()
        {
            m_Iron = m_IronTask > 0 ? m_IronTask : 300;
            m_Scrap = m_Iron * 0.2;
            s_Iron.fpSet("T", t_Iron);
            s_Steel.fpSet("T", t_Steel);
            s_Steel.fpSet("TeH", 1550);
            s_Steel.fpSet("cp", 0.22);
            s_Steel.fpSet("eH", 380);
            s_Scrap.fpSet("T", t_Scrap);
            s_Fom.fpSet("T", t_Scrap);
            s_DolMax.fpSet("T", t_Scrap);
            s_Coke.fpSet("T", t_Scrap);
            s_DolomS.fpSet("T", t_Scrap);
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
            //m_SiO2 = mass001(1, 51, 1);
            m_SiO2 = m_Iron * s_Iron.fpNorm(1) + m_Scrap * s_Scrap.fpNorm(1);
            m_SiO2 *= s_Dust.fp[51].Mm/s_Dust.fp[1].Mm;
            m_SiO2 += m_Iron*s_Iron.fpNorm(51) + m_Scrap*s_Scrap.fpNorm(51);
            m_MnO = mass001(2, 53, 1);
            m_Al2O3 = mass001(5, 62, 2);
            m_CaO = basiticy * m_SiO2;
            m_slag = (m_CaO + m_SiO2 + m_MnO + m_Al2O3)*100/(100 - p_FeO - p_MgO);
            m_MgO = m_slag * p_MgO * 0.01;
            m_FeO = m_Iron*s_Iron.fpNorm(61) + m_Scrap*s_Scrap.fpNorm(61);
            m_Fe_k_oxidaci = (m_slag*0.01*p_FeO + m_Dust*s_Dust.fpNorm(61) - m_FeO)*s_Scrap.fp[32].Mm/s_Scrap.fp[61].Mm;
            e_Si_ox = (m_Iron * s_Iron.fpNorm(1) + m_Scrap * s_Scrap.fpNorm(1)) * s_Scrap.fp[1].E_ox1 * s_Scrap.fp[1].Eta_ox1;
            e_Mn_ox = (m_Iron * s_Iron.fpNorm(2) + m_Scrap * s_Scrap.fpNorm(2)) * s_Scrap.fp[2].E_ox1 * s_Scrap.fp[2].Eta_ox1;
            e_Al_ox = (m_Iron * s_Iron.fpNorm(5) + m_Scrap * s_Scrap.fpNorm(5)) * s_Scrap.fp[5].E_ox1 * s_Scrap.fp[5].Eta_ox1;
            e_Fe_ox = m_Fe_k_oxidaci * s_Scrap.fp[32].E_ox1 * s_Scrap.fp[32].Eta_ox1;
            e_C_ox = (m_Iron*s_Iron.fpNorm(0) + m_Scrap*s_Scrap.fpNorm(0))*
                        (s_Scrap.fp[0].E_ox1*s_Scrap.fp[0].Eta_ox1 + PC * s_Scrap.fp[0].E_ox2*s_Scrap.fp[0].Eta_ox2);
            e_Coke = m_Coke * 0.97 * (s_Scrap.fp[0].E_ox1 * s_Scrap.fp[0].Eta_ox1 + PC * s_Scrap.fp[0].E_ox2 * s_Scrap.fp[0].Eta_ox2);

            switch (calcPattern)
            {
                case 0x1100:
                    solve(out m_Lime, s_Lime, out m_DolomS, s_DolomS, m_Fom, s_Fom, m_DolMax, s_DolMax);
                    break;
                case 0x1010:
                    solve(out m_Lime, s_Lime, out m_Fom, s_Fom, m_DolomS, s_DolomS, m_DolMax, s_DolMax);
                    break;
                case 0x1001:
                    solve(out m_Lime, s_Lime, out m_DolMax, s_DolMax, m_Fom, s_Fom, m_DolomS, s_DolomS);
                    break;
                case 0x0110:
                    solve(out m_DolomS, s_DolomS, out m_Fom, s_Fom, m_DolMax, s_DolMax, m_Lime, s_Lime);
                    break;
                case 0x0101:
                    solve(out m_DolomS, s_DolomS, out m_DolMax, s_DolMax, m_Fom, s_Fom, m_Lime, s_Lime);
                    break;
                case 0x0011:
                    solve(out m_Fom, s_Fom, out m_DolMax, s_DolMax, m_Lime, s_Lime, m_DolomS, s_DolomS);
                    break;
                default:
                    throw new Exception(string.Format("неизвестная маска сыпучих {0:x}", calcPattern));
            }

            e_SlagForming = 
                 m_Fom * (s_Fom.fpNorm(70) / s_Fom.fpNorm(72)) * s_Fom.fpNorm(69)
                 + m_DolomS * (s_DolomS.fpNorm(70) / s_DolomS.fpNorm(72)) * s_DolomS.fpNorm(69)
                 + m_Lime * (s_Lime.fpNorm(70) / s_Lime.fpNorm(72)) * s_Lime.fpNorm(69)
                 + m_DolMax * (s_DolMax.fpNorm(70) / s_DolMax.fpNorm(72)) * s_DolMax.fpNorm(69);
            e_SlagFormingExtra =
                m_Fom*(s_Fom.fpNorm(71)*(t_Steel - s_Fom.fpNorm(72)) + s_Fom.fpNorm(70))
                + m_DolomS*(s_DolomS.fpNorm(71)*(t_Steel - s_DolomS.fpNorm(72)) + s_DolomS.fpNorm(70))
                + m_Lime*(s_Lime.fpNorm(71)*(t_Steel - s_Lime.fpNorm(72)) + s_Lime.fpNorm(70))
                + m_DolMax*(s_DolMax.fpNorm(71)*(t_Steel - s_DolMax.fpNorm(72)) + s_DolMax.fpNorm(70));
            e_Common = e_Iron + e_Scrap + e_SlagForming + e_Si_ox + e_Mn_ox + e_Al_ox + e_Fe_ox + e_C_ox + e_Coke;
        }
        private static void solve(out double m_X, FPCarrier s_X, out double m_Y, FPCarrier s_Y,
                                  double m_A, FPCarrier s_A, double m_B, FPCarrier s_B)
        {
            m_CaO = m_CaO - d_A(m_B, s_B, "CaO") - d_A(m_A, s_A, "CaO") - d_Met("CaO");
            m_MgO = m_MgO - d_A(m_B, s_B, "MgO") - d_A(m_A, s_A, "MgO") - d_Met("MgO");
            resXY(out m_X, s_X, out m_Y, s_Y);
        }
        private static void resXY(out double m_X, FPCarrier s_X, out double m_Y, FPCarrier s_Y)
        {
            m_X = (m_CaO / s_Y.fpNorm("CaO") - m_MgO / s_Y.fpNorm("MgO"))
                / (s_X.fpNorm("CaO") / s_Y.fpNorm("CaO") - s_X.fpNorm("MgO") / s_Y.fpNorm("MgO"));
            m_Y = (m_CaO / s_X.fpNorm("CaO") - m_MgO / s_X.fpNorm("MgO"))
                / (s_Y.fpNorm("CaO") / s_X.fpNorm("CaO") - s_Y.fpNorm("MgO") / s_X.fpNorm("MgO"));
            m_X /= s_X.fpNorm("Yield");
            m_Y /= s_Y.fpNorm("Yield");
        }
        private static double d_A(double m_A, FPCarrier s_A, string fpId)
        {
            return m_A * s_A.fpNorm(fpId) * s_A.fpNorm("Yield");
        }
        private static double d_Met(string fpId)
        {
            return m_Iron * s_Iron.fpNorm(fpId) + m_Scrap * s_Scrap.fpNorm(fpId);
        }

        public static bool Ready()
        {
            Double heatTime = 15; // min
            Double powerDissipation = 300; // kWh/min
            Double e_Accumulation = 6000; // kWh
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
            e_Curr = e_Steel + (e_Accumulation + heatTime*powerDissipation*0.016666666) * 0.001 + e_Dust + e_SlagFormingExtra;
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
            m_DolMax *= scaleFactor;
            m_DolomS *= scaleFactor;
            m_Lime *= scaleFactor;
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

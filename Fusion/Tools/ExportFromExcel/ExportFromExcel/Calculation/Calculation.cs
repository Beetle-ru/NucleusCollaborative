using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator
{
    public class Calculation
    {

        #region Материальный баланс

        /// <summary>
        /// Интенсивность окисления элементов металла при работе сводовой фурмы, кг/с
        /// mOxL(i)=nOxLFe(i)*muFe+nOxLC(i)*muC+nOxLMn(i)*muMn+nOxLSi(i)*muSi
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mOxL(Protocol prt, Currents cur)
        {
            return nOxLFe(prt, cur) * Constants.muFe + nOxLC(prt, cur) * Constants.muC + nOxLMn(prt, cur) * Constants.muMn +
                   nOxLSi(prt, cur) * Constants.muSi;
        }

        /// <summary>
        /// Интенсивность окисления элементов металла при работе горелки RCB в режиме фурмы, кг/с
        /// mOxR(i)=nOxRFe(i)*muFe+nOxRC(i)*muC+nOxRMn(i)*muMn+nOxRSi(i)*muSi
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mOxR(Protocol prt, Currents cur)
        {
            return nOxRFe(prt, cur) * Constants.muFe + nOxRC(prt, cur) * Constants.muC + nOxRMn(prt, cur) * Constants.muMn +
                   nOxRSi(prt, cur) * Constants.muSi;
        }

        /// <summary>
        ///	Интенсивность перехода кислорода в шлак при работе горелки RCB в режиме фурмы, кг/с
        /// mActOR(i)=(DOxFe(i)+DOxC(i)+DOxMn(i)+DOxSi(i))*mGRL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mActOR(Protocol prt, Currents cur)
        {
            return (DOxFe(cur) + DOxC(cur) + DOxMn(cur) + DOxSi(cur)) * mGRL(prt);
        }

        /// <summary>
        ///	Интенсивность перехода кислорода в шлак при работе сводовой фурмы, кг/с
        /// mActOL(i)=(DOxFe(i)+DOxC(i)+DOxMn(i)+DOxSi(i))*mGL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mActOL(Protocol prt, Currents cur)
        {
            return (DOxFe(cur) + DOxC(cur) + DOxMn(cur) + DOxSi(cur)) * mGL(prt);
        }

        /// <summary>
        ///	Интенсивность восстановления элементов из шлака при работе угольного инжектора, кг/с
        /// mRedP(i)=nRedPFeO(i)*muFeO+nRedPMnO(i)*muMnO+nRedPSiO2(i)*muSiO2+nRedPC(i)*muC
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mRedP(Protocol prt, Currents cur)
        {
            return nRedPFeO(prt, cur) * Constants.muFeO + nRedPMnO(prt, cur) * Constants.muMnO +
                   nRedPSiO2(prt, cur) * Constants.muSiO2 + nRedPC(prt, cur) * Constants.muC;
        }

        /// <summary>
        ///	Масса разовой порции шлакообразующих материалов в загрузочном бункере, кг
        /// mSlAdd=mLmAdd+mDlmtAdd
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        public double mSlAdd(Protocol prt)
        {
            return prt.mLmAdd + prt.mDlmtAdd;
        }

        /// <summary>
        ///	Интенсивность вдувания газов через горелку, кг/с
        /// mGB(i)=mO2B(i)+mCH4B(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        public double mGB(Protocol prt)
        {
            return mO2B(prt) + mCH4B(prt);
        }

        /// <summary>
        /// Интенсивность вдувания газов через горелку RCB в режиме горелки	кг/с
        /// mGRB(i)=mO2RB(i)+mCH4RB(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        public double mGRB(Protocol prt)
        {
            return mO2RB(prt) + mCH4RB(prt);
        }

        /// <summary>
        ///	Интенсивность образования отходящих газов при работе горелки, кг/с
        /// mOffGB(i)=mOffCO2B(i)+mOffH2OB(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        public double mOffGB(Protocol prt)
        {
            return mOffCO2B(prt) + mOffH2OB(prt);
        }

        /// <summary>
        ///	Интенсивность образования отходящих газов при работе горелки, кг/с
        /// mOffGRB(i)=mOffCO2RB(i)+mOffH2ORB(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        public double mOffGRB(Protocol prt)
        {
            return mOffCO2RB(prt) + mOffH2ORB(prt);
        }

        /// <summary>
        ///	Интенсивность образования отходящих газов при работе горелки RCB в режиме фурмы, кг/с
        /// mOffGRL(i)=mOffCORL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mOffGRL(Protocol prt, Currents cur)
        {
            return mOffCORL(prt, cur);
        }

        /// <summary>
        /// Интенсивность образования отходящих газов при работе сводовой фурмы, кг/с
        /// mOffGL(i)=mOffCOL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mOffGL(Protocol prt, Currents cur)
        {
            return mOffCOL(prt, cur);
        }

        /// <summary>
        /// Интенсивность образования отходящих газов при работе угольного инжектора, кг/с
        /// mOffGP(i)=mOffCOP(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mOffGP(Protocol prt, Currents cur)
        {
            return mOffCOP(prt, cur);
        }

        #endregion

        #region Основные параметры

        /// <summary>
        /// Масса металла в печи,кг
        /// mSt(i)=mSt(0)+mC(i)+mMn(i)+mSi(i)+mO(i)+mFe(i)
        /// Масса металла в печи перед началом плавки,кг
        /// mSt(0)=mStH
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mSt(Protocol prt, Currents cur)
        {
            return prt.mStH + cur.mC + cur.mMn + cur.mSi + cur.mO + cur.mFe;
        }

        /// <summary>
        /// Масса шлака в печи, кг
        /// mSl(i)=mSl(0)+mFeO(i)+mMnO(i)+mSiO2(i)+mCaO(i)
        /// Масса шлака в печи перед началом плавки,кг
        /// mSl(0)=mSlH 
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mSl(Protocol prt, Currents cur)
        {
            return prt.mSlH + cur.mFeO + cur.mMnO + cur.mSiO2 + cur.mCaO;
        }

        /// <summary>
        /// Концентрация углерода в металле, % масс
        /// CC(i)=mC(i)/mSt(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CC(Protocol prt, Currents cur)
        {
            return cur.mC / cur.mSt * 100;
        }

        /// <summary>
        /// Концентрация марганца в металле, % масс
        /// CMn(i)=mMn(i)/mSt(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CMn(Protocol prt, Currents cur)
        {
            return cur.mMn / cur.mSt * 100;
        }

        /// <summary>
        /// Концентрация кремния в металле, % масс
        /// CSi(i)=mSi(i)/mSt(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CSi(Protocol prt, Currents cur)
        {
            return cur.mSi / cur.mSt * 100;
        }

        /// <summary>
        /// Концентрация кислорода в металле, % масс
        /// CO(i)=mO(i)/mSt(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CO(Protocol prt, Currents cur)
        {
            return cur.mO / cur.mSt * 100;
        }

        /// <summary>
        /// Концентрация FeO в шлаке, % масс
        /// CFeO(i)=mFeO(i)/mSl(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CFeO(Protocol prt, Currents cur)
        {
            return cur.mFeO / cur.mSl * 100;
        }

        /// <summary>
        /// Концентрация MnO в шлаке, % масс
        /// CMnO(i)=mMnO(i)/mSl(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CMnO(Protocol prt, Currents cur)
        {
            return cur.mMnO / cur.mSl * 100;
        }

        /// <summary>
        /// Концентрация SiO2 в шлаке, % масс
        /// CSiO2(i)=mSiO2(i)/mSl(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CSiO2(Protocol prt, Currents cur)
        {
            return cur.mSiO2 / cur.mSl * 100;
        }

        /// <summary>
        /// Концентрация CaO в шлаке, % масс
        /// CCaO(i)=mCaO(i)/mSl(i)*100
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CCaO(Protocol prt, Currents cur)
        {
            return cur.mCaO / cur.mSl * 100;
        }

        /// <summary>
        /// Средняя температура системы металл-шлак, К
        /// при средней температуре металла менее температуры ликвидус
        /// при ECPh(i)-cSl*Tav(i-1)*mSl(i) меньше или = (EStL*mSt(i)-lmbdSt*mSt(i))
        /// Tav(i)=(ECPh(i)+cStS*mSt(i)*Tenv(i))/(cStS*mSt(i)+cSl*mSl(i))
        /// при средней температуре металла равной температуре ликвидус
        /// при ECPh(i)-cSl*Tav(i)*mSl(i) меньше или = EStL*mSt(i)
        /// Tav(i)=TStL	
        /// при средней температуре металла более температуры ликвидус
        /// в остальных случаях: 
        /// Tav(i)=(ECPh(i)-mSt(i)*(cStS*TStL-cStS*Tenv(i)-cStL*TStL+lmbdSt))/(cStL*mSt(i)+cSl*mSl(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double Tav(Protocol prt, Currents cur)
        {
            var ttemp1 = cur.ECPh - Constants.cSl * cur.Tav * cur.mSt;
            var ttemp2 = (Constants.EStL * cur.mSt - Constants.lmbdSt * cur.mSt);
            var ttemp3 = Constants.EStL * cur.mSt;
           if (ttemp1 <= ttemp2)
           {
               return (cur.ECPh + Constants.cStS * cur.mSt * prt.Tenv) /
                      (Constants.cStS * cur.mSt + Constants.cSl * cur.mSl);
           }
           if (ttemp1 <= ttemp3)
           {
              return Constants.TStL;
           }
           return (cur.ECPh - cur.mSt * 
                    (Constants.cStS*Constants.TStL - Constants.cStS*prt.Tenv - Constants.cStL*Constants.TStL +
                     Constants.lmbdSt)) / (Constants.cStL * cur.mSt + Constants.cSl * cur.mSl);

        }

        /// <summary>
        ///	Расход электроэнергии, МВт·ч
        /// CnsEE(i)=CnsEE(i-1)+We(i)*dt/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsEE(Protocol prt, Currents cur)
        {
            return cur.CnsEE + prt.We * prt.dt / 3600;
        }

        /// <summary>
        ///	Суммарный расход кислорода через фурмы, м3
        /// CnsO2L(i)=CnsO2L(i-1)+(UO2L(i)+UO2RL(i))*dt/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsO2L(Protocol prt, Currents cur)
        {
            return cur.CnsO2L + (prt.UO2L + prt.UO2RL) * prt.dt / 3600;
        }


        /// <summary>
        ///	Суммарный расход кислорода через горелки, м3
        /// CnsO2B(i)=CnsOBL(i-1)+(UO2B(i)+UO2RB(i))*dt/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsO2B(Protocol prt, Currents cur)
        {
            return cur.CnsO2B + (prt.UO2B + prt.UO2RB) * prt.dt / 3600;
        }

        /// <summary>
        ///	Суммарный расход природного газа, м3
        /// CnsCH4(i)=CnsCH4(i-1)+(UCH4B(i)+UCH4RB(i))*dt/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsCH4(Protocol prt, Currents cur)
        {
            return cur.CnsCH4 + (prt.UCH4B + prt.UCH4RB) * prt.dt / 3600;
        }

        /// <summary>
        ///	Расход лома, кг
        /// если t = t(Ch1), 
        /// CnsScr(i)=CnsScr(i-1)+MCh1+MCh2(i)*dt 
        /// иначе 
        /// CnsScr(i)=CnsScr(i-1)+MCh2(i)*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsScr(Protocol prt, Currents cur)
        {
            var cnsScr = cur.CnsScr + MR(prt, cur) * prt.dt;
            if (prt.t == prt.Ch1)
            {
                cnsScr = cnsScr + prt.mCh1;
            }
            return cnsScr;
        }

        /// <summary>
        ///	Расход жидкого чугуна, кг
        /// CnsHI(i)=CnsHI(i-1)+mHI(i)*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsHI(Protocol prt, Currents cur)
        {
            return cur.CnsHI + prt.mHI * prt.dt;
        }

        /// <summary>
        ///	Расход порошка углерода, кг
        /// CnsCP(i)=CnsCP(i-1)+mCP(i)*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsCP(Protocol prt, Currents cur)
        {
            return cur.CnsCP + prt.mCP * prt.dt;
        }

        /// <summary>
        ///	Расход извести, кг
        /// CnsLm(i)=CnsLm(i-1)+mLm(i)*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsLm(Protocol prt, Currents cur)
        {
            return cur.CnsLm + mLm(prt) * prt.dt;
        }

        /// <summary>
        ///	 Расход доломита, кг
        /// CnsDlmt(i)=CnsDlmt(i-1)+mDlmt(i)*dt	
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsDlmt(Protocol prt, Currents cur)
        {
            return cur.CnsDlmt + mDlmt(prt) * prt.dt;
        }

        /// <summary>
        ///	Расход кокса, кг
        /// CnsCk(i)=CnsCk(i-1)+mCk(i)*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double CnsCk(Protocol prt, Currents cur)
        {
            return cur.CnsCk + mCk(prt) * prt.dt;
        }

        /// <summary>
        /// Cкорость плавления металла в печи, кг/с
        /// MR(i)=(mStL(i)-mStL(i-1))/dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double MR(Protocol prt, Currents cur)
        {
            return (cur.mStL - cur.mStLPrev) / prt.dt;
        }

        /// <summary>
        /// Масса жидкой ванны в печи, кг
        /// при Tav(i)>TStL:   mStL(i)=mSt(i)	
        /// при Tav(i)>TStL:   mStL(i)=(ECPh(i)-cSl*mSl(i)*Tav(i))/EStL	
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mStL(Currents cur)
        {
            return cur.Tav > Constants.TStL ? cur.mSt : (cur.ECPh - Constants.cSl * cur.mSl * cur.Tav) / Constants.EStL;
        }


        /// <summary>
        /// Масса FeO в шлаке,кг
        /// mFeO(i)=mFeO(i-1)+(nOxRFe(i)+nOxLFe(i)-nRedPFeO(i))*muFeO*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mFeO(Protocol prt, Currents cur)
        {
            return cur.mFeO + (nOxRFe(prt, cur) + nOxLFe(prt, cur) - nRedPFeO(prt, cur)) * Constants.muFeO * prt.dt;
        }

        /// <summary>
        /// Масса MnO в шлаке,кг
        /// mMnO(i)=mMnO(i-1)+(nOxRMn(i)+nOxLMn(i)-nRedPMnO(i))*muMnO*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mMnO(Protocol prt, Currents cur)
        {
            return cur.mMnO + (nOxRMn(prt, cur) + nOxLMn(prt, cur) - nRedPMnO(prt, cur)) * Constants.muMnO * prt.dt;
        }

        /// <summary>
        /// Масса SiO2 в шлаке,кг
        /// mSiO2(i)=mSiO2(i-1)+(nOxRSi(i)+nOxLSi(i)-nRedPSiO2(i))*muSiO2*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mSiO2(Protocol prt, Currents cur)
        {
            return cur.mSiO2 + (nOxRSi(prt, cur) + nOxLSi(prt, cur) - nRedPSiO2(prt, cur)) * Constants.muSiO2 * prt.dt;
        }

        /// <summary>
        /// Масса CaO в шлаке,кг
        /// mCaO(i)=mCaO(i-1)	
        /// присадка извести: mCaO(i)=mCaO(i-1)+mLmAdd+mDlmtAdd	
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mCaO(Protocol prt, Currents cur)
        {
            return cur.mCaO + prt.mLmAdd + prt.mDlmtAdd;
        }

        /// <summary>
        /// Масса углерода в металле, кг
        /// mC(i)=mC(i-1)+(nRedPC(i)-nOxRC(i)-nOxLC(i))*muC*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mC(Protocol prt, Currents cur)
        {
            return cur.mC + (nRedPC(prt, cur) - nOxRC(prt, cur) - nOxLC(prt, cur)) * Constants.muC * prt.dt;
        }

        /// <summary>
        /// Масса марганца в металле, кг
        /// mMn(i)=mMn(i-1)+(nRedPMnO(i)-nOxRMn(i)-nOxLMn(i))*muMn*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mMn(Protocol prt, Currents cur)
        {
            return cur.mMn + (nRedPMnO(prt, cur) - nOxRMn(prt, cur) - nOxLMn(prt, cur)) * Constants.muMn * prt.dt;
        }

        /// <summary>
        /// Масса кремния в металле, кг
        /// mSi(i)=mSi(i-1)+(nRedPSiO2(i)-nOxRSi(i)-nOxLSi(i))*muSi*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <param name="prev">Предыдущие значения параметров</param>
        /// <returns></returns>
        public double mSi(Protocol prt, Currents cur)
        {
            return cur.mSi + (nRedPSiO2(prt, cur) - nOxRSi(prt, cur) - nOxLSi(prt, cur)) * Constants.muSi * prt.dt;
        }

        /// <summary>
        /// Масса кислорода в металле, кг
        /// mO(i)=mO(i-1)+(mSolOR(i)+mSolOL(i))*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mO(Protocol prt, Currents cur)
        {
            return cur.mO + (mSolOR(prt, cur) + mSolOL(prt, cur)) * prt.dt;
        }

        /// <summary>
        /// Масса железа в металле, кг
        /// mFe(i)=mFe(i-1)+(nRedPFeO(i)-nOxRFe(i)-nOxLFe(i))*muFe*dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double mFe(Protocol prt, Currents cur)
        {
            return cur.mFe + (nRedPFeO(prt, cur) - nOxRFe(prt, cur) - nOxLFe(prt, cur)) * Constants.muFe * prt.dt;
        }

        #endregion

        #region Основные параметры. Вспомогательные формулы

        /// <summary>
        /// Интенсивность растворения кислорода в металле при работе горелки RCB в режиме фурмы, кг/с
        /// mSolOR(i)=DOxO(i)*mGRL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double mSolOR(Protocol prt, Currents cur)
        {
            return DOxO(cur) * mGRL(prt);
        }

        /// <summary>
        /// Интенсивность растворения кислорода в металле при работе сводовой фурмы, кг/с
        /// mSolOL(i)=DOxO(i)*mGL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double mSolOL(Protocol prt, Currents cur)
        {
            return DOxO(cur) * mGL(prt);
        }

        /// <summary>
        /// Интенсивность окисления железа при работе горелки RCB в режиме фурмы, моль/с
        /// nOxRFe(i)=DOxFe(i)*mGRL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxRFe(Protocol prt, Currents cur)
        {
           return DOxFe(cur) * mGRL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность окисления углерода при работе горелки RCB в режиме фурмы, моль/с
        /// nOxRC(i)=DOxC(i)*mGRL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxRC(Protocol prt, Currents cur)
        {
           return DOxC(cur) * mGRL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность окисления марганца при работе горелки RCB в режиме фурмы, моль/с
        /// nOxRMn(i)=DOxMn(i)*mGRL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxRMn(Protocol prt, Currents cur)
        {
            return DOxMn(cur) * mGRL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность окисления кремния при работе горелки RCB в режиме фурмы, моль/с
        /// nOxRSi(i)=DOxSi(i)*mGRL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxRSi(Protocol prt, Currents cur)
        {
           return DOxSi(cur) * mGRL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность окисления железа при работе сводовой фурмы, моль/с
        /// nOxLFe(i)=DOxFe(i)*mGL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxLFe(Protocol prt, Currents cur)
        {
            return DOxFe(cur) * mGL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность окисления углерода при работе сводовой фурмы, моль/с
        /// nOxLC(i)=DOxC(i)*mGL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxLC(Protocol prt, Currents cur)
        {
            return DOxC(cur) * mGL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность окисления марганца при работе сводовой фурмы, моль/с
        /// nOxLMn(i)=DOxMn(i)*mGL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxLMn(Protocol prt, Currents cur)
        {
           return DOxMn(cur) * mGL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность окисления кремния при работе сводовой фурмы, моль/с
        /// nOxLSi(i)=DOxSi(i)*mGL(i)/muO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nOxLSi(Protocol prt, Currents cur)
        {
            return DOxSi(cur) * mGL(prt) / Constants.muO;
        }

        /// <summary>
        /// Интенсивность вдувания газов через горелку RCB в режиме фурмы, кг/с
        /// mGRL(i)=mO2RL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mGRL(Protocol prt)
        {
            return mO2RL(prt);
        }

        /// <summary>
        /// Интенсивность вдувания O2 через горелку RCB в режиме фурмы, кг/с
        /// mO2RL(i)=2*UO2RL(i)*muO/(Vmol*3600)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mO2RL(Protocol prt)
        {
            return 2 * prt.UO2RL * Constants.muO / (Constants.Vmol * 3600);
        }

        /// <summary>
        /// Интенсивность вдувания газов через сводовую фурму, кг/с
        /// mGL(i)=mO2L(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mGL(Protocol prt)
        {
            return mO2L(prt);
        }

        /// <summary>
        /// Интенсивность вдувания O2 через сводовую фурму, кг/с
        /// mO2L(i)=
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mO2L(Protocol prt)
        {
            return 2 * prt.UO2L * Constants.muO / (Constants.Vmol * 3600);
        }

        /// <summary>
        /// Интенсивность восстановления FeO из шлака при работе угольного инжектора, моль/с
        /// nRedPFeO(i)=DRedFeO(i)*mCP(i)/muC
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nRedPFeO(Protocol prt, Currents cur)
        {
           return DRedFeO(cur) * prt.mCP / Constants.muC;
        }

        /// <summary>
        /// Интенсивность восстановления MnO из шлака при работе угольного инжектора, моль/с
        /// nRedPMnO(i)=DRedMnO(i)*mCP(i)/muC
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nRedPMnO(Protocol prt, Currents cur)
        {
            return DRedMnO(cur) * prt.mCP / Constants.muC;
        }

        /// <summary>
        /// Интенсивность восстановления SiO2 из шлака при работе угольного инжектора, моль/с
        /// nRedPSiO2(i)=DRedSiO2(i)*mCP(i)/muC
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nRedPSiO2(Protocol prt, Currents cur)
        {
            return DRedSiO2(cur) * prt.mCP / Constants.muC;
        }

        /// <summary>
        /// Интенсивность растворения вдуваемого углерода в металле при работе угольного инжектора, моль/с
        /// nRedPC(i)=DRedC(i)*mCP(i)/muC
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nRedPC(Protocol prt, Currents cur)
        {
            return DRedC(cur) * prt.mCP / Constants.muC;
        }

        /// <summary>
        /// Коэффициент распределения кислорода на окисление железа
        /// DOxFe(i)=GOxFeO(i)/(GOxFeO(i)+GOxCO(i)+GOxMnO(i)+GOxSiO2(i)+GOxO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DOxFe(Currents cur)
        {
            return GOxFeO(cur) / (GOxFeO(cur) + GOxCO(cur) + GOxMnO(cur) + GOxSiO2(cur) + GOxO(cur));
        }

        /// <summary>
        /// Коэффициент распределения кислорода на окисление углерода
        /// DOxC(i)=GOxCO(i)/(GOxFeO(i)+GOxCO(i)+GOxMnO(i)+GOxSiO2(i)+GOxO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DOxC(Currents cur)
        {
            return GOxCO(cur) / (GOxFeO(cur) + GOxCO(cur) + GOxMnO(cur) + GOxSiO2(cur) + GOxO(cur));
        }

        /// <summary>
        /// Коэффициент распределения кислорода на окисление марганца
        /// DOxMn(i)=GOxMnO(i)/(GOxFeO(i)+GOxCO(i)+GOxMnO(i)+GOxSiO2(i)+GOxO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DOxMn(Currents cur)
        {
            return GOxMnO(cur) / (GOxFeO(cur) + GOxCO(cur) + GOxMnO(cur) + GOxSiO2(cur) + GOxO(cur));
        }

        /// <summary>
        /// Коэффициент распределения кислорода на окисление кремния
        /// DOxSi(i)=GOxSiO2(i)/(GOxFeO(i)+GOxCO(i)+GOxMnO(i)+GOxSiO2(i)+GOxO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DOxSi(Currents cur)
        {
            return GOxSiO2(cur) / (GOxFeO(cur) + GOxCO(cur) + GOxMnO(cur) + GOxSiO2(cur) + GOxO(cur));
        }

        /// <summary>
        /// Доля кислорода, растворившегося в металле
        /// DOxO(i)=GOxO(i)/(GOxFeO(i)+GOxCO(i)+GOxMnO(i)+GOxSiO2(i)+GOxO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DOxO(Currents cur)
        {
            return GOxO(cur) / (GOxFeO(cur) + GOxCO(cur) + GOxMnO(cur) + GOxSiO2(cur) + GOxO(cur));
        }

        /// <summary>
        /// Коэффициент расходования порошка углерода на восстановление FeO из шлака
        /// DRedFeO(i)=GRedFeO(i)/(GRedFeO(i)+GRedMnO(i)+GRedSiO2(i)+GRedC(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DRedFeO(Currents cur)
        {
            return GRedFeO(cur) / (GRedFeO(cur) + GRedMnO(cur) + GRedSiO2(cur) + GRedC(cur));
        }

        /// <summary>
        /// Коэффициент расходования порошка углерода на восстановление MnO из шлака
        /// DRedMnO(i)=GRedMnO(i)/(GRedFeO(i)+GRedMnO(i)+GRedSiO2(i)+GRedC(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DRedMnO(Currents cur)
        {
            return GRedMnO(cur) / (GRedFeO(cur) + GRedMnO(cur) + GRedSiO2(cur) + GRedC(cur));
        }

        /// <summary>
        /// Коэффициент расходования порошка углерода на восстановление SiO2 из шлака
        /// DRedSiO2(i)=GRedSiO2(i)/(GRedFeO(i)+GRedMnO(i)+GRedSiO2(i)+GRedC(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DRedSiO2(Currents cur)
        {
           return GRedSiO2(cur) / (GRedFeO(cur) + GRedMnO(cur) + GRedSiO2(cur) + GRedC(cur));
        }

        /// <summary>
        /// Коэффициент расходования порошка углерода на растворение углерода в металле
        /// DRedC(i)=GRedC(i)/(GRedFeO(i)+GRedMnO(i)+GRedSiO2(i)+GRedC(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double DRedC(Currents cur)
        {
            return GRedC(cur) / (GRedFeO(cur) + GRedMnO(cur) + GRedSiO2(cur) + GRedC(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции окисления Fe, Дж/моль
        /// GOxFeO(i)=-R*Tav(i)*ln(KeOxFeO(i)/KfOxFeO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GOxFeO(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeOxFeO(cur) / KfOxFeO(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции окисления C, Дж/моль
        /// GOxCO(i)=-R*Tav(i)*ln(KeOxCO(i)/KfOxCO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GOxCO(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeOxCO(cur) / KfOxCO(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции окисления Mn, Дж/моль
        /// GOxMnO(i)=-R*Tav(i)*ln(KeOxMnO(i)/KfOxMnO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GOxMnO(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeOxMnO(cur) / KfOxMnO(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции окисления Si, Дж/моль
        /// GOxSiO2(i)=-R*Tav(i)*ln(KeOxSiO2(i)/KfOxSiO2(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GOxSiO2(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeOxSiO2(cur) / KfOxSiO2(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции растворения кислорода в металле, Дж/моль
        /// GOxO(i)=-R*Tav(i)*ln(KeOxO(i)/KfOxO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GOxO(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeOxO(cur) / KfOxO(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции восстановления FeO из шлака при вдувании порошка углерода, Дж/моль
        /// GRedFeO(i)=-R*Tav(i)*ln(KeRedFeO(i)/KfRedFeO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GRedFeO(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeRedFeO(cur) / KfRedFeO(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции восстановления MnO из шлака при вдувании порошка углерода, Дж/моль
        /// GRedMnO(i)=-R*Tav(i)*ln(KeRedMnO(i)/KfRedMnO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GRedMnO(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeRedMnO(cur) / KfRedMnO(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции восстановления SiO2 из шлака при вдувании порошка углерода, Дж/моль
        /// GRedSiO2(i)=-R*Tav(i)*ln(KeRedSiO2(i)/KfRedSiO2(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GRedSiO2(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeRedSiO2(cur) / KfRedSiO2(cur));
        }

        /// <summary>
        /// Изменение энергии Гиббса реакции растворения С при вдувании порошка углерода, Дж/моль
        /// GRedC(i)=-R*Tav(i)*ln(KeRedC(i)/KfRedC(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GRedC(Currents cur)
        {
            return -Constants.R * cur.Tav * Math.Log(KeRedC(cur) / KfRedC(cur));
        }

        /// <summary>
        /// Константа равновесия реакции окисления железа
        /// KeOxFeO(i)=exp(-GstOxFeO(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeOxFeO(Currents cur)
        {
            return Math.Exp(-GstOxFeO(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции окисления углерода
        /// KeOxCO(i)=exp(-GstOxCO(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeOxCO(Currents cur)
        {
            return Math.Exp(-GstOxCO(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции окисления марганца
        /// KeOxMnO(i)=exp(-GstOxMnO(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeOxMnO(Currents cur)
        {
            return Math.Exp(-GstOxMnO(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции окисления кремния
        /// KeOxSiO2(i)=exp(-GstOxSiO2(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeOxSiO2(Currents cur)
        {
            return Math.Exp(-GstOxSiO2(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции растворения кислорода
        /// KeOxO(i)=exp(-GstOxO(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeOxO(Currents cur)
        {
            return Math.Exp(-GstOxO(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции восстановления FeO из шлака при вдувании порошка углерода
        /// KeRedFeO(i)=exp(-GstRedFeO(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeRedFeO(Currents cur)
        {
            return Math.Exp(-GstRedFeO(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции восстановления MnO из шлака при вдувании порошка углерода
        /// KeRedMnO(i)=exp(-GstRedMnO(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeRedMnO(Currents cur)
        {
            return Math.Exp(-GstRedMnO(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции восстановления SiO2 из шлака при вдувании порошка углерода
        /// KeRedSiO2(i)=exp(-GstRedSiO2(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeRedSiO2(Currents cur)
        {
            return Math.Exp(-GstRedSiO2(cur) / (cur.Tav * Constants.R));
        }

        /// <summary>
        /// Константа равновесия реакции растворения C при вдувании порошка углерода
        /// KeRedC(i)=exp(-GstRedC(i)/(Tav(i)*R))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KeRedC(Currents cur)
        {
            return Math.Exp(-GstRedC(cur) / (cur.Tav * Constants.R));
        }
        /// <summary>
        /// Функция фактического состояния реакции окисления Fe
        /// KfOxFeO(i)=aFeO(i)/PO2^0,5
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfOxFeO(Currents cur)
        {
            return aFeO(cur) / Math.Sqrt(Constants.PO2);
        }

        /// <summary>
        /// Функция фактического состояния реакции окисления C
        /// KfOxCO(i)=PCO/(aC(i)*PO2^0,5)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfOxCO(Currents cur)
        {
            return Constants.PCO / (aC(cur) * Math.Sqrt(Constants.PO2));
        }

        /// <summary>
        /// Функция фактического состояния реакции окисления Mn
        /// KfOxMnO(i)=aMnO(i)/(aMn(i)*PO2^0,5)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfOxMnO(Currents cur)
        {
            return aMnO(cur) / (aMn(cur) * Math.Sqrt(Constants.PO2));
        }

        /// <summary>
        /// Функция фактического состояния реакции окисления Si
        /// KfOxSiO2(i)=aSiO2(i)/(aSi(i)*PO2^0,5)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfOxSiO2(Currents cur)
        {
            return aSiO2(cur) / (aSi(cur) * Math.Sqrt(Constants.PO2));
        }

        /// <summary>
        /// Функция фактического состояния реакции растворения O
        /// KfOxO(i)=aO/PO2^0,5
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfOxO(Currents cur)
        {

            return aO(cur) / Math.Sqrt(Constants.PO2);
        }

        /// <summary>
        /// Функция фактического состояния реакции восстановления FeO из шлака при вдувании порошка углерода
        /// KfRedFeO(i)=PCO/aFeO(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfRedFeO(Currents cur)
        {
            return Constants.PCO / aFeO(cur);
        }

        /// <summary>
        /// Функция фактического состояния реакции восстановления MnO из шлака при вдувании порошка углерода
        /// KfRedMnO(i)=aMn(i)*PCO/aMnO(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfRedMnO(Currents cur)
        {
            return aMn(cur) * Constants.PCO / aMnO(cur);
        }

        /// <summary>
        /// Функция фактического состояния реакции восстановления SiO2 из шлака при вдувании порошка углерода
        /// KfRedSiO2(i)=aSi(i)*PCO^2/aSiO2(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfRedSiO2(Currents cur)
        {
            return aSi(cur) * Math.Pow(Constants.PCO, 2) / aSiO2(cur);
        }

        /// <summary>
        /// Функция фактического состояния реакции растворения C при вдувании порошка углерода
        /// KfRedC(i)=aC(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double KfRedC(Currents cur)
        {
            return aC(cur);
        }

        /// <summary>
        /// Стандартная энергия Гиббса восстановления FeO из шлака, Дж/моль
        /// GstRedFeO(i)=HRedFeO-136,77*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstRedFeO(Currents cur)
        {
            return Constants.HRedFeO - 136 / 77 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса восстановления MnO из шлака, Дж/моль
        /// GstRedMnO(i)=HRedMnO-212,87*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstRedMnO(Currents cur)
        {
            return Constants.HRedMnO - 212.87 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса восстановления SiO2 из шлака, Дж/моль
        /// GstRedSiO2(i)=HRedSiO2-382,86*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstRedSiO2(Currents cur)
        {
            return Constants.HRedSiO2 - 382.86 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса растворения C в металле, Дж/моль
        /// GstRedC(i)=HRedC-42,3*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstRedC(Currents cur)
        {
            return Constants.HRedC - 42.3 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса окисления Fe, Дж/моль
        /// GstOxFeO(i)=-HOxFeO+53*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstOxFeO(Currents cur)
        {
            return -Constants.HOxFeO + 53 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса окисления C, Дж/моль
        /// GstOxCO(i)=-HOxCO-41,47*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstOxCO(Currents cur)
        {
            return -Constants.HOxCO - 41.47 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса окисления Mn, Дж/моль
        /// GstOxMnO(i)=-HOxMnO+129,1*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstOxMnO(Currents cur)
        {
            return -Constants.HOxMnO + 129.1 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса окисления Si, Дж/моль
        /// GstOxSiO2(i)=-HOxSiO2+215,32*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstOxSiO2(Currents cur)
        {
            return -Constants.HOxSiO2 + 215.32 * cur.Tav;
        }

        /// <summary>
        /// Стандартная энергия Гиббса растворения O, Дж/моль
        /// GstOxO(i)=-HOxO-2,89*Tav(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double GstOxO(Currents cur)
        {
            return -Constants.HOxO - 2.89 * cur.Tav;
        }

        /// <summary>
        /// Активность углерода в металле
        /// aC(i)=fC(i)*CC(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double aC(Currents cur)
        {
            return fC(cur) * cur.CC;
        }

        /// <summary>
        /// Активность марганца в металле
        /// aMn(i)=fMn(i)*CMn(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double aMn(Currents cur)
        {
            return fMn(cur) * cur.CMn;
        }

        /// <summary>
        /// Активность кремния в металле
        /// aSi(i)=fSi(i)*CSi(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double aSi(Currents cur)
        {
            return fSi(cur) * cur.CSi;
        }

        /// <summary>
        /// Активность кислорода в металле
        /// aO(i)=fO(i)*CO(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double aO(Currents cur)
        {
            return fO(cur) * cur.CO;
        }

        /// <summary>
        /// Активность FeO в шлаке
        /// aFeO(i)=xFe(i)*xO
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double aFeO(Currents cur)
        {
            return xFe(cur) * Constants.xO;
        }

        /// <summary>
        /// Активность MnO в шлаке
        /// aMnO(i)=xMn(i)*xO
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double aMnO(Currents cur)
        {
            return xMn(cur) * Constants.xO;
        }

        /// <summary>
        /// Активность SiO2 в шлаке
        /// aSiO2(i)=xSi(i)*xO
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double aSiO2(Currents cur)
        {
            return xSi(cur) * Constants.xO;
        }

        /// <summary>
        /// Коэффициент активности углерода в металле
        /// fC(i)=10^(eCC(i)*CC(i)+eCMn*CMn(i)+eCSi(i)*Csi(i)+eCO*CO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double fC(Currents cur)
        {
            return Math.Pow(10, eCC(cur)*cur.CC + Constants.eCMn*cur.CMn + eCSi(cur)*cur.CSi + Constants.eCO*cur.CO);
        }

        /// <summary>
        /// Коэффициент активности марганца в металле
        /// fMn(i)=10^()
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double fMn(Currents cur)
        {
            return Math.Pow(10,
                            Constants.eMnC*cur.CC + Constants.eMnMn*cur.CMn + Constants.eMnSi*cur.CSi +
                            Constants.eMnO*cur.CO);
        }

        /// <summary>
        /// Коэффициент активности кремния в металле
        /// fSi(i)=10^(eSiC(i)*CC(i)+eSiMn*CMn(i)+eSiSi(i)*Csi(i)+eSiO*CO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double fSi(Currents cur)
        {
            return Math.Pow(10, eSiC(cur)*cur.CC + Constants.eSiMn*cur.CMn + eSiSi(cur)*cur.CSi + Constants.eSiO*cur.CO);
        }

        /// <summary>
        /// Коэффициент активности кислорода в металле
        /// fO(i)=10^(eOC*CC(i)+eOMn*CMn(i)+eOSi*CSi(i)+eOO(i)*CO(i))
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double fO( Currents cur)
        {
            return Math.Pow(10, Constants.eOC*cur.CC + Constants.eOMn*cur.CMn + Constants.eOSi*cur.CSi + eOO(cur)*cur.CO);
        }

        /// <summary>
        /// Параметр взаимодействия C по С
        /// eCC(i)=158/Tav(i)+0,0581
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double eCC(Currents cur)
        {
            return 158 / cur.Tav + 0.0581;
        }

        /// <summary>
        /// Параметр взаимодействия C по Si
        /// eCSi(i)=162/Tav(i)+0,008
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double eCSi(Currents cur)
        {
            return 162 / cur.Tav + 0.008;
        }

        /// <summary>
        /// Параметр взаимодействия Si по С
        /// eSiC(i)=380/Tav(i)-0,023
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double eSiC(Currents cur)
        {
            return 380 / cur.Tav - 0.023;
        }

        /// <summary>
        /// Параметр взаимодействия Si по Si
        /// eSiSi(i)=-34,5/Tav(i)+0,089
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double eSiSi(Currents cur)
        {
            return -34.5 / cur.Tav + 0.089;
        }

        /// <summary>
        /// Параметр взаимодействия O по O
        /// eOO(i)=-1750/Tav(i)+0,734
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double eOO(Currents cur)
        {
            return -1750 / cur.Tav + 0.734;
        }

        /// <summary>
        /// Доля катионов железа в шлаке
        /// xFe(i)=nFe(i)/nSumK(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double xFe(Currents cur)
        {
            return nFe(cur) / nSumK(cur);
        }

        /// <summary>
        /// Доля катионов марганца в шлаке
        /// xMn(i)=nMn(i)/nSumK(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double xMn(Currents cur)
        {
            return nMn(cur) / nSumK(cur);
        }

        /// <summary>
        /// Доля катионов кремния в шлаке
        /// xSi(i)=nSi(i)/nSumK(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double xSi(Currents cur)
        {
            return nSi(cur) / nSumK(cur);
        }

        /// <summary>
        /// Количество моль катионов железа в 100 кг шлака, моль/100 кг
        /// nFe(i)=CFeO(i)/muFeO
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nFe(Currents cur)
        {
            return cur.CFeO / Constants.muFeO;
        }

        /// <summary>
        /// Количество моль катионов марганца в 100 кг шлака, моль/100 кг
        /// nMn(i)=CMnO(i)/muMnO
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nMn( Currents cur)
        {
            return cur.CMnO / Constants.muMnO;
        }

        /// <summary>
        /// Количество моль катионов кремния в 100 кг шлака, моль/100 кг
        /// nSi(i)=CSiO2(i)/muSiO2
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nSi( Currents cur)
        {
            return cur.CSiO2 / Constants.muSiO2;
        }

        /// <summary>
        /// Количество моль катионов кальция в 100 кг шлака, моль/100 кг
        /// nCa(i)=CCaO(i)/muCaO
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nCa( Currents cur)
        {
            return cur.CCaO / Constants.muCaO;
        }

        /// <summary>
        /// Количество моль катионов в 100 кг шлака, моль/100 кг
        /// nSumK(i)=nFe(i)+nMn(i)+nSi(i)+nCa(i)
        /// </summary>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double nSumK(Currents cur)
        {
            return nFe(cur) + nMn(cur) + nSi(cur) + nCa(cur);
        }

        /// <summary>
        /// Энтальпия конденсированных фаз в печи в исходном состоянии, МДж
        /// ECPh(0)=mSt(0)*(cStS*(TStL-Tenv(i))+lmbdSt+cStL*(TH-TStL))+mSl(0)*cSl*TH
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        public double ECPh_0(Protocol prt)
        {
            return prt.mStH *
                   (Constants.cStS * (Constants.TStL - prt.Tenv) + Constants.lmbdSt +
                    Constants.cStL * (prt.TH - Constants.TStL)) + prt.mSlH * Constants.cSl * prt.TH;
        }

        /// <summary>
        /// Энтальпия конденсированных фаз в печи, МДж
        /// ECPh(i)=ECPh(0)+WAirIn(i)+WB(i)+WCh1+WCh2(i)+We(i)+WHI(i)+WHISl(i)+WCk(i)+WSlAdd(i)+WRB(i)+WRL(i)+WL(i)+WP(i)-WHL(i)-WSlTip(i)-WSlTap(i)-WStTap(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double ECPh(Protocol prt, Currents cur)
        {
          return ECPh_0(prt) + WAirIn(prt) + WB(prt) + WCh1(prt) + WCh2(prt, cur) + prt.We + WHI(prt) + WHISl(prt) + WCk(prt) +
                   WSlAdd(prt) + WRB(prt) + WRL(prt, cur) + WL(prt, cur) + WP(prt, cur) - WHL(prt, cur) - WSlTip(prt, cur) - WSlTap(prt, cur) - WStTap(prt, cur);
        }

        /// <summary>
        ///	Физическое тепло подсасываемого воздуха, МВт
        /// WAirIn(i)=mAir(i)*cAirC*Tenv(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WAirIn(Protocol prt)
        {
            return prt.mAir * Constants.cAirC * prt.Tenv;
        }

        /// <summary>
        ///	Мощность горелки, МВт
        /// WB(i)=(rCH4+cNG*Tenv(i))*mCH4B(i)+cOxg*Tenv(i)*mO2B(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WB(Protocol prt)
        {
            return (Constants.rCH4 + Constants.cNG * prt.Tenv) * mCH4B(prt) + Constants.cOxg * prt.Tenv * mO2B(prt);
        }

        /// <summary>
        ///	Интенсивность вдувания CH4 через горелку, кг/с
        /// mO2B(i)=UCH4B(i)*muCH4/(Vmol*3600)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mCH4B(Protocol prt)
        {
            return prt.UCH4B * Constants.muCH4 / (Constants.Vmol * 3600);
        }

        /// <summary>
        ///	Интенсивность вдувания O2 через горелку, кг/с
        /// mO2B(i)=2*UO2B(i)*muO/(Vmol*3600)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mO2B(Protocol prt)
        {
            return 2 * prt.UO2B * Constants.muO / (Constants.Vmol * 3600);
        }

        /// <summary>
        ///	Скорость ввода физического тепла подвалки, МВт
        /// WCh2(i)=EScrS(i)*mCh2(i)/mScrS(i)
        /// Интенсивность опускания лома подвалки из шахты,	кг/с
        /// mCh2(i) = MR(i)  
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WCh2(Protocol prt, Currents cur)
        {
            var pmScrS = mScrS(prt, cur);
            return (int) pmScrS == 0 ? 0 : cur.EScrS * MR(prt, cur) / mScrS(prt, cur);
        }

        /// <summary>
        ///	Масса лома в шахте, кг
        /// mScrS(i)=mCh2S+mCh1SN-mCh1-mCh2(i)
        /// Интенсивность опускания лома подвалки из шахты,	кг/с
        /// mCh2(i) = MR(i)  
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double mScrS(Protocol prt, Currents cur)
        {
            return prt.mCh2S + prt.mCh1SN - prt.mCh1 - MR(prt, cur);
        }

        /// <summary>
        ///	Энтальпия лома в шахте, МДж
        /// EScrS(i)=EScrS(0)+WOffG(i)+WCh2S+WCh1SN-WHLS(i))-WCh1-WCh2(i)
        /// Энтальпия лома в шахте в начале плавки, МДж
        /// EScrS(0)=WCh1SP
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        public double EScrS(Protocol prt, Currents cur)
        {
            return prt.WCh1SP + WOffG(prt, cur) + WCh2S(prt) + WCh1SN(prt) - WHLS(prt, cur) - WCh1(prt) -
                   WCh2(prt, cur);
        }

        /// <summary>
        ///	Скорость ввода физического тепла чугуна, МВт
        /// WHI(i)=mHI(i)*(EIL+cHI*(THI(i)-TIL))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WHI(Protocol prt)
        {
            return mHI(prt) * (Constants.EIL + Constants.cHI * (prt.THI - Constants.TIL));
        }

        /// <summary>
        ///	Интенсивность заливки чугуна, кг/с
        /// mHI(i)=mHI/(t(HIE)-t(HIB))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mHI(Protocol prt)
        {
            if (prt.HIE == 0 && prt.HIB == 0) return 0;
            return prt.mHI / (prt.HIE - prt.HIB);
        }

        /// <summary>
        ///	Скорость ввода физического тепла шлака, содержащегося в чугуновозном ковше, МВт
        /// WHISl(i)=mHISl(i)*cHISl*THI(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WHISl(Protocol prt)
        {
            return prt.mHISl * Constants.cHISl * prt.THI;
        }

        /// <summary>
        ///	Физическое тепло разовой порции УСМ, МВт
        /// WCk(i)=mCk*cCk*Tenv(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCk(Protocol prt)
        {
            return mCk(prt) * Constants.cCk * prt.Tenv;
        }

        /// <summary>
        ///	Физическое тепло разовой порции шлакообразующих материалов, МВт
        /// WSlAdd(i)=WLmAdd(i)+WDlmtAdd(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WSlAdd(Protocol prt)
        {
            return WLmAdd(prt) + WDlmtAdd(prt);
        }

        /// <summary>
        /// Физическое тепло разовой порции извести, МВт
        /// WLmAdd(i)=mLmAdd*cLm*Tenv(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WLmAdd(Protocol prt)
        {
            return mLm(prt) * Constants.cLm * prt.Tenv;
        }

        /// <summary>
        ///	Физическое тепло разовой порции доломита, МВт
        /// WDlmtAdd(i)=mDlmt*cDlmt*Tenv(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WDlmtAdd(Protocol prt)
        {
            return mDlmt(prt) * Constants.cDlmt * prt.Tenv;
        }

        /// <summary>
        ///	Мощность горелки RCB при работе в режиме горелки, МВт
        /// WRB(i)=(rCH4+cNG*Tenv(i))*mCH4RB(i)+cOxg*Tenv(i)*mO2RB(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WRB(Protocol prt)
        {
            return (Constants.rCH4 + Constants.cNG * prt.Tenv) * mCH4RB(prt) + Constants.cOxg * prt.Tenv * mO2RB(prt);
        }

        /// <summary>
        ///	Интенсивность вдувания O2 через горелку RCB в режиме горелки, кг/с
        /// mO2RB(i)=2*UO2RB(i)*muO/(Vmol*3600)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mO2RB(Protocol prt)
        {
            return 2 * prt.UO2RB * Constants.muO / (Constants.Vmol * 3600);
        }

        /// <summary>
        ///	Интенсивность вдувания CH4 через горелку RCB в режиме горелки, кг/с
        /// mO2B(i)=UCH4RB(i)*muCH4/(Vmol*3600)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mCH4RB(Protocol prt)
        {
            return prt.UCH4RB * Constants.muCH4 / (Constants.Vmol * 3600);
        }

        /// <summary>
        ///	Мощность, выделяющаяся при работе горелки RCB в режиме фурмы, МВт
        /// WRL(i)=(nOxRFe(i)*HOxFeO+nOxRC(i)*HOxCO+nOxRMn(i)*HOxMnO+nOxRSi(i)*HOxSiO2+mSolOR(i)*HOxO/muO)*10^(-6)+cOxg*Tenv(i)*mO2RL(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WRL(Protocol prt, Currents cur)
        {
            return (nOxRFe(prt, cur) * Constants.HOxFeO + nOxRC(prt, cur) * Constants.HOxCO +
                    nOxRMn(prt, cur) * Constants.HOxMnO + nOxRSi(prt, cur) * Constants.HOxSiO2 +
                    mSolOR(prt, cur) * Constants.HOxO / Constants.muO) * Math.Pow(10, -6) + Constants.cOxg * prt.Tenv * mO2RL(prt);
        }

        /// <summary>
        ///	Мощность, выделяющаяся при работе сводовой фурмы, МВт
        /// WL(i)=(nOxLFe(i)*HOxFeO+nOxLC(i)*HOxCO+nOxLMn(i)*HOxMnO+nOxLSi(i)*HOxSiO2+mSolOL(i)*HOxO/muO)*10^(-6)+cOxg*Tenv(i)*mO2L(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WL(Protocol prt, Currents cur)
        {
            return (nOxLFe(prt, cur) * Constants.HOxFeO + nOxLC(prt, cur) * Constants.HOxCO +
                    nOxLMn(prt, cur) * Constants.HOxMnO + nOxLSi(prt, cur) * Constants.HOxSiO2 +
                    mSolOL(prt, cur) * Constants.HOxO / Constants.muO) * Math.Pow(10, -6) + Constants.cOxg * prt.Tenv * mO2L(prt);
        }

        /// <summary>
        ///	Мощность, выделяющаяся при работе угольного инжектора, МВт
        /// WP(i)=-(nRedPFeO(i)*HRedFeO+nRedPC(i)*HRedC+nRedPMnO(i)*HRedMnO+nRedPSiO2(i)*HRedSiO2)*10^(-6)+cCP*Tenv(i)*mCP(i)F
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WP(Protocol prt, Currents cur)
        {
            return
                -(nRedPFeO(prt, cur) * Constants.HRedFeO + nRedPC(prt, cur) * Constants.HRedC +
                  nRedPMnO(prt, cur) * Constants.HRedMnO + nRedPSiO2(prt, cur) * Constants.HRedSiO2) * Math.Pow(10, -6) +
                Constants.cCP * prt.Tenv * prt.mCP;
        }

        /// <summary>
        ///	Мощность тепловых потерь печи, МВт
        /// WHL(i)=WCW(i)+WHC(i)+WOffG(i)+WSD(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WHL(Protocol prt, Currents cur)
        {
            return WCW(prt) + WHC(prt) + WOffG(prt, cur) + WSD(prt, cur);
        }

        /// <summary>
        ///	Убыль физического тепла при сливе шлака, МВт
        /// WSlTip(i)=mSlTip(i)*cSl*Tav(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WSlTip(Protocol prt, Currents cur)
        {
            return prt.mSlTip * Constants.cSl * cur.Tav;
        }

        /// <summary>
        ///	Убыль физического тепла при выпуске за счет уменьшения массы шлака, МВт
        /// WSlTap(i)=mSlTap(i)*cSl*Tav(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WSlTap(Protocol prt, Currents cur)
        {
            return prt.mSlTap * Constants.cSl * cur.Tav;
        }

        /// <summary>
        ///	Убыль физического тепла при выпуске за счет уменьшения массы металла, МВт
        /// WStTap(i)=mStTap(i)*cStL*Tav(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WStTap(Protocol prt, Currents cur)
        {
            return prt.mStTap * Constants.cStL * cur.Tav;
        }

        /// <summary>
        ///	Физическое тепло отходящих газов, МВт
        /// WOffG(i)=((mOffCO2B(i)+mOffCO2RB(i))*cCO2+(mOffCOL(i)+mOffCOP(i)+mOffCORL(i))*cCO+(mOffH2OB(i)+mOffH2ORB(i))*cH2O+mAir(i)*cAirH)*Tav(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WOffG(Protocol prt, Currents cur)
        {
            return ((mOffCO2B(prt) + mOffCO2RB(prt)) * Constants.cCO2 +
                    (mOffCOL(prt, cur) + mOffCOP(prt, cur) + mOffCORL(prt, cur)) * Constants.cCO +
                    (mOffH2OB(prt) + mOffH2ORB(prt)) * Constants.cH2O + prt.mAir * Constants.cAirH) * cur.Tav;
        }

        /// <summary>
        ///	Интенсивность образования CO2 при работе горелки, кг/с
        /// mOffCO2B(i)=mCH4B(i)/muCH4*muCO2
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mOffCO2B(Protocol prt)
        {
            return mCH4B(prt) / Constants.muCH4 * Constants.muCO2;
        }

        /// <summary>
        ///	Интенсивность образования CO2 при работе горелки RCB в режиме горелки, кг/с
        /// mOffCO2RB(i)=mCH4RB(i)/muCH4*muCO2
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mOffCO2RB(Protocol prt)
        {
            return mCH4RB(prt) / Constants.muCH4 * Constants.muCO2;
        }

        /// <summary>
        ///	Интенсивность образования CO при работе горелки RCB в режиме фурмы, кг/с
        /// mOffCORL(i)=nOxRC(i)*muCO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double mOffCORL(Protocol prt, Currents cur)
        {
            return nOxRC(prt, cur) * Constants.muCO;
        }

        /// <summary>
        ///	Интенсивность образования CO при работе сводовой фурмы, кг/с
        /// mOffCOL(i)=nOxLC(i)*muCO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double mOffCOL(Protocol prt, Currents cur)
        {
            return nOxLC(prt, cur) * Constants.muCO;
        }

        /// <summary>
        ///	Интенсивность образования CO при работе угольного инжектора, кг/с
        /// mOffCOP(i)=(nRedPFeO(i)+nRedPMnO(i)+nRedPSiO2(i)*2)*muCO
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double mOffCOP(Protocol prt, Currents cur)
        {
            return (nRedPFeO(prt, cur) + nRedPMnO(prt, cur) + nRedPSiO2(prt, cur) * 2) * Constants.muCO;
        }

        /// <summary>
        ///	Интенсивность образования H2O при работе горелки, кг/с
        /// mOffH2OB(i)=2*mCH4B(i)/muCH4*muH2O
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mOffH2OB(Protocol prt)
        {
            return 2 * mCH4B(prt) / Constants.muCH4 * Constants.muH2O;
        }

        /// <summary>
        ///	Интенсивность образования H2O при работе горелки RCB в режиме горелки, кг/с
        /// mOffH2ORB(i)=2*mCH4RB(i)/muCH4*muH2O
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mOffH2ORB(Protocol prt)
        {
            return 2 * mCH4RB(prt) / Constants.muCH4 * Constants.muH2O;
        }

        /// <summary>
        ///	Физическое тепло подвалки, МДж
        /// WCh2S=mCh2S*cStS*Tenv(Ch2S)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCh2S(Protocol prt)
        {
            return prt.mCh2S * Constants.cStS * prt.Tenv;
        }

        /// <summary>
        ///	Физическое тепло завалки на следующую плавку, МДж
        /// WCh1SN=mCh1SN*cStS*Tenv(Ch1SN)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCh1SN(Protocol prt)
        {
            return prt.mCh1SN * Constants.cStS * prt.Tenv;
        }

        /// <summary>
        ///	Мощность тепловых потерь шахты, МВт
        /// WHLS(i)=WOffG(i)*(1-COES(i))+WCWS(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private double WHLS(Protocol prt, Currents cur)
        {
            return WOffG(prt, cur) * (1 - Constants.COES) + WCWS(prt);
        }

        /// <summary>
        ///	Мощность тепловых потерь с охлаждающей водой шахты, МВт
        /// WCWS(i)=mCWS(i)*cCW*(TSout(i)-TSin(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCWS(Protocol prt)
        {
            return mCWS(prt) * Constants.cCW * (prt.TSout - prt.TSin);
        }

        /// <summary>
        ///	Массовый расход охлаждающей воды в контуре охлаждения шахты, кг/с
        /// mCWS(i)=UCWS(i)*roCW/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mCWS(Protocol prt)
        {
            return prt.UCWS * Constants.roCW / 3600;
        }

        /// <summary>
        ///	Мощность тепловых потерь с охлаждающей водой, МВт
        /// WCW(i)=WCWW(i)+WCWF(i)+WCWR(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCW(Protocol prt)
        {
            return WCWW(prt) + WCWF(prt) + WCWR(prt);
        }

        /// <summary>
        ///	Мощность тепловых потерь за счет теплопроводности футеровки, конвекции и излучения с поверхности корпуса печи	МВт
        /// WHC(i)=(WCp(i)+WRp(i))*AShell
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WHC(Protocol prt)
        {
            return (WCp(prt) + WRp(prt)) * Constants.AShell;
        }

        /// <summary>
        ///	Конвективная составляющая теплового потока неводоохлаждаемой части корпуса печи, МВт/м2
        /// WCp(i)=alfSI*(TS(i)-Tenv(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCp(Protocol prt)
        {
            return Constants.alfSI * (prt.TS - prt.Tenv);
        }

        /// <summary>
        ///	Радиационная составляющая теплового потока неводоохлаждаемой части корпуса печи, МВт/м2
        /// WRp(i)=sgm*epsS*(TS(i)^4-Tenv(i)^4)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WRp(Protocol prt)
        {
            return Constants.sgm * Constants.epsS * (Math.Pow(prt.TS, 4) - Math.Pow(prt.Tenv, 4));
        }

        /// <summary>
        ///	Мощность тепловых потерь излучением через рабочее окно, МВт
        /// WSD(i)=sgm*epsSD*(Tav(i)^4-Tenv(i)^4)*ASD*OCSD(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double WSD(Protocol prt, Currents cur)
        {
            return Constants.sgm * Constants.epsSD * (Math.Pow(cur.Tav, 4) - Math.Pow(prt.Tenv, 4)) * Constants.ASD *
                   prt.OCSD;
        }

        /// <summary>
        ///	Мощность тепловых потерь с охлаждающей водой стен, МВт
        /// WCWW(i)=mCWW(i)*cCW*(TWout(i)-TWin(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCWW(Protocol prt)
        {
            return mCWW(prt) * Constants.cCW * (prt.TWout - prt.TWin);
        }

        /// <summary>
        ///	Массовый расход охлаждающей воды в контуре охлаждения стен, кг/с
        /// mCWW(i)=UCWW(i)*roCW/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mCWW(Protocol prt)
        {
            return prt.UCWW * Constants.roCW / 3600;
        }

        /// <summary>
        ///	Мощность тепловых потерь с охлаждающей водой пальцев, МВт
        /// WCWF(i)=mCWF(i)*cCW*(TFout(i)-TFin(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCWF(Protocol prt)
        {
            return mCWF(prt) * Constants.cCW * (prt.TFout - prt.TFin);
        }

        /// <summary>
        ///	Массовый расход охлаждающей воды в контуре охлаждения пальцев, кг/с
        /// mCWF(i)=UCWF(i)*roCW/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mCWF(Protocol prt)
        {
            return prt.UCWF * Constants.roCW / 3600;
        }

        /// <summary>
        ///	Мощность тепловых потерь с охлаждающей водой свода, МВт
        /// WCWR(i)=mCWR(i)*cCW*(TRout(i)-TRin(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCWR(Protocol prt)
        {
            return mCWR(prt) * Constants.cCW * (prt.TRout - prt.TRin);
        }

        /// <summary>
        ///	Массовый расход охлаждающей воды в контуре охлаждения пальцев, кг/с
        /// mCWR(i)=UCWR(i)*roCW/3600
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mCWR(Protocol prt)
        {
            return prt.UCWR * Constants.roCW / 3600;
        }

        /// <summary>
        ///	Интенсивность ввода извести, кг/с
        /// mLm(i)=mLmAdd/(SlAddE-SlAddB)/dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mLm(Protocol prt)
        {
            if (prt.SlAddE == 0 && prt.SlAddB == 0) return 0;
            return prt.mLmAdd / ((prt.SlAddE - prt.SlAddB) * prt.dt);
        }

        /// <summary>
        ///	Интенсивность ввода доломита, кг/с
        /// mDlmt(i)=mDlmtAdd/(SlAddE-SlAddB)/dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mDlmt(Protocol prt)
        {
            if (prt.SlAddE == 0 && prt.SlAddB == 0) return 0;
            return prt.mDlmtAdd / ((prt.SlAddE - prt.SlAddB) * prt.dt);
        }

        /// <summary>
        ///	Интенсивность подачи кокса	кг/с
        /// mCk(i)=mCkAdd/(SlAddE-SlAddB)/dt
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double mCk(Protocol prt)
        {
            if (prt.SlAddE == 0 && prt.SlAddB == 0) return 0;
            return prt.mCkAdd / ((prt.SlAddE - prt.SlAddB) * prt.dt);
        }

        /// <summary>
        ///	Физическое тепло завалки с пальцев, МДж
        /// WCh1=WCh1SP
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <returns></returns>
        private static double WCh1(Protocol prt)
        {
            return prt.WCh1SP;
        }

        #endregion

        #region Основные параметры. Вспомогательные формулы продолжение, где WCh1 уже расчитывается по другому.

        /// <summary>
        ///	Физическое тепло завалки, МДж
        /// WCh1=mCh1*cStS*TScr(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        ///<returns></returns>
        public double WCh1(Protocol prt, Currents cur)
        {
            return prt.mCh1 * Constants.cStS * TScr(prt, cur);
        }

        /// <summary>
        ///	Температура лома в шахте, К
        /// TScr(i)=(ECPhS(i)+cStS*mS(i)*Tenv(i))/(cStS*mS(i))
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private double TScr(Protocol prt, Currents cur)
        {
            return (ECPhS(prt, cur) + Constants.cStS * mS(prt, cur) * prt.Tenv) / (Constants.cStS * mS(prt, cur));
        }

        /// <summary>
        ///	Энтальпия конденсированных фаз в шахте, МДж
        /// ECPhS(i)=WOffG(i)+WCh2S+WCh1SN-WHLS(i))-WCh1-WCh2(i)
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private double ECPhS(Protocol prt, Currents cur)
        {
            return WOffG(prt, cur) + WCh2S(prt) + WCh1SN(prt) - WHLS(prt, cur) - WCh1(prt) - WCh2(prt, cur);
        }

        /// <summary>
        ///	Масса твердых материалов в шахте, кг
        /// mS(i)=mCh2S+mCh1SN-mCh1-mCh2(i)
        /// Интенсивность опускания лома подвалки из шахты,	кг/с
        /// mCh2(i) = MR(i)  
        /// </summary>
        /// <param name="prt">Протокол плавки</param>
        /// <param name="cur">Текущие значения параметров</param>
        /// <returns></returns>
        private static double mS(Protocol prt, Currents cur)
        {
            return prt.mCh2S + prt.mCh1SN - prt.mCh1 - MR(prt, cur);
        }

        #endregion


    }
}

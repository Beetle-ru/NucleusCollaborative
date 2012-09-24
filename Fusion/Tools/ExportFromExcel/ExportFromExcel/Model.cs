using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Emulator
{
    class Model
    {
        private Currents _cur;
        private readonly Calculation _calc;

        public Model(Protocol prt, double pCCH, double pCFeH, double pCMnH, double pCSiH, double pCOH, double pCFeOH, double pCMnOH, double pCSiO2H, double pCCaOH)
        {
            _calc = new Calculation();
            _cur = new Currents();
            _cur.CC = pCCH;
            _cur.CMn = pCMnH;
            _cur.CSi = pCSiH;
            _cur.CO = pCOH;
            _cur.CFeO = pCFeOH;
            _cur.CMnO = pCMnOH;
            _cur.CSiO2 = pCSiO2H;
            _cur.CCaO = pCCaOH;
            _cur.mC = _cur.CC * prt.mStH / 100;
            _cur.mFe = pCFeH * prt.mStH / 100;
            _cur.mMn = _cur.CMn * prt.mStH / 100;
            _cur.mSi = _cur.CSi * prt.mStH / 100;
            _cur.mO = _cur.CO * prt.mStH / 100;
            _cur.mFeO = _cur.CFeO * prt.mSlH / 100;
            _cur.mMnO = _cur.CMnO * prt.mSlH / 100;
            _cur.mSiO2 = _cur.CSiO2 * prt.mSlH / 100;
            _cur.mCaO = _cur.CCaO * prt.mSlH / 100;
            _cur.mSt = prt.mStH;
            _cur.mSl = prt.mSlH;
            _cur.Tav = prt.TH;
            _cur.EScrS = prt.WCh1SP;
            _cur.ECPh = _calc.ECPh_0(prt);
            _cur.mStL = 0;
            _cur.mStLPrev = 0;

        }


        //расчитываем текущие расходы
        private void GetConsumption(Protocol prt)
        {
            _cur.CnsEE = prt.We > 0 ? _calc.CnsEE(prt, _cur) : _cur.CnsEE;
            _cur.CnsCH4 = prt.UCH4B + prt.UCH4RB > 0 ? _calc.CnsCH4(prt, _cur) : _cur.CnsCH4;
            _cur.CnsO2B = prt.UO2B + prt.UO2RB > 0 ? _calc.CnsO2B(prt, _cur) : _cur.CnsO2B;
            _cur.CnsO2L = prt.UO2L + prt.UO2RL > 0 ? _calc.CnsO2L(prt, _cur) : _cur.CnsO2L;
            _cur.CnsCP = prt.mCP > 0 ? _calc.CnsCP(prt, _cur) : _cur.CnsCP;
            _cur.CnsCk = prt.mCkAdd > 0 ? _calc.CnsCk(prt, _cur) : _cur.CnsCk;
            _cur.CnsDlmt = prt.mDlmtAdd > 0 ? _calc.CnsDlmt(prt, _cur) : _cur.CnsDlmt;
            _cur.CnsLm = prt.mLmAdd > 0 ? _calc.CnsLm(prt, _cur) : _cur.CnsLm;
            _cur.CnsHI = prt.mHI > 0 ? _calc.CnsHI(prt, _cur) : _cur.CnsHI;
            _cur.CnsScr = prt.mCh1 + prt.mCh2S > 0 ? _calc.CnsScr(prt, _cur) : _cur.CnsScr;
        }

        private void GetMasses(Protocol prt)
        {
            _cur.mC = _calc.mC(prt, _cur) < 0 ? _cur.mC : _calc.mC(prt, _cur);
            _cur.mFe = _calc.mFe(prt, _cur) < 0 ? _cur.mFe : _calc.mFe(prt, _cur);
            _cur.mMn = _calc.mMn(prt, _cur) < 0 ? _cur.mMn : _calc.mMn(prt, _cur);
            _cur.mSi = _calc.mSi(prt, _cur) < 0 ? _cur.mSi : _calc.mSi(prt, _cur);
            _cur.mO = _calc.mO(prt, _cur) < 0 ? _cur.mO : _calc.mO(prt, _cur);
            _cur.mCaO = _calc.mCaO(prt, _cur) < 0 ? _cur.mCaO : _calc.mCaO(prt, _cur);
            _cur.mFeO = _calc.mFeO(prt, _cur) < 0 ? _cur.mFeO : _calc.mFeO(prt, _cur);
            _cur.mMnO = _calc.mMnO(prt, _cur) < 0 ? _cur.mMnO : _calc.mMnO(prt, _cur);
            _cur.mSiO2 = _calc.mSiO2(prt, _cur) < 0 ? _cur.mSiO2 : _calc.mSiO2(prt, _cur);
            _cur.mSt = _calc.mSt(prt, _cur);
            _cur.mSl = _calc.mSl(prt, _cur);
        }
        
        public Currents GetMainParamsHeat(Protocol prt)
        {

            _cur.mStLPrev = _cur.mStL;
            GetConsumption(prt);
            GetMasses(prt);
            _cur.EScrS = _calc.EScrS(prt, _cur);
            _cur.ECPh = _calc.ECPh(prt, _cur);

            var TavAUX = _cur.Tav;
            while (TavAUX - _cur.Tav > 0 && TavAUX - _cur.Tav < 2)
            {
                TavAUX = _calc.Tav(prt, _cur);
                GetMasses(prt);
                _cur.EScrS = _calc.EScrS(prt, _cur);
                _cur.ECPh = _calc.ECPh(prt, _cur);
                _cur.mStLPrev = _cur.mStL;
                _cur.mStL = _calc.mStL(_cur);
                _cur.CC = _calc.CC(prt, _cur);
                _cur.CMn = _calc.CMn(prt, _cur);
                _cur.CSi = _calc.CSi(prt, _cur);
                _cur.CO = _calc.CO(prt, _cur);
                _cur.CFeO = _calc.CFeO(prt, _cur);
                _cur.CMnO = _calc.CMnO(prt, _cur);
                _cur.CSiO2 = _calc.CSiO2(prt, _cur);
                _cur.CCaO = _calc.CCaO(prt, _cur);
                _cur.Tav = _calc.Tav(prt, _cur);
        
            }

            return _cur;
          
        }


    }
}

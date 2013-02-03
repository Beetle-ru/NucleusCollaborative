using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    public class ChargingOutput
    {
        public double? ReplaceDolomitCoef;
        /// <summary>
        /// ErrCode =
        /// 0 - OK
        /// -1 - Charged more CaO and MgO then requested.
        /// -2 - Charged more CaO then requested.
        /// -3 - Charged more MgO then requested.
        /// </summary>
        public int ErrCode;

        public double ForecastTemperature_C;
        public int OxygenAmountTotalEnd_Nm3;
        public double OxygenAmountTotal1stStep_Nm3;
        public double OxygenAmountC_Nm3;
        public double OxygenAmountSi_Nm3;
        public double OxygenAmountMn_Nm3;
        public double OxygenAmountP_Nm3;
        public double OxygenAmountAl_Nm3;
        public double OxygenAmountFe_Nm3;

        public double H_Vsazka;
        public double H_SZ;
        public double H_Srot;
        public double H_Ocel;
        public double H_Struskotvorne;
        public double H_Struskotvorne_Vystup;
        public double H_Si_oxidace;
        public double H_Mn_oxidace;
        public double H_Al_oxidace;
        public double H_Fe_oxidace;
        public double H_C_oxidace;
        public double H_Koks;
        public double H_Odprasky;

        public double m_lime;
        public double m_dolomite;

        public string SForecastTemperature_C { get { return ForecastTemperature_C.ToString("0.00"); } set { } }
        public string SOxygenAmountTotalEnd_Nm3 { get { return OxygenAmountTotalEnd_Nm3.ToString("0.00"); } set {} }
        public string SOxygenAmountTotal1stStep_Nm3 { get { return OxygenAmountTotal1stStep_Nm3.ToString("0.00"); } set {} }
        public string SOxygenAmountC_Nm3 { get { return OxygenAmountC_Nm3.ToString("0.00"); } set {} }
        public string SOxygenAmountSi_Nm3 { get { return OxygenAmountSi_Nm3.ToString("0.00"); } set {} }
        public string SOxygenAmountMn_Nm3 { get { return OxygenAmountMn_Nm3.ToString("0.00"); } set {} }
        public string SOxygenAmountP_Nm3 { get { return OxygenAmountP_Nm3.ToString("0.00"); } set {} }
        public string SOxygenAmountAl_Nm3 { get { return OxygenAmountAl_Nm3.ToString("0.00"); } set {} }
        public string SOxygenAmountFe_Nm3 { get { return OxygenAmountFe_Nm3.ToString("0.00"); } set {} }

        public string SH_Vsazka { get { return H_Vsazka.ToString("0.00"); } set {} }
        public string SH_SZ { get { return H_SZ.ToString("0.00"); } set {} }
        public string SH_Srot { get { return H_Srot.ToString("0.00"); } set {} }
        public string SH_Ocel { get { return H_Ocel.ToString("0.00"); } set {} }
        public string SH_Struskotvorne { get { return H_Struskotvorne.ToString("0.00"); } set {} }
        public string SH_Struskotvorne_Vystup { get { return H_Struskotvorne_Vystup.ToString("0.00"); } set {} }
        public string SH_Si_oxidace { get { return H_Si_oxidace.ToString("0.00"); } set {} }
        public string SH_Mn_oxidace { get { return H_Mn_oxidace.ToString("0.00"); } set {} }
        public string SH_Al_oxidace { get { return H_Al_oxidace.ToString("0.00"); } set {} }
        public string SH_Fe_oxidace { get { return H_Fe_oxidace.ToString("0.00"); } set {} }
        public string SH_C_oxidace { get { return H_C_oxidace.ToString("0.00"); } set {} }
        public string SH_Koks { get { return H_Koks.ToString("0.00"); } set {} }
        public string SH_Odprasky { get { return H_Odprasky.ToString("0.00"); } set {} }
    }
}

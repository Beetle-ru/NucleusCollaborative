// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:40
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class L1L2_CyclicDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Int32 ConverterNo { get; set; }

        public DateTime TimeProcessed { get; set; }

        public Boolean Considered { get; set; }

        public Nullable<Int32> OxygenFlow_Nm3_min { get; set; }

        public Nullable<Int32> OxygenConsumption_m3 { get; set; }

        public Nullable<Int32> OxygenPressure_MPa { get; set; }

        public Nullable<Int32> LanceDistance_mm { get; set; }

        public Nullable<Int32> WastegasFlow_Nm3_min { get; set; }

        public Nullable<Int32> Wastegas_T_C { get; set; }

        public Nullable<Double> Wastegas_CO_p { get; set; }

        public Nullable<Double> Wastegas_CO2_p { get; set; }

        public Nullable<Double> Wastegas_O2_p { get; set; }

        public Nullable<Double> Wastegas_H2_p { get; set; }

        public Nullable<Double> Wastegas_N2_p { get; set; }

        public Nullable<Double> Wastegas_Ar_p { get; set; }

        public Nullable<Double> H2O_R_Flow { get; set; }

        public Nullable<Double> H2O_R_Pressure { get; set; }

        public Nullable<Double> H2O_R_QInput { get; set; }

        public Nullable<Double> H2O_R_QOutput { get; set; }

        public Nullable<Double> H2O_R_TInput { get; set; }

        public Nullable<Double> H2O_R_TOutput { get; set; }

        public Nullable<Double> H2O_R_Weight { get; set; }

        public Nullable<Double> H2O_R_Crust { get; set; }

        public Nullable<Double> H2O_L_Flow { get; set; }

        public Nullable<Double> H2O_L_Pressure { get; set; }

        public Nullable<Double> H2O_L_QInput { get; set; }

        public Nullable<Double> H2O_L_QOutput { get; set; }

        public Nullable<Double> H2O_L_TInput { get; set; }

        public Nullable<Double> H2O_L_TOutput { get; set; }

        public Nullable<Double> H2O_L_Weight { get; set; }

        public Nullable<Double> H2O_L_Crust { get; set; }
    }
}

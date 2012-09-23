// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:15:08
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MOUT_SlagAnalysisDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Boolean Considered { get; set; }

        public Guid MINP_HeatID { get; set; }

        public Nullable<Double> CaO { get; set; }

        public Nullable<Double> SiO2 { get; set; }

        public Nullable<Double> Cr2O3 { get; set; }

        public Nullable<Double> MnO { get; set; }

        public Nullable<Double> NiO { get; set; }

        public Nullable<Double> P2O5 { get; set; }

        public Nullable<Double> S { get; set; }

        public Nullable<Double> TiO2 { get; set; }

        public Nullable<Double> TiFeO { get; set; }

        public Nullable<Double> CaF2 { get; set; }

        public Nullable<Double> Fe { get; set; }

        public Nullable<Double> FeO { get; set; }

        public Nullable<Double> Al2O3 { get; set; }

        public Nullable<Double> MgO { get; set; }

        public Nullable<Double> CaC2 { get; set; }

        public MINP_HeatDTO MINP_Heat { get; set; }
    }
}

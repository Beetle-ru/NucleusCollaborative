// Generated with EntitiesToDTOs.v2.1 (entitiestodtos.codeplex.com).
// Timestamp: 29.6.2012 - 0:14:51
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public partial class MINP_ModelParametersDTO
    {
        public Guid ID { get; set; }

        public DateTime C__Created { get; set; }

        public Guid MINP_HeatID { get; set; }

        public String Name { get; set; }

        public String Unit { get; set; }

        public String GroupName { get; set; }

        public String Description { get; set; }

        public Double Value { get; set; }

        public MINP_HeatDTO MINP_Heat { get; set; }
    }
}

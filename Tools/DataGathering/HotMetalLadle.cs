using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;


namespace HeatInfo
{
    public class HotMetalLadle
    {
        public int Number { get; set; }
        public DateTime ChargeTime { get; set; }
        public List<HotMetalTorpedo> Torpedes { get; set; }

        public HotMetalLadle()
        {
            Torpedes = new List<HotMetalTorpedo>();
        }

        public int Weight
        {
            get
            {
                if (Torpedes != null && Torpedes.Count > 0)
                {
                    return Torpedes.Sum(p => p.Weight);
                }
                return 0;
            }
        }

        public HotMetalAnalysys Analysys
        {
            get
            {
                if (Torpedes != null && Torpedes.Count > 0)
                {
                    return new HotMetalAnalysys()
                    {
                        C = Torpedes.Average(p => p.Analysys.C),
                        Mn = Torpedes.Average(p => p.Analysys.Mn),
                        P = Torpedes.Average(p => p.Analysys.P),
                        S = Torpedes.Average(p => p.Analysys.S),
                        Si = Torpedes.Average(p => p.Analysys.Si)
                    };
                }
                return new HotMetalAnalysys();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.MainGate
{
    public partial class OffGasEvent
    {
        public OffGasEvent(double H2, double O2, double CO, double CO2, double N2, double Ar)
        {
            this.H2 = H2;
            this.O2 = O2;
            this.CO = CO;
            this.CO2 = CO2;
            this.N2 = N2;
            this.Ar = Ar;
        }
    }
}

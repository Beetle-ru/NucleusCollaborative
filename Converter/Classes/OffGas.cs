using System;

namespace Converter
{
    [Serializable]
    public class OffGas
    {
        public int Id { get; set; }
        public int FusionId { get; set; }
        public DateTime Date { set; get; }
        public double H2 { set; get; }
        public double O2 { set; get; }
        public double CO { set; get; }
        public double CO2 { set; get; }
        public double N2 { set; get; }
        public double Ar { set; get; }
        public int Temperature { set; get; }
        public int Flow { set; get; }
        public string PhaseNo { set; get; }
        public double TemperatureOnExit { get; set; }
        public double PrecollingTemperature { get; set; }
        public double TemperatureAfter1Step { get; set; }
        public double TemperatureAfter2Step { get; set; }     

    }
}





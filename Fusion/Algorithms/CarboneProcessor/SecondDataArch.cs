using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarboneProcessor
{
    class SecondDataArch
    {
        public char Separator                                   { set; get; }
        public string HeatingName                               { set; get; }
        public Int64 NumberHeating                              { set; get; }
        public string TimeHeating                               { set; get; }
        public List<SecondData> SD                              { set; get; }
        
        public SecondDataArch()
        {
            HeatingName = "TRASH/SecondDataArch";
            NumberHeating = -1133;
            TimeHeating = "";
            SD = new List<SecondData>();
        }
        public override string ToString()
        {
            string str="";
            str = String.Format("NumberHeating{0}TimeHeating\n",
                Separator
                );
            str += String.Format("{1}{0}{2}\n",
                Separator,
                NumberHeating,
                TimeHeating
                );
            str += String.Format("Time{0}RemainCarbonPercent%{0}Model{0}CarboneMonoxide%{0}CarboneOxide%{0}HeightLance{0}OxygenVolumeCurrent\n",
                Separator
                );
            SD.ForEach(delegate(SecondData item)
            {
                str += String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}\n",
                    Separator,
                    item.Time.Replace(Separator, '_'),
                    item.CarboneCalc,
                    item.Model.Replace(Separator, '_'),
                    item.CarboneMonoxide,
                    item.CarboneOxide,
                    item.HeightLance,
                    item.OxygenVolumeCurrent
                    );
            });
            return str;
        }
    }
    class SecondData
    {
        public string Time                                      { set; get; }
        public double CarboneCalc                               { set; get; }
        public string Model                                     { set; get; }
        public double CarboneMonoxide                           { set; get; }
        public double CarboneOxide                              { set; get; }
        public int HeightLance                                  { set; get; }
        public double OxygenVolumeCurrent                       { set; get; }
        
        public SecondData()
        {
            Time = "";
            CarboneCalc = -11.33;
            Model = "";
            CarboneMonoxide = -11.33;
            CarboneOxide = -11.33;
            HeightLance = -1133;
            OxygenVolumeCurrent = -11.33;
        }
        
    }
}

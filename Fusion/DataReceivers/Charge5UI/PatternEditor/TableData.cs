using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charge5UI.PatternEditor
{
    public class TableRow
    {
        public double MinSiHotIron { set; get; }
        public double MaxSiHotIron { set; get; }
        public double MinTHotIron { set; get; }
        public double MaxTHotIron { set; get; }
        public double MassHotIron { set; get; }
        public double MassScrap { set; get; }
        public double MassLime { set; get; }
        public double MassDolom { set; get; }
        public double UVSMassDolom { set; get; }
        public double MassFOM { set; get; }
        public double UVSMassFOM { set; get; }
        public double MassDolomS { set; get; }
    }

    public class TableData
    {
        public List<TableRow> Rows = new List<TableRow>();
    }
}

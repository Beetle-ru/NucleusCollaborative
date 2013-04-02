using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EndBlowingTCorrectionTableEditor
{
    public class TableRow {
        public int Item;
        public double CMin { set; get; }
        public double CMax { set; get; }
        public double Oxygen { set; get; }
        public double Heating { set; get; }

        public TableRow() {
            
        }

        public TableRow(TableRow tr) {
            Item = tr.Item;
            CMin = tr.CMin;
            CMax = tr.CMax;
            Oxygen = tr.Oxygen;
            Heating = tr.Heating;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implements {
    public class RollingAverage {
        private class RA_Item {
            public Double val;
            public DateTime time;

            public RA_Item(Double _val) {
                val = _val;
                time = DateTime.Now;
            }

            public RA_Item() {
                val = 0.0;
                time = DateTime.MinValue;
            }
        }

        private int _N_;
        private double _DEFVAL_;
        private RA_Item[] m_buffer;

        public void Reset() {
            for (int i = 0; i < _N_; i++)
                m_buffer[i] = new RA_Item();
        }

        public RollingAverage(int bufsize = 50, double defval = Double.NaN) {
            _N_ = bufsize;
            _DEFVAL_ = defval;
            m_buffer = new RA_Item[_N_];
            Reset();
        }

        private int m_current = 0;

        private int _next(int ix) {
            var next = ix + 1;
            if (next == _N_) next = 0;
            return next;
        }

        private int _prev(int ix) {
            var prev = (ix == 0) ? _N_ - 1 : ix - 1;
            return prev;
        }

        public void Add(Double val) {
            if (Double.IsNaN(val)) return;
            lock (m_buffer) {
                m_buffer[m_current] = new RA_Item(val);
                m_current = _next(m_current);
            }
        }

        public double Average(int intervalSec = -1) {
            lock (m_buffer) {
                DateTime barrier = DateTime.Now.AddSeconds(-intervalSec);
                if (intervalSec <= 0) barrier = DateTime.MinValue;
                Double sum = 0;
                int cnt = 0;
                var ix = _prev(m_current);
                while (m_buffer[ix].time > barrier) {
                    sum += m_buffer[ix].val;
                    ix = _prev(ix);
                    if (++cnt > _N_) break;
                }
                if (cnt == 0) return _DEFVAL_;
                return sum/cnt;
            }
        }
    }
}
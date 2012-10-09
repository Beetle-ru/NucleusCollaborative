using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleRuner
{
    public class MsgLoop
    {
        private List<String> m_msgList = new List<string>();
        public int BufferSize = 1000;
        public void Add(string str)
        {
            if (m_msgList.Count > BufferSize)
            {
                if (m_msgList.Count > 0)
                {
                    m_msgList.RemoveAt(0);
                }
            }
            m_msgList.Add(str);
        }
        public override string ToString()
        {
            return m_msgList.Aggregate("", (current, line) => current + String.Format("{0}\n", line));
        }
    }
}

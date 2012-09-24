using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Converter;

namespace ConnectionProvider
{
    public class FlexHelper
    {
        public FlexEvent evt;
        public FlexHelper(string Operation)
        {
            evt = new FlexEvent(Operation);
        }
        public FlexHelper(FlexEvent evt_)
        {
            evt = evt_;
        }
        public void AddArg(string Key, object Value)
        {
            evt.Arguments.Add(Key, Value);
        }
        public void Fire(Client CoreGate)
        {
            evt.Id = Guid.NewGuid();
            evt.Time = DateTime.Now;
            CoreGate.PushEvent(evt);
        }
        public void ClearArgs()
        {
            evt.Arguments.Clear();
        }
    }
}
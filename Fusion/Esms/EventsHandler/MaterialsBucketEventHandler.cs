using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(MaterialsBucketEvent _event)
        {
            try
            {
                _Module.Heat.MaterialsBucketHistory.Add(_event);
            }
            catch { }
        }
    }
}
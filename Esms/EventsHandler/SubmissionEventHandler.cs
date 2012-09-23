using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(SubmissionEvent _event)
        {
            try
            {
                _Module.Heat.SubmissionHistory.Add(_event);
            }
            catch { }
        }
    }
}
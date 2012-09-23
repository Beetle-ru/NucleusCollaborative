using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(DrivesBunkersEvent _event)
        {
            try
            {
                _Module.Heat.DrivesBunkersHistory.Add(_event);
            }
            catch { }
        }
    }
}
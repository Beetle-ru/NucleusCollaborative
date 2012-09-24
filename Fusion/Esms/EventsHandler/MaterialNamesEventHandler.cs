using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(MaterialNamesEvent _event)
        {
            try
            {
               _Module.Heat.MaterialNamesHistory.Add(_event);
            }
            catch { }
        }
    }
}
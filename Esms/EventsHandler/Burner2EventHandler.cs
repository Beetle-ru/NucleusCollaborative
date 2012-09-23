using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(Burner2Event _event)
        {
            try
            {
                _Module.Heat.Burner2History.Add(_event);
            }
            catch { }
        }
    }
}

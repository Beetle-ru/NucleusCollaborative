using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(Burner1Event _event)
        {
            try
            {
                _Module.Heat.Burner1History.Add(_event);
            }
            catch { }
        }
    }
}
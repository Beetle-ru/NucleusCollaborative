using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(VibratingChannel4Event _event)
        {
            try
            {
                _Module.Heat.VibratingChannel4History.Add(_event);
            }
            catch { }
        }
    }
}
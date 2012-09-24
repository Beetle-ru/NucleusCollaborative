using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(VibratingChannel3Event _event)
        {
            try
            {
                _Module.Heat.VibratingChannel3History.Add(_event);
            }
            catch { }
        }
    }
}
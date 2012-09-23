using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(FurnaceSwitch1Event _event)
        {
            try
            {
                _Module.Heat.FurnaceSwitch1History.Add(_event);
            }
            catch { }
        }
    }
}
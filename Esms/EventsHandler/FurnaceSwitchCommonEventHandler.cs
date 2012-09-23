using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(FurnaceSwitchCommonEvent _event)
        {
            try
            {
                _Module.Heat.FurnaceSwitchCommonHistory.Add(_event);
            }
            catch { }
        }
    }
}
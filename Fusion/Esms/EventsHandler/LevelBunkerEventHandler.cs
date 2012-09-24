using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(LevelBunkerEvent _event)
        {
            try
            {
                _Module.Heat.LevelBunkerHistory.Add(_event);
            }
            catch { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(visBlowingHeatEvent _event)
        {
            
            try
            {
                this._Module._Heat.visBlowingHeatHistory.Add(_event);
                // Тут можно вызвать метод который выберет из базы все данные по плавке,
                // или не все а те которые нужны.
                //Heat heat = DBWorker.Instance.GetHeatInfo(_event.CurrentHeatNumber);
            }
            catch { }
        }

    }
}

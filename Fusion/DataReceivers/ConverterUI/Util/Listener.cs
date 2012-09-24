using CommonTypes;
using ConnectionProvider;
using Converter;
using Implements;
using System;

namespace ConverterUI.Util
{
    /// <summary>
    /// Слушает события ядра и отрабатывает событие, когда что-то пришло
    /// </summary>
    public class Listener : IEventListener
    {
        #region IEventListener Members

        public void OnEvent(BaseEvent e)
        {
            if (e is HeatSchemaStepEvent)
            {
                InstantLogger.log(DateTime.Now.ToLongTimeString() + " step event\r\n");
            }            
            OnEventFired(this, e);
        }

        public delegate void OnEventHandler(object sender, object data);

        public event OnEventHandler OnEventFired;

        #endregion
    }
}

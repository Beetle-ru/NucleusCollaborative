using System;
using CommonTypes;

namespace Converter
{
    public class Module : IModule
    {
        public Type APIType {set; get;}
        public int ConverterNumber { get; private set; }
        public static Module Instance { get; private set; }
        ConverterEventsHandler _eventsHandler;
        public Heat _Heat;

        static Module()
        {
            Instance = null;
        }

        #region IModule Members

        public void Init()
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            ConverterNumber = int.Parse(mainConf.AppSettings.Settings["ConverterNumber"].Value);
            _eventsHandler = new ConverterEventsHandler(this);
            Instance = this;
            APIType = typeof(API.ConverterAPI);
            _Heat = new Heat {AggregateNumber = ConverterNumber};
        }

        public void PushEvent(BaseEvent newEvent)
        {
            _eventsHandler.Process(newEvent);
        }

        #endregion
    }
}

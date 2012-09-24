using System;
using CommonTypes;

namespace Esms
{
    public class Module : IModule
    {
        public int ConverterNumber { get; private set; }
        public static Module Instance { get; private set; }
        EsmsEventsHandler _eventsHandler;
        public Type APIType { set; get; }
        public Heat Heat;

        //public 


        #region IModule Members

        static Module()
        {
            Instance = null;
        }

        public void Init()
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            ConverterNumber = int.Parse(mainConf.AppSettings.Settings["ConverterNumber"].Value);
            _eventsHandler = new EsmsEventsHandler(this);
            Instance = this;
            Heat = new Heat();
        }

        public void PushEvent(BaseEvent newEvent)
        {
            //throw new NotImplementedException();
            _eventsHandler.Process(newEvent);
        }

        #endregion
    }
}

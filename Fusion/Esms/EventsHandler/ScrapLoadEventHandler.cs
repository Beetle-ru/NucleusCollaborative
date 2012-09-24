namespace Esms
{
    partial class EsmsEventsHandler
    {
        public void Process(ScrapLoadEvent _event)
        {
            try
            {
                _Module.Heat.ScrapLoadHistory.Add(_event);
            }
            catch { }
        }
    }
}
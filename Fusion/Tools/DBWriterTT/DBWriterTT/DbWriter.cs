using System;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace DBWriterTT
{
    class DbWriter
    {
       public void Start(string unit)
       {
          var thread = new Thread(() =>
            {
                var events = new EventsListener(unit);
                var mainGate = new ConnectionProvider.Client(unit, events);
                mainGate.Subscribe();
            }) { IsBackground = true };
            thread.Start();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;

namespace FlexEventSaver
{
    class Program
    {
        static ConnectionProvider.Client m_listenGate;
        static void Main(string[] args)
        {
            var o = new TestEvent();
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();
            Console.WriteLine("FlexEventSaver started");
            Console.ReadLine();
        }
    }
}

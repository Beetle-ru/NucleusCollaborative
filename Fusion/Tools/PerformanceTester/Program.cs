using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;

namespace PerformanceTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionProvider.Client mainGate;
            var o = new HeatChangeEvent();
            mainGate = new ConnectionProvider.Client(new Listener());
            mainGate.Subscribe();
            Console.WriteLine("Breake Listenere Started");
            Console.ReadLine();
        }
    }
}

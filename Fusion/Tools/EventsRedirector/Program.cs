using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;

namespace EventsRedirector
{
    class Program
    {
        public static Client MainGateReceiver;
        public static Client MainGateProvider;
        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            MainGateReceiver = new Client("Receiv", new Listener());
            MainGateReceiver.Subscribe();

            MainGateProvider = new Client("Fire");
            Console.WriteLine("Press Enter for exit");
            Console.ReadLine();
        }
    }
}

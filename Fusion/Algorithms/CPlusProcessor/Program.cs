using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;

namespace CPlusProcessor
{
    class Program
    {
        public static Client MainGate;
        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();
            //Iterator.Init();
            Console.WriteLine("Press Enter for exit");
            Console.ReadLine();
        }
    }
}

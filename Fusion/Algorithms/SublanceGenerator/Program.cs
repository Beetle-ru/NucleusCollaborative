using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;

namespace SublanceGenerator {
    internal class Program {
        public static Client MainGate;

        private static void Main(string[] args) {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();
            Iterator.Init();
            Console.WriteLine("Press Enter for exit");
            Console.ReadLine();
        }
    }
}
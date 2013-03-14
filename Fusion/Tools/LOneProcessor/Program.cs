using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using LOneProcessor.SubSystems;

namespace LOneProcessor {
    internal class Program {
        public static Client MainGate;

        private static void Main(string[] args) {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            EventLoop.Init();

            Keeper.SetMainGate(MainGate);
            EventLoop.HandlerList.Add(Keeper.MainHandler);

            EventLoop.RunLoop();

            Console.WriteLine("Press Enter for exit\n");
            Console.ReadLine();
        }
    }
}
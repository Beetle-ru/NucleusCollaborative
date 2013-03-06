using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using LOneProcessor.SubSystems;

namespace LOneProcessor
{
    class Program
    {
        public static Client MainGate;
        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            EventLoop.Init();

            Keeper.MainGate = MainGate;
            EventLoop.HandlerList.Add(Keeper.Handler);

            EventLoop.RunLoop();

            Console.WriteLine("Press Enter for exit\n");
            Console.ReadLine();
        }
    }
}

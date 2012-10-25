using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CommonTypes;
using System.Configuration;
using Converter;

namespace OPCFlex
{
    class Program
    {

        public static Configuration MainConf;
        public static Type FlexEventType;
        public static ConnectionProvider.Client MainGate;

        static void Main(string[] args)
        {

            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");

            MainGate = new ConnectionProvider.Client(new CoreListener());
            MainGate.Subscribe();
            
            FlexEvent desc = new FlexEvent("sample-flex-event");
            desc.Arguments.Add("first-opc-item","int;PLC1,DB1W1");
            CartridgeElement ce =new CartridgeElement();
            ce.Add(desc);

            Console.ReadLine();
        }
    }
}

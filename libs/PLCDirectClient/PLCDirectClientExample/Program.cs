using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLCDirectClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string IPAdress = args[0];
            int port = int.Parse(args[1]);
            int rack = int.Parse(args[2]);
            int slot = int.Parse(args[3]);


            Nordsteel.Data.PLC.DirectClient client = new Nordsteel.Data.PLC.DirectClient(port, IPAdress, rack, slot);
            if (client.Connect())
            {
                Console.WriteLine("Connected to PLC on {0}:{1}", IPAdress, port);
                Console.WriteLine("Write to PLC ...");

                int writeCount = client.WriteBytes(new byte[] {0x00, 0x01 }, 1, 2);
                Console.WriteLine("{0} bytes writed.", writeCount);

                //byte[] read = new byte[4];
                //read = client.ReadBytes(1, 4, 4);
                //Console.WriteLine("{0} readed from PLC", read[0]);

                client.Disconnect();
            }
            else
            {
                Console.WriteLine("Can't connect to PLC");
            }

            Console.ReadLine();
        }
    }
}

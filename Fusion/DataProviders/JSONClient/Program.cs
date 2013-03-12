using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Threading;
using CommonTypes;
using Converter;
using ConnectionProvider;

namespace JSONClient {
    internal class Program {
        public static ConnectionProvider.Client m_pushGate;
        public static ConnectionProvider.Client m_listenGate;

        private static void Main(string[] args) {
            m_pushGate = new Client();
            // m_pushGate.PushEvent(new BlowingEvent());
            /* MemoryStream stream = new MemoryStream();
            BlowingEvent be = new BlowingEvent() {BlowingFlag = 1,O2TotalVol = 333};

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(be.GetType());
            jsonSerializer.WriteObject(stream, be);

            stream.Position = 0;
            string str = new StreamReader(stream).ReadToEnd();
            Console.WriteLine(str);

            byte[] b = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                b[i] = Convert.ToByte(str.ToCharArray()[i]);
            }

            MemoryStream stream2 = new MemoryStream(b);
            

            stream.Position = 0;
            var des = jsonSerializer.ReadObject(stream);

            Console.WriteLine(des.ToString());

            stream2.Position = 0;
            var des2 = jsonSerializer.ReadObject(stream2);
            Console.WriteLine(des2.ToString());*/
            //UDPTools srv = new UDPTools();
            //srv.StartUDPServer();

            var o = new HeatChangeEvent();
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();

            UDPDataProvider.Init();
            while (true) {
                Thread.Sleep(1000);
                UDPDataProvider.SendMessage(o);
            }
            //UDPMessage m = new UDPMessage();
            //m.PackEventClass(o);
            //var res = m.RestoreEventClass(BaseEvent.GetEvents());
            //Console.WriteLine(res.ToString());

            Console.WriteLine("JSONClient стартовал, нажмите \"Enter\" для выхода");
            Console.ReadLine();
        }
    }
}
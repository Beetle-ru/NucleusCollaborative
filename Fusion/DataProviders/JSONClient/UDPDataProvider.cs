using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using CommonTypes;
using Converter;
using Implements;

namespace JSONClient {
    internal static class UDPDataProvider {
        private static UDPMessage m_message; // = new UDPMessage();
        private static UDPTools m_udpSrv; // = new UDPTools();

        public static void Init() {
            //m_message = new UDPMessage();
            m_udpSrv = new UDPTools(9050);
            m_udpSrv.Subscribe(ReceiveMessage);
            m_udpSrv.StartUDPServer();
        }

        public static void SendMessage(BaseEvent classEvent) {
            m_message = new UDPMessage();
            m_message.PackEventClass(classEvent);
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof (UDPMessage));

            MemoryStream stream = new MemoryStream();
            JSONSerializer.WriteObject(stream, m_message);

            stream.Position = 0;
            m_udpSrv.Send(new StreamReader(stream).ReadToEnd());
        }

        public static int ReceiveMessage(string message) {
            BaseEvent be = new BlowingEvent();
            m_message = new UDPMessage();
            m_message = (UDPMessage) new SerializeTools().RestoreFromString(message, typeof (UDPMessage));
            be = m_message.RestoreEventClass(BaseEvent.GetEvents());

            InstantLogger.log(message, "", InstantLogger.TypeMessage.normal);
            InstantLogger.log(be.ToString(), "", InstantLogger.TypeMessage.normal);
            return 0;
        }
    }
}
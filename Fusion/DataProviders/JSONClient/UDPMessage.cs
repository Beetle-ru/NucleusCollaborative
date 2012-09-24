using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using CommonTypes;
using Converter;

namespace JSONClient
{
    [Serializable]
    class UDPMessage
    {
        private string m_eventClassName;
        private string m_eventClassJSONData;
        public void PackEventClass(BaseEvent classEvent)
        {
            var eventType = classEvent.GetType();
            m_eventClassName = eventType.Name;

            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(classEvent.GetType());

            MemoryStream stream = new MemoryStream();
            JSONSerializer.WriteObject(stream, classEvent);

            stream.Position = 0;
            m_eventClassJSONData = new StreamReader(stream).ReadToEnd();

            //Console.WriteLine(m_eventClassJSONData);
        }
        
        public BaseEvent RestoreEventClass(Type[] assembleTypes)
        {
            BaseEvent be = new BlowingEvent();
            for (int at = 0; at < assembleTypes.Length; at++)
            {
                if (assembleTypes[at].Name == m_eventClassName)
                {
                    be = (BaseEvent)new SerializeTools().RestoreFromString(m_eventClassJSONData, assembleTypes[at]);
                    break;
                }
            }
            return be;
        }
    }
}

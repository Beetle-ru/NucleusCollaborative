using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ConnectionProvider;
using Core;
using Converter;
using CommonTypes;
using ConnectionProvider.MainGate;
using Implements;
using System.Runtime.Serialization.Json;


namespace JSONClient
{
    class Listener : IEventListener
    {
        public Listener()
        {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        ~Listener()
        {
           // logFile.Close();
        }
        public void OnEvent(BaseEvent newEvent)
        {
            //Logger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.error);
           
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(newEvent.GetType());
            
            MemoryStream stream = new MemoryStream();
            jsonSerializer.WriteObject(stream, newEvent);
            
            stream.Position = 0;
            string str = new StreamReader(stream).ReadToEnd();
            
            InstantLogger.log(str, "Received and Serialize", InstantLogger.TypeMessage.normal);
        }
    }
}

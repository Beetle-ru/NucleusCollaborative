using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using CommonTypes;
using Implements;

namespace JSONClient
{
    class SerializeTools
    {
        public object RestoreFromString(string data, Type restoreClassType)
        {
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(restoreClassType);
            byte[] byteData = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                try
                {
                    byteData[i] = Convert.ToByte(data.ToCharArray()[i]);
                }
                catch (Exception e)
                {
                   InstantLogger.err("error convert: {0} ", e.ToString()); 
                    
                }
                
            }

            var stream = new MemoryStream(byteData);
            try
            {
                return JSONSerializer.ReadObject(stream);
            }
            catch (Exception e)
            {
                InstantLogger.err("error serialize: {0} ", e.ToString());
                return null;
            }
            
            
        }
    }
}

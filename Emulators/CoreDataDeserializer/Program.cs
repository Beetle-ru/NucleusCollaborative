using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Converter;


namespace CoreDataDeserializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var binaryFormatter = new BinaryFormatter();
            var file = new FileInfo("h:\\118200.dat");
            var fileStream = file.OpenRead();
            var heat= (Heat) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

namespace Converter.API
{
    sealed class PreMergeToMergedDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            // For each assemblyName/typeName that you want to deserialize to 
            // a different type, set typeToDeserialize to the desired type. 
            String exeAssembly = Assembly.GetExecutingAssembly().FullName;


            // The following line of code returns the type. 
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
                typeName, exeAssembly));

            return typeToDeserialize;
        }
    }
}

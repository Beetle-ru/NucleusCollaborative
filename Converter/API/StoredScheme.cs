using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.API
{
    [Serializable]
    public class StoredScheme
    {
        public comBlowingSchemaEvent[] BlowingSchemas { set; get; }
    }
}

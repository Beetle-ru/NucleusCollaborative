using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nordsteel.Data.Exceptions
{
    class NoDBGroupAttributesException : Exception
    {
        public NoDBGroupAttributesException()
        {
        }

        public override string Message
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return "Для заданного типа отстутствует аттрибут DBGroup" ;
        }
    }
}

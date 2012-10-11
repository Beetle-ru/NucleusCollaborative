using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;

namespace OPCFlex
{
    class CartridgeElement
    {
        public List<Element> ElementList; 
        public CartridgeElement()
        {
            ElementList = new List<Element>();
        }

        public void Add(FlexEvent descriptionEvent)
        {
            ElementList.Add(new Element(descriptionEvent));
        }
    }
}

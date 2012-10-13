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

        public Path FindByHandleClient(int handleClient)
        {
            var p = new Path();
            for (int e = 0; e < ElementList.Count; e++)
            {
                //for (int a = 0; a < ElementList[e].; a++)
                //{
                    
                //}
            }
            return p;
        }
    }
    class Path
    {
        public int ElementId { get; set; }
        public int ArgumentId { get; set; }
        public Path()
        {
            ElementId = -1;
            ArgumentId = -1;
        }
    }
}

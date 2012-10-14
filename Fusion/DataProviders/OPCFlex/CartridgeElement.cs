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

        public Path FindByClientHandle(int clientHandle)
        {
            var p = new Path();
            for (int e = 0; e < ElementList.Count; e++)
            {
                for (int a = 0; a < ElementList[e].ClientHandles.Arguments.Count; a++)
                {
                    if (clientHandle == ElementList[e].GetClientHandle(a))
                    {
                        p.ElementId = e;
                        p.ArgumentId = a;
                        return p;
                    }
                }
            }
            return p;
        }

        public Path FindByServerHandle(int serverHandle)
        {
            var p = new Path();
            for (int e = 0; e < ElementList.Count; e++)
            {
                for (int a = 0; a < ElementList[e].ServerHandles.Arguments.Count; a++)
                {
                    if (serverHandle == ElementList[e].GetServerHandle(a))
                    {
                        p.ElementId = e;
                        p.ArgumentId = a;
                        return p;
                    }
                }
            }
            return p;
        }

        public object GetValueByPath(Path p)
        {
            return ElementList[p.ElementId].ObjEvent.Arguments.ElementAt(p.ArgumentId).Value;
        }

        public bool SetValueByPath(Path p, object value)
        {
            var key = ElementList[p.ElementId].ObjEvent.Arguments.ElementAt(p.ArgumentId).Key;
            if (ElementList[p.ElementId].ObjEvent.Arguments[key].GetType() == value.GetType())
            {
                ElementList[p.ElementId].ObjEvent.Arguments[key] = value;
                return true;
            }
            return false;
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

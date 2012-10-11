using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;

namespace OPCFlex
{
    class Element
    {
        private FlexEvent m_descriptionEvent;
        public FlexEvent ObjEvent;
        public const char Separator = ';';

        public Element(FlexEvent descriptionEvent)
        {
            

            m_descriptionEvent = descriptionEvent;

            ObjEvent = new FlexEvent(m_descriptionEvent.Operation);
            ObjEvent.Flags = m_descriptionEvent.Flags;
            ObjEvent.Id = Guid.NewGuid();
            ObjEvent.Time = DateTime.Now;

            for (int i = 0; i < m_descriptionEvent.Arguments.Count; i++)
            {
                if (m_descriptionEvent.Arguments.ElementAt(i).Value is string)
                {
                    var key = m_descriptionEvent.Arguments.ElementAt(i).Key;
                    if (!ObjEvent.Arguments.ContainsKey(key))
                    {
                        //format description:
                        // type;fullPLCAdress
                        string[] values = ((string)m_descriptionEvent.Arguments.ElementAt(i).Value).Split(Separator);
                        if (values.Count() == 2)
                        {
                            if (values[0] == "int")
                            {
                                int _int = 0;
                                ObjEvent.Arguments.Add(key, _int);
                            } else if (values[0] == "Int64")
                            {
                                Int64 _Int64 = 0;
                                ObjEvent.Arguments.Add(key, _Int64);
                            } else if (values[0] == "double")
                            {
                                double _double = 0.0;
                                ObjEvent.Arguments.Add(key, _double);
                            } else if (values[0] == "float")
                            {
                                float _float = 0.0f;
                                ObjEvent.Arguments.Add(key, _float);
                            } else if (values[0] == "string")
                            {
                                string _string = "";
                                ObjEvent.Arguments.Add(key, _string);
                            } else if (values[0] == "byte")
                            {
                                byte _byte = 0x00;
                                ObjEvent.Arguments.Add(key, _byte);
                            } else if (values[0] == "bool")
                            {
                                bool _bool = false;
                                ObjEvent.Arguments.Add(key, _bool);
                            } else if (values[0] == "char")
                            {
                                char _char = ' ';
                                ObjEvent.Arguments.Add(key, _char);
                            } else
                            {
                                // unknow type
                            }
                        }
                    }
                }
            }
        }

        public bool ThisI(string operation)
        {
            return operation == ObjEvent.Operation;
        }

        public bool ThisI(Guid id)
        {
            return id == ObjEvent.Id;
        }

        public bool ThisI(DateTime time)
        {
            return time == ObjEvent.Time;
        }

        public bool ThisI(FlexEventFlag flags)
        {
            return flags == ObjEvent.Flags;
        }

        public FlexEvent GetDescription()
        {
            return m_descriptionEvent;
        }
        /// <summary>
        /// returned key
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public string FindByAdress(string adress)
        {
            for (int i = 0; i < m_descriptionEvent.Arguments.Count; i++)
            {
                string[] values = ((string)m_descriptionEvent.Arguments.ElementAt(i).Value).Split(Separator);
                if (values[1] == adress)
                {
                    return m_descriptionEvent.Arguments.ElementAt(i).Key;
                }
                
            }
            return "";
        }
    }
}

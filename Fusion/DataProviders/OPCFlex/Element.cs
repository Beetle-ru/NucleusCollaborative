using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;

namespace OPCFlex
{
    class Element
    {
        public readonly FlexEvent DescriptionEvent;
        public FlexEvent ClientHandles;
        public FlexEvent ServerHandles;
        public FlexEvent ObjEvent;
        public const char Separator = ';';
        //public int ClientHandles { get; set; }
        //public int ServerHandles { get; set; }

        public Element(FlexEvent descriptionEvent)
        {

            //ClientHandles = -1;
            //ServerHandles = -1;

            DescriptionEvent = descriptionEvent;

            ObjEvent = new FlexEvent(DescriptionEvent.Operation);
            ObjEvent.Flags = DescriptionEvent.Flags;
            ObjEvent.Id = Guid.NewGuid();
            ObjEvent.Time = DateTime.Now;

            ClientHandles = new FlexEvent(String.Format("ClientHandles.{0}", DescriptionEvent.Operation));
            ClientHandles.Id = Guid.NewGuid();
            ClientHandles.Time = DateTime.Now;

            ServerHandles = new FlexEvent(String.Format("ServerHandles.{0}", DescriptionEvent.Operation));
            ServerHandles.Id = Guid.NewGuid();
            ServerHandles.Time = DateTime.Now;

            for (int i = 0; i < DescriptionEvent.Arguments.Count; i++)
            {
                if (DescriptionEvent.Arguments.ElementAt(i).Value is string)
                {
                    var key = DescriptionEvent.Arguments.ElementAt(i).Key;
                    if (!ObjEvent.Arguments.ContainsKey(key))
                    {
                        //format description:
                        // type;fullPLCAdress
                        string[] values = ((string)DescriptionEvent.Arguments.ElementAt(i).Value).Split(Separator);
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
                                //Console.WriteLine("unknow type");
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < ObjEvent.Arguments.Count; i++)
            {
                int _int = Int32.MinValue;
                var key = ObjEvent.Arguments.ElementAt(i).Key;
                ClientHandles.Arguments.Add(key, _int);
                ServerHandles.Arguments.Add(key, _int);
            }
        }

        public void SetClientHandle(int clientHandle, string key, Logger l)
        {
            if (ClientHandles.Arguments.ContainsKey(key))
            {
                ClientHandles.Arguments[key] = clientHandle;
            }
            else
            {
                l.err("Invalid key <{0}> passed to SetClientHandle", key);   
            }
        }

        public void SetClientHandle(int clientHandle, int id)
        {
            ClientHandles.Arguments[ClientHandles.Arguments.ElementAt(id).Key] = clientHandle;
        }

        public int GetClientHandle(string key)
        {
            int clientHandle = Int32.MinValue;
            if (ClientHandles.Arguments.ContainsKey(key))
            {
                 clientHandle = (int)ClientHandles.Arguments[key];
            }
            return clientHandle;
        }

        public int GetClientHandle(int id)
        {
            int clientHandle = Int32.MinValue;
            if (id < ClientHandles.Arguments.Count)
            {
                var key = (string)ClientHandles.Arguments.ElementAt(id).Value;
                clientHandle = (int)ClientHandles.Arguments[key];
            }
            return clientHandle;
        }

        public void SetServerHandle(int serverHandle, string key)
        {
            if (ServerHandles.Arguments.ContainsKey(key))
            {
                ServerHandles.Arguments[key] = serverHandle;
            }
        }

        public void SetServerHandle(int serverHandle, int id)
        {
            if (id < ServerHandles.Arguments.Count)
            {
                var key = (string)ServerHandles.Arguments.ElementAt(id).Value;
                ServerHandles.Arguments[key] = serverHandle;
            }
        }

        public int GetServerHandle(string key)
        {
            int serverHandle = Int32.MinValue;
            if (ServerHandles.Arguments.ContainsKey(key))
            {
                serverHandle = (int)ServerHandles.Arguments[key];
            }
            return serverHandle;
        }

        public int GetServerHandle(int id)
        {
            int serverHandle = Int32.MinValue;
            if (id < ServerHandles.Arguments.Count)
            {
                var key = (string)ServerHandles.Arguments.ElementAt(id).Value;
                serverHandle = (int)ServerHandles.Arguments[key];
            }
            return serverHandle;
        }

        public bool IsMe(string operation)
        {
            return operation == ObjEvent.Operation;
        }

        public bool IsMe(Guid id)
        {
            return id == ObjEvent.Id;
        }

        public bool IsMe(DateTime time)
        {
            return time == ObjEvent.Time;
        }

        public bool IsMe(FlexEventFlag flags)
        {
            return flags == ObjEvent.Flags;
        }

        public FlexEvent GetDescription()
        {
            return DescriptionEvent;
        }
        /// <summary>
        /// returned key
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public string FindByAdress(string adress)
        {
            for (int i = 0; i < DescriptionEvent.Arguments.Count; i++)
            {
                string[] values = ((string)DescriptionEvent.Arguments.ElementAt(i).Value).Split(Separator);
                if (values[1] == adress)
                {
                    return DescriptionEvent.Arguments.ElementAt(i).Key;
                }
                
            }
            return "";
        }
    }
}

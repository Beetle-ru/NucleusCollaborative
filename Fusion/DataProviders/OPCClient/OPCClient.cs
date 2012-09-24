using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPC.Data;
using OPC.Common;
using OPC.Data.Interface;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using CommonTypes;
using Implements;

namespace OPC
{
    public class Client
    {
        private OpcServer m_OPCServer;

        private bool m_Connected = false;
        private readonly string m_ConfigPath;

        private Dictionary<string, ConnectionProvider.Client> MainGates { get; set; }
        private Dictionary<string, EventsListener> EventsToWrite { get; set; }

        public string OPCServerName { set; get; }
        public string ServerStatus { set; get; }
        private List<Group> m_ClientGroups;
        private List<string> m_AviableEvents;
        public Client(string ServerName, string configPath)
        {
            MainGates = new Dictionary<string, ConnectionProvider.Client>();
            EventsToWrite = new Dictionary<string, EventsListener>();
            this.OPCServerName = ServerName;
            m_OPCServer = new OpcServer();
            m_OPCServer.ShutdownRequested += new ShutdownRequestEventHandler(ShutdownRequested);
            m_ClientGroups = new List<Group>();
            m_AviableEvents = GetAviableEvents();
            m_ConfigPath = configPath;
        }

        private List<string> GetAviableEvents()
        {
            List<string> avEvents = new List<string>();
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            avEvents = mainConf.AppSettings.Settings["AviableEvents"].Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim()).ToList();
            return avEvents;
        }

        public bool Connect()
        {
            try
            {
                m_OPCServer.Connect(OPCServerName);
                Thread.Sleep(100);
                m_OPCServer.SetClientName("OPCClient " + Process.GetCurrentProcess().Id);
                SERVERSTATUS serverStatus;
                m_OPCServer.GetStatus(out serverStatus);
                if (serverStatus != null)
                    ServerStatus = string.Format("Ver:{0}.{1}.{2}", serverStatus.wMajorVersion, serverStatus.wMinorVersion, serverStatus.wBuildNumber);
                InstantLogger.log(string.Format("Connection to OPC {0} resuls in {1}", OPCServerName, ServerStatus), "Connection to OPC", InstantLogger.TypeMessage.caution);
            }
            catch (Exception Exc)
            {
                InstantLogger.log(string.Format("Connection to OPC {0} error -- {1} ", OPCServerName, Exc.Message), "Connection to OPC", InstantLogger.TypeMessage.error);
                return false;
            }
            m_Connected = true;
            return (CreateGroups() && RegisterGroups());
        }

        private bool RegisterGroups()
        {
            if (!m_Connected)
                return false;
            for (int i = 0; i < m_ClientGroups.Count; i++)
            {
                var group = m_ClientGroups[i];
                try
                {
                    OpcGroup opcGroup = m_OPCServer.AddGroup(group.Name, true, 500);
                    OPCItemResult[] arrRes;
                    opcGroup.AddItems(group.ItemDefs.ToArray(), out arrRes);
                    //InstantLogger.log("!!!!!!!!!!!!!!!!!", "CreateGroups", InstantLogger.TypeMessage.death);
                    InstantLogger.log(string.Format("add items generic result {0}", arrRes[0].Error.ToString()), "OPC",
                                       InstantLogger.TypeMessage.important);
                    if (!group.IsWriteble)
                    {
                        opcGroup.DataChanged += new DataChangeEventHandler(GroupDataChange);
                    }
                    else
                    {
                        opcGroup.WriteCompleted += new WriteCompleteEventHandler(WriteDataCompleted);
                        for (int ii = 0; ii < arrRes.Length; ii++)
                        {
                            if (arrRes[ii] != null && group.Points[ii] != null)
                            {
                                if (arrRes[ii].Error == HRESULTS.S_OK)
                                {
                                    group.Points[ii].ServerHandle = arrRes[ii].HandleServer;
                                }
                                else
                                {
                                    group.Points[ii].ServerHandle = -1133;
                                }
                            }
                        }

                    }
                    opcGroup.HandleClient = i;
                    group.ServerGroup = opcGroup;


                    int cancelID;
                    opcGroup.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE, 7788, out cancelID);
                }
                catch (Exception exc)
                {

                    InstantLogger.log(string.Format("OPC Group {0} registration failure -- {1} ", group.Name, exc.Message), "OPC");
                    return false;
                }
            }
            Thread writeThread = new Thread(new ThreadStart(ProcessWritebleEventsAsync));
            writeThread.Start();
            return true;
        }

        private bool CreateGroups()
        {

            m_ClientGroups = Group.LoadGroupsFromXmlFile(m_ConfigPath).Where(p => IsInConfig(p.Destination) && m_AviableEvents.Contains(p.Type.Name)).ToList();
            InstantLogger.log(string.Format("OPC Groups count {0}", m_ClientGroups.Count), "OPC");

            foreach (Group group in m_ClientGroups)
            {
                if (!MainGates.ContainsKey(group.Destination))
                {
                    EventsListener eventListener = new EventsListener();
                    ConnectionProvider.Client mainGateClient = new ConnectionProvider.Client(group.Destination, eventListener);
                    MainGates.Add(group.Destination, mainGateClient);
                    EventsToWrite.Add(group.Destination, eventListener);
                    mainGateClient.Subscribe();

                }
                InstantLogger.log(string.Format("==== GroupName {0} GroupeType={1} GroupLocation={2}", group.Name, group.Type.ToString(), group.Location), "OPC");
                foreach (Point point in group.Points)
                {
                    InstantLogger.log(string.Format("FieldName={0} Location={1} Type={2} OPCLocation={3}", point.FieldName, point.Location, point.Type.ToString(), point.OPCLocation), "OPC");
                }

            }

            return true;
        }

        private void ProcessWritebleEventsAsync()
        {
            while (true)
            {
                foreach (var pair in EventsToWrite)
                {
                    if (pair.Value.Queue.Count > 0)
                    {
                        BaseEvent _event;
                        lock (pair.Value.Queue)
                        {
                            _event = pair.Value.Queue.Dequeue();
                        }
                        
                        Group group = m_ClientGroups.Find(p => p.Type == _event.GetType() && p.Destination == pair.Key);
                        if (group != null && group.IsWriteble)
                        {
                            List<object> values = new List<object>();
                            foreach (var point in group.Points)
                            {
                                object value = _event.GetType().GetProperty(point.FieldName).GetValue(_event, null);
                                if (point.IsBoolean)
                                {
                                    value = ((Byte) value).SetBit(point.BitNumber, (bool) value);
                                }
                                else if (point.Type == typeof(string))
                                {
                                    InstantLogger.log(String.Format("writing string {0} + {2} server handle {3} = {1}", point.Location.ToString(), value.ToString(), point.OPCLocation.ToString(), point.ServerHandle.ToString()));

                                    value = (int) 1133;
                                    //value = ((string) value).Normalize();
                                    /*value = ((string) value).ToCharArray();
                                    short[] str = {0x20,0x20,0x20,0x20,0x20,0x20};
                                    
                                    for (int i =0; i < ((char[])value).Count(); i++)
                                    {
                                        str[i] = (short) ((char[]) value).ElementAt(i);
                                    }
                                    value = str;*/
                                }
                              
                                values.Add(value);
                            }
                            int[] errs;
                           // group.ServerGroup.Write(group.Points.Select(p => p.ServerHandle).ToArray(), values.ToArray(),
                            //                        out errs);
                            int[] srvh = group.Points.Select(p => p.ServerHandle).ToArray();
                            InstantLogger.log(String.Format("srvh 0 = {0}",srvh[0]));
                            group.ServerGroup.Write
                                (srvh, values.ToArray(),
                                                   out errs);

                            InstantLogger.log(string.Format("Write generic result {0}", errs[0]), "OPC",
                                       InstantLogger.TypeMessage.important);
                        }
                        else if (group != null && (!group.IsWriteble))
                        {
                            InstantLogger.log(string.Format("Group {0} is not writable", pair.Key), "OPC",
                                       InstantLogger.TypeMessage.terror);
                            
                        }
                        else
                        {
                            InstantLogger.log(string.Format("Group {0} not found", pair.Key), "OPC",
                                       InstantLogger.TypeMessage.terror);
                        }
                    }
                }
            }
        }


        private bool IsInConfig(string Destination)
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            System.ServiceModel.Configuration.ClientSection clientSection = (System.ServiceModel.Configuration.ClientSection)mainConf.SectionGroups["system.serviceModel"].Sections["client"];
            for (int i = 0; i < clientSection.Endpoints.Count; i++)
            {
                if (clientSection.Endpoints[i].Name == Destination)
                    return true;
            }
            return false;
        }
        private void ShutdownRequested(object sender, ShutdownRequestEventArgs e)
        {
            InstantLogger.log(string.Format("OPC server shuts down because:" + e.shutdownReason), "OPC");
        }

        private void GroupDataChange(object sender, DataChangeEventArgs e)
        {
            try
            {
                // Запоминаем ссылку на событие по groupHandleClient группы
                BaseEvent _event = m_ClientGroups[e.groupHandleClient].Event;
                InstantLogger.log(string.Format("DataChange GroupId={0} GroupName={1} GroupLocation={2}", e.groupHandleClient,
                    m_ClientGroups[e.groupHandleClient].Name, m_ClientGroups[e.groupHandleClient].Location), "OPC");
                bool somethingChanged = false;
                // Пробегаемся рефлекшином по всем полям класса события, для которых пришли данные с контроллера
                // и заполняем их. При этом смотрим были ли какие то изменения в пропертях.
                // Так же если проперть имеет тип байт заполняем его через метод расширения класса Byte
                foreach (OPCItemState state in e.sts)
                {
                    PropertyInfo property = _event.GetType().GetProperty(m_ClientGroups[e.groupHandleClient].Points[state.HandleClient].FieldName);
                    object value = !m_ClientGroups[e.groupHandleClient].Points[state.HandleClient].IsBoolean ? 
                        Client.Convert(state.DataValue, m_ClientGroups[e.groupHandleClient].Points[state.HandleClient].Encoding):
                        ((Byte) state.DataValue).GetBit(m_ClientGroups[e.groupHandleClient].Points[state.HandleClient].BitNumber) ;
                    somethingChanged = value != property.GetValue(_event, null) ? true : false;
                    property.SetValue(_event, value, null);
                }
                _event.Time = DateTime.Now;
                InstantLogger.log(_event.ToString(), "OPC");
                // Проверяем существует ли фильтр для данного события и если существует, то смотрим равняется ли заданная проперть
                // фильтра заданному значению фильтра.
                // По итогам проверки отсылаем копию события либо нет. 
                if (string.IsNullOrEmpty(m_ClientGroups[e.groupHandleClient].FilterPropertyName) ||
                    _event.GetType().GetProperty(m_ClientGroups[e.groupHandleClient].FilterPropertyName).GetValue(_event, null).ToString() == m_ClientGroups[e.groupHandleClient].FilterPropertyValue)
                {
                    // Отсылаем событие только в том случае если хоть одна проперть в нем поменялась либо это самая первая посылка
                    // данного события.
                    if (somethingChanged || m_ClientGroups[e.groupHandleClient].FirstSend)
                    {
                        MainGates[m_ClientGroups[e.groupHandleClient].Destination].PushEvent(_event.ShallowCopy());
                        m_ClientGroups[e.groupHandleClient].FirstSend = false;
                    }
                }
            }
            catch (Exception exc)
            {
                InstantLogger.log(string.Format("Data Change error -- {0}", exc.Message), "OPC");
            }
        }

        private static object Convert(object Data, string EncodingName)
        {
            switch (Data.GetType().Name)
            {
                case "UInt16":
                case "UInt32":
                case "uint": return System.Convert.ToInt32(Data);
                case "SByte[]": return !string.IsNullOrEmpty(EncodingName) ? Encoding.GetEncoding(EncodingName).GetString((byte[])Data) : Encoding.ASCII.GetString((byte[])Data);
                default: return Data;
            }
        }

        private void WriteDataCompleted(object sender, WriteCompleteEventArgs e)
        {
        }

        public void Disconnect()
        {

            foreach (Group group in m_ClientGroups)
            {
                group.ServerGroup.Remove(true);
            }
            m_OPCServer.Disconnect();
        }
    }
}

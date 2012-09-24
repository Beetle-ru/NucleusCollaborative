using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using OPC.Data.Interface;
using OPC.Data;

using OPC.Common;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace OPCTool
{

    enum OPCToolStatus
    {
        NoConnection,
        Connecting,
        Connected,
        CanConnected,
        Reading,
        WriteComplete,
        CanWrite,
    }

    [Serializable]
    class OPCToolManager
    {
        private OPCToolStatus m_Status;
        public OPCToolStatus Status { get { return m_Status; } set { m_Status = value; if (OnCurrentStatusChanged != null) OnCurrentStatusChanged(); } }
        public List<OPCServerInfo> ServerNames { get; set; }

        public OPCServerInfo CurrentOPCServerInfo { get; set; }

        public Dictionary<OPCToolStatus, string> StatusStrings { get; set; }

        public delegate void OnChanged();
        public event OnChanged OnCurrentStatusChanged;

        public delegate void OnOPCDataChanged(object sender, DataChangeEventArgs e);
        public event OnOPCDataChanged OnOPCDataChangedHandler;
        private OpcGroup opcGroup;
        private OpcServer m_OPCServer = new OpcServer();
        private int[] m_serverHandle;
        public OPCToolManager()
        {
            CurrentOPCServerInfo = new OPCServerInfo();
            ServerNames = new List<OPCServerInfo>(); 
            Initialize();

        }
        
        private void Initialize()
        {
            StatusStrings = new Dictionary<OPCToolStatus, string>();
            StatusStrings.Add(OPCToolStatus.NoConnection, "Подключитесь к серверу");
            StatusStrings.Add(OPCToolStatus.Connecting, "Подключение к серверу");
            StatusStrings.Add(OPCToolStatus.Connected, "Вы подключились к серверу ");
            StatusStrings.Add(OPCToolStatus.CanConnected, "Не могу подключиться к серверу");
            StatusStrings.Add(OPCToolStatus.Reading, "Чтение");
            StatusStrings.Add(OPCToolStatus.WriteComplete, "Запись значения успешно");
            StatusStrings.Add(OPCToolStatus.CanWrite, "Не могу записать значение");
        }



        public bool Connect(string OPCServerName)
        {
            try
            {
                m_OPCServer.Connect(OPCServerName);
                Thread.Sleep(100);
                m_OPCServer.SetClientName("OPCClient " + Process.GetCurrentProcess().Id);
                SERVERSTATUS serverStatus;
                m_OPCServer.GetStatus(out serverStatus);

                if (serverStatus != null)
                {
                    CurrentOPCServerInfo.ServerName = OPCServerName;
                    CurrentOPCServerInfo.ServerVersion = string.Format("Ver:{0}.{1}.{2}", serverStatus.wMajorVersion, serverStatus.wMinorVersion, serverStatus.wBuildNumber);
                    return true;
                }

                CurrentOPCServerInfo.ServerName = OPCServerName;
                CurrentOPCServerInfo.ServerVersion = string.Format("Ver:{0}.{1}.{2}", "Test1", "Test2", "Test3");
                return true;
            }
            catch
            {
            }

            return false;
        }

        public bool Disconnect()
        {
            try
            {
                m_OPCServer.Disconnect();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public void Reading(string adress)
        {
            opcGroup = m_OPCServer.AddGroup("testGroup", true, 500);
            OPCItemResult[] arrRes;
            opcGroup.AddItems(new OPCItemDef[] { new OPCItemDef(adress, true, 1, VarEnum.VT_EMPTY) }, out arrRes);
            opcGroup.DataChanged += new DataChangeEventHandler(GroupDataChange);
            int cancelID;
            opcGroup.HandleClient = 1;
            opcGroup.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE, 7788, out cancelID);
        }

        public void StopReading(string adress)
        {
            opcGroup.DataChanged -= new DataChangeEventHandler(GroupDataChange);
            opcGroup.Remove(true);
        }

        private void GroupDataChange(object sender, DataChangeEventArgs e)
        {
            if (OnOPCDataChangedHandler != null)
            {
                OnOPCDataChangedHandler(sender, e);
            }

        }

        public bool Write(string adress, object value)
        {
            opcGroup = m_OPCServer.AddGroup("testGroup", true, 500);
            OPCItemResult[] arrRes;
            opcGroup.AddItems(new OPCItemDef[] { new OPCItemDef(adress, true, 1, VarEnum.VT_EMPTY) }, out arrRes);
            m_serverHandle = arrRes.Select(p => p.HandleServer).ToArray();
            int cancelID;
            opcGroup.HandleClient = 1;
            int[] errors;
            return opcGroup.Write(m_serverHandle, new object[] { value }, out errors);
        }
    }
}

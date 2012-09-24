using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CommonTypes;
using System.ServiceModel;
using System.ServiceModel.Description;
using Core.Exceptions;

namespace Core
{
    public class Core
    {

        private static Core _Core = new Core();

        public static Core Instance { get { return _Core; } }

        private Core() { }

        private IModule _module = null;
        private ServiceHost _host = null;
        private ServiceHost _hostAPI = null;

        public IModule Module { get { return _module; } }

        private int m_Port;
        private int m_PortAPI;
        private string m_Module;
        public void LoadModule(string ModuleName)
        {
            if (ModuleName == "Dummy") return;
            Assembly a = null;
            try
            {
                a = Assembly.LoadFrom(ModuleName);
            }
            catch
            {
                throw new ModuleLoadException(string.Format("Не могу загрузить сборку \"{0}\"", ModuleName));
            }

            Type[] allTypes = a.GetTypes();
            foreach (Type type in allTypes) // ищем во всех классах интерфейс IModule
            {
                Type IModule = type.GetInterface("IModule");
                if (IModule != null)
                {
                    _module = (IModule)Activator.CreateInstance(type);
                    break;
                }
            }

            if (_module == null)
            {
                throw new ModuleLoadException(string.Format("В сборке \"{0}\" не найден интерфейс IModule.", ModuleName));
            }

            _module.Init();

        }

        public void Start(int Port, int PortAPI)
        {
            _host = new ServiceHost(
                typeof(MainGateService),
                new Uri[] { new Uri(string.Format("net.tcp://localhost:{0}", Port)) });
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //smb.HttpGetEnabled = true;
            //smb.HttpGetUrl = new Uri("http://localhost:8001/MainGateService");

            //_host.Description.Behaviors.Add(smb);

            _host.AddServiceEndpoint(typeof(IMainGate),
                new NetTcpBinding(SecurityMode.None), "MainGateService");
            _host.Open();

            // открываем API

            if (_module.APIType != null)
            {
                _hostAPI = new ServiceHost(
                    _module.APIType,
                    new Uri[] { new Uri(string.Format("net.tcp://localhost:{0}", PortAPI)) });
                //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                //smb.HttpGetEnabled = true;
                //smb.HttpGetUrl = new Uri("http://localhost:8001/MainGateServiceAPI");

                //_hostAPI.Description.Behaviors.Add(smb);

                _hostAPI.AddServiceEndpoint(_module.APIType.GetInterfaces()[0],
                    new NetTcpBinding(SecurityMode.None), "MainGateServiceAPI");
                _hostAPI.Open();

            }
        }

        public void Stop()
        {
            _host.Close();
        }

    }
}


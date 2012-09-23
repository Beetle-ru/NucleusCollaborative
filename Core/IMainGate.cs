using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Core;
using System.Runtime.Serialization;
using CommonTypes;

namespace Core
{
    [ServiceContract(Namespace = "http://Core.MainGate", 
        SessionMode = SessionMode.Required, 
        CallbackContract = typeof(IMainGateCallback))]
    public interface IMainGate
    {
        [OperationContract()]
        bool Autentificate(string login, string password);
        [OperationContract()]
        void PushEvent(BaseEvent baseEvent);
        [OperationContract()]
        bool Subscribe();
        [OperationContract()]
        bool Unsubscribe();
    }
}



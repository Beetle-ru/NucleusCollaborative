using System;
using System.ServiceModel;
using CommonTypes;

namespace Core {
    public interface IMainGateCallback {
        [OperationContract(IsOneWay = true)]
        void OnEvent(BaseEvent newEvent);
    }
}
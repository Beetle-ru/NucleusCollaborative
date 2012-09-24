using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes;

namespace ConnectionProvider
{
    public interface IEventListener
    {
        void OnEvent(BaseEvent newEvent);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace CommonTypes
{
    public interface IModule
    {
        void Init(); // Вызывается после загрузки сборки. Тут надо проинициализировать всё необходимое для работы.

        void PushEvent(BaseEvent newEvent); // Отсылка события в моуль.

        Type APIType {set; get;}
    }
}

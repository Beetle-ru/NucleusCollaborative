using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

namespace CommonTypes
{
    [Serializable]
    [DataContract]
    [KnownType("GetEvents")]
    public abstract class BaseEvent
    {
        [DataMember]
        public DateTime Time { set; get; }

        #region GetEvents
        public static Type[] GetEvents()
        {
            /* Так как у нас логика модуля цеха лежит в одной сборке с событиями по этому цеху,
             * находим загруженную сборку логики цеха (определяем по интерфейсу IModule)
             * и тягаем оттуда события
             */ 
            Type[] res = new Type[0];
            try
            {
                res = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.GetTypes().Where(t => t.GetInterface("IModule") != null).Count() != 0)
                    .FirstOrDefault().GetTypes().Where(IsSubClassOfBaseEvent).ToArray();
            }
            catch
            {
            }
            return res;
        }

        private static bool IsSubClassOfBaseEvent(Type TypeToCheck)
        {
            if (TypeToCheck.IsAbstract) return false;

            while (TypeToCheck != typeof(object))
            {
                if (TypeToCheck.BaseType == null) break;

                if (TypeToCheck.BaseType == typeof(BaseEvent))
                {
                    return true;
                }
                TypeToCheck = TypeToCheck.BaseType;
            }
            return false;
        }

        #endregion

        public override string ToString()
        {

            var properties = this.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:", GetType().Name);

            foreach (var property in properties)
            {
                sb.AppendFormat(" {0}={1};", property.Name, property. GetValue(this, null));
            }

            return sb.ToString();
        }

        public BaseEvent ShallowCopy()
        {
            return (BaseEvent)this.MemberwiseClone();
        }

    }
}

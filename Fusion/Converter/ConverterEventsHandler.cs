using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Core;
using CommonTypes;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        private Dictionary<string, MethodInfo> _Methods = new Dictionary<string, MethodInfo>();

        private Module _Module;

        public ConverterEventsHandler(Module module)
        {
            var methods = this.GetType().GetMethods();

            _Module = module;

            foreach (var method in methods)
            {
                if (method.Name == "Process")
                {
                    _Methods.Add(method.GetParameters()[0].ParameterType.Name, method);
                }
            }
        }

        public void Process(BaseEvent _event)
        {
            if (_Methods.ContainsKey(_event.GetType().Name))
            {
                _Methods[_event.GetType().Name].Invoke(this, new object[] { _event });
            }
            else
            {
                //throw new NotImplementedException(string.Format("Метод обработки события {0} не найден.", _event.GetType().Name));
                Console.WriteLine(string.Format("Метод обработки события {0} не найден.", _event.GetType().Name));
            }
        }

    }
}

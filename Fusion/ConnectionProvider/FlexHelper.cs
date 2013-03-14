using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using Converter;

namespace ConnectionProvider {
    /// <summary>
    /// FlexEvent Helper Class
    /// </summary>
    public class FlexHelper {
        public FlexEvent evt;

        /// <summary>
        /// Constructor for newly created FlexEvent
        /// </summary>
        public FlexHelper(string Operation) {
            evt = new FlexEvent(Operation);
        }

        /// <summary>
        /// Constructor for FlexEvent recieved by .OnEvent processor
        /// </summary>
        public FlexHelper(FlexEvent evt_) {
            evt = new FlexEvent(evt_.Operation);
            evt.Flags = evt_.Flags & (~FlexEventFlag.FlexEventCreated);
            evt.Arguments = evt_.Arguments;
            evt.Id = evt_.Id;
            evt.Time = evt_.Time;
        }

        public void Fire(Client CoreGate) {
            evt.Id = Guid.NewGuid();
            evt.Time = DateTime.Now;
            CoreGate.PushEvent(evt);
        }

        public void ClearArgs() {
            evt.Arguments.Clear();
        }

        /// <summary>
        /// </summary>
        public void AddArg(string Key, object Value) {
            evt.Arguments.Add(Key, Value);
        }

        public void AddDbl(string Key, object Value) {
            evt.Arguments.Add(Key, Convert.ToDouble(Value));
        }

        public void AddInt(string Key, object Value) {
            evt.Arguments.Add(Key, Convert.ToInt32(Value));
        }

        public void AddStr(string Key, object Value) {
            evt.Arguments.Add(Key, Convert.ToString(Value));
        }

        /// <summary>
        /// </summary>
        public object GetArg(string Key) {
            if (!evt.Arguments.ContainsKey(Key)) return null;
            return evt.Arguments[Key];
        }

        public double GetDbl(string Key) {
            if (!evt.Arguments.ContainsKey(Key)) return Double.NaN;
            return Convert.ToDouble(evt.Arguments[Key]);
        }

        public int GetInt(string Key) {
            if (!evt.Arguments.ContainsKey(Key)) return Int32.MinValue;
            return Convert.ToInt32(evt.Arguments[Key]);
        }

        public string GetStr(string Key) {
            if (!evt.Arguments.ContainsKey(Key)) return null;
            return Convert.ToString(evt.Arguments[Key]);
        }

        public void AddComplexArg(string Key, object Value) {
            var serializer = new DataContractJsonSerializer(Value.GetType());
            var ms = new MemoryStream();

            serializer.WriteObject(ms, Value);
            ms.Close();
            var arr = ms.ToArray();
            var str = Encoding.UTF8.GetString(arr);

            evt.Arguments.Add(Key, str);
        }

        public object GetComplexArg(string Key, Type ValueType) {
            if (!evt.Arguments.ContainsKey(Key)) return null;
            if (evt.Arguments[Key].GetType() != typeof (string)) return null;

            var serializer = new DataContractJsonSerializer(ValueType);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes((string) evt.Arguments[Key]));

            return serializer.ReadObject(ms);
        }
    }
}
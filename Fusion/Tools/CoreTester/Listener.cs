using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace CoreTester {
    internal class Listener : IEventListener {
        public Listener() {
            //MessageBox.Show("qqq", "ww");
        }

        public void OnEvent(BaseEvent newEvent) {
            if (newEvent is TestEvent) {
                var te = newEvent as TestEvent;
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.ListenerReceive(te); }));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Converter;

namespace WeigherReleaseEventSender {
    public partial class WeigherReleaseSender : Form {
        private static ConnectionProvider.Client m_pushGate;

        public WeigherReleaseSender() {
            m_pushGate = new ConnectionProvider.Client();
            InitializeComponent();
        }

        private void btnReleaseW3_Click(object sender, EventArgs e) {
            m_pushGate.PushEvent(new ReleaseWeigherEvent() {WeigherId = 0});
            if (cbEmul.Checked)
                WeigherEmpty(0);
        }

        private void btnReleaseW4_Click(object sender, EventArgs e) {
            m_pushGate.PushEvent(new ReleaseWeigherEvent() {WeigherId = 1});
            if (cbEmul.Checked)
                WeigherEmpty(1);
        }

        private void btnReleaseW5_Click(object sender, EventArgs e) {
            m_pushGate.PushEvent(new ReleaseWeigherEvent() {WeigherId = 2});
            if (cbEmul.Checked)
                WeigherEmpty(2);
        }

        private void btnReleaseW6_Click(object sender, EventArgs e) {
            m_pushGate.PushEvent(new ReleaseWeigherEvent() {WeigherId = 3});
            if (cbEmul.Checked)
                WeigherEmpty(3);
        }

        private void btnReleaseW7_Click(object sender, EventArgs e) {
            m_pushGate.PushEvent(new ReleaseWeigherEvent() {WeigherId = 4});
            if (cbEmul.Checked)
                WeigherEmpty(4);
        }

        private void WeigherEmpty(int weigherId) {
            var wse = new WeighersStateEvent();
            switch (weigherId) {
                case 0:
                    wse.Weigher3Empty = 1;
                    wse.Weigher3LoadFree = 1;
                    wse.Weigher3UnLoadFree = 0;
                    break;
                case 1:
                    wse.Weigher4Empty = 1;
                    wse.Weigher4LoadFree = 1;
                    wse.Weigher4UnLoadFree = 0;
                    break;
                case 2:
                    wse.Weigher5Empty = 1;
                    wse.Weigher5LoadFree = 1;
                    wse.Weigher5UnLoadFree = 0;
                    break;
                case 3:
                    wse.Weigher6Empty = 1;
                    wse.Weigher6LoadFree = 1;
                    wse.Weigher6UnLoadFree = 0;
                    break;
                case 4:
                    wse.Weigher7Empty = 1;
                    wse.Weigher7LoadFree = 1;
                    wse.Weigher7UnLoadFree = 0;
                    break;
            }
            m_pushGate.PushEvent(wse);
            //WeighersStateEvent;
        }
    }
}
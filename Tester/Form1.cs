using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Converter;

namespace Tester
{
    public partial class Form1 : Form
    {
        ConnectionProvider.Client coreClient = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coreClient.PushEvent(new comO2FlowRateEvent() { O2TotalVol = Int32.Parse(textBox1.Text), SublanceStartO2Vol = Int32.Parse(textBox2.Text) });
            coreClient.PushEvent(new cntO2FlowRateEvent());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            coreClient = new ConnectionProvider.Client();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comBlowingSchemaEvent blow = new comBlowingSchemaEvent();
            blow.LancePositionStep1 = 101;
            blow.LancePositionStep2 = 102;
            blow.LancePositionStep3 = 103;
            blow.LancePositionStep4 = 104;
            blow.LancePositionStep5 = 105;
            blow.LancePositionStep6 = 206;
            blow.LancePositionStep7 = 107;
            blow.LancePositionStep8 = 108;
            blow.LancePositionStep9 = 109;
            blow.LancePositionStep10 = 110;
            blow.LancePositionStep11 = 111;
            blow.LancePositionStep12 = 112;
            blow.LancePositionStep13 = 113;
            blow.LancePositionStep14 = 114;
            blow.LancePositionStep15 = 115;
            blow.LancePositionStep16 = 116;
            blow.LancePositionStep17 = 117;
            blow.LancePositionStep18 = 118;
            blow.LancePositionStep19 = 119;
            blow.LancePositionStep20 = 120;

            blow.O2FlowStep1 = 201;
            blow.O2FlowStep2 = 202;
            blow.O2FlowStep3 = 203;
            blow.O2FlowStep4 = 204;
            blow.O2FlowStep5 = 205;
            blow.O2FlowStep6 = 206;
            blow.O2FlowStep7 = 207;
            blow.O2FlowStep8 = 208;
            blow.O2FlowStep9 = 209;
            blow.O2FlowStep10 = 210;
            blow.O2FlowStep11 = 211;
            blow.O2FlowStep12 = 212;
            blow.O2FlowStep13 = 213;
            blow.O2FlowStep14 = 214;
            blow.O2FlowStep15 = 215;
            blow.O2FlowStep16 = 216;
            blow.O2FlowStep17 = 217;
            blow.O2FlowStep18 = 218;
            blow.O2FlowStep19 = 219;
            blow.O2FlowStep20 = 220;

            blow.O2VolStep1 = 301;
            blow.O2VolStep2 = 302;
            blow.O2VolStep3 = 303;
            blow.O2VolStep4 = 304;
            blow.O2VolStep5 = 305;
            blow.O2VolStep6 = 306;
            blow.O2VolStep7 = 307;
            blow.O2VolStep8 = 308;
            blow.O2VolStep9 = 309;
            blow.O2VolStep10 = 310;
            blow.O2VolStep11 = 311;
            blow.O2VolStep12 = 312;
            blow.O2VolStep13 = 313;
            blow.O2VolStep14 = 314;
            blow.O2VolStep15 = 315;
            blow.O2VolStep16 = 316;
            blow.O2VolStep17 = 317;
            blow.O2VolStep18 = 318;
            blow.O2VolStep19 = 319;
            blow.O2VolStep20 = 320;
            coreClient.PushEvent(blow);
            coreClient.PushEvent(new cntBlowingSchemaEvent());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comAdditionsSchemaEvent com = new comAdditionsSchemaEvent();

            com.Material1Portion1Weight = 101;
            com.Material2Portion1Weight = 102;
            com.Material3Portion1Weight = 103;
            com.Material4Portion1Weight = 104;
            com.Material5Portion1Weight = 105;
            com.Material6Portion1Weight = 106;
            com.Material7Portion1Weight = 107;
            com.Material8Portion1Weight = 108;
            com.Material9Portion1Weight = 109;
            com.Material10Portion1Weight = 110;

            com.Material1Portion2Weight = 201;
            com.Material2Portion2Weight = 202;
            com.Material3Portion2Weight = 203;
            com.Material4Portion2Weight = 204;
            com.Material5Portion2Weight = 205;
            com.Material6Portion2Weight = 206;
            com.Material7Portion2Weight = 207;
            com.Material8Portion2Weight = 208;
            com.Material9Portion2Weight = 209;
            com.Material10Portion2Weight = 210;

            com.Material1Portion3Weight = 301;
            com.Material2Portion3Weight = 302;
            com.Material3Portion3Weight = 303;
            com.Material4Portion3Weight = 304;
            com.Material5Portion3Weight = 305;
            com.Material6Portion3Weight = 306;
            com.Material7Portion3Weight = 307;
            com.Material8Portion3Weight = 308;
            com.Material9Portion3Weight = 309;
            com.Material10Portion3Weight = 310;

            com.O2VolPortion1Material1 = 401;
            com.O2VolPortion1Material2 = 402;
            com.O2VolPortion1Material3 = 403;
            com.O2VolPortion1Material4 = 404;
            com.O2VolPortion1Material5 = 405;
            com.O2VolPortion1Material6 = 406;
            com.O2VolPortion1Material7 = 407;
            com.O2VolPortion1Material8 = 408;
            com.O2VolPortion1Material9 = 409;
            com.O2VolPortion1Material10 = 410;

            com.O2VolPortion2Material1 = 501;
            com.O2VolPortion2Material2 = 502;
            com.O2VolPortion2Material3 = 503;
            com.O2VolPortion2Material4 = 504;
            com.O2VolPortion2Material5 = 505;
            com.O2VolPortion2Material6 = 506;
            com.O2VolPortion2Material7 = 507;
            com.O2VolPortion2Material8 = 508;
            com.O2VolPortion2Material9 = 509;
            com.O2VolPortion2Material10 = 510;

            com.O2VolPortion3Material1 = 601;
            com.O2VolPortion3Material2 = 602;
            com.O2VolPortion3Material3 = 603;
            com.O2VolPortion3Material4 = 604;
            com.O2VolPortion3Material5 = 605;
            com.O2VolPortion3Material6 = 606;
            com.O2VolPortion3Material7 = 607;
            com.O2VolPortion3Material8 = 608;
            com.O2VolPortion3Material9 = 609;
            com.O2VolPortion3Material10 = 610;

            coreClient.PushEvent(com);
            coreClient.PushEvent(new cntAdditionsEvent());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //coreClient.PushEvent(new cntWatchDogEvent());
            coreClient.PushEvent(new cntWatchDogPLC01Event());
        }
    }
}

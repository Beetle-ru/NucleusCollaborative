using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;
using System.Threading;

namespace HeatDataVisualizer
{
    class Listener : IEventListener
    {
        public Listener()
        {
        }
        public void OnEvent(BaseEvent newEvent)
        {
            if (newEvent is BoundNameMaterialsEvent)
            {
                var bnme = newEvent as BoundNameMaterialsEvent;
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.MaterialsFill(bnme); }));
 }

            if (newEvent is SteelMakingPatternEvent)
            {
                var smpe = newEvent as SteelMakingPatternEvent;
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.DataFill(smpe); })); 
            }

            if (
                (newEvent is comJobW3Event) ||
                (newEvent is comJobW4Event) ||
                (newEvent is comJobW5Event) ||
                (newEvent is comJobW6Event) ||
                (newEvent is comJobW7Event)
               )
            {
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.ChangeBunker(newEvent); }));
            }

            if (
                (newEvent is cntWeigher3JobReadyEvent) ||
                (newEvent is cntWeigher4JobReadyEvent) ||
                (newEvent is cntWeigher5JobReadyEvent) ||
                (newEvent is cntWeigher6JobReadyEvent) ||
                (newEvent is cntWeigher7JobReadyEvent)
               )
            {
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.cntJobReady(newEvent); }));
            }
            
            
            if (newEvent is HeatSchemaStepEvent)
            {
                var hsse = newEvent as HeatSchemaStepEvent;
                if (hsse.Step >= 0)
                {
                    Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.BacklightStep(hsse.Step); }));
                }
            }

            if (newEvent is ModeLanceEvent)
            {
                var MLE = newEvent as ModeLanceEvent;
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.SetLanceMode(MLE.LanceMode); }));
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.SetOxygenMode(MLE.O2FlowMode); }));
            }

            if (newEvent is ModeVerticalPathEvent)
            {
                var MVPE = newEvent as ModeVerticalPathEvent;
                Program.MainForm.Invoke(new MethodInvoker(delegate() { Program.MainForm.SetVerticalPathMode(MVPE.VerticalPathMode); }));
            }
        }
    }
}

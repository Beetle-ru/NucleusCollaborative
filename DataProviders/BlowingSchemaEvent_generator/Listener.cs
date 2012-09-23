using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ConnectionProvider;
using Core;
using Converter;
using CommonTypes;
using ConnectionProvider.MainGate;
using Implements;
//using System.ServiceModel;
//using System.Windows.Forms;

namespace BlowingSchemaEvent_generator
{
    class Listener : IEventListener
    {
        //private StreamWriter logFile;
        //string timeLine;
        public Listener()
        {

            //DateTime.Now;
            //timeLine = DateTime.Now.ToString();
            //timeLine = timeLine.Replace(':', '_');
            //timeLine = timeLine.Replace('.', '_');
            //logFile = File.CreateText(@"logs\" + timeLine + "_listen.log");
            //logFile.AutoFlush = true;
           
            /*Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Listener......................................................................................[started]\n");
            Console.Write(timeLine);
            Console.ForegroundColor = ConsoleColor.White;*/
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        ~Listener()
        {
           // logFile.Close();
        }
        public void OnEvent(BaseEvent newEvent)
        {
            System.Threading.Thread.Sleep(3000);
           // InstantLogger.log(newEvent.GetType().GetCustomAttributesData()[5].ToString());
            //InstantLogger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.error);
            if (newEvent is visSpectrluksEvent)
            {
                InstantLogger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.error);
            }
            //var v = (PLCGroup)newEvent.GetType().GetCustomAttributes(false).Where(x => x.GetType().Name == "PLCGroup").FirstOrDefault();
            //object first = null;
   /*         InstantLogger.log(newEvent.GetType().FullName);
            var plcg = new PLCGroup();
            foreach (object x in newEvent.GetType().GetCustomAttributes(false))
            {
                if (x.GetType().Name == "PLCGroup")
                {
                    plcg = (PLCGroup)x;
                    InstantLogger.log("    " + plcg.Location + " -- " + plcg.Destination);
                }
            }
            var plcp = new PLCPoint();
            foreach (var prop in newEvent.GetType().GetProperties())
            {
                foreach (object x in prop.GetCustomAttributes(false))
                {
                    if (x.GetType().Name == "PLCPoint")
                    {

                        if (((PLCPoint)x).IsWritable)
                        {
                            
                            plcp = (PLCPoint)x;
                            //prop.GetValue(newEvent, null);
                            //prop.GetValue(newEvent);
                            InstantLogger.log("        " + prop.Name + " = " + prop.GetValue(newEvent, null).ToString());
                            InstantLogger.log("            IsWritable = " + plcp.IsWritable.ToString());
                            InstantLogger.log("            " + plcp.Location);
                           // break;
                        }
                        
                    }
                }
            }
     */       

            //if (
            //    (newEvent is cntBlowingSchemaEvent) ||
            //    (newEvent is comBlowingSchemaEvent) ||
            //    (newEvent is cntWatchDogPLC01Event) ||
            //    (newEvent is cntWatchDogPLC1Event) ||
            //    (newEvent is cntWatchDogPLC2Event) ||
            //    (newEvent is cntWatchDogPLC3Event) ||
            //    (newEvent is comO2FlowRateEvent) ||
            //   (newEvent is cntO2FlowRateEvent)
            //    )
            //{

            //   // SteelMakingPatternEvent steelMakingPatternEvent = newEvent as SteelMakingPatternEvent;
            //    InstantLogger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.death);
            //}
            //if( 
            //    (newEvent is comAdditionsEvent) ||
            //    (newEvent is comAdditionsSchemaEvent)
            //   )
            //{
            //    InstantLogger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.terror);
            //}
          
            //if (newEvent is HeatSchemaStepEvent)
            //{
            //    InstantLogger.log("step = " + newEvent.ToString(), "Received", InstantLogger.TypeMessage.caution);
            //}
            //if ((newEvent is cntWeigher3JobReadyEvent) ||
            //    (newEvent is cntWeigher4JobReadyEvent)||
            //    (newEvent is cntWeigher5JobReadyEvent)||
            //    (newEvent is cntWeigher6JobReadyEvent)||
            //    (newEvent is cntWeigher7JobReadyEvent))
            //{
            //    InstantLogger.log("step = " + newEvent.ToString(), "Received", InstantLogger.TypeMessage.normal);
            //}
            /*if (newEvent is SteelMakingPatternEvent)
            {

                SteelMakingPatternEvent steelMakingPatternEvent = newEvent as SteelMakingPatternEvent;
                InstantLogger.log(steelMakingPatternEvent.steps[0].Period.ToString(), "Received", InstantLogger.TypeMessage.important);
            }*/
           // lock (Program.consoleLock)
            //{
              /* Console.BackgroundColor = ConsoleColor.Blue;
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine(" " + newEvent.ToString() + "\n");
               Console.BackgroundColor = ConsoleColor.DarkBlue;
               Console.ForegroundColor = ConsoleColor.Gray;

               
               logFile.Write(newEvent.ToString() + "\n");*/
               // InstantLogger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.unimportant);
               
               //logFile.Close();
            //}
            
            //MessageBox.Show("ll", "ll");
        }
    }
}

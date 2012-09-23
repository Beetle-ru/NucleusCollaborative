using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Windows.Forms;
using Converter;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ConverterVisio.MainGate;
using System.Windows;
using CommonTypes;

namespace ConverterVisio
{
    class EventsListener : IMainGateCallback
    {
        MainWindow window;
        bool isBlowing = false;
        int gasCount = 0;

        public EventsListener(MainWindow targetWindow)
        {
            window = targetWindow;
        }

        public void OnEvent(BaseEvent newEvent)
        {
            if (window.StartTime == null)
            {
                window.StartTime = DateTime.Now;
            }
            if (newEvent is ConverterAngleEvent)
            {
                window.UpdateConverterAngle((newEvent as ConverterAngleEvent).Angle);
            }
            if (newEvent is LanceEvent)
            {
                if (isBlowing)
                {
                    if (gasCount == 10)
                    {
                        LanceEvent lEvent = newEvent as LanceEvent;
                        window.AddLancePoints(lEvent);                        
                    }
                }
            }

            if (newEvent is OffGasAnalysisEvent)
            {
                if (isBlowing)
                {
                    if (gasCount == 10)
                    {
                        OffGasAnalysisEvent ogaEvent = newEvent as OffGasAnalysisEvent;
                        window.AddGasPoints(ogaEvent);
                        gasCount = 0;
                    }
                    else
                    {
                        gasCount++;
                    }
                }
            }
            if (newEvent is BlowingEvent)
            {
                BlowingEvent be = newEvent as BlowingEvent;
                if (be.BlowingFlag == 1)
                {
                    isBlowing = true;
                    window.StartBlowingTime = DateTime.Now;
                }
                else
                {
                    isBlowing = false;
                    window.StartBlowingTime = null;
                }
            }

        }

    }
}

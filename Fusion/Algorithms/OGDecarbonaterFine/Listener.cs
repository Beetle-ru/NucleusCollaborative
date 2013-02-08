﻿using System;
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

namespace OGDecarbonaterFine
{
    class Listener : IEventListener
    {

        public Int64 CHeatNumber;
        public int LanceHeithPrevious;
        

        public Listener()
        {
            InstantLogger.log("Listener", "Started\n", InstantLogger.TypeMessage.important);
        }
        public Int64 HeatNumberToShort(Int64 heatNLong)
        {
            Int64 reminder = 0;
            Int64 res = Math.DivRem(heatNLong, 10000, out reminder);
            return res * 1000 + reminder;
        }

        public Int64 HeatNumberToLong(Int64 heatNShort)
        {
            Int64 reminder = 0;
            Int64 res = Math.DivRem(heatNShort, 10000, out reminder);
            return res * 100000 + reminder;
        }
        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("Listener"))
            {
                if (evt is LanceEvent)
                {
                    var le = evt as LanceEvent;

                }
                if (evt is BlowingEvent)
                {
                    var be = evt as BlowingEvent;
                    Iterator.HDSmoother.HeatIsStarted = be.BlowingFlag == 1;
                }
                if (evt is OffGasAnalysisEvent)
                {
                    var ogae = evt as OffGasAnalysisEvent;
                    Iterator.HDSmoother.CO.Add(ogae.CO);
                    Iterator.HDSmoother.CO2.Add(ogae.CO2);
                    Iterator.HDSmoother.N2.Add(ogae.N2);
                    Iterator.HDSmoother.O2.Add(ogae.O2);
                    Iterator.HDSmoother.H2.Add(ogae.H2);
                }
                if (evt is OffGasEvent)
                {
                    var oge = evt as OffGasEvent;
                }
                if (evt is HeatChangeEvent)
                {
                    var hce = evt as HeatChangeEvent;
                    if (CHeatNumber != hce.HeatNumber)
                    {
                        CHeatNumber = hce.HeatNumber;
                        Iterator.Reset();
                        Iterator.CurrentState.HeatNumber = hce.HeatNumber;
                        l.msg("Heat Changed. New Heat ID: {0}\n", Iterator.CurrentState.HeatNumber);
                    }
                    else
                    {
                        l.msg("Heat No Changed. Heat ID: {0}\n", hce.HeatNumber);
                    }
                }

                if (evt is SublanceStartEvent)
                {
                    var sse = evt as SublanceStartEvent;
                    if (sse.SublanceStartFlag == 1)
                    {

                    }
                    if (sse.SublanceStartFlag == 0)
                    {
                        //
                    }
                }

                if (evt is visSpectrluksEvent) // углерод со спектролюкса
                {
                    var vse = evt as visSpectrluksEvent;
                }
            }
        }
    }
}
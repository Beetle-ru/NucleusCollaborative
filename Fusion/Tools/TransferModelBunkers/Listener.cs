using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;
using Core;

namespace TransferModelBunkers
{
    internal class Listener : IEventListener
    {
        public static string ReEncoder(string str)
        {
            char[] charArray = str.ToCharArray();
            str = "";
            foreach (char c in charArray)
            {
                if (c > 127)
                {
                    str += (char)(c - 848);
                }
                else
                {
                    str += c;
                }
            }
            return str;
        }
        public static long HeatNumber = 228223;
        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("ModelRunner::Listener"))
            {
                if (evt is OPCDirectReadEvent)
                {
                    var odr = evt as OPCDirectReadEvent;
                    if (odr.EventName == "HeatChangeEvent")
                    {
                        var hce = new HeatChangeEvent();
                        hce.HeatNumber = /*++*/HeatNumber;
                        Program.m_pushGate.PushEvent(hce);
                    }
                    else if (odr.EventName == "visAdditionTotalEvent")
                    {
                        var vate = new visAdditionTotalEvent();
                        vate.RB5TotalWeight = 5;
                        vate.RB6TotalWeight = 6;
                        vate.RB7TotalWeight = 7;
                        vate.RB8TotalWeight = 8;
                        vate.RB9TotalWeight = 9;
                        vate.RB10TotalWeight = 10;
                        vate.RB11TotalWeight = 11;
                        vate.RB12TotalWeight = 12;
                        Program.m_pushGate.PushEvent(vate);
                    }
                    else if (odr.EventName == "BoundNameMaterialsEvent")
                    {
                        var bnme = new BoundNameMaterialsEvent();
                        bnme.Bunker5MaterialName = ReEncoder("ДОЛОМС");
                        bnme.Bunker6MaterialName = ReEncoder("ALKонц");
                        bnme.Bunker7MaterialName = ReEncoder("KOKS  ");
                        bnme.Bunker8MaterialName = ReEncoder("ИЗВЕСТ");
                        bnme.Bunker9MaterialName = ReEncoder("ИЗВЕСТ");
                        bnme.Bunker10MaterialName = ReEncoder("ФОМ   ");
                        bnme.Bunker11MaterialName = ReEncoder("ДОЛМИТ");
                        bnme.Bunker12MaterialName = ReEncoder("ДОЛОМС");
                        Program.m_pushGate.PushEvent(bnme);
                    }
                }
            }
        }
    }
}

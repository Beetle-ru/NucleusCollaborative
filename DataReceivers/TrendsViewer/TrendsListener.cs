using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrendsViewer.MainGate;
using Core;
using ZedGraph;
using System.Drawing;
using CommonTypes;

namespace Client
{
    class TrendsListener : IMainGateCallback
    {
        #region IEventCallback Members

        public ZedGraphControl zedGraph;
        public List<PointPairList> pointPairLists;
        public int currentSecond = 0;

        public TrendsListener(ZedGraphControl zedGraphParam, List<PointPairList> pointPairListsParam)
        {
            zedGraph = zedGraphParam;
            pointPairLists = pointPairListsParam;
        }

        public void OnEvent(BaseEvent newEvent)
        {
            if (newEvent is OffGasEvent)
            {
                
                OffGasEvent offGasEvent = newEvent as OffGasEvent;
                pointPairLists[0].Add(currentSecond, offGasEvent.H2);
                pointPairLists[1].Add(currentSecond, offGasEvent.CO);
                pointPairLists[2].Add(currentSecond, offGasEvent.CO2);
                pointPairLists[3].Add(currentSecond, offGasEvent.N2);
                pointPairLists[4].Add(currentSecond, offGasEvent.O2);
                pointPairLists[5].Add(currentSecond, offGasEvent.Ar);
                currentSecond++;

                zedGraph.GraphPane.CurveList.Clear();
                zedGraph.GraphPane.AddCurve("H2", pointPairLists[0], Color.Green, SymbolType.None);
                zedGraph.GraphPane.AddCurve("O2", pointPairLists[1], Color.Blue, SymbolType.None);
                zedGraph.GraphPane.AddCurve("CO", pointPairLists[2], Color.Red, SymbolType.None);
                zedGraph.GraphPane.AddCurve("CO2", pointPairLists[3], Color.Orange, SymbolType.None);
                zedGraph.GraphPane.AddCurve("N2", pointPairLists[4], Color.Black, SymbolType.None);
                zedGraph.GraphPane.AddCurve("Ar", pointPairLists[5], Color.Turquoise, SymbolType.None);
                zedGraph.AxisChange();
                zedGraph.Invalidate();

                //TrendsViewer.Trends.ActiveForm.
                // тут добавляем на график по текущей сикунде
            }
        }

        #endregion
    }
}

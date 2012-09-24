using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Implements
{
    public class SimpleGrp
    {
        public List<Curve> Curves;
        public int HorisontalShiftLine = 30;
        public int VerticalShiftLine = 25;
        public Font FontLables;
        private Bitmap DrawArea;
        
        public SimpleGrp(Font _font)
        {
            //Curves = _curves;
            FontLables = _font;
            //DrawArea = new Bitmap(1000, 1000);
        }
        
        private void DrawGraps()
        {
            var g = Graphics.FromImage(DrawArea);

            float yStep = (float)((DrawArea.Height - VerticalShiftLine) * 0.01);
            float xStep = (float)((DrawArea.Width - HorisontalShiftLine) * 0.01);
            int t = 0;
 
            foreach (var curve in Curves)
            {
                if (curve.Length > 1)
                {
                    var points = new PointF[curve.Length];
                    float xValue = 0.0f;
                    float yValue = 0.0f;
                    
                    for (int i = 0; i < curve.Length; i++)
                    {
                        xValue = curve.GetData().XPercent[i];
                        yValue = curve.GetData().YPercent[i];
                        if (xValue > 100)
                        {
                            xValue = 100f;
                        }

                        if (yValue > 100)
                        {
                            yValue = 100f;
                        }

                        if (xValue < 0)
                        {
                            xValue = 0f;
                        }

                        if (yValue < 0)
                        {
                            yValue = 0f;
                        }

                        points[i].X = xValue * xStep + HorisontalShiftLine;
                        points[i].Y = DrawArea.Height - yValue * yStep - VerticalShiftLine;
                        
                    }
                    if (points.Count() > 1)
                    {
                        g.DrawCurve(new Pen(curve.ColorCurve, 3), points);
                    }
                }

            }
        }
        
        private void DrawScalses()
        {
            var g = Graphics.FromImage(DrawArea);
            var lenghtLine = 5;

            var shiftText = 5;
            var vertShiftHorisontalText = 15;
            var percent = 0.0f;

            float yStep = (float)((DrawArea.Height - VerticalShiftLine) * 0.1);

            for (float height = DrawArea.Height - VerticalShiftLine; height > 0; height -= yStep)
            {

                g.DrawString(percent.ToString() + "%", FontLables, new SolidBrush(Color.DarkOrange), 0, height - shiftText);
                percent += 10f;

                g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.Black, Color.Brown), 2), HorisontalShiftLine, height, DrawArea.Width, height);
                g.DrawLine(new Pen(new SolidBrush(Color.Gold), 4), HorisontalShiftLine, height, lenghtLine + HorisontalShiftLine, height);
            }

            float xStep = (float)Math.Round(((DrawArea.Width - HorisontalShiftLine) * 0.1), 7);
            percent = 0.0f;
            for (float width = HorisontalShiftLine; width < DrawArea.Width; width += xStep)
            {
                g.DrawString(percent.ToString() + "%", FontLables, new SolidBrush(Color.DarkOrange), width - shiftText, DrawArea.Height - vertShiftHorisontalText);
                percent += 10f;
                g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.Black, Color.Brown), 2), width, DrawArea.Height - VerticalShiftLine, width, 0);
                g.DrawLine(new Pen(new SolidBrush(Color.Gold), 4), width, DrawArea.Height - VerticalShiftLine, width, DrawArea.Height - VerticalShiftLine - lenghtLine);
            }
        }

        public Bitmap Redraw(int width, int height, List<Curve> _curves)
        {
            DrawArea = new Bitmap(width, height);
            Curves = _curves;
            DrawScalses();
            DrawGraps();
            return DrawArea;
        }
    }
}

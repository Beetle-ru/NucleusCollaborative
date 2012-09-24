using System;

namespace Converter
{
    [Serializable]
    public class ConverterAngle
    {
        public int Date { set; get; }
        public int Value { set; get; }
        public string SectorName(int converterNumber)
        {
            switch (converterNumber)
            {
                case 1:
                    if (Value >= 0 && Value <= 10 || Value >= 350 && Value < 360)
                        return "Продувка";
                    if (Value >= 45 && Value <= 56)
                        return "Заливка чугуна";
                    if (Value > 58 && Value <= 78)
                        return "Завалка лома";
                    if (Value >= 80 && Value <= 180)
                        return "Слив шлака";
                    if (Value >= 200 && Value <= 300)
                        return "Слив стали";
                    break;
                case 2:
                case 3:
                    if (Value >= 0 && Value <= 10 || Value >= 350 && Value < 360)
                        return "Продувка";
                    if (Value >= 45 && Value <= 58)
                        return "Заливка чугуна";
                    if (Value > 56 && Value <= 78)
                        return "Завалка лома";
                    if (Value >= 80 && Value <= 180)
                        return "Слив шлака";
                    if (Value >= 200 && Value <= 292)
                        return "Слив стали";
                    break;
            }
            return "";
        }
    }
}

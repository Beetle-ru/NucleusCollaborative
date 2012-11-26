using System;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;
using System.IO;
using Charge5Classes;

namespace Charge5
{
    internal partial class Program
    {
        static public void Alg(CSVTableParser table, InData inData, out OutData outData)
        {
            outData = new OutData();
            //const double maxSiHi = 0.8; // максимальный кремний после которого считаем не по таблице

            foreach (var row in table.Rows)
            {
                var hitDownSiHiRange = (double) (row.Cell["MinSiHotIron"]) > inData.SiHi;
                var hitUpSiHiRange = (double)(row.Cell["MaxSiHotIron"]) <= inData.SiHi;
                var hitDownTHiRange = (double)(row.Cell["MinTHotIron"]) > inData.THi;
                var hitUpTHiRange = (double)(row.Cell["MaxTHotIron"]) <= inData.THi;

                if (hitDownSiHiRange && hitUpSiHiRange && hitDownTHiRange && hitUpTHiRange)
                {
                    double knownTableVal = 1;
                    double unknownTableVal = 1;
                    if (inData.MHi > 0)
                    {
                        outData.MHi = inData.MHi;
                        knownTableVal = (double) row.Cell["MassHotIron"];
                        if (inData.MSc > 0)
                        {
                            outData.MSc = inData.MSc;
                        }
                        else
                        {
                            unknownTableVal = (double)row.Cell["MassScrap"];
                            outData.MSc = (int)Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));
                        }
                    } 
                    else if (inData.MSc > 0)
                    {
                        outData.MSc = inData.MSc;
                        knownTableVal = (double)row.Cell["MassScrap"];
                        unknownTableVal = (double)row.Cell["MassHotIron"];
                        outData.MHi = (int)Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));
                    }
                    else
                    {
                        return;
                    }

                    unknownTableVal = (double)row.Cell["MassLime"];
                    outData.MLi = (int)Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));

                    unknownTableVal = inData.IsProcessingUVS
                                            ? (double)row.Cell["UVSMassDolom"]
                                            : (double)row.Cell["MassDolom"];
                    outData.MDlm = (int)Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));

                    unknownTableVal = inData.IsProcessingUVS
                                            ? (double)row.Cell["UVSMassFOM"]
                                            : (double)row.Cell["MassFOM"];
                    outData.MFom = (int)Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));

                    unknownTableVal = (double)row.Cell["MassDolomS"];
                    outData.MDlms = (int)Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));

                    // досчитываем по замечаниям

                    //######################################################################
                    //Если, при шихтовке, металлолома берётся больше заданного значения, 
                    //то убирается «долом С», из расчёта на 1т металлолома – 0,5 т «долом С»
                    if ((inData.MSc > 0) && (inData.MHi > 0))
                    {
                        knownTableVal = (double)row.Cell["MassHotIron"];
                        unknownTableVal = (double)row.Cell["MassScrap"];
                        var calcScrap = (int)Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));
                        var scrapDifference = inData.MSc - calcScrap;
                        if (scrapDifference > 0)
                        {
                            var k = 0.5;
                            outData.MDlms -= (int)Math.Round(scrapDifference*k);
                        }
                    }
                    //######################################################################


                    break;
                }
            }
        }

        static public double CalcUnknownVal(double knownVal, double knownTableVal, double unknownTableVal)
        {
            if (knownTableVal != 0)
            {
                return (unknownTableVal/knownTableVal)*knownVal;
            }
            return 0.0;
        }
    }
}
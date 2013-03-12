using System;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;
using System.IO;
using Charge5Classes;

namespace Charge5 {
    internal partial class Program {
        public static void Alg(CSVTableParser table, InData inData, out OutData outData) {
            outData = new OutData();
            //const double maxSiHi = 0.8; // максимальный кремний после которого считаем не по таблице

            foreach (var row in table.Rows) {
                var hitDownSiHiRange = (double) (row.Cell["MinSiHotIron"]) <= inData.SiHi;
                var hitUpSiHiRange = (double) (row.Cell["MaxSiHotIron"]) >= inData.SiHi;
                var hitDownTHiRange = (double) (row.Cell["MinTHotIron"]) <= inData.THi;
                var hitUpTHiRange = (double) (row.Cell["MaxTHotIron"]) >= inData.THi;
                outData.IsFound = false;

                if (hitDownSiHiRange && hitUpSiHiRange && hitDownTHiRange && hitUpTHiRange) {
                    outData.IsFound = true;

                    #region новый расчет

                    outData.MHi = (int) Math.Round((double) row.Cell["MassHotIron"]);
                    outData.MSc = (int) Math.Round((double) row.Cell["MassScrap"]);
                    outData.MLi = (int) Math.Round((double) row.Cell["MassLime"]);
                    outData.MDlms = (int) Math.Round((double) row.Cell["MassDolomS"]);
                    outData.MDlm = inData.IsProcessingUVS
                                       ? (int) Math.Round((double) row.Cell["UVSMassDolom"])
                                       : (int) Math.Round((double) row.Cell["MassDolom"]);
                    outData.MFom = inData.IsProcessingUVS
                                       ? (int) Math.Round((double) row.Cell["UVSMassFOM"])
                                       : (int) Math.Round((double) row.Cell["MassFOM"]);

                    #endregion

                    #region старый расчет

                    //double knownTableVal = 1;
                    //double unknownTableVal = 1;
                    //double knownVal = 1;
                    //if (inData.MHi > 0)
                    //{
                    //    outData.MHi = inData.MHi;
                    //    knownVal = inData.MHi;
                    //    knownTableVal = (double) row.Cell["MassHotIron"];
                    //    if (inData.MSc > 0)
                    //    {
                    //        outData.MSc = inData.MSc;
                    //    }
                    //    else
                    //    {
                    //        unknownTableVal = (double)row.Cell["MassScrap"];
                    //        outData.MSc = (int)Math.Round(CalcUnknownVal(knownVal, knownTableVal, unknownTableVal));
                    //    }
                    //} 
                    //else if (inData.MSc > 0)
                    //{
                    //    outData.MSc = inData.MSc;
                    //    knownVal = inData.MSc;
                    //    knownTableVal = (double)row.Cell["MassScrap"];
                    //    unknownTableVal = (double)row.Cell["MassHotIron"];
                    //    outData.MHi = (int)Math.Round(CalcUnknownVal(knownVal, knownTableVal, unknownTableVal));
                    //}
                    //else
                    //{
                    //    return;
                    //}

                    //unknownTableVal = (double)row.Cell["MassLime"];
                    //outData.MLi = (int)Math.Round(CalcUnknownVal(knownVal, knownTableVal, unknownTableVal));

                    //unknownTableVal = inData.IsProcessingUVS
                    //                        ? (double)row.Cell["UVSMassDolom"]
                    //                        : (double)row.Cell["MassDolom"];
                    //outData.MDlm = (int)Math.Round(CalcUnknownVal(knownVal, knownTableVal, unknownTableVal));

                    //unknownTableVal = inData.IsProcessingUVS
                    //                        ? (double)row.Cell["UVSMassFOM"]
                    //                        : (double)row.Cell["MassFOM"];
                    //outData.MFom = (int)Math.Round(CalcUnknownVal(knownVal, knownTableVal, unknownTableVal));

                    //unknownTableVal = (double)row.Cell["MassDolomS"];
                    //outData.MDlms = (int)Math.Round(CalcUnknownVal(knownVal, knownTableVal, unknownTableVal));

                    #endregion

                    // досчитываем по замечаниям

                    //######################################################################
                    //Если, при шихтовке, металлолома берётся больше заданного значения, 
                    //то убирается «долом С», из расчёта на 1т металлолома – 0,5 т «долом С»
                    if ((inData.MSc > 0) && (inData.MHi > 0)) {
                        var knownTableVal = (double) row.Cell["MassHotIron"];
                        var unknownTableVal = (double) row.Cell["MassScrap"];
                        var calcScrap = (int) Math.Round(CalcUnknownVal(inData.MHi, knownTableVal, unknownTableVal));
                        var scrapDifference = inData.MSc - calcScrap;
                        if (scrapDifference > 0) {
                            var k = 0.5;
                            outData.MDlms -= (int) Math.Round(scrapDifference*k);
                            if (outData.MDlms < 0)
                                outData.MDlms = 0;
                        }
                    }
                    //######################################################################


                    break;
                }
            }
        }

        public static double CalcUnknownVal(double knownVal, double knownTableVal, double unknownTableVal) {
            if (knownTableVal != 0)
                return (unknownTableVal/knownTableVal)*knownVal;
            return 0.0;
        }
    }
}
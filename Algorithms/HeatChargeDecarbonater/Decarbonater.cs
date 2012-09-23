using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeatCharge
{
    public class Decarbonater
    {
        public static double test(double arg)
        {
            return arg*arg; 
        }
        public static double HeatCarbonMass(
            double IronMass, double IronCarbonPercent, 
            double ScrapMass, double ScrapCarbonPercent, 
            double SteelCarbonPercent
            )
        {
            return IronMass * IronCarbonPercent * 0.01 +
                ScrapMass * ScrapCarbonPercent * 0.01 -
                0.88 * (IronMass + ScrapMass) * SteelCarbonPercent * 0.01
            ;
        }
        public static double GasanCarbonMass(
            double CarbonMonoxideVolumePercent,
            double OffgasVolumeRate, 
            double DeltaT = 15.0,
            double Kgasan = 0.6357
            )
        {
            var offgasVolumeRatePerSec = OffgasVolumeRate / 3600;
            return 1.25 * 0.43 * CarbonMonoxideVolumePercent * 0.01 * offgasVolumeRatePerSec * Kgasan * DeltaT;
        }
        public static double MultiFactorCarbonMass(List<MFCMData> matrixStateData, MFCMData currentStateData)
        {
            const int nFeatures = 4;
            int nFeaturesCoefficcients;
            int info = 0;
            var inVector = new double[matrixStateData.Count, nFeatures+1];
            double[] coefficcients;
            var lm = new alglib.linearmodel();
            var lr = new alglib.lrreport();

            int lenghtData = matrixStateData.Count;
            for (int item = 0; item < lenghtData; item++)
            {
                inVector[item, 0] = matrixStateData[item].CarbonMonoxideVolumePercent; // X1
                inVector[item, 1] = matrixStateData[item].CarbonOxideVolumePercent;    // X2
                inVector[item, 2] = matrixStateData[item].HeightLanceCentimeters;      // X3
                inVector[item, 3] = matrixStateData[item].OxygenVolumeRate;            // X4
                inVector[item, 4] = matrixStateData[item].SteelCarbonPercent;          // Y
            }

            alglib.lrbuild(inVector, lenghtData, nFeatures, out info, out lm, out lr);
            if (info != 1)
            {
                return info;
            }
            alglib.lrunpack(lm, out coefficcients, out nFeaturesCoefficcients);
            if (nFeaturesCoefficcients != nFeatures)
            {
                return -2.011;
            }
            double calculatedCarbon = coefficcients[4];
            calculatedCarbon += coefficcients[0] * currentStateData.CarbonMonoxideVolumePercent;
            calculatedCarbon += coefficcients[1] * currentStateData.CarbonOxideVolumePercent;
            calculatedCarbon += coefficcients[2] * currentStateData.HeightLanceCentimeters;
            calculatedCarbon += coefficcients[3] * currentStateData.OxygenVolumeRate;
            return calculatedCarbon;
        }

        private static alglib.multilayerperceptron m_complexCmp;
        private const int NIn = 4;
        private const int NOut = 1;

        public static void ComplexNMCTrain(List<MFCMData> matrixStateData)
        {
           

            if (m_complexCmp == null)
            {
                alglib.mlpcreate0(NIn, NOut, out m_complexCmp);
            }

            var inPoints = matrixStateData.Count;
            var trainData = new double[inPoints, NIn + NOut];

            for (int i = 0; i < inPoints; i++)
            {
                trainData[i, 0] = matrixStateData[i].CarbonMonoxideVolumePercent; // IN
                trainData[i, 1] = matrixStateData[i].CarbonOxideVolumePercent;    // IN
                trainData[i, 2] = matrixStateData[i].HeightLanceCentimeters;      // IN
                trainData[i, 3] = matrixStateData[i].OxygenVolumeRate;            // IN
                trainData[i, 4] = matrixStateData[i].SteelCarbonPercent;          // OUT
            }
            
            alglib.mlpreport rep;
            int info;
            const double decay = 0.0001;
            const int restarts = 10;

            alglib.mlptrainlm(m_complexCmp, trainData, inPoints, decay, restarts, out info, out rep);

            if (info != 2)
            {
                Console.WriteLine("error code :{0}", info);
                return;
            }
        }

        public static double ComplexNMCProcess(MFCMData currentStateData)
        {
            if (m_complexCmp != null)
            {
                var processData = new double[NIn];
                var processResult = new double[NOut];

                processData[0] = currentStateData.CarbonMonoxideVolumePercent;
                processData[1] = currentStateData.CarbonOxideVolumePercent;
                processData[2] = currentStateData.HeightLanceCentimeters;
                processData[3] = currentStateData.OxygenVolumeRate;

                alglib.mlpprocess(m_complexCmp, processData, ref processResult);

                return processResult[0];
            }
            return -0.1133;
        }
    }
    
    public class MFCMData
    {
        public double CarbonMonoxideVolumePercent { set; get; } // X1
        public double CarbonOxideVolumePercent { set; get; }    // X2
        public Int32 HeightLanceCentimeters { set; get; }       // X3
        public double OxygenVolumeRate { set; get; }            // X4
        public double SteelCarbonPercent { set; get; }          // Y
        public MFCMData()
        {
            CarbonMonoxideVolumePercent = 0.0;
            CarbonOxideVolumePercent = 0.0;
            HeightLanceCentimeters = 0;
            OxygenVolumeRate = 0.0;
            SteelCarbonPercent = 0.0;
        }
    }
}

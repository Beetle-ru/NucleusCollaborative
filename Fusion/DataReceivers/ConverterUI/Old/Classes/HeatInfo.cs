using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes.Classes;
using Converter;
using System.ComponentModel;

namespace ConverterUI
{
    class HeatInfo : Heat, INotifyPropertyChanged
    {

        //public Heat Heat;

        
        #region Privates
        private AggregateAngle m_AggregateAngle; // текущий угол наколона конвертера

        private int m_CurrentConverterAngle;

        private int m_LanceHeight; // Текущее положение фурмы

        private int m_LanceRealHeight; // Текущее положение фурмы

        private int m_SublanceHeight; // Текущее положение измерительной фурмы

        private double m_OFlow; // текущее значение интенсивности кислорода

        private double m_ORealFlow; // текущее значение интенсивности кислорода

        private double m_TotalO2Volume; //текущее суммарное количество кислорода за продувку

        private int m_SublanceDown;// признак запуска измерительного зонда

        private double m_SublanceC;// Значение С после замера измерительным зондом

        private double m_SublanceTemperature;// Значение температуры после замера измерительным зондом

        private double m_SublanceOxidation;// Значение окисленности после замера измерительным зондом

        private int m_SublanceMetalLevel;

        private DateTime m_SublanceStartTime;

        private DateTime? m_BlowingStartTime;

        private double m_SublanceAl;

        private double m_SublanceTotalO;

        private double m_ValveValue;

        private double m_O2Pressure;

        private double m_NSlagBlowingPressure;

        private double m_NSlagBlowingFlow;

        private int m_LanceNFlow;

        public int m_BoilerNFlow;

        private double m_CandleGasPressure;

        private int m_CandleGasFlow;

        private double m_CandleSteamPressure;

        private int m_CandleSteamFlow;

        private string m_Bunker12MaterialName;

        private string m_Bunker11MaterialName;

        private string m_Bunker10MaterialName;

        private string m_Bunker9_2MaterialName;

        private string m_Bunker9_1MaterialName;

        private string m_Bunker8_2MaterialName;

        private string m_Bunker8_1MaterialName;

        private string m_Bunker7MaterialName;

        private string m_Bunker6MaterialName;

        private string m_Bunker5MaterialName;

        private bool m_Bunker12FeederVibrating;

        private bool m_Bunker11FeederVibrating;

        private bool m_Bunker10FeederVibrating;

        private bool m_Bunker9_2FeederVibrating;

        private bool m_Bunker9_1FeederVibrating;

        private bool m_Bunker8_2FeederVibrating;

        private bool m_Bunker8_1FeederVibrating;

        private bool m_Bunker7FeederVibrating;

        private bool m_Bunker6FeederVibrating;

        private bool m_Bunker5FeederVibrating;

        private double m_Scale7Weight;

        private string m_Scale7CurrentBunker;

        private double m_Scale7Material1Weight;

        private double m_Scale7Material2Weight;

        private double m_Scale7Material3Weight;

        private double m_Scale6Weight;

        private string m_Scale6CurrentBunker;

        private double m_Scale6Material1Weight;

        private double m_Scale5Weight;

        private string m_Scale5CurrentBunker;

        private double m_Scale5Material1Weight;

        private double m_Scale4Weight;

        private string m_Scale4CurrentBunker;

        private double m_Scale4Material1Weight;

        private double m_Scale3Weight;

        private string m_Scale3CurrentBunker;

        private double m_Scale3Material1Weight;

        private double m_Scale3Material2Weight;

        private bool m_Scale7Opened;

        private bool m_Scale6Opened;

        private bool m_Scale5Opened;

        private bool m_Scale4Opened;

        private bool m_Scale3Opened;

        private bool m_Bunker1Opened;

        private bool m_Bunker2Opened;

        private double m_Bunker1Material1Weight;

        private double m_Bunker1Material2Weight;

        private double m_Bunker1Material3Weight;

        private double m_Bunker1Material4Weight;

        private double m_Bunker2Material1Weight;

        private double m_Bunker2Material2Weight;

        private double m_Bunker2Material3Weight;

        private double m_Bunker2Material4Weight;

        private double m_OffGassQ;

        private double m_OffGassCO;

        private double m_OffGassCO2;

        private double m_OffGassO2;

        private double m_OffGassH2;

        private double m_OffGassN2;

        private double m_OffGassAr;

        private double m_O2RightLanceWaterInput;

        private double m_O2RightLanceWaterPressure;

        private double m_O2RightLanceWaterTempInput;

        private double m_O2RightLanceWaterOutput;

        private double m_O2RightLanceWaterTempOutput;

        private double m_O2RightLanceWaterTempDiff;

        private int m_O2RightLanceLeck;

        private int m_O2Summary;

        private double m_PPM;

        private bool m_CandleFire;                       // Факел на свече горит "1" / не горит "0" # AS33/A3_SIGN.G3A_SFAKEL

        private bool m_CandleFireError;                   // Факел на свече не зажегся # AS33/A3_SIGN.A3G_SFAKELneZAG

        private bool m_CandleSparkFire;

        private int m_OffGasFilterControlPos;
        #endregion

        #region Publics
        public int CurrentNumber
        {
            get
            {
                return Number;
            }
            set
            {
                Number = value;
                RaisePropertyChanged("CurrentNumber");
            }
        }

        public DateTime CurrentStartDate
        {
            get
            {
                return StartDate;
            }
            set
            {
                StartDate = value;
                RaisePropertyChanged("CurrentStartDate");
            }
        }

        public string CurrentGrade
        {
            get
            {
                return Grade;
            }
            set
            {
                Grade = value;
                RaisePropertyChanged("CurrentGrade");
            }
        }

        public int CurrentConverterNumber
        {
            get
            {                
                return AggregateNumber;
            }
            set
            {
                AggregateNumber = value;
                RaisePropertyChanged("CurrentConverterNumber");
            }
        }

        public int CurrentTeamNumber
        {
            get
            {
                return TeamNumber;
            }
            set
            {
                TeamNumber = value;
                RaisePropertyChanged("CurrentTeamNumber");
            }
        }

        public SteelAttributes CurrentPlanned
        {
            get
            {
                return Planned;
            }
            set
            {
                Planned = value;
                RaisePropertyChanged("CurrentPlanned");
            }
        }

        public SteelAttributes CurrentActual
        {
            get
            {
                return Actual;
            }
            set
            {
                Actual = value;
                RaisePropertyChanged("CurrentActual");
            }
        }

        public AggregateAngle AggregateAngle
        {
            get
            {
                if (m_AggregateAngle == null)
                {
                    m_AggregateAngle = new AggregateAngle();
                }
                return m_AggregateAngle;
            }
            set
            {
                m_AggregateAngle = value;
                CurrentConverterAngle = value.Value;
                RaisePropertyChanged("AggregateAngle");
            }
        }

        public int CurrentConverterAngle
        {
            get
            {
                return m_CurrentConverterAngle;
            }
            set
            {
                m_CurrentConverterAngle = value;
                RaisePropertyChanged("CurrentConverterAngle");
            }
        }

        public int LanceHeight
        {
            get { return m_LanceHeight; }
            set
            {
                m_LanceHeight = value;
                RaisePropertyChanged("LanceHeight");
            }
        }

        public int LanceRealHeight
        {
            get { return m_LanceRealHeight; }
            set
            {
                m_LanceRealHeight = value;
                RaisePropertyChanged("LanceRealHeight");
            }
        }

        public int SublanceHeight
        {
            get { return m_SublanceHeight; }
            set
            {
                m_SublanceHeight = value;
                RaisePropertyChanged("SublanceHeight");
            }
        }

        public double OFlow
        {
            get { return m_OFlow; }
            set
            {
                m_OFlow = value;
                RaisePropertyChanged("OFlow");
            }
        }

        public double ORealFlow
        {
            get { return m_ORealFlow; }
            set
            {
                m_ORealFlow = value;
                RaisePropertyChanged("ORealFlow");
            }
        }

        public double TotalO2Volume
        {
            get { return m_TotalO2Volume; }
            set
            {
                m_TotalO2Volume = value;
                RaisePropertyChanged("TotalO2Volume");
            }
        }

        public int SublanceMetalLevel
        {
            get { return m_SublanceMetalLevel; }
            set
            {
                m_SublanceMetalLevel = value;
                RaisePropertyChanged("SublanceMetalLevel");
            }
        }

        public double SublanceAl
        {
            get { return m_SublanceAl; }
            set
            {
                m_SublanceAl = value;
                RaisePropertyChanged("SublanceAl");
            }
        }

        public double SublanceTotalO2
        {
            get { return m_SublanceTotalO; }
            set
            {
                m_SublanceTotalO = value;
                RaisePropertyChanged("SublanceTotalO");
            }
        }

        public int SublanceDown
        {
            get { return m_SublanceDown; }
            set
            {
                m_SublanceDown = value;
                RaisePropertyChanged("SublanceDown");
            }
        }

        public double SublanceC
        {
            get { return m_SublanceC; }
            set
            {
                m_SublanceC = value;
                RaisePropertyChanged("SublanceС");
            }
        }
        public double SublanceOxidation
        {
            get { return m_SublanceOxidation; }
            set
            {
                m_SublanceOxidation = value;
                RaisePropertyChanged("SublanceOxidation");
            }
        }
        public double SublanceTemperature
        {
            get { return m_SublanceTemperature; }
            set
            {
                m_SublanceTemperature = value;
                RaisePropertyChanged("SublanceTemperature");
            }
        }

        public double ValveValue
        {
            get { return m_ValveValue; }
            set
            {
                m_ValveValue = value;
                RaisePropertyChanged("ValveValue");
            }
        }

        public double NSlagBlowingPressure
        {
            get { return m_NSlagBlowingPressure; }
            set
            {
                m_NSlagBlowingPressure = value;
                RaisePropertyChanged("NSlagBlowingPressure");
            }
        }

        public double NSlagBlowingFlow
        {
            get { return m_NSlagBlowingFlow; }
            set
            {
                m_NSlagBlowingFlow = value;
                RaisePropertyChanged("NSlagBlowingFlow");
            }
        }

        public double O2Pressure
        {
            get { return m_O2Pressure; }
            set
            {
                m_O2Pressure = value;
                RaisePropertyChanged("O2Pressure");
            }
        }


        public int LanceNFlow // Расход азота на уплотнение фурменных окон
        {
            get { return m_LanceNFlow; }
            set
            {
                m_LanceNFlow = value;
                RaisePropertyChanged("LanceNFlow");
            }
        }

        public int BoilerNFlow
        {
            get { return m_BoilerNFlow; }
            set
            {
                m_BoilerNFlow = value;
                RaisePropertyChanged("BoilerNFlow");
            }
        }

        public double CandleGasPressure
        {
            get { return m_CandleGasPressure; }
            set
            {
                m_CandleGasPressure = value;
                RaisePropertyChanged("CandleGasPressure");
            }
        }

        public int CandleGasFlow
        {
            get { return m_CandleGasFlow; }
            set
            {
                m_CandleGasFlow = value;
                RaisePropertyChanged("CandleGasFlow");
            }
        }

        public double CandleSteamPressure
        {
            get { return m_CandleSteamPressure; }
            set
            {
                m_CandleSteamPressure = value;
                RaisePropertyChanged("CandleSteamPressure");
            }
        }

        public int CandleSteamFlow
        {
            get { return m_CandleSteamFlow; }
            set
            {
                m_CandleSteamFlow = value;
                RaisePropertyChanged("CandleSteamFlow");
            }
        }

        public string Bunker12MaterialName
        {
            get { return m_Bunker12MaterialName; }
            set
            {
                m_Bunker12MaterialName = value;
                RaisePropertyChanged("Bunker12MaterialName");
            }
        }

        public string Bunker11MaterialName
        {
            get { return m_Bunker11MaterialName; }
            set
            {
                m_Bunker11MaterialName = value;
                RaisePropertyChanged("Bunker11MaterialName");
            }
        }

        public string Bunker10MaterialName
        {
            get { return m_Bunker10MaterialName; }
            set
            {
                m_Bunker10MaterialName = value;
                RaisePropertyChanged("Bunker10MaterialName");
            }
        }

        public string Bunker9_2MaterialName
        {
            get { return m_Bunker9_2MaterialName; }
            set
            {
                m_Bunker9_2MaterialName = value;
                RaisePropertyChanged("Bunker9_2MaterialName");
            }
        }

        public string Bunker9_1MaterialName
        {
            get { return m_Bunker9_1MaterialName; }
            set
            {
                m_Bunker9_1MaterialName = value;
                RaisePropertyChanged("Bunker9_1MaterialName");
            }
        }

        public string Bunker8_2MaterialName
        {
            get { return m_Bunker8_2MaterialName; }
            set
            {
                m_Bunker8_2MaterialName = value;
                RaisePropertyChanged("Bunker8_2MaterialName");
            }
        }

        public string Bunker8_1MaterialName
        {
            get { return m_Bunker8_1MaterialName; }
            set
            {
                m_Bunker8_1MaterialName = value;
                RaisePropertyChanged("Bunker8_1MaterialName");
            }
        }

        public string Bunker7MaterialName
        {
            get { return m_Bunker7MaterialName; }
            set
            {
                m_Bunker7MaterialName = value;
                RaisePropertyChanged("Bunker7MaterialName");
            }
        }

        public string Bunker6MaterialName
        {
            get { return m_Bunker6MaterialName; }
            set
            {
                m_Bunker6MaterialName = value;
                RaisePropertyChanged("Bunker6MaterialName");
            }
        }

        public string Bunker5MaterialName
        {
            get { return m_Bunker5MaterialName; }
            set
            {
                m_Bunker5MaterialName = value;
                RaisePropertyChanged("Bunker5MaterialName");
            }
        }

        public bool Bunker12FeederVibrating
        {
            get { return m_Bunker12FeederVibrating; }
            set
            {
                m_Bunker12FeederVibrating = value;
                RaisePropertyChanged("Bunker12FeederVibrating");
            }
        }

        public bool Bunker11FeederVibrating
        {
            get { return m_Bunker11FeederVibrating; }
            set
            {
                m_Bunker11FeederVibrating = value;
                RaisePropertyChanged("Bunker11FeederVibrating");
            }
        }

        public bool Bunker10FeederVibrating
        {
            get { return m_Bunker10FeederVibrating; }
            set
            {
                m_Bunker10FeederVibrating = value;
                RaisePropertyChanged("Bunker10FeederVibrating");
            }
        }

        public bool Bunker9_2FeederVibrating
        {
            get { return m_Bunker9_2FeederVibrating; }
            set
            {
                m_Bunker9_2FeederVibrating = value;
                RaisePropertyChanged("Bunker9_2FeederVibrating");
            }
        }

        public bool Bunker9_1FeederVibrating
        {
            get { return m_Bunker9_1FeederVibrating; }
            set
            {
                m_Bunker9_1FeederVibrating = value;
                RaisePropertyChanged("Bunker9_1FeederVibrating");
            }
        }

        public bool Bunker8_2FeederVibrating
        {
            get { return m_Bunker8_2FeederVibrating; }
            set
            {
                m_Bunker8_2FeederVibrating = value;
                RaisePropertyChanged("Bunker8_2FeederVibrating");
            }
        }

        public bool Bunker8_1FeederVibrating
        {
            get { return m_Bunker8_1FeederVibrating; }
            set
            {
                m_Bunker8_1FeederVibrating = value;
                RaisePropertyChanged("Bunker8_1FeederVibrating");
            }
        }

        public bool Bunker7FeederVibrating
        {
            get { return m_Bunker7FeederVibrating; }
            set
            {
                m_Bunker7FeederVibrating = value;
                RaisePropertyChanged("Bunker7FeederVibrating");
            }
        }

        public bool Bunker6FeederVibrating
        {
            get { return m_Bunker6FeederVibrating; }
            set
            {
                m_Bunker6FeederVibrating = value;
                RaisePropertyChanged("Bunker6FeederVibrating");
            }
        }

        public bool Bunker5FeederVibrating
        {
            get { return m_Bunker5FeederVibrating; }
            set
            {
                m_Bunker5FeederVibrating = value;
                RaisePropertyChanged("Bunker5FeederVibrating");
            }
        }


        public double Scale7Weight
        {
            get { return m_Scale7Weight; }
            set
            {
                m_Scale7Weight = value;
                RaisePropertyChanged("Scale7Weight");
            }
        }

        public double Scale6Weight
        {
            get { return m_Scale6Weight; }
            set
            {
                m_Scale6Weight = value;
                RaisePropertyChanged("Scale6Weight");
            }
        }

        public double Scale5Weight
        {
            get { return m_Scale5Weight; }
            set
            {
                m_Scale5Weight = value;
                RaisePropertyChanged("Scale5Weight");
            }
        }

        public double Scale4Weight
        {
            get { return m_Scale4Weight; }
            set
            {
                m_Scale4Weight = value;
                RaisePropertyChanged("Scale4Weight");
            }
        }

        public double Scale3Weight
        {
            get { return m_Scale3Weight; }
            set
            {
                m_Scale3Weight = value;
                RaisePropertyChanged("Scale3Weight");
            }
        }

        public bool Scale7Opened
        {
            get { return m_Scale7Opened; }
            set
            {
                m_Scale7Opened = value;
                RaisePropertyChanged("Scale7Opened");
            }
        }

        public bool Scale6Opened
        {
            get { return m_Scale6Opened; }
            set
            {
                m_Scale6Opened = value;
                RaisePropertyChanged("Scale6Opened");
            }
        }

        public bool Scale5Opened
        {
            get { return m_Scale5Opened; }
            set
            {
                m_Scale5Opened = value;
                RaisePropertyChanged("Scale5Opened");
            }
        }

        public bool Scale4Opened
        {
            get { return m_Scale4Opened; }
            set
            {
                m_Scale4Opened = value;
                RaisePropertyChanged("Scale4Opened");
            }
        }

        public bool Scale3Opened
        {
            get { return m_Scale3Opened; }
            set
            {
                m_Scale3Opened = value;
                RaisePropertyChanged("Scale3Opened");
            }
        }

        public bool Bunker1Opened
        {
            get { return m_Bunker1Opened; }
            set
            {
                m_Bunker1Opened = value;
                RaisePropertyChanged("Bunker1Opened");
            }
        }

        public bool Bunker2Opened
        {
            get { return m_Bunker2Opened; }
            set
            {
                m_Bunker2Opened = value;
                RaisePropertyChanged("Bunker2Opened");
            }
        }

        public string Scale7CurrentBunker
        {
            get { return m_Scale7CurrentBunker; }
            set
            {
                m_Scale7CurrentBunker = value;
                RaisePropertyChanged("Scale7CurrentBunker");
            }
        }

        public double Scale7Material1Weight
        {
            get { return m_Scale7Material1Weight; }
            set
            {
                m_Scale7Material1Weight = value;
                RaisePropertyChanged("Scale7Material1Weight");
            }
        }

        public double Scale7Material2Weight
        {
            get { return m_Scale7Material2Weight; }
            set
            {
                m_Scale7Material2Weight = value;
                RaisePropertyChanged("Scale7Material2Weight");
            }
        }
        public double Scale7Material3Weight
        {
            get { return m_Scale7Material3Weight; }
            set
            {
                m_Scale7Material3Weight = value;
                RaisePropertyChanged("Scale7Material3Weight");
            }
        }


        public string Scale6CurrentBunker
        {
            get { return m_Scale6CurrentBunker; }
            set
            {
                m_Scale6CurrentBunker = value;
                RaisePropertyChanged("Scale6CurrentBunker");
            }
        }

        public double Scale6Material1Weight
        {
            get { return m_Scale6Material1Weight; }
            set
            {
                m_Scale6Material1Weight = value;
                RaisePropertyChanged("Scale6Material1Weight");
            }
        }


        public string Scale5CurrentBunker
        {
            get { return m_Scale5CurrentBunker; }
            set
            {
                m_Scale5CurrentBunker = value;
                RaisePropertyChanged("Scale5CurrentBunker");
            }
        }

        public double Scale5Material1Weight
        {
            get { return m_Scale5Material1Weight; }
            set
            {
                m_Scale5Material1Weight = value;
                RaisePropertyChanged("Scale5Material1Weight");
            }
        }


        public string Scale4CurrentBunker
        {
            get { return m_Scale4CurrentBunker; }
            set
            {
                m_Scale4CurrentBunker = value;
                RaisePropertyChanged("Scale4CurrentBunker");
            }
        }

        public double Scale4Material1Weight
        {
            get { return m_Scale4Material1Weight; }
            set
            {
                m_Scale4Material1Weight = value;
                RaisePropertyChanged("Scale4Material1Weight");
            }
        }

        public string Scale3CurrentBunker
        {
            get { return m_Scale3CurrentBunker; }
            set
            {
                m_Scale3CurrentBunker = value;
                RaisePropertyChanged("Scale3CurrentBunker");
            }
        }

        public double Scale3Material1Weight
        {
            get { return m_Scale3Material1Weight; }
            set
            {
                m_Scale3Material1Weight = value;
                RaisePropertyChanged("Scale3Material1Weight");
            }
        }

        public double Scale3Material2Weight
        {
            get { return m_Scale3Material2Weight; }
            set
            {
                m_Scale3Material2Weight = value;
                RaisePropertyChanged("Scale3Material2Weight");
            }
        }

        public double Bunker1Material1Weight
        {
            get { return m_Bunker1Material1Weight; }
            set
            {
                m_Bunker1Material1Weight = value;
                RaisePropertyChanged("Bunker1Material1Weight");
            }
        }

        public double Bunker1Material2Weight
        {
            get { return m_Bunker1Material2Weight; }
            set
            {
                m_Bunker1Material2Weight = value;
                RaisePropertyChanged("Bunker1Material2Weight");
            }
        }
        public double Bunker1Material3Weight
        {
            get { return m_Bunker1Material3Weight; }
            set
            {
                m_Bunker1Material3Weight = value;
                RaisePropertyChanged("Bunker1Material3Weight");
            }
        }
        public double Bunker1Material4Weight
        {
            get { return m_Bunker1Material4Weight; }
            set
            {
                m_Bunker1Material4Weight = value;
                RaisePropertyChanged("Bunker1Material4Weight");
            }
        }


        public double Bunker2Material1Weight
        {
            get { return m_Bunker2Material1Weight; }
            set
            {
                m_Bunker2Material1Weight = value;
                RaisePropertyChanged("Bunker2Material1Weight");
            }
        }

        public double Bunker2Material2Weight
        {
            get { return m_Bunker2Material2Weight; }
            set
            {
                m_Bunker2Material2Weight = value;
                RaisePropertyChanged("Bunker2Material2Weight");
            }
        }
        public double Bunker2Material3Weight
        {
            get { return m_Bunker2Material3Weight; }
            set
            {
                m_Bunker2Material3Weight = value;
                RaisePropertyChanged("Bunker2Material3Weight");
            }
        }

        public double Bunker2Material4Weight
        {
            get { return m_Bunker2Material4Weight; }
            set
            {
                m_Bunker2Material4Weight = value;
                RaisePropertyChanged("Bunker2Material4Weight");
            }
        }


        public DateTime? BlowingStartTime
        {
            get { return m_BlowingStartTime; }
            set
            {
                m_BlowingStartTime = value;
                RaisePropertyChanged("BlowingStartTime");
            }
        }

        public DateTime SublanceStartTime
        {
            get { return m_SublanceStartTime; }
            set
            {
                m_SublanceStartTime = value;
                RaisePropertyChanged("SublanceStartTime");
            }
        }

        public double OffGassQ
        {
            get { return m_OffGassQ; }
            set
            {
                m_OffGassQ = value;
                RaisePropertyChanged("OffGassQ");
            }
        }

        public double OffGassCO
        {
            get { return m_OffGassCO; }
            set
            {
                m_OffGassCO = value;
                RaisePropertyChanged("OffGassCO");
            }
        }

        public double OffGassCO2
        {
            get { return m_OffGassCO2; }
            set
            {
                m_OffGassCO2 = value;
                RaisePropertyChanged("OffGassCO2");
            }
        }

        public double OffGassO2
        {
            get { return m_OffGassO2; }
            set
            {
                m_OffGassO2 = value;
                RaisePropertyChanged("OffGassO2");
            }
        }
        public double OffGassH2
        {
            get { return m_OffGassH2; }
            set
            {
                m_OffGassH2 = value;
                RaisePropertyChanged("OffGassH2");
            }
        }
        public double OffGassN2
        {
            get { return m_OffGassN2; }
            set
            {
                m_OffGassN2 = value;
                RaisePropertyChanged("OffGassN2");
            }
        }
        public double OffGassAr
        {
            get { return m_OffGassAr; }
            set
            {
                m_OffGassAr = value;
                RaisePropertyChanged("OffGassAr");
            }
        }

        public double O2RightLanceWaterInput
        {
            get { return m_O2RightLanceWaterInput; }
            set
            {
                m_O2RightLanceWaterInput = value;
                RaisePropertyChanged("O2RightLanceWaterInput");
            }
        }

        public double O2RightLanceWaterPressure
        {
            get { return m_O2RightLanceWaterPressure; }
            set
            {
                m_O2RightLanceWaterPressure = value;
                RaisePropertyChanged("O2RightLanceWaterPressure");
            }
        }

        public double O2RightLanceWaterTempInput
        {
            get { return m_O2RightLanceWaterTempInput; }
            set
            {
                m_O2RightLanceWaterTempInput = value;
                RaisePropertyChanged("O2RightLanceWaterTempInput");
            }
        }

        public double O2RightLanceWaterOutput
        {
            get { return m_O2RightLanceWaterOutput; }
            set
            {
                m_O2RightLanceWaterOutput = value;
                RaisePropertyChanged("O2RightLanceWaterOutput");
            }
        }


        public double O2RightLanceWaterTempOutput
        {
            get { return m_O2RightLanceWaterTempOutput; }
            set
            {
                m_O2RightLanceWaterTempOutput = value;
                RaisePropertyChanged("O2RightLanceWaterTempOutput");
            }
        }

        public double O2RightLanceWaterTempDiff
        {
            get { return m_O2RightLanceWaterTempDiff; }
            set
            {
                m_O2RightLanceWaterTempDiff = value;
                RaisePropertyChanged("O2RightLanceWaterTempDiff");
            }
        }


        public int O2RightLanceLeck
        {
            get { return m_O2RightLanceLeck; }
            set
            {
                m_O2RightLanceLeck = value;
                RaisePropertyChanged("O2RightLanceLeck");
            }
        }


        public int O2Summary
        {
            get { return m_O2Summary; }
            set
            {
                m_O2Summary = value;
                RaisePropertyChanged("O2Summary");
            }
        }

        public double PPM
        {
            get { return m_PPM; }
            set
            {
                m_PPM = value;
                RaisePropertyChanged("PPM");
            }
        }


        public bool CandleFire
        {
            get { return m_CandleFire; }
            set
            {
                m_CandleFire = value;
                RaisePropertyChanged("CandleFire");
            }
        }

        public bool CandleFireError
        {
            get { return m_CandleFireError; }
            set
            {
                m_CandleFireError = value;
                RaisePropertyChanged("CandleFireError");
            }
        }

  
        public bool CandleSparkFire
        {
            get { return m_CandleSparkFire; }
            set
            {
                m_CandleSparkFire = value;
                RaisePropertyChanged("CandleSparkFire");
            }
        }

        public int OffGasFilterControlPos
        {
            get { return m_OffGasFilterControlPos; }
            set
            {
                m_OffGasFilterControlPos = value;
                RaisePropertyChanged("OffGasFilterControlPos");
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName)
        {
            var e = this.PropertyChanged;
            if (e != null)
            {
                e(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}

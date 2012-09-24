using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;

namespace Converter
{
    [Serializable]
    [DataContract]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    public class comBlowingSchemaEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT4")]
        public int O2SchemaNumber { set; get; }                         // O2 номер в схеме продувки                # SP_CX_BLOWINGSCHEMEID

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT6")]
        public int O2VolStep1 { set; get; }                             // O2 расход шаг 1                          # SP_CX_BLOWINGSTEPCHANGE1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT8")]
        public int O2VolStep2 { set; get; }                             // O2 расход шаг 2                          # SP_CX_BLOWINGSTEPCHANGE2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT10")]
        public int O2VolStep3 { set; get; }                             // O2 расход шаг 3                          # SP_CX_BLOWINGSTEPCHANGE3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT12")]
        public int O2VolStep4 { set; get; }                             // O2 расход шаг 4                          # SP_CX_BLOWINGSTEPCHANGE4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT14")]
        public int O2VolStep5 { set; get; }                             // O2 расход шаг 5                          # SP_CX_BLOWINGSTEPCHANGE5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT16")]
        public int O2VolStep6 { set; get; }                             // O2 расход шаг 6                          # SP_CX_BLOWINGSTEPCHANGE6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT18")]
        public int O2VolStep7 { set; get; }                             // O2 расход шаг 7                          # SP_CX_BLOWINGSTEPCHANGE7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT20")]
        public int O2VolStep8 { set; get; }                             // O2 расход шаг 8                          # SP_CX_BLOWINGSTEPCHANGE8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT22")]
        public int O2VolStep9 { set; get; }                             // O2 расход шаг 9                          # SP_CX_BLOWINGSTEPCHANGE9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT24")]
        public int O2VolStep10 { set; get; }                            // O2 расход шаг 10                         # SP_CX_BLOWINGSTEPCHANGE10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT26")]
        public int O2VolStep11 { set; get; }                            // O2 расход шаг 11                         # SP_CX_BLOWINGSTEPCHANGE11

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT28")]
        public int O2VolStep12 { set; get; }                            // O2 расход шаг 12                         # SP_CX_BLOWINGSTEPCHANGE12

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT30")]
        public int O2VolStep13 { set; get; }                            // O2 расход шаг 13                         # SP_CX_BLOWINGSTEPCHANGE13

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT32")]
        public int O2VolStep14 { set; get; }                            // O2 расход шаг 14                         # SP_CX_BLOWINGSTEPCHANGE14

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT34")]
        public int O2VolStep15 { set; get; }                            // O2 расход шаг 15                         # SP_CX_BLOWINGSTEPCHANGE15

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT36")]
        public int O2VolStep16 { set; get; }                            // O2 расход шаг 16                         # SP_CX_BLOWINGSTEPCHANGE16

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT38")]
        public int O2VolStep17 { set; get; }                            // O2 расход шаг 17                         # SP_CX_BLOWINGSTEPCHANGE17

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT40")]
        public int O2VolStep18 { set; get; }                            // O2 расход шаг 18                         # SP_CX_BLOWINGSTEPCHANGE18

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT42")]
        public int O2VolStep19 { set; get; }                            // O2 расход шаг 19                         # SP_CX_BLOWINGSTEPCHANGE19

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT44")]
        public int O2VolStep20 { set; get; }                            // O2 расход шаг 20                         # SP_CX_BLOWINGSTEPCHANGE20

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT46")]
        public int LancePositionStep1 { set; get; }                     // положение фурмы шаг 1                    # SP_CX_LANCEPOSITIONSTEP1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT48")]
        public int LancePositionStep2 { set; get; }                     // положение фурмы шаг 2                    # SP_CX_LANCEPOSITIONSTEP2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT50")]
        public int LancePositionStep3 { set; get; }                     // положение фурмы шаг 3                    # SP_CX_LANCEPOSITIONSTEP3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT52")]
        public int LancePositionStep4 { set; get; }                     // положение фурмы шаг 4                    # SP_CX_LANCEPOSITIONSTEP4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT54")]
        public int LancePositionStep5 { set; get; }                     // положение фурмы шаг 5                    # SP_CX_LANCEPOSITIONSTEP5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT56")]
        public int LancePositionStep6 { set; get; }                     // положение фурмы шаг 6                    # SP_CX_LANCEPOSITIONSTEP6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT58")]
        public int LancePositionStep7 { set; get; }                     // положение фурмы шаг 7                    # SP_CX_LANCEPOSITIONSTEP7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT60")]
        public int LancePositionStep8 { set; get; }                     // положение фурмы шаг 8                    # SP_CX_LANCEPOSITIONSTEP8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT62")]
        public int LancePositionStep9 { set; get; }                     // положение фурмы шаг 9                    # SP_CX_LANCEPOSITIONSTEP9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT64")]
        public int LancePositionStep10 { set; get; }                    // положение фурмы шаг 10                   # SP_CX_LANCEPOSITIONSTEP10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT66")]
        public int LancePositionStep11 { set; get; }                    // положение фурмы шаг 11                    # SP_CX_LANCEPOSITIONSTEP11

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT68")]
        public int LancePositionStep12 { set; get; }                    // положение фурмы шаг 12                    # SP_CX_LANCEPOSITIONSTEP12

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT70")]
        public int LancePositionStep13 { set; get; }                     // положение фурмы шаг 13                   # SP_CX_LANCEPOSITIONSTEP13

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT72")]
        public int LancePositionStep14 { set; get; }                     // положение фурмы шаг 14                   # SP_CX_LANCEPOSITIONSTEP14

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT74")]
        public int LancePositionStep15 { set; get; }                     // положение фурмы шаг 15                    # SP_CX_LANCEPOSITIONSTEP15

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT76")]
        public int LancePositionStep16 { set; get; }                     // положение фурмы шаг 16                    # SP_CX_LANCEPOSITIONSTEP16

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT78")]
        public int LancePositionStep17 { set; get; }                     // положение фурмы шаг 17                    # SP_CX_LANCEPOSITIONSTEP17

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT80")]
        public int LancePositionStep18 { set; get; }                     // положение фурмы шаг 18                    # SP_CX_LANCEPOSITIONSTEP18

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT82")]
        public int LancePositionStep19 { set; get; }                     // положение фурмы шаг 19                    # SP_CX_LANCEPOSITIONSTEP19

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT84")]
        public int LancePositionStep20 { set; get; }                     // положение фурмы шаг 20                    # SP_CX_LANCEPOSITIONSTEP20

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL86")]
        public double O2FlowStep1 { set; get; }                             // O2 интенсивность шаг 1                    # SP_CX_OXYGENFLOWSTEP1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL90")]
        public double O2FlowStep2 { set; get; }                             // O2 интенсивность шаг 2                    # SP_CX_OXYGENFLOWSTEP2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL94")]
        public double O2FlowStep3 { set; get; }                             // O2 интенсивность шаг 3                    # SP_CX_OXYGENFLOWSTEP3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL98")]
        public double O2FlowStep4 { set; get; }                             // O2 интенсивность шаг 4                    # SP_CX_OXYGENFLOWSTEP4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL102")]
        public double O2FlowStep5 { set; get; }                             // O2 интенсивность шаг 5                    # SP_CX_OXYGENFLOWSTEP5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL106")]
        public double O2FlowStep6 { set; get; }                             // O2 интенсивность шаг 6                    # SP_CX_OXYGENFLOWSTEP6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL110")]
        public double O2FlowStep7 { set; get; }                             // O2 интенсивность шаг 7                    # SP_CX_OXYGENFLOWSTEP7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL114")]
        public double O2FlowStep8 { set; get; }                             // O2 интенсивность шаг 8                    # SP_CX_OXYGENFLOWSTEP8
        
        [DataMember]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL118")]
        [DBPoint(IsStored = true)]
        public double O2FlowStep9 { set; get; }                             // O2 интенсивность шаг 9                    # SP_CX_OXYGENFLOWSTEP9
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL122")]
        public double O2FlowStep10 { set; get; }                             // O2 интенсивность шаг 10                    # SP_CX_OXYGENFLOWSTEP10
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL126")]
        public double O2FlowStep11 { set; get; }                             // O2 интенсивность шаг 11                    # SP_CX_OXYGENFLOWSTEP11
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL130")]
        public double O2FlowStep12 { set; get; }                             // O2 интенсивность шаг 12                    # SP_CX_OXYGENFLOWSTEP12
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL134")]
        public double O2FlowStep13 { set; get; }                             // O2 интенсивность шаг 13                    # SP_CX_OXYGENFLOWSTEP13
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL138")]
        public double O2FlowStep14 { set; get; }                             // O2 интенсивность шаг 14                    # SP_CX_OXYGENFLOWSTEP14
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL142")]
        public double O2FlowStep15 { set; get; }                             // O2 интенсивность шаг 15                    # SP_CX_OXYGENFLOWSTEP15

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL146")]
        public double O2FlowStep16 { set; get; }                             // O2 интенсивность шаг 16                    # SP_CX_OXYGENFLOWSTEP16
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL150")]
        public double O2FlowStep17 { set; get; }                             // O2 интенсивность шаг 17                    # SP_CX_OXYGENFLOWSTEP17
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL154")]
        public double O2FlowStep18 { set; get; }                             // O2 интенсивность шаг 18                    # SP_CX_OXYGENFLOWSTEP18
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL158")]
        public double O2FlowStep19 { set; get; }                             // O2 интенсивность шаг 19                    # SP_CX_OXYGENFLOWSTEP19
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL162")]
        public double O2FlowStep20 { set; get; }                             // O2 интенсивность шаг 20                    # SP_CX_OXYGENFLOWSTEP20

        public comBlowingSchemaEvent()
        {
            // Задаем значения по умолчанию
            O2SchemaNumber = 0;

            LancePositionStep1 = -1;
            LancePositionStep2 = -1;
            LancePositionStep3 = -1;
            LancePositionStep4 = -1;
            LancePositionStep5 = -1;
            LancePositionStep6 = -1;
            LancePositionStep7 = -1;
            LancePositionStep8 = -1;
            LancePositionStep9 = -1;
            LancePositionStep10 = -1;
            LancePositionStep11 = -1;
            LancePositionStep12 = -1;
            LancePositionStep13 = -1;
            LancePositionStep14 = -1;
            LancePositionStep15 = -1;
            LancePositionStep16 = -1;
            LancePositionStep17 = -1;
            LancePositionStep18 = -1;
            LancePositionStep19 = -1;
            LancePositionStep20 = -1;

            O2VolStep1 = -1;
            O2VolStep2 = -1;
            O2VolStep3 = -1;
            O2VolStep4 = -1;
            O2VolStep5 = -1;
            O2VolStep6 = -1;
            O2VolStep7 = -1;
            O2VolStep8 = -1;
            O2VolStep9 = -1;
            O2VolStep10 = -1;
            O2VolStep11 = -1;
            O2VolStep12 = -1;
            O2VolStep13 = -1;
            O2VolStep14 = -1;
            O2VolStep15 = -1;
            O2VolStep16 = -1;
            O2VolStep17 = -1;
            O2VolStep18 = -1;
            O2VolStep19 = -1;
            O2VolStep20 = -1;

            O2FlowStep1 = -1;
            O2FlowStep2 = -1;
            O2FlowStep3 = -1;
            O2FlowStep4 = -1;
            O2FlowStep5 = -1;
            O2FlowStep6 = -1;
            O2FlowStep7 = -1;
            O2FlowStep8 = -1;
            O2FlowStep9 = -1;
            O2FlowStep10 = -1;
            O2FlowStep11 = -1;
            O2FlowStep12 = -1;
            O2FlowStep13 = -1;
            O2FlowStep14 = -1;
            O2FlowStep15 = -1;
            O2FlowStep16 = -1;
            O2FlowStep17 = -1;
            O2FlowStep18 = -1;
            O2FlowStep19 = -1;
            O2FlowStep20 = -1;

        }

    }
}

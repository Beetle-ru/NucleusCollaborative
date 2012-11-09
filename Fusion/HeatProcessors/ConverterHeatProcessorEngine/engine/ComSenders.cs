using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonTypes;
using Converter;
using ConnectionProvider;
using Implements;

namespace ConverterHeatProcessorEngine
{
    public static partial class HeatEngine
    {
        public static int ComSender(List<LanceQuant> lanceStepsFrame)
        {
            comBlowingSchemaEvent blowingSchemaEvent = new comBlowingSchemaEvent();

            blowingSchemaEvent.LancePositionStep1 = lanceStepsFrame[0].LancePositin;
            blowingSchemaEvent.O2FlowStep1 = lanceStepsFrame[0].O2Flow;
            blowingSchemaEvent.O2VolStep1 = lanceStepsFrame[0].O2Volume;
            blowingSchemaEvent.LancePositionStep2 = lanceStepsFrame[1].LancePositin;
            blowingSchemaEvent.O2FlowStep2 = lanceStepsFrame[1].O2Flow;
            blowingSchemaEvent.O2VolStep2 = lanceStepsFrame[1].O2Volume;
            blowingSchemaEvent.LancePositionStep3 = lanceStepsFrame[2].LancePositin;
            blowingSchemaEvent.O2FlowStep3 = lanceStepsFrame[2].O2Flow;
            blowingSchemaEvent.O2VolStep3 = lanceStepsFrame[2].O2Volume;
            blowingSchemaEvent.LancePositionStep4 = lanceStepsFrame[3].LancePositin;
            blowingSchemaEvent.O2FlowStep4 = lanceStepsFrame[3].O2Flow;
            blowingSchemaEvent.O2VolStep4 = lanceStepsFrame[3].O2Volume;
            blowingSchemaEvent.LancePositionStep5 = lanceStepsFrame[4].LancePositin;
            blowingSchemaEvent.O2FlowStep5 = lanceStepsFrame[4].O2Flow;
            blowingSchemaEvent.O2VolStep5 = lanceStepsFrame[4].O2Volume;
            blowingSchemaEvent.LancePositionStep6 = lanceStepsFrame[5].LancePositin;
            blowingSchemaEvent.O2FlowStep6 = lanceStepsFrame[5].O2Flow;
            blowingSchemaEvent.O2VolStep6 = lanceStepsFrame[5].O2Volume;
            blowingSchemaEvent.LancePositionStep7 = lanceStepsFrame[6].LancePositin;
            blowingSchemaEvent.O2FlowStep7 = lanceStepsFrame[6].O2Flow;
            blowingSchemaEvent.O2VolStep7 = lanceStepsFrame[6].O2Volume;
            blowingSchemaEvent.LancePositionStep8 = lanceStepsFrame[7].LancePositin;
            blowingSchemaEvent.O2FlowStep8 = lanceStepsFrame[7].O2Flow;
            blowingSchemaEvent.O2VolStep8 = lanceStepsFrame[7].O2Volume;
            blowingSchemaEvent.LancePositionStep9 = lanceStepsFrame[8].LancePositin;
            blowingSchemaEvent.O2FlowStep9 = lanceStepsFrame[8].O2Flow;
            blowingSchemaEvent.O2VolStep9 = lanceStepsFrame[8].O2Volume;
            blowingSchemaEvent.LancePositionStep10 = lanceStepsFrame[9].LancePositin;
            blowingSchemaEvent.O2FlowStep10 = lanceStepsFrame[9].O2Flow;
            blowingSchemaEvent.O2VolStep10 = lanceStepsFrame[9].O2Volume;
            blowingSchemaEvent.LancePositionStep11 = lanceStepsFrame[10].LancePositin;
            blowingSchemaEvent.O2FlowStep11 = lanceStepsFrame[10].O2Flow;
            blowingSchemaEvent.O2VolStep11 = lanceStepsFrame[10].O2Volume;
            blowingSchemaEvent.LancePositionStep12 = lanceStepsFrame[11].LancePositin;
            blowingSchemaEvent.O2FlowStep12 = lanceStepsFrame[11].O2Flow;
            blowingSchemaEvent.O2VolStep12 = lanceStepsFrame[11].O2Volume;
            blowingSchemaEvent.LancePositionStep13 = lanceStepsFrame[12].LancePositin;
            blowingSchemaEvent.O2FlowStep13 = lanceStepsFrame[12].O2Flow;
            blowingSchemaEvent.O2VolStep13 = lanceStepsFrame[12].O2Volume;
            blowingSchemaEvent.LancePositionStep14 = lanceStepsFrame[13].LancePositin;
            blowingSchemaEvent.O2FlowStep14 = lanceStepsFrame[13].O2Flow;
            blowingSchemaEvent.O2VolStep14 = lanceStepsFrame[13].O2Volume;
            blowingSchemaEvent.LancePositionStep15 = lanceStepsFrame[14].LancePositin;
            blowingSchemaEvent.O2FlowStep15 = lanceStepsFrame[14].O2Flow;
            blowingSchemaEvent.O2VolStep15 = lanceStepsFrame[14].O2Volume;
            blowingSchemaEvent.LancePositionStep16 = lanceStepsFrame[15].LancePositin;
            blowingSchemaEvent.O2FlowStep16 = lanceStepsFrame[15].O2Flow;
            blowingSchemaEvent.O2VolStep16 = lanceStepsFrame[15].O2Volume;
            blowingSchemaEvent.LancePositionStep17 = lanceStepsFrame[16].LancePositin;
            blowingSchemaEvent.O2FlowStep17 = lanceStepsFrame[16].O2Flow;
            blowingSchemaEvent.O2VolStep17 = lanceStepsFrame[16].O2Volume;
            blowingSchemaEvent.LancePositionStep18 = lanceStepsFrame[17].LancePositin;
            blowingSchemaEvent.O2FlowStep18 = lanceStepsFrame[17].O2Flow;
            blowingSchemaEvent.O2VolStep18 = lanceStepsFrame[17].O2Volume;
            blowingSchemaEvent.LancePositionStep19 = lanceStepsFrame[18].LancePositin;
            blowingSchemaEvent.O2FlowStep19 = lanceStepsFrame[18].O2Flow;
            blowingSchemaEvent.O2VolStep19 = lanceStepsFrame[18].O2Volume;
            blowingSchemaEvent.LancePositionStep20 = lanceStepsFrame[19].LancePositin;
            blowingSchemaEvent.O2FlowStep20 = lanceStepsFrame[19].O2Flow;
            blowingSchemaEvent.O2VolStep20 = lanceStepsFrame[19].O2Volume;

            m_pushGate.PushEvent(blowingSchemaEvent);
            Thread.Sleep(DelayRefrashData);
            m_pushGate.PushEvent(new cntBlowingSchemaEvent());
            return 0;
        }

        public static int ComSender(List<AdditionsQuant> additionsStepsFrame)
        {
            comAdditionsSchemaEvent comAdditionsSchema = new comAdditionsSchemaEvent();

            //порция 1
            comAdditionsSchema.Material1Portion1Weight = additionsStepsFrame[0].Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion1Material1 = additionsStepsFrame[0].Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion1Weight = additionsStepsFrame[0].Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion1Material2 = additionsStepsFrame[0].Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion1Weight = additionsStepsFrame[0].Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion1Material3 = additionsStepsFrame[0].Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion1Weight = additionsStepsFrame[0].Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion1Material4 = additionsStepsFrame[0].Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion1Weight = additionsStepsFrame[0].Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion1Material5 = additionsStepsFrame[0].Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion1Weight = additionsStepsFrame[0].Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion1Material6 = additionsStepsFrame[0].Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion1Weight = additionsStepsFrame[0].Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion1Material7 = additionsStepsFrame[0].Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion1Weight = additionsStepsFrame[0].Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion1Material8 = additionsStepsFrame[0].Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion1Weight = additionsStepsFrame[0].Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion1Material9 = additionsStepsFrame[0].Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion1Weight = additionsStepsFrame[0].Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion1Material10 = additionsStepsFrame[0].Addition[9].O2VolPortionMaterial;
            //порция 2
            comAdditionsSchema.Material1Portion2Weight = additionsStepsFrame[1].Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion2Material1 = additionsStepsFrame[1].Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion2Weight = additionsStepsFrame[1].Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion2Material2 = additionsStepsFrame[1].Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion2Weight = additionsStepsFrame[1].Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion2Material3 = additionsStepsFrame[1].Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion2Weight = additionsStepsFrame[1].Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion2Material4 = additionsStepsFrame[1].Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion2Weight = additionsStepsFrame[1].Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion2Material5 = additionsStepsFrame[1].Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion2Weight = additionsStepsFrame[1].Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion2Material6 = additionsStepsFrame[1].Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion2Weight = additionsStepsFrame[1].Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion2Material7 = additionsStepsFrame[1].Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion2Weight = additionsStepsFrame[1].Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion2Material8 = additionsStepsFrame[1].Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion2Weight = additionsStepsFrame[1].Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion2Material9 = additionsStepsFrame[1].Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion2Weight = additionsStepsFrame[1].Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion2Material10 = additionsStepsFrame[1].Addition[9].O2VolPortionMaterial;
            //порция 3
            comAdditionsSchema.Material1Portion3Weight = additionsStepsFrame[2].Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion3Material1 = additionsStepsFrame[2].Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion3Weight = additionsStepsFrame[2].Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion3Material2 = additionsStepsFrame[2].Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion3Weight = additionsStepsFrame[2].Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion3Material3 = additionsStepsFrame[2].Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion3Weight = additionsStepsFrame[2].Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion3Material4 = additionsStepsFrame[2].Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion3Weight = additionsStepsFrame[2].Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion3Material5 = additionsStepsFrame[2].Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion3Weight = additionsStepsFrame[2].Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion3Material6 = additionsStepsFrame[2].Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion3Weight = additionsStepsFrame[2].Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion3Material7 = additionsStepsFrame[2].Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion3Weight = additionsStepsFrame[2].Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion3Material8 = additionsStepsFrame[2].Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion3Weight = additionsStepsFrame[2].Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion3Material9 = additionsStepsFrame[2].Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion3Weight = additionsStepsFrame[2].Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion3Material10 = additionsStepsFrame[2].Addition[9].O2VolPortionMaterial;


            m_pushGate.PushEvent(comAdditionsSchema);
            m_pushGate.PushEvent(new cntAdditionsSchemaEvent());
            return 0;
        }

        public static int ComSender(AdditionsQuant additionsFrame)
        {
            comAdditionsSchemaEvent comAdditionsSchema = new comAdditionsSchemaEvent();

            //порция 1
            comAdditionsSchema.Material1Portion1Weight = additionsFrame.Addition[0].MaterialPortionWeight;  //материал 1
            comAdditionsSchema.O2VolPortion1Material1 = additionsFrame.Addition[0].O2VolPortionMaterial;
            comAdditionsSchema.Material2Portion1Weight = additionsFrame.Addition[1].MaterialPortionWeight;  //материал 2
            comAdditionsSchema.O2VolPortion1Material2 = additionsFrame.Addition[1].O2VolPortionMaterial;
            comAdditionsSchema.Material3Portion1Weight = additionsFrame.Addition[2].MaterialPortionWeight;  //материал 3
            comAdditionsSchema.O2VolPortion1Material3 = additionsFrame.Addition[2].O2VolPortionMaterial;
            comAdditionsSchema.Material4Portion1Weight = additionsFrame.Addition[3].MaterialPortionWeight;  //материал 4
            comAdditionsSchema.O2VolPortion1Material4 = additionsFrame.Addition[3].O2VolPortionMaterial;
            comAdditionsSchema.Material5Portion1Weight = additionsFrame.Addition[4].MaterialPortionWeight;  //материал 5
            comAdditionsSchema.O2VolPortion1Material5 = additionsFrame.Addition[4].O2VolPortionMaterial;
            comAdditionsSchema.Material6Portion1Weight = additionsFrame.Addition[5].MaterialPortionWeight;  //материал 6
            comAdditionsSchema.O2VolPortion1Material6 = additionsFrame.Addition[5].O2VolPortionMaterial;
            comAdditionsSchema.Material7Portion1Weight = additionsFrame.Addition[6].MaterialPortionWeight;  //материал 7
            comAdditionsSchema.O2VolPortion1Material7 = additionsFrame.Addition[6].O2VolPortionMaterial;
            comAdditionsSchema.Material8Portion1Weight = additionsFrame.Addition[7].MaterialPortionWeight;  //материал 8
            comAdditionsSchema.O2VolPortion1Material8 = additionsFrame.Addition[7].O2VolPortionMaterial;
            comAdditionsSchema.Material9Portion1Weight = additionsFrame.Addition[8].MaterialPortionWeight;  //материал 9
            comAdditionsSchema.O2VolPortion1Material9 = additionsFrame.Addition[8].O2VolPortionMaterial;
            comAdditionsSchema.Material10Portion1Weight = additionsFrame.Addition[9].MaterialPortionWeight; //материал 10
            comAdditionsSchema.O2VolPortion1Material10 = additionsFrame.Addition[9].O2VolPortionMaterial;

            m_pushGate.PushEvent(comAdditionsSchema);
            m_pushGate.PushEvent(new cntAdditionsSchemaEvent());
            return 0;
        }

        public static int ComSenderMaterialNames(SteelMakingPatternEvent smPatt)
        {
            comAdditionsEvent comANames = new comAdditionsEvent();
            for (int i = 0; i < smPatt.materialsName.Count; i++)
            {
                if (smPatt.materialsName[i] == null || smPatt.materialsName[i] == "")
                {
                    smPatt.materialsName[i] = " ";
                }
                while (smPatt.materialsName[i].Length < 6)
                {
                    smPatt.materialsName[i] += " ";
                }
                if (smPatt.materialsName[i].Length > 6)
                {
                    //int extra = smPatt.materialsName[i].Length - 6;;
                    smPatt.materialsName[i] = smPatt.materialsName[i].Remove(6);

                }
            }

            comANames.Bunker1MaterialName = smPatt.materialsName[0];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[0], "Material 1", InstantLogger.TypeMessage.normal);
            comANames.Bunker2MaterialName = smPatt.materialsName[1];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[1], "Material 2", InstantLogger.TypeMessage.normal);
            comANames.Bunker3MaterialName = smPatt.materialsName[2];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[2], "Material 3", InstantLogger.TypeMessage.normal);
            comANames.Bunker4MaterialName = smPatt.materialsName[3];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[3], "Material 4", InstantLogger.TypeMessage.normal);
            comANames.Bunker5MaterialName = smPatt.materialsName[4];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[4], "Material 5", InstantLogger.TypeMessage.normal);
            comANames.Bunker6MaterialName = smPatt.materialsName[5];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[5], "Material 6", InstantLogger.TypeMessage.normal);
            comANames.Bunker7MaterialName = smPatt.materialsName[6];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[6], "Material 7", InstantLogger.TypeMessage.normal);
            comANames.Bunker8MaterialName = smPatt.materialsName[7];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[7], "Material 8", InstantLogger.TypeMessage.normal);
            comANames.Bunker9MaterialName = smPatt.materialsName[8];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[8], "Material 9", InstantLogger.TypeMessage.normal);
            comANames.Bunker10MaterialName = smPatt.materialsName[9];
            InstantLogger.log("Material name loaded = " + smPatt.materialsName[9], "Material 10", InstantLogger.TypeMessage.normal);

            
            m_pushGate.PushEvent(comANames);
            Random rnd1 = new Random();
            m_pushGate.PushEvent(new cntAdditionsEvent() {Counter = rnd1.Next(1000)});
            return 0;
        }

        public static int SenderWeigherLoadMaterial(List<WeigherQuant> weigherQuant)
        {
            // m_pushGate.PushEvent(new HeatSchemaStepEvent() { Step = currentStep });
            //comAdditionsSchemaEvent bunkers = new comAdditionsSchemaEvent();

            var jobW3Event = new comJobW3Event();
            var jobW4Event = new comJobW4Event();
            var jobW5Event = new comJobW5Event();
            var jobW6Event = new comJobW6Event();
            var jobW7Event = new comJobW7Event();

            var w3HaveJob = false;
            var w4HaveJob = false;
            var w5HaveJob = false;
            var w6HaveJob = false;
            var w7HaveJob = false;

            // заполняем значениями, что не заполнили останется -1 - важно для правильной работы весов(только 1 материал грузим)
            int weigher = 0;
            if (weigherQuant[weigher].BunkerId == 0)                                           // W3 RB5 (0,0)
            {
                jobW3Event.RB5Weight = weigherQuant[weigher].PortionWeight;
                jobW3Event.RB5Oxygen = weigherQuant[weigher].OxygenTreshold;
                
                if (jobW3Event.RB5Weight > 0)
                {
                    w3HaveJob = true;
                }
            }
            if (weigherQuant[weigher].BunkerId == 1)                                           // W3 RB6 (0,1)
            {
                jobW3Event.RB6Weight = weigherQuant[weigher].PortionWeight;
                jobW3Event.RB6Oxygen = weigherQuant[weigher].OxygenTreshold;
                
                if (jobW3Event.RB6Weight > 0)
                {
                    w3HaveJob = true;
                }
            }
            weigher = 1;
            if (weigherQuant[weigher].BunkerId == 2)                                           // W4 RB7 (1,2)
            {
                jobW4Event.RB7Weight = weigherQuant[weigher].PortionWeight;
                jobW4Event.RB7Oxygen = weigherQuant[weigher].OxygenTreshold;

                if (jobW4Event.RB7Weight > 0)
                {
                    w4HaveJob = true;
                }
            }
            weigher = 2;
            if (weigherQuant[weigher].BunkerId == 3)                                           // W5 RB8 (2,3)
            {
                jobW5Event.RB8Weight = weigherQuant[weigher].PortionWeight;
                jobW5Event.RB8Oxygen = weigherQuant[weigher].OxygenTreshold;

                if (jobW5Event.RB8Weight > 0)
                {
                    w5HaveJob = true;
                }
            }
            weigher = 3;
            if (weigherQuant[weigher].BunkerId == 4)                                           // W6 RB9 (3,4)
            {
                jobW6Event.RB9Weight = weigherQuant[weigher].PortionWeight;
                jobW6Event.RB9Oxygen = weigherQuant[weigher].OxygenTreshold;

                if (jobW6Event.RB9Weight > 0)
                {
                    w6HaveJob = true;
                }
            }
            weigher = 4;
            if (weigherQuant[weigher].BunkerId == 5)                                           // W7 RB10 (4,5)
            {
                jobW7Event.RB10Weight = weigherQuant[weigher].PortionWeight;
                jobW7Event.RB10Oxygen = weigherQuant[weigher].OxygenTreshold;

                if (jobW7Event.RB10Weight > 0)
                {
                    w7HaveJob = true;
                }
            }
            if (weigherQuant[weigher].BunkerId == 6)                                           // W7 RB11 (4,6)
            {
                jobW7Event.RB11Weight = weigherQuant[weigher].PortionWeight;
                jobW7Event.RB11Oxygen = weigherQuant[weigher].OxygenTreshold;

                if (jobW7Event.RB11Weight > 0)
                {
                    w7HaveJob = true;
                }
            }
            if (weigherQuant[weigher].BunkerId == 7)                                           // W7 RB12 (4,7)
            {
                jobW7Event.RB12Weight = weigherQuant[weigher].PortionWeight;
                jobW7Event.RB12Oxygen = weigherQuant[weigher].OxygenTreshold;

                if (jobW7Event.RB12Weight > 0)
                {
                    w7HaveJob = true;
                }
            }
            //  m_pushGate.PushEvent(bunkers);                                                     // отправляем задание

            for (int i = 0; i < weigherQuant.Count; i++)
            {
                var quant = weigherQuant[i];
                if (quant.PortionWeight > 0)
                {
                    InstantLogger.log("JOB SENDER:");
                    Thread.Sleep(DelayRefrashData);

                    if (w3HaveJob)
                    {
                        m_pushGate.PushEvent(jobW3Event);
                        InstantLogger.log(string.Format("PushEvent: jobW3Event ==> {0}", jobW3Event.ToString()));
                    }
                    if (w4HaveJob)
                    {
                        m_pushGate.PushEvent(jobW4Event);
                        InstantLogger.log(string.Format("PushEvent: jobW4Event ==> {0}", jobW4Event.ToString()));
                    }
                    if (w5HaveJob)
                    {
                        m_pushGate.PushEvent(jobW5Event);
                        InstantLogger.log(string.Format("PushEvent: jobW5Event ==> {0}", jobW5Event.ToString()));
                    }
                    if (w6HaveJob)
                    {
                        m_pushGate.PushEvent(jobW6Event);
                        InstantLogger.log(string.Format("PushEvent: jobW6Event ==> {0}", jobW6Event.ToString()));
                    }
                    if (w7HaveJob)
                    {
                        m_pushGate.PushEvent(jobW7Event);
                        InstantLogger.log(string.Format("PushEvent: jobW7Event ==> {0}", jobW7Event.ToString()));
                    }
                    
                    Thread.Sleep(DelayRefrashData);

                    if (w3HaveJob)
                    {
                        m_pushGate.PushEvent(new cntWeigher3JobReadyEvent() { Counter = m_cntWeighersJobReady[0]++ });              // подтверждаем задание для весов w3
                        InstantLogger.log(string.Format("job for w3 ==> {0}", weigherQuant[0].ToString()));
                    }
                    if (w4HaveJob)
                    {
                        m_pushGate.PushEvent(new cntWeigher4JobReadyEvent() { Counter = m_cntWeighersJobReady[1]++ });              // подтверждаем задание для весов w4
                        InstantLogger.log(string.Format("job for w4 ==> {0}", weigherQuant[1].ToString()));
                    }
                    if (w5HaveJob)
                    {
                        m_pushGate.PushEvent(new cntWeigher5JobReadyEvent() { Counter = m_cntWeighersJobReady[2]++ });              // подтверждаем задание для весов w5
                        InstantLogger.log(string.Format("job for w5 ==> {0}", weigherQuant[2].ToString()));
                    }
                    if (w6HaveJob)
                    {
                        m_pushGate.PushEvent(new cntWeigher6JobReadyEvent() { Counter = m_cntWeighersJobReady[3]++ });              // подтверждаем задание для весов w6
                        InstantLogger.log(string.Format("job for w6 ==> {0}", weigherQuant[3].ToString()));
                    }
                    if (w7HaveJob)
                    {
                        m_pushGate.PushEvent(new cntWeigher7JobReadyEvent() { Counter = m_cntWeighersJobReady[4]++ });              // подтверждаем задание для весов w7
                        InstantLogger.log(string.Format("job for w7 ==> {0}", weigherQuant[4].ToString()));
                        //InstantLogger.log(string.Format("job for w7 ==> {0}", jobW7Event.ToString()));
                    }
                    
                    Thread.Sleep(DelayRefrashData);
                    
                    break;
                }
            }
            return 0;
        }

        public static int ComSendOxygenMode(int weigherId, bool isVirtualOxygen)
        {
            switch (weigherId)
                       {
                           case 0:
                               m_pushGate.PushEvent(new comSelectOxygenModeW3Event()
                                                        {SelectOxygenSimulation = ConvertBoolToInt(isVirtualOxygen)});
                               break;
                           case 1:
                               m_pushGate.PushEvent(new comSelectOxygenModeW4Event() 
                                                        { SelectOxygenSimulation = ConvertBoolToInt(isVirtualOxygen) });
                               break;
                           case 2:
                               m_pushGate.PushEvent(new comSelectOxygenModeW5Event() 
                                                        { SelectOxygenSimulation = ConvertBoolToInt(isVirtualOxygen) });
                               break;
                           case 3:
                               m_pushGate.PushEvent(new comSelectOxygenModeW6Event() 
                                                        { SelectOxygenSimulation = ConvertBoolToInt(isVirtualOxygen) });
                               break;
                           case 4:
                               m_pushGate.PushEvent(new comSelectOxygenModeW7Event() 
                                                        { SelectOxygenSimulation = ConvertBoolToInt(isVirtualOxygen) });
                               break;
                       }
            return 0;
        }
        public static int SenderCurrentStep(int currentStep)
        {
           // HeatSchemaStepEvent heatSchemaStepEvent = new HeatSchemaStepEvent() { Step = currentStep };
            m_pushGate.PushEvent    (new HeatSchemaStepEvent() { Step = currentStep });
            return 0;
        }

        /// <summary>
        /// Запрос состояния весов
        /// </summary>
        public  static void RequestWeighersState()
        {
            m_pushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(WeighersStateEvent).Name });
        }
        public static void ResetAllState(int cntValue)
        {
            //m_pushGate.PushEvent();
            m_pushGate.PushEvent(new comAdditionsSchemaEvent());

            m_pushGate.PushEvent(m_lanceHeight > 1
                                     ? new comBlowingSchemaEvent() {LancePositionStep1 = m_lanceHeight, O2VolStep1 = 0}
                                     : new comBlowingSchemaEvent());

            Thread.Sleep(DelayRefrashData);
            m_pushGate.PushEvent(new cntWeigher3JobReadyEvent() { Counter = cntValue });
            m_pushGate.PushEvent(new cntWeigher4JobReadyEvent() { Counter = cntValue });
            m_pushGate.PushEvent(new cntWeigher5JobReadyEvent() { Counter = cntValue });
            m_pushGate.PushEvent(new cntWeigher6JobReadyEvent() { Counter = cntValue });
            m_pushGate.PushEvent(new cntWeigher7JobReadyEvent() { Counter = cntValue });
            //m_pushGate.PushEvent(new comBlowingSchemaEvent());

            m_pushGate.PushEvent(new cntBlowingSchemaEvent() { Counter = cntValue });
            m_pushGate.PushEvent(new cntO2FlowRateEvent() { Counter = cntValue });

            for (int i = 0; i < WeightCounter; i++)
            {
                ComSendOxygenMode(i, false);
            }

            Thread.Sleep(DelayRefrashData);
        }
        public static void SetControlMode(bool isAutomatic)
        {
            var fex = new ConnectionProvider.FlexHelper("OPC.ComControlMode");
            if (isAutomatic)
            {
                fex.AddArg("LanceMode", 3);
                fex.AddArg("VpathMode", 3);
            }
            else
            {
                fex.AddArg("LanceMode", 0);
                fex.AddArg("VpathMode", 0);
            }
            fex.Fire(m_pushGate);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterHeatProcessorEngine
{
    class WatchSteps
    {
        public int m_currentStep; //private
        public readonly List<bool> m_stepComplete; //private
        public WatchSteps()
        {
            m_currentStep = 0;
            m_stepComplete = new List<bool> {false};
        }
        public int Increase()
        {
            m_currentStep++;
            if (m_stepComplete.Count <= m_currentStep)
            {
                m_stepComplete.Add(false);
            }
            return m_currentStep;
        }

        public int IncreaseComplete()
        {
            m_stepComplete[m_currentStep] = true;
            m_currentStep++;
            if (m_stepComplete.Count <= m_currentStep)
            {
                m_stepComplete.Add(false);
            }
            return m_currentStep;
        }

        public int GetCurrentStep()
        {
            return m_currentStep;
        }

        public bool GetStepCompleteStatus(int stepNumber)
        {
            if (stepNumber > m_stepComplete.Count)
            {
                return false;
            }
            return m_stepComplete[stepNumber];
        }

        public bool GetCurrentStepCompleteStatus()
        {
            return m_stepComplete[m_currentStep];
        }

        public int SetStepCompleteStatus(int stepNumber, bool status)
        {
            if (stepNumber > m_stepComplete.Count)
            {
                return 1;
            }
            m_stepComplete[stepNumber] = status;
            return 0;
        }

        public int SetCurrentStepCompleteStatus(bool status)
        {
            m_stepComplete[m_currentStep] = status;
            return 0;
        }

        public bool GetStepAliveStatus(int stepNumber)
        {
            if (stepNumber > m_stepComplete.Count)
            {
                return false;
            }
            return true;
        }
        
    }
}

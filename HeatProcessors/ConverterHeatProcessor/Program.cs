using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConverterHeatProcessorEngine;
using Implements;

namespace ConverterHeatProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HeatEngine.Init();
                HeatEngine.HeatOn = true;
            }
            catch (Exception e)
            {
                InstantLogger.log(e.ToString(), "FATAL", InstantLogger.TypeMessage.important);
                throw;
            }
            
            InstantLogger.log("ConverterHeatProcessor", "Started", InstantLogger.TypeMessage.important);
            InstantLogger.log("Нажмите <ENTER> для выхода.");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.EventArgs
{
    public class CommunicationL1L2MatAddEventArgs : System.EventArgs
    {
        public List<DTO.L1L2_MatAddDTO> MaterialsAdded;

        /// <summary>
        /// Initializes a new instance of the CommunicationL1L2MatAddEventArgs class.
        /// </summary>
        /// <param name="aMaterialsAdded"></param>
        public CommunicationL1L2MatAddEventArgs(List<DTO.L1L2_MatAddDTO> aMaterialsAdded)
        {
            MaterialsAdded = aMaterialsAdded;
        }
    }
    public class CommunicationL1L2CyclicEventArgs : System.EventArgs
    {
        public DTO.L1L2_CyclicDTO CyclicData;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="aCyclicData"></param>
        public CommunicationL1L2CyclicEventArgs(DTO.L1L2_CyclicDTO aCyclicData)
        {
            CyclicData = aCyclicData;
        }
    }
    public class CommunicationL1L2TempMeasEventArgs : System.EventArgs
    {
        public DTO.L1L2_TempMeasDTO TempMeasData;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="aTempMeasData"></param>
        public CommunicationL1L2TempMeasEventArgs(DTO.L1L2_TempMeasDTO aTempMeasData)
        {
            TempMeasData = aTempMeasData;
        }
    }
    public class CommunicationL1L2CommandEventArgs : System.EventArgs
    {
        public Common.Enumerations.L1L2_Command Command;

        /// <summary>
        /// Initializes a new instance of the CommunicationL1L2CommandEventArgs class.
        /// </summary>
        /// <param name="aCommand"></param>
        public CommunicationL1L2CommandEventArgs(Common.Enumerations.L1L2_Command aCommand)
        {
            Command = aCommand;
        }
    }
}

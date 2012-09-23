using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Converter.API
{
    [ServiceContract(Namespace = "http://Converter.API", SessionMode = SessionMode.Required)]
    public interface IConverterAPI
    {
        [OperationContract()]
        Int64 GetHeatNumber();

        [OperationContract()]
        int GetConverterNumber();

        [OperationContract()]
        int GetTeamNumber();

        [OperationContract()]
        string GetGrade();

        [OperationContract()]
        SteelAttributes GetActualSteelAttributes();

        [OperationContract()]
        SteelAttributes GetPlannedSteelAttributes();

        [OperationContract()]
        DateTime GetBlowingStartTime();

        [OperationContract()]
        List<OffGasEvent> GetOffGasEvents();

        [OperationContract()]
        List<LanceEvent> GetLanceEvents();

        [OperationContract()]
        List<OffGasAnalysisEvent> GetOffGasAnalysisEvents();

        [OperationContract()]
        List<string> GetBlowingSchemas();

        [OperationContract()]
        comBlowingSchemaEvent[] LoadBlowingSchema(string schemaName);

        [OperationContract()]
        void SaveBlowingSchema(string schemaName, comBlowingSchemaEvent[] schema);

        [OperationContract()]
        void RunBlowingSchema(string schemaName);

    }
}
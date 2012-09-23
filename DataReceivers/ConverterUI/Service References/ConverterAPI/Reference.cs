﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using Converter;

namespace ConverterUI.ConverterAPI {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Converter.API", ConfigurationName="ConverterAPI.IConverterAPI", SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IConverterAPI {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetHeatNumber", ReplyAction="http://Converter.API/IConverterAPI/GetHeatNumberResponse")]
        int GetHeatNumber();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetConverterNumber", ReplyAction="http://Converter.API/IConverterAPI/GetConverterNumberResponse")]
        int GetConverterNumber();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetTeamNumber", ReplyAction="http://Converter.API/IConverterAPI/GetTeamNumberResponse")]
        int GetTeamNumber();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetGrade", ReplyAction="http://Converter.API/IConverterAPI/GetGradeResponse")]
        string GetGrade();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetActualSteelAttributes", ReplyAction="http://Converter.API/IConverterAPI/GetActualSteelAttributesResponse")]
        SteelAttributes GetActualSteelAttributes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetPlannedSteelAttributes", ReplyAction="http://Converter.API/IConverterAPI/GetPlannedSteelAttributesResponse")]
        SteelAttributes GetPlannedSteelAttributes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetBlowingStartTime", ReplyAction="http://Converter.API/IConverterAPI/GetBlowingStartTimeResponse")]
        System.DateTime GetBlowingStartTime();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetOffGasEvents", ReplyAction="http://Converter.API/IConverterAPI/GetOffGasEventsResponse")]
        Converter.OffGasEvent[] GetOffGasEvents();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetLanceEvents", ReplyAction="http://Converter.API/IConverterAPI/GetLanceEventsResponse")]
        Converter.LanceEvent[] GetLanceEvents();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetOffGasAnalysisEvents", ReplyAction="http://Converter.API/IConverterAPI/GetOffGasAnalysisEventsResponse")]
        Converter.OffGasAnalysisEvent[] GetOffGasAnalysisEvents();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/GetBlowingSchemas", ReplyAction="http://Converter.API/IConverterAPI/GetBlowingSchemasResponse")]
        string[] GetBlowingSchemas();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/LoadBlowingSchema", ReplyAction="http://Converter.API/IConverterAPI/LoadBlowingSchemaResponse")]
        Converter.comBlowingSchemaEvent[] LoadBlowingSchema(string schemaName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/SaveBlowingSchema", ReplyAction="http://Converter.API/IConverterAPI/SaveBlowingSchemaResponse")]
        void SaveBlowingSchema(string schemaName, Converter.comBlowingSchemaEvent[] schema);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Converter.API/IConverterAPI/RunBlowingSchema", ReplyAction="http://Converter.API/IConverterAPI/RunBlowingSchemaResponse")]
        void RunBlowingSchema(string schemaName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IConverterAPIChannel : ConverterUI.ConverterAPI.IConverterAPI, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConverterAPIClient : System.ServiceModel.ClientBase<ConverterUI.ConverterAPI.IConverterAPI>, ConverterUI.ConverterAPI.IConverterAPI {
        
        public ConverterAPIClient() {
        }
        
        public ConverterAPIClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConverterAPIClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConverterAPIClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConverterAPIClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int GetHeatNumber() {
            return base.Channel.GetHeatNumber();
        }
        
        public int GetConverterNumber() {
            return base.Channel.GetConverterNumber();
        }
        
        public int GetTeamNumber() {
            return base.Channel.GetTeamNumber();
        }
        
        public string GetGrade() {
            return base.Channel.GetGrade();
        }
        
        public SteelAttributes GetActualSteelAttributes() {
            return base.Channel.GetActualSteelAttributes();
        }
        
        public SteelAttributes GetPlannedSteelAttributes() {
            return base.Channel.GetPlannedSteelAttributes();
        }
        
        public System.DateTime GetBlowingStartTime() {
            return base.Channel.GetBlowingStartTime();
        }
        
        public Converter.OffGasEvent[] GetOffGasEvents() {
            return base.Channel.GetOffGasEvents();
        }
        
        public Converter.LanceEvent[] GetLanceEvents() {
            return base.Channel.GetLanceEvents();
        }
        
        public Converter.OffGasAnalysisEvent[] GetOffGasAnalysisEvents() {
            return base.Channel.GetOffGasAnalysisEvents();
        }
        
        public string[] GetBlowingSchemas() {
            return base.Channel.GetBlowingSchemas();
        }
        
        public Converter.comBlowingSchemaEvent[] LoadBlowingSchema(string schemaName) {
            return base.Channel.LoadBlowingSchema(schemaName);
        }
        
        public void SaveBlowingSchema(string schemaName, Converter.comBlowingSchemaEvent[] schema) {
            base.Channel.SaveBlowingSchema(schemaName, schema);
        }
        
        public void RunBlowingSchema(string schemaName) {
            base.Channel.RunBlowingSchema(schemaName);
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileStorage.ConsoleClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IFileStorageService")]
    public interface IFileStorageService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/UploadFile", ReplyAction="http://tempuri.org/IFileStorageService/UploadFileResponse")]
        void UploadFile(System.IO.Stream fileData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/UploadFile", ReplyAction="http://tempuri.org/IFileStorageService/UploadFileResponse")]
        System.Threading.Tasks.Task UploadFileAsync(System.IO.Stream fileData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/GetFile", ReplyAction="http://tempuri.org/IFileStorageService/GetFileResponse")]
        System.IO.Stream GetFile();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/GetFile", ReplyAction="http://tempuri.org/IFileStorageService/GetFileResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> GetFileAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileStorageServiceChannel : FileStorage.ConsoleClient.ServiceReference1.IFileStorageService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileStorageServiceClient : System.ServiceModel.ClientBase<FileStorage.ConsoleClient.ServiceReference1.IFileStorageService>, FileStorage.ConsoleClient.ServiceReference1.IFileStorageService {
        
        public FileStorageServiceClient() {
        }
        
        public FileStorageServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileStorageServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileStorageServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileStorageServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void UploadFile(System.IO.Stream fileData) {
            base.Channel.UploadFile(fileData);
        }
        
        public System.Threading.Tasks.Task UploadFileAsync(System.IO.Stream fileData) {
            return base.Channel.UploadFileAsync(fileData);
        }
        
        public System.IO.Stream GetFile() {
            return base.Channel.GetFile();
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> GetFileAsync() {
            return base.Channel.GetFileAsync();
        }
    }
}
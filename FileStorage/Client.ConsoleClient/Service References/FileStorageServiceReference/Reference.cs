﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.WCFServiceConsoleClient.FileStorageServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RemoteFileStreamMessage", Namespace="http://schemas.datacontract.org/2004/07/WCFService.ServiceLibrary")]
    [System.SerializableAttribute()]
    public partial class RemoteFileStreamMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.IO.Stream dataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid fileIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string fileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long streamLengthField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.IO.Stream data {
            get {
                return this.dataField;
            }
            set {
                if ((object.ReferenceEquals(this.dataField, value) != true)) {
                    this.dataField = value;
                    this.RaisePropertyChanged("data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid fileId {
            get {
                return this.fileIdField;
            }
            set {
                if ((this.fileIdField.Equals(value) != true)) {
                    this.fileIdField = value;
                    this.RaisePropertyChanged("fileId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string fileName {
            get {
                return this.fileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.fileNameField, value) != true)) {
                    this.fileNameField = value;
                    this.RaisePropertyChanged("fileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long streamLength {
            get {
                return this.streamLengthField;
            }
            set {
                if ((this.streamLengthField.Equals(value) != true)) {
                    this.streamLengthField = value;
                    this.RaisePropertyChanged("streamLength");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FileStorageServiceReference.IFileStorageService")]
    public interface IFileStorageService {
        
        // CODEGEN: Generating message contract since the operation UploadFile is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/UploadFile", ReplyAction="http://tempuri.org/IFileStorageService/UploadFileResponse")]
        Client.WCFServiceConsoleClient.FileStorageServiceReference.UploadFileResponse UploadFile(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/UploadFile", ReplyAction="http://tempuri.org/IFileStorageService/UploadFileResponse")]
        System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.UploadFileResponse> UploadFileAsync(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 request);
        
        // CODEGEN: Generating message contract since the wrapper name (RemoteFileStreamRequest) of message RemoteFileStreamRequest does not match the default value (DownloadFile)
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/DownloadFile", ReplyAction="http://tempuri.org/IFileStorageService/DownloadFileResponse")]
        Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 DownloadFile(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/DownloadFile", ReplyAction="http://tempuri.org/IFileStorageService/DownloadFileResponse")]
        System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1> DownloadFileAsync(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/GetAllFilesMetadata", ReplyAction="http://tempuri.org/IFileStorageService/GetAllFilesMetadataResponse")]
        Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage[] GetAllFilesMetadata();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/GetAllFilesMetadata", ReplyAction="http://tempuri.org/IFileStorageService/GetAllFilesMetadataResponse")]
        System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage[]> GetAllFilesMetadataAsync();
        
        // CODEGEN: Generating message contract since the operation DeleteFile is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/DeleteFile", ReplyAction="http://tempuri.org/IFileStorageService/DeleteFileResponse")]
        Client.WCFServiceConsoleClient.FileStorageServiceReference.DeleteFileResponse DeleteFile(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileStorageService/DeleteFile", ReplyAction="http://tempuri.org/IFileStorageService/DeleteFileResponse")]
        System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.DeleteFileResponse> DeleteFileAsync(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RemoteFileStreamMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class RemoteFileStreamMessage1 {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public System.Guid fileId;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string fileName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public long streamLength;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream data;
        
        public RemoteFileStreamMessage1() {
        }
        
        public RemoteFileStreamMessage1(System.Guid fileId, string fileName, long streamLength, System.IO.Stream data) {
            this.fileId = fileId;
            this.fileName = fileName;
            this.streamLength = streamLength;
            this.data = data;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadFileResponse {
        
        public UploadFileResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RemoteFileStreamRequest", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class RemoteFileStreamRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public System.Guid fileId;
        
        public RemoteFileStreamRequest() {
        }
        
        public RemoteFileStreamRequest(System.Guid fileId) {
            this.fileId = fileId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DeleteFileResponse {
        
        public DeleteFileResponse() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileStorageServiceChannel : Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileStorageServiceClient : System.ServiceModel.ClientBase<Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService>, Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService {
        
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
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Client.WCFServiceConsoleClient.FileStorageServiceReference.UploadFileResponse Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService.UploadFile(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 request) {
            return base.Channel.UploadFile(request);
        }
        
        public void UploadFile(System.Guid fileId, string fileName, long streamLength, System.IO.Stream data) {
            Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 inValue = new Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1();
            inValue.fileId = fileId;
            inValue.fileName = fileName;
            inValue.streamLength = streamLength;
            inValue.data = data;
            Client.WCFServiceConsoleClient.FileStorageServiceReference.UploadFileResponse retVal = ((Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService)(this)).UploadFile(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.UploadFileResponse> Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService.UploadFileAsync(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 request) {
            return base.Channel.UploadFileAsync(request);
        }
        
        public System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.UploadFileResponse> UploadFileAsync(System.Guid fileId, string fileName, long streamLength, System.IO.Stream data) {
            Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 inValue = new Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1();
            inValue.fileId = fileId;
            inValue.fileName = fileName;
            inValue.streamLength = streamLength;
            inValue.data = data;
            return ((Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService)(this)).UploadFileAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService.DownloadFile(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request) {
            return base.Channel.DownloadFile(request);
        }
        
        public string DownloadFile(ref System.Guid fileId, out long streamLength, out System.IO.Stream data) {
            Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest inValue = new Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest();
            inValue.fileId = fileId;
            Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1 retVal = ((Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService)(this)).DownloadFile(inValue);
            fileId = retVal.fileId;
            streamLength = retVal.streamLength;
            data = retVal.data;
            return retVal.fileName;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1> Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService.DownloadFileAsync(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request) {
            return base.Channel.DownloadFileAsync(request);
        }
        
        public System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage1> DownloadFileAsync(System.Guid fileId) {
            Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest inValue = new Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest();
            inValue.fileId = fileId;
            return ((Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService)(this)).DownloadFileAsync(inValue);
        }
        
        public Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage[] GetAllFilesMetadata() {
            return base.Channel.GetAllFilesMetadata();
        }
        
        public System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamMessage[]> GetAllFilesMetadataAsync() {
            return base.Channel.GetAllFilesMetadataAsync();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Client.WCFServiceConsoleClient.FileStorageServiceReference.DeleteFileResponse Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService.DeleteFile(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request) {
            return base.Channel.DeleteFile(request);
        }
        
        public void DeleteFile(System.Guid fileId) {
            Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest inValue = new Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest();
            inValue.fileId = fileId;
            Client.WCFServiceConsoleClient.FileStorageServiceReference.DeleteFileResponse retVal = ((Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService)(this)).DeleteFile(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.DeleteFileResponse> Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService.DeleteFileAsync(Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest request) {
            return base.Channel.DeleteFileAsync(request);
        }
        
        public System.Threading.Tasks.Task<Client.WCFServiceConsoleClient.FileStorageServiceReference.DeleteFileResponse> DeleteFileAsync(System.Guid fileId) {
            Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest inValue = new Client.WCFServiceConsoleClient.FileStorageServiceReference.RemoteFileStreamRequest();
            inValue.fileId = fileId;
            return ((Client.WCFServiceConsoleClient.FileStorageServiceReference.IFileStorageService)(this)).DeleteFileAsync(inValue);
        }
    }
}

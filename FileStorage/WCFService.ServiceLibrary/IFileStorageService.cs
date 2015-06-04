using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService.ServiceLibrary
{
    [ServiceContract]
    public interface IFileStorageService
    {
        [OperationContract]
        void UploadFile(Stream fileData);

        [OperationContract]
        Stream GetFile(Guid fileId);

        [OperationContract]
        void UploadFileEnvelope(UploadStreamMessage fileData);

        //[OperationContract]
        //FileDataEnvelope GetFileEnvelope(Guid fileId);
    }
}

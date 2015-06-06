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
        Stream DownloadFile(Guid fileId);

        [OperationContract]
        void UploadFileWithMetadata(RemoteFileStreamMessage fileData);

        [OperationContract]
        RemoteFileStreamMessage DownloadFileWithMetadata(RemoteFileStreamMessage fileRequest);
    }
}

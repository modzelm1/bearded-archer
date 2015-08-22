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
        void UploadFile(RemoteFileStreamMessage fileData);

        [OperationContract]
        RemoteFileStreamMessage DownloadFile(RemoteFileStreamRequest fileRequest);

        [OperationContract]
        List<RemoteFileStreamMessage> GetAllFilesMetadata();

        [OperationContract]
        void DeleteFile(RemoteFileStreamRequest fileRequest);
    }
}

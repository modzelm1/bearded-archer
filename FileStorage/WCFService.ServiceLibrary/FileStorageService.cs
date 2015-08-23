using FileStorage;
using SQLStreamLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace WCFService.ServiceLibrary
{
    public class FileStorageService : IFileStorageService
    {
        public void UploadFile(RemoteFileStreamMessage fileData)
        {
            var sqlFileStorage = new SqlFileStorage();
            var fileEnvelope = new FileEnvelope()
            {
                FileId = fileData.fileId,
                FileName = fileData.fileName,
                FileData = fileData.data
            };
            sqlFileStorage.AddFile(fileEnvelope);
            fileData.data.Close();
        }

        public RemoteFileStreamMessage DownloadFile(RemoteFileStreamRequest fileRequest)
        {
            var sqlFileStorage = new SqlFileStorage();
            var fileEnvelope = sqlFileStorage.GetFileById(fileRequest.fileId);

            var remoteFileStreamMessage = new RemoteFileStreamMessage()
            {
                fileId = fileEnvelope.FileId,
                streamLength = fileEnvelope.FileData.Length,
                fileName = fileEnvelope.FileName,
                data = fileEnvelope.FileData 
            };

            return remoteFileStreamMessage;
        }

        public List<RemoteFileStreamMessage> GetAllFilesMetadata()
        {
            var sqlFileStorage = new SqlFileStorage();
            var fileEnvelopesList =  sqlFileStorage.GetAllFilesMetadata();
            var remoteFilesList = fileEnvelopesList.Select(x =>
                new RemoteFileStreamMessage()
                {
                    fileId = x.FileId,
                    fileName = x.FileName, 
                    data = null, 
                    streamLength = -1
                }).ToList();

            return remoteFilesList;
        }

        public void DeleteFile(RemoteFileStreamRequest fileRequest)
        {
            var sqlFileStorage = new SqlFileStorage();
            sqlFileStorage.DeleteFileById(fileRequest.fileId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService.ServiceLibrary
{
    public class FileStorageService : IFileStorageService
    {
        public void UploadFile(Stream fileData)
        {
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["uploadResultFilePath"], 
                FileMode.OpenOrCreate, 
                FileAccess.Write))
            {
                fileData.CopyTo(fs, 512);
            }
            fileData.Close();
        }

        public Stream DownloadFile(Guid fileId)
        {
            //return File.OpenRead(ConfigurationManager.AppSettings["fileToDownloadPath"]);
            return new SqlFileStreamWrapper(fileId);
        }

        public void UploadFileWithMetadata(RemoteFileStreamMessage fileData)
        {
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["uploadResultFilePath"],
                FileMode.OpenOrCreate,
                FileAccess.Write))
            {
                fileData.data.CopyTo(fs, 512);
            }
            fileData.data.Close();
        }

        public RemoteFileStreamMessage DownloadFileWithMetadata(RemoteFileStreamMessage fileRequest)
        {
            var file = File.OpenRead(ConfigurationManager.AppSettings["fileToDownloadPath"]);
            RemoteFileStreamMessage rfsm = new RemoteFileStreamMessage();
            rfsm.fileId = Guid.NewGuid();
            rfsm.fileName = "test";
            rfsm.streamLength = file.Length;
            rfsm.data = file;
            return rfsm;
        }
    }
}

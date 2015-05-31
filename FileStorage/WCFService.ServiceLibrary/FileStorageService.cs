using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFService.ServiceLibrary.Entities;

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

        public Stream GetFile()
        {
            return File.OpenRead(ConfigurationManager.AppSettings["fileToDownloadPath"]);
        }


        public void UploadFileEnvelope(RemoteStreamEnvelope fileData)
        {
            throw new NotImplementedException();
        }

        public RemoteStreamEnvelope GetFileEnvelope(Guid fileId)
        {
            throw new NotImplementedException();
        }
    }
}

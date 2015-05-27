using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FileStorage.ServiceLibrary
{
    public class FileStorageService : IFileStorageService
    {
        public void UploadFile(System.IO.Stream fileData)
        {
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["uploadResultFilePath"], 
                FileMode.OpenOrCreate, 
                FileAccess.Write))
            {
                fileData.CopyTo(fs, 512);
            }
            fileData.Close();
        }

        public System.IO.Stream GetFile()
        {
            return File.OpenRead(ConfigurationManager.AppSettings["fileToDownloadPath"]);
        }
    }
}

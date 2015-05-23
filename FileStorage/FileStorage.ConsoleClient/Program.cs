using FileStorage.Lib;
using FileStorage.Mock;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestFileDownload();
        }

        private static void TestFileUpload()
        {
            ServiceReference1.FileStorageServiceClient sc = new ServiceReference1.FileStorageServiceClient();
            MockFileStorage msf = new MockFileStorage(ConfigurationManager.AppSettings["fileToUploadPath"]);
            sc.UploadFile(msf.GetFile(Guid.Empty));
            sc.Close();
        }

        private static void TestFileDownload()
        {
            ServiceReference1.FileStorageServiceClient sc = new ServiceReference1.FileStorageServiceClient();
            MockFileStorage msf = new MockFileStorage(ConfigurationManager.AppSettings["downloadResultFilePath"]);
            msf.AddFile(sc.GetFile());
            sc.Close();
            sc.Close();
        }
    }
}

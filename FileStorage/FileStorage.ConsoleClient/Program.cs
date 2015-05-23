using FileStorage.FileStorageMock;
using FileStorage.Lib;
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
            ServiceReference1.FileStorageServiceClient TargetFileStorage = new ServiceReference1.FileStorageServiceClient();
            MockFileStorage SourceFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["fileToUploadPath"]);

            TargetFileStorage.UploadFile(SourceFileStorage.GetFile(Guid.Empty));
            TargetFileStorage.Close();
        }

        private static void TestFileDownload()
        {
            ServiceReference1.FileStorageServiceClient SourceFileStorage = new ServiceReference1.FileStorageServiceClient();
            MockFileStorage TargetFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["downloadResultFilePath"]);

            TargetFileStorage.AddFile(SourceFileStorage.GetFile());
            SourceFileStorage.Close();
            SourceFileStorage.Close();
        }

        private void CreateTestFile()
        {
            using (var source = new LongStream())
                using (var target = File.Create(""))
                target.CopyTo(source);
        }
    }
}

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
            CreateTestFile();
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

        private static void CreateTestFile()
        {
            int buffSize = 512;
            byte[] buff = new byte[buffSize];
            int count = 0;
            long totalSize = 0;
            using (var source = new LongStream())
                using (var target = File.Create(@"C:\TestStorage\Client\fileToUpload.txt"))
                {
                    while ((count = source.Read(buff, 0, buffSize)) != 0)
                    {
                        target.Write(buff, 0, count);
                        Console.WriteLine("Readed bytes: {0}", (totalSize+=count));
                    }
                    //target.CopyTo(source);
                }
            Console.WriteLine("End!");
            Console.ReadKey();
        }
    }
}

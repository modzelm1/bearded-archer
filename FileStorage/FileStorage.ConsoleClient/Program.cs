using FileStorage.FileStorageMock;
using FileStorage.StreamCore;
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
            //CreateTestFile();
            TestFileUpload();
        }

        private static void TestFileUpload()
        {
            MockFileStorage LocalFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["fileToUploadPath"]);
            using (ServiceReference1.FileStorageServiceClient TargetFileStorageService = new ServiceReference1.FileStorageServiceClient())
            {
                using (var fileToUpload = new RemoteStream(LocalFileStorage.GetFile(Guid.Empty)))
                {
                    fileToUpload.ReadPart += (a, b, c) => { Console.WriteLine("Progress {0}", b); };
                    TargetFileStorageService.UploadFile(fileToUpload);
                }
            }
            Console.ReadKey();
        }

        private static void TestFileDownload()
        {
            ServiceReference1.FileStorageServiceClient SourceFileStorageService = new ServiceReference1.FileStorageServiceClient();
            MockFileStorage LocalFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["downloadResultFilePath"]);

            LocalFileStorage.AddFile(SourceFileStorageService.GetFile());
            SourceFileStorageService.Close();
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

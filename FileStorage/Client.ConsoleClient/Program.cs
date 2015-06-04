using FileStorageRepository.FileStorageMock;
using SharedKernel.StreamExtension;
using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;

namespace Client.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            Console.WriteLine("Create file to upload ...");
            GenerateTestFile();

            Console.WriteLine("Upload file stream ...");
            TestFileUpload();

            //TestFileDownload();

            Console.WriteLine("Upload message with file stream ...");
            TestUploadFileObject();

            Console.WriteLine("End!");
            Console.ReadKey();
        }

        private static void TestUploadFileObject()
        {
            MockFileStorage LocalFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["fileToUploadPath"]);
            FileStorageServiceReference.FileStorageServiceClient TargetFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient();
            var streamToUpload = new ProgressStreamWrapper(LocalFileStorage.GetFile(Guid.Empty));
            streamToUpload.ReportReadProgressEvent += (a, b, c) => { Console.WriteLine("Progress {0}", b); };
            TargetFileStorageService.UploadFileEnvelope(streamToUpload.Length.ToString(), streamToUpload);
        }

        private static void TestFileUpload()
        {
            MockFileStorage LocalFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["fileToUploadPath"]);
            using (FileStorageServiceReference.FileStorageServiceClient TargetFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient())
            {
                using (var fileToUpload = new ProgressStreamWrapper(LocalFileStorage.GetFile(Guid.Empty)))
                {
                    fileToUpload.ReportReadProgressEvent += (a, b, c) => { Console.WriteLine("Progress {0}", b); };
                    TargetFileStorageService.UploadFile(fileToUpload);
                }
            }
        }

        private static void TestFileDownload()
        {
            MockFileStorage LocalFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["downloadResultFilePath"]);

            using (FileStorageServiceReference.FileStorageServiceClient SourceFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient())
            {
                using (var downloadedFile = new ProgressStreamWrapper(SourceFileStorageService.GetFile()))
                {
                    downloadedFile.ReportReadProgressEvent += (a, b, c) => { Console.WriteLine("Progress {0}", b); };
                    LocalFileStorage.AddFile(downloadedFile);
                }
            }
        }

        private static void GenerateTestFile()
        {
            int buffSize = 512;
            byte[] buff = new byte[buffSize];
            int count = 0;
            long totalSize = 0;
            using (var source = new LongStream())
            using (var target = File.Create(ConfigurationManager.AppSettings["fileToUploadPath"]))
            {
                while ((count = source.Read(buff, 0, buffSize)) != 0)
                {
                    target.Write(buff, 0, count);
                    Console.WriteLine("Writed bytes: {0}", (totalSize += count));
                }
            }
        }
    }
}

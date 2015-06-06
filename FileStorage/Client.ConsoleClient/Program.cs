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

            //Console.WriteLine("Upload file stream ...");
            //TestFileUpload();

            //Console.WriteLine("Download file stream ...");
            //TestFileDownload();

            //Console.WriteLine("Upload message with file stream ...");
            //TestUploadFileWithMetadata();

            //Console.WriteLine("Download message with file stream ...");
            //TestDownloadFileWithMetadata();

            Console.WriteLine("End!");
            Console.ReadKey();
        }

        private static void TestDownloadFileWithMetadata()
        {
            MockFileStorage LocalFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["downloadResultFilePath"]);

            using (FileStorageServiceReference.FileStorageServiceClient SourceFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient())
            {
                Stream downloadedFile = new MemoryStream();
                Guid fileId = Guid.NewGuid();
                string fileName = string.Empty;
                long streamLength = 0;

                SourceFileStorageService.DownloadFileWithMetadata(ref fileId, ref fileName,
                    ref streamLength, ref downloadedFile);

                using (var downloadedFileWraper = new ProgressStreamWrapper(downloadedFile))
                {
                    downloadedFileWraper.ReportReadProgressEvent += (a, b, c) => 
                    { Console.WriteLine("Progress: {0}", b); };
                    LocalFileStorage.AddFile(downloadedFileWraper);
                }
            }
        }

        private static void TestUploadFileWithMetadata()
        {
            MockFileStorage LocalFileStorage = new MockFileStorage(ConfigurationManager.AppSettings["fileToUploadPath"]);

            FileStorageServiceReference.FileStorageServiceClient TargetFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient();

            var streamToUpload = new ProgressStreamWrapper(LocalFileStorage.GetFile(Guid.Empty));
            streamToUpload.ReportReadProgressEvent += (a, b, c) => { Console.WriteLine("Progress: {0}", b); };

            TargetFileStorageService.UploadFileWithMetadata(Guid.NewGuid(), "TestFileName.txt", 
                streamToUpload.Length, streamToUpload);
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
                using (var downloadedFile = new ProgressStreamWrapper(SourceFileStorageService.DownloadFile(Guid.NewGuid())))
                {
                    downloadedFile.ReportReadProgressEvent += (a, b, c) => { Console.WriteLine("Progress: {0}", b); };
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

﻿using FileStorageRepository.FileStorageMock;
using SharedKernel.StreamExtension;
using System;
using System.Configuration;
using System.IO;

namespace Client.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTestFile();
            //TestFileUpload();
            //TestFileDownload();
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
            Console.ReadKey();
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
            Console.ReadKey();
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

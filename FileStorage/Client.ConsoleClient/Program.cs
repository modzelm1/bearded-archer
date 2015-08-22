using Client.WCFServiceConsoleClient.FileStorageServiceReference;
using FileSystemStreamHelper;
using StreamExtension;
using System;
using System.Configuration;
using System.IO;

namespace Client.WCFServiceConsoleClient
{
    class Program
    {
        private static string localStorePath = @"C:\TestStorage\Client\";

        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            TestWCFSqlFileStorage();

            Console.WriteLine("End!");
            Console.ReadKey();
        }

        private static void ReportProgress(long inStepBytesRead, long totalBytesRead, long streamLength)
        {
            //Console.WriteLine("Bytes read: {0}", totalBytesRead.ToString("D16"));
        }

        private static void TestWCFSqlFileStorage()
        {
            using (var FileStorageService = new FileStorageServiceClient())
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(localStorePath);
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    AddFileToWCFService(FileStorageService, fileInfo.Name, fileInfo.OpenRead());
                    ShowAllFiles(FileStorageService);
                }

                Console.WriteLine("Now delete all files");

                DeleteAllFiles(FileStorageService);
                ShowAllFiles(FileStorageService);

                Console.WriteLine("Files deleted!");
            }
        }

        private static void AddFileToWCFService(FileStorageServiceClient fileStorageService, string fileName, Stream fileData)
        {
            fileStorageService.UploadFile(
                Guid.NewGuid(), 
                fileName, 
                fileData.Length, 
                ProgressStreamDecorator.GetProgressStreamDecorator(fileData, ReportProgress));
        }

        private static void ShowAllFiles(FileStorageServiceClient fileStorageService)
        {
            var filesList = fileStorageService.GetAllFilesMetadata();
            Console.WriteLine("Sql Database Contains:");
            foreach (var fileEnvelope in filesList)
            {
                Console.WriteLine("File: {0}", fileEnvelope.fileName);
            }
            Console.WriteLine();
        }

        private static void DeleteAllFiles(FileStorageServiceClient fileStorageService)
        {
            var filesList = fileStorageService.GetAllFilesMetadata();
            Console.WriteLine("Deleting files from database:");
            foreach (var fileEnvelope in filesList)
            {
                Console.WriteLine("Deleting file: {0} ...", fileEnvelope.fileName);
                fileStorageService.DeleteFile(fileEnvelope.fileId);
                Console.WriteLine("File: {0} deleted!", fileEnvelope.fileName);
                Console.WriteLine();
            }
            Console.WriteLine();
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

using FileStorage;
using FileSystemStreamHelper;
using StreamExtension;
using System;
using System.Collections.Generic;
using System.IO;

namespace Client.LocalDatabaseConsoleClient
{
    class Program
    {
        private static string localStorePath = @"C:\TestStorage\Client\";

        static void Main(string[] args)
        {
            TestLocalSqlFileStorage();

            Console.ReadKey();
        }

        private static void ReportProgress(long inStepBytesRead, long totalBytesRead, long streamLength)
        {
            Console.WriteLine("Bytes read: {0}", totalBytesRead.ToString("D16"));
        }

        private static void TestLocalSqlFileStorage()
        {
            var sqlFileStorage = new SqlFileStorage();
            var streamHelper = new StreamHelper();

            DirectoryInfo directoryInfo = new DirectoryInfo(localStorePath);
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                AddFile(sqlFileStorage, fileInfo.Name, fileInfo.OpenRead());
                ShowAllFiles(sqlFileStorage);
            }

            Console.WriteLine("Now delete all files");

            DeleteAllFiles(sqlFileStorage);
            ShowAllFiles(sqlFileStorage);

            Console.WriteLine("Files deleted!");
        }

        private static void AddFile(SqlFileStorage sqlFileStorage, string fileName, Stream fileData)
        {
            var fileEnvelope = new FileEnvelope()
            {
                FileId = Guid.NewGuid(),
                FileName = fileName,
                FileData = ProgressStreamDecorator.GetProgressStreamDecorator(fileData, ReportProgress)
            };
            Console.WriteLine("Adding file: {0}", fileName);
            sqlFileStorage.AddFile(fileEnvelope);
            Console.WriteLine("File: {0} added", fileName);
        }

        private static void DeleteAllFiles(SqlFileStorage sqlFileStorage)
        {
            var filesList = sqlFileStorage.GetAllFilesMetadata();
            Console.WriteLine("Deleting files from database:");
            foreach (var fileEnvelope in filesList)
            {
                Console.WriteLine("Deleting file: {0} ...", fileEnvelope.FileName);
                sqlFileStorage.DeleteFileById(fileEnvelope.FileId);
                Console.WriteLine("File: {0} deleted!", fileEnvelope.FileName);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void ShowAllFiles(SqlFileStorage sqlFileStorage)
        {
            var filesList = sqlFileStorage.GetAllFilesMetadata();
            Console.WriteLine("Sql Database Contains:");
            foreach (var fileEnvelope in filesList)
            {
                Console.WriteLine("File: {0}", fileEnvelope.FileName);
            }
            Console.WriteLine();
        }
    }
}

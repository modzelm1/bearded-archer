using FileSystemStreamHelper;
using StreamExtension;
using System;
using System.Configuration;
using System.IO;

namespace Client.WCFServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            //Console.WriteLine("Create file to upload ...");
            //GenerateTestFile();

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
            StreamHelper sh = new StreamHelper();

            using (FileStorageServiceReference.FileStorageServiceClient SourceFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient())
            {
                Stream downloadedFile = new MemoryStream();
                Guid fileId = Guid.NewGuid();
                string fileName = string.Empty;
                long streamLength = 0;

                fileName = SourceFileStorageService.DownloadFile(ref fileId, out streamLength, out downloadedFile);

                using (var downloadedFileWraper = 
                    ProgressStreamDecorator.GetProgressStreamDecorator
                    (downloadedFile, (a, b, c) => { Console.WriteLine("Progress: {0}", b); }))
                {
                    sh.SaveFileStream(ConfigurationManager.AppSettings["downloadResultFilePath"], 
                        downloadedFileWraper);
                }
            }
        }

        private static void TestUploadFileWithMetadata()
        {
            StreamHelper sh = new StreamHelper();

            FileStorageServiceReference.FileStorageServiceClient TargetFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient();

            var streamToUpload = 
                ProgressStreamDecorator.GetProgressStreamDecorator(
                sh.GetFileStream(ConfigurationManager.AppSettings["fileToUploadPath"]),
                (a, b, c) => { Console.WriteLine("Progress: {0}", b); });
            
            //streamToUpload.ReportReadProgressEvent += (a, b, c) => { Console.WriteLine("Progress: {0}", b); };

            TargetFileStorageService.UploadFile(Guid.NewGuid(), "TestFileName.txt", streamToUpload.Length, streamToUpload);
        }

        //private static void TestFileUpload()
        //{
        //    StreamHelper sh = new StreamHelper();

        //    using (FileStorageServiceReference.FileStorageServiceClient TargetFileStorageService =
        //        new FileStorageServiceReference.FileStorageServiceClient())
        //    {
        //        using (var fileToUpload = 
        //            new ProgressStreamDecorator(sh.GetFile(
        //                ConfigurationManager.AppSettings["fileToUploadPath"])))
        //        {
        //            fileToUpload.ReportReadProgressEvent 
        //                += (a, b, c) => { Console.WriteLine("Progress {0}", b); };
        //            TargetFileStorageService.UploadFile(fileToUpload);
        //        }
        //    }
        //}

        //private static void TestFileDownload()
        //{
        //    StreamHelper sh = new StreamHelper();

        //    using (FileStorageServiceReference.FileStorageServiceClient SourceFileStorageService =
        //        new FileStorageServiceReference.FileStorageServiceClient())
        //    {
        //        var rs = SourceFileStorageService.DownloadFile(Guid.Parse("4B776A5C-AC11-E511-825F-E8B1FC35C9BE"));
        //        using (var downloadedFile = new ProgressStreamDecorator(rs))
        //        {
        //            downloadedFile.ReportReadProgressEvent 
        //                += (a, b, c) => { Console.WriteLine("Progress: {0}", b); };
        //            sh.AddFile(ConfigurationManager.AppSettings["downloadResultFilePath"], downloadedFile);
        //        }
        //    }
        //}

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

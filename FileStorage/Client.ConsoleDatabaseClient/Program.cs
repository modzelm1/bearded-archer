using FileStorage;
using FileSystemStreamHelper;
using System;

namespace Client.LocalDatabaseConsoleClient
{
    /// <summary>
    /// Application for testing sql file stream. It is not a part of the solution.
    /// </summary>
    class Program
    {
        private static Guid testGuid = Guid.Parse("EA6A7E73-500D-E511-825E-E8B1FC35C9BE");

        static void Main(string[] args)
        {
            TestAddFile();
            //TestGetFile();

            Console.ReadKey();
        }

        //Action<long, long, long> reportProgress = (a, b, c) => { Console.WriteLine("Progress: {0}", b); };

        private static void TestAddFile()
        {
            StreamHelper streamHelper = new StreamHelper();
            SqlFileStorage localFileStorage = new SqlFileStorage();

            FileEnvelope fileEnvelope =
                new FileEnvelope() {
                    FileId = Guid.NewGuid(), 
                    FileName = "my file 2",
                    FileData = streamHelper.GetFile(@"C:\TestStorage\Client\fileToUpload.txt")
                };

            localFileStorage.AddFile(fileEnvelope);
        }

        private static void TestGetFile()
        {
            SqlFileStorage localFileStorage = new SqlFileStorage();
            var file = localFileStorage.GetFileById(testGuid);

            StreamHelper streamHelper = new StreamHelper();
            streamHelper.AddFile(@"C:\TestStorage\Client\fileToUpload.txt", file.FileData);
        }
    }
}

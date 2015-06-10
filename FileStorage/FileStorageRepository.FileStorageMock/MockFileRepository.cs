using FileStorageRepository.Interface;
using System;
using System.IO;

namespace FileStorageRepository.MockFileRepository
{
    public class MockFileRepository : IFileRepository
    {
        string targetPath = "";

        public MockFileRepository(string targetPath)
        {
            this.targetPath = targetPath;
        }

        public void AddFile(System.IO.Stream fileData)
        {
            using (FileStream fs = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileData.CopyTo(fs, 512);
            }
        }

        public System.IO.Stream GetFile(Guid id)
        {
            return File.OpenRead(targetPath); ;
        }
    }
}

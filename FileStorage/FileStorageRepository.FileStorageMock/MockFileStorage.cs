using FileStorageRepository.Interface;
using System;
using System.IO;

namespace FileStorageRepository.FileStorageMock
{
    public class MockFileStorage : IFileStorage
    {
        string targetPath = "";

        public MockFileStorage(string targetPath)
        {
            this.targetPath = targetPath;
        }

        public void AddFileMetadata(StoreFileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public void AddFile(System.IO.Stream fileData)
        {
            using (FileStream fs = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileData.CopyTo(fs, 512);
            }
        }

        public StoreFileInfo GetFileMetadata(Guid id)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetFile(Guid id)
        {
            return File.OpenRead(targetPath); ;
        }
    }
}

using FileStorage.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Mock
{
    public class MockFileStorage : IFileStorage
    {
        string targetPath = "";

        public MockFileStorage(string targetPath)
        {
            this.targetPath = targetPath;
        }

        public System.IO.Stream EchoFileStream(System.IO.Stream inStreeam)
        {
            return inStreeam;
        }

        public void AddFileMetadata(RemoteFileInfo fileInfo)
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

        public RemoteFileInfo GetFileMetadata(Guid id)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetFile(Guid id)
        {
            return File.OpenRead(targetPath); ;
        }
    }
}

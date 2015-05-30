using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageRepository.Interface
{
    public interface IFileStorage
    {
        void AddFileMetadata(StoreFileInfo fileInfo);

        void AddFile(Stream fileData);

        StoreFileInfo GetFileMetadata(Guid id);

        Stream GetFile(Guid id);
    }
}

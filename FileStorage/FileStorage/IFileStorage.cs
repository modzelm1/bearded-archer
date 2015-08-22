using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage
{
    interface IFileStorage
    {
        void AddFile(FileEnvelope file);

        FileEnvelope GetFileById(Guid fileId);

        List<FileEnvelope> GetAllFilesMetadata();

        void DeleteFileById(Guid fileId);

        void UpdateFileMetadata(FileEnvelope file);
    }
}

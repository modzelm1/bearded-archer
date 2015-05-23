using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Interface
{
    public class RemoteFileInfo
    {
        Guid FileId;

        string FileName;

        string FileExtension;
    }

    public interface IFileStorage
    {
        Stream EchoFileStream(Stream inStreeam);

        void AddFileMetadata(RemoteFileInfo fileInfo);

        void AddFile(Stream fileData);

        RemoteFileInfo GetFileMetadata(Guid id);

        Stream GetFile(Guid id);
    }
}

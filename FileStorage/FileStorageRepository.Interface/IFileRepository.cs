using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageRepository.Interface
{
    public interface IFileRepository
    {
        void AddFile(Stream fileData);

        Stream GetFile(Guid id);
    }
}

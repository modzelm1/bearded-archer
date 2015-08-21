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
        FileEnvelope GetFileById(Guid fileId);

        void AddFile(FileEnvelope file);
    }
}

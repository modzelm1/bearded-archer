using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage
{
    public class FileEnvelope
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public Stream FileData { get; set; }
    }
}

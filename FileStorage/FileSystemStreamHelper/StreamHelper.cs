using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemStreamHelper
{
    public class StreamHelper
    {
        public void AddFile(string targetPath, System.IO.Stream fileData)
        {
            using (FileStream fs = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileData.CopyTo(fs, 512);
            }
        }

        public System.IO.Stream GetFile(string targetPath)
        {
            return File.OpenRead(targetPath); ;
        }
    }
}

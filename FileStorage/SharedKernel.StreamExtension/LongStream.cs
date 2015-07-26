using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExtension
{
    public class LongStream : Stream
    {
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            base.FlushAsync();
        }

        public override long Length
        {
            get { return streamLength; }
        }

        public override long Position
        {
            get;
            set;
        }

        //1GB stream
        int streamLength = 1024 * 1024 * 1024;// * 1024;
        
        public override int Read(byte[] buffer, int offset, int count)
        {
            int i = 0;
            for (; i < count && Position < streamLength; Position++, i++)
            {
                buffer[i] = 1;
            }
            return i;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}

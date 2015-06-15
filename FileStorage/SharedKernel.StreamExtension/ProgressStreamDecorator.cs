using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.StreamExtension
{
    [Serializable]
    public class ProgressStreamDecorator : Stream
    {
        Stream baseStream;

        public ProgressStreamDecorator(Stream baseStream)
        {
            this.baseStream = baseStream;
        }

        public override bool CanRead
        {
            get { return baseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return baseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return baseStream.CanWrite; }
        }

        public override void Flush()
        {
            baseStream.Flush();
        }

        public override long Length
        {
            get 
            {
                if (baseStream.CanSeek)
                    return baseStream.Length;
                else
                    return 0;
            }
        }

        public override long Position
        {
            get { return baseStream.Position; }
            set { baseStream.Position = value; }
        }

        long totalBytesReaded = 0;
        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesReaded = baseStream.Read(buffer, offset, count);
            totalBytesReaded += bytesReaded;

            ReportReadProgressEvent(bytesReaded, totalBytesReaded, this.Length);

            return bytesReaded;
        }

        /// <summary>
        /// a - in step bytes read
        /// b - total bytes read
        /// c - stream length
        /// </summary>
        public event Action<long, long, long> ReportReadProgressEvent = (a, b, c) => { };

        public override long Seek(long offset, SeekOrigin origin)
        {
            return baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            baseStream.Write(buffer, offset, count);
        }
    }
}

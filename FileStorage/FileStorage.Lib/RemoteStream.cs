﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.StreamCore
{
    public class RemoteStream : Stream
    {
        Stream baseStream;

        public RemoteStream(Stream baseStream)
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
            get { return baseStream.Length; }
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

            ReadPart(bytesReaded, totalBytesReaded, baseStream.Length);

            return bytesReaded;
        }

        public event Action<long, long, long> ReadPart = (a, b, c) => { };

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
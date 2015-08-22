using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStreamExtensionLib
{
    /// <summary>
    /// Wrapps sql filestrem and close it when done
    /// </summary>
    public class SqlFileStreamDecorator : Stream
    {
        private readonly SqlConnection sqlConnection;
        private readonly SqlTransaction sqlTransaction;
        private readonly SqlFileStream sqlFileStream;

        public static SqlFileStreamDecorator 
            GetSqlFileStreamDecorator(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlFileStream sqlFileStream)
        {
            SqlFileStreamDecorator sqlFileStreamDecorator = new SqlFileStreamDecorator(sqlConnection, sqlTransaction, sqlFileStream);
            return sqlFileStreamDecorator;
        }

        private SqlFileStreamDecorator
            (SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlFileStream sqlFileStream)
        {
            this.sqlConnection = sqlConnection;
            this.sqlTransaction = sqlTransaction;
            this.sqlFileStream = sqlFileStream;
        }

        protected override void Dispose(bool disposing)
        {
            sqlFileStream.Close();
            sqlTransaction.Commit();
            sqlConnection.Close();
        }

        public override void Flush()
        {
            sqlFileStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return sqlFileStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            sqlFileStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return sqlFileStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            sqlFileStream.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return sqlFileStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return sqlFileStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return sqlFileStream.CanWrite; }
        }

        public override long Length
        {
            get { return sqlFileStream.Length; }
        }

        public override long Position
        {
            get { return sqlFileStream.Position; }
            set { sqlFileStream.Position = value; }
        }
    }
}

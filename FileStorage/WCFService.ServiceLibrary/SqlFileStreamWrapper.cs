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

namespace WCFService.ServiceLibrary
{
    /// <summary>
    /// Not tested yet...
    /// Wrapps sql filestrem
    /// </summary>
    class SqlFileStreamWrapper : Stream
    {
        private readonly SqlConnection sqlConnection;
        private readonly SqlTransaction sqlTransaction;
        private readonly SqlFileStream sqlFileStream;

        /// <summary>
        /// Initialization should be moved to the factory.
        /// Constructor should't be doing this ...
        /// </summary>
        public SqlFileStreamWrapper(Guid FileId)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["FileStorageDatabase"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlTransaction = sqlConnection.BeginTransaction();

            var serverPathName = default(string);
            var transactionContext = default(byte[]);

            using(var cmd = new SqlCommand("GetFileData", sqlConnection, sqlTransaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fileId", FileId);
                
                using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    rdr.Read();
                    serverPathName = rdr.GetSqlString(0).Value;
                    transactionContext = rdr.GetSqlBinary(1).Value;
                    rdr.Close();
                }
            }
            
            sqlFileStream = new SqlFileStream(serverPathName, transactionContext, FileAccess.Read);
        }

        protected override void Dispose(bool disposing)
        {
            //sqlDataReader.Close();
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

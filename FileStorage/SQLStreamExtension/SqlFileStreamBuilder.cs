using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLStreamExtension
{
    public class SqlFileStreamBuilder
    {
        public SqlConnection Connection { get; private set; }
        public SqlTransaction Transaction { get; private set; }
        public SqlFileStream FileStream { get; private set; }
        public string SqlFileName { get; private set; }
        public Guid SqlFileId { get; private set; }

        public SqlFileStreamBuilder(Guid fileId)
        {
            SqlFileId = fileId;
        }

        public void OpenSqlDatabaseConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void BeginSqlTransaction()
        {
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public void GetSqlFileMetadata()
        {
            using (var cmd = new SqlCommand("GetFileMetadata", Connection, Transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fileId", SqlFileId);
                using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    rdr.Read();
                    SqlFileName = rdr.GetSqlString(1).Value;
                    rdr.Close();
                }
            }
        }

        public void GetSqlFileStream()
        {
            var serverPathName = default(string);
            var transactionContext = default(byte[]);
            using (var cmd = new SqlCommand("GetFileData", Connection, Transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fileId", SqlFileId);
                using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    rdr.Read();
                    serverPathName = rdr.GetSqlString(0).Value;
                    transactionContext = rdr.GetSqlBinary(1).Value;
                    rdr.Close();
                }
            }
            FileStream = new SqlFileStream(serverPathName, transactionContext, FileAccess.Read);
        }
    }
}
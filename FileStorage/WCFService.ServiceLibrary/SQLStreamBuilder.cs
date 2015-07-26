using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.ServiceLibrary
{
    public class SQLStreamBuilder
    {
        public SqlConnection Connection { get; private set; }
        public SqlTransaction Transaction { get; private set; }
        public SqlFileStream FileStream { get; private set; }

        public void CreateSqlConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void CreateSqlTransaction()
        {
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public void CreateSqlFileStream(Guid fileId)
        {
            var serverPathName = default(string);
            var transactionContext = default(byte[]);
            using (var cmd = new SqlCommand("GetFileData", Connection, Transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fileId", fileId);
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

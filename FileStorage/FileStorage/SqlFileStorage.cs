using SQLStreamExtensionLib;
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
using System.Transactions;

namespace FileStorage
{
    public class SqlFileStorage : IFileStorage
    {
        public void AddFile(FileEnvelope file)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["FileStorageDatabase"].ConnectionString;

            using (var dbConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("AddFileData", dbConnection))
                {
                    using (var transactionScope = new TransactionScope())
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@fileId", file.FileId);
                        sqlCommand.Parameters.AddWithValue("@fileName", file.FileName);

                        var serverPathName = default(string);
                        var serverTxnContext = default(byte[]);

                        dbConnection.Open();
                        using (var rdr = sqlCommand.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            rdr.Read();
                            serverPathName = rdr.GetSqlString(0).Value;
                            serverTxnContext = rdr.GetSqlBinary(1).Value;
                            rdr.Close();
                        }
                        dbConnection.Close();

                        using (var source = file.FileData)
                        {
                            using (var dest =
                            new SqlFileStream(serverPathName, serverTxnContext, FileAccess.Write))
                            {
                                source.CopyTo(dest, 4096);
                                dest.Close();
                            }
                        }
                        transactionScope.Complete();
                    }
                }
            }
        }

        public FileEnvelope GetFileById(Guid fileId)
        {
            SqlFileStreamBuilder builder = new SqlFileStreamBuilder(fileId);
            builder.OpenSqlDatabaseConnection(ConfigurationManager.ConnectionStrings["FileStorageDatabase"].ConnectionString);
            builder.BeginSqlTransaction();
            builder.GetSqlFileMetadata();
            builder.GetSqlFileStream();

            var sqlFileStreamWrapper = 
                SqlFileStreamDecorator.GetSqlFileStreamDecorator(builder.Connection, builder.Transaction, builder.FileStream);

            var fileEnvelope = new FileEnvelope() 
            { FileId = fileId, FileName = builder.SqlFileName, FileData = sqlFileStreamWrapper };

            return fileEnvelope;
        }

        public List<FileEnvelope> GetAllFilesMetadata()
        {
            var result = new List<FileEnvelope>();

            using(var Connection = 
                new SqlConnection(ConfigurationManager.ConnectionStrings["FileStorageDatabase"].ConnectionString))
            {
                Connection.Open();
                using (var cmd = new SqlCommand("GetAllFilesMetadata", Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            var fileEnvelope = new FileEnvelope();
                            fileEnvelope.FileId = rdr.GetGuid(0);
                            fileEnvelope.FileName = rdr.GetSqlString(1).Value;
                            fileEnvelope.FileData = null;
                            result.Add(fileEnvelope);
                        }
                        rdr.Close();
                    }
                }
            }

            return result;
        }

        public void DeleteFileById(Guid fileId)
        {
            using (var Connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["FileStorageDatabase"].ConnectionString))
            {
                Connection.Open();
                using (var cmd = new SqlCommand("DeleteFileById", Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fileId", SqlDbType.UniqueIdentifier).Value = fileId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFileMetadata(FileEnvelope file)
        {
            throw new NotImplementedException();
        }
    }
}

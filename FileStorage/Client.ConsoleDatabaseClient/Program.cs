using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Transactions;

namespace Client.ConsoleDatabaseClient
{
    /// <summary>
    /// Application for testing sql file stream. It is not a part of the solution.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //TestAddFile();
            TestGetFile();
        }

        private static void TestAddFile()
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
                        sqlCommand.Parameters.AddWithValue("@fileId", Guid.Parse("EA6A7E73-500D-E511-825E-E8B1FC35C9BE"));
                        sqlCommand.Parameters.AddWithValue("@fileName", "test file");

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

                        using (var source =
                                new FileStream(@"C:\TestStorage\test blob.txt", FileMode.OpenOrCreate))
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

        private static void TestGetFile()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["FileStorageDatabase"].ConnectionString;

            using (var dbConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("GetFileData", dbConnection))
                {
                    using (var transactionScope = new TransactionScope())
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        //fileId value from my local database. Supply corect one
                        sqlCommand.Parameters.
                            AddWithValue("@fileId", Guid.Parse("EA6A7E73-500D-E511-825E-E8B1FC35C9BE"));

                        var serverPathName = default(string);
                        var transactionContext = default(byte[]);

                        dbConnection.Open();
                        using (var dataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            dataReader.Read();
                            serverPathName = dataReader.GetSqlString(0).Value;
                            transactionContext = dataReader.GetSqlBinary(1).Value;
                            dataReader.Close();
                        }
                        dbConnection.Close();

                        using (var sourceSqlFileStream =
                            new SqlFileStream(serverPathName, transactionContext, FileAccess.Read))
                        {
                            using (var destinationFileStream =
                                new FileStream(@"C:\TestStorage\test result.txt", FileMode.OpenOrCreate))
                            {
                                sourceSqlFileStream.CopyTo(destinationFileStream, 4096);
                                destinationFileStream.Close();
                            }
                            sourceSqlFileStream.Close();
                        }

                        transactionScope.Complete();
                    }
                }
            }
        }
    }
}

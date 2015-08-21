using SQLStreamExtension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;

namespace WCFService.ServiceLibrary
{
    public class FileStorageService : IFileStorageService
    {
        public void UploadFile(RemoteFileStreamMessage fileData)
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
                        sqlCommand.Parameters.AddWithValue("@fileId", fileData.fileId);
                        sqlCommand.Parameters.AddWithValue("@fileName", fileData.fileName);

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

                        using (var dest = new SqlFileStream(serverPathName, serverTxnContext, FileAccess.Write))
                        {
                            fileData.data.CopyTo(dest, 4096);
                            dest.Close();
                        }
                        transactionScope.Complete();
                    }
                }
            }
            fileData.data.Close();
        }

        public RemoteFileStreamMessage DownloadFile(RemoteFileStreamRequest fileRequest)
        {
            var builder = new SqlFileStreamBuilder(fileRequest.fileId);
            builder.OpenSqlDatabaseConnection(ConfigurationManager.ConnectionStrings["FileStorageDatabase"].ConnectionString);
            builder.BeginSqlTransaction();
            builder.GetSqlFileStream();

            var sqlFileStreamWrapper = 
                SqlFileStreamDecorator.GetSqlFileStreamDecorator(builder.Connection, builder.Transaction, builder.FileStream);

            var remoteFileStreamMessage = new RemoteFileStreamMessage() 
                { fileId = fileRequest.fileId, streamLength = sqlFileStreamWrapper.Length, data = sqlFileStreamWrapper };

            return remoteFileStreamMessage;
        }

        //public void UploadFileWithMetadata(RemoteFileStreamMessage fileData)
        //{
        //    using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["uploadResultFilePath"],
        //        FileMode.OpenOrCreate,
        //        FileAccess.Write))
        //    {
        //        fileData.data.CopyTo(fs, 512);
        //    }
        //    fileData.data.Close();
        //}

        //public RemoteFileStreamMessage DownloadFileWithMetadata(RemoteFileStreamMessage fileRequest)
        //{
        //    var file = File.OpenRead(ConfigurationManager.AppSettings["fileToDownloadPath"]);
        //    RemoteFileStreamMessage rfsm = new RemoteFileStreamMessage();
        //    rfsm.fileId = Guid.NewGuid();
        //    rfsm.fileName = "test";
        //    rfsm.streamLength = file.Length;
        //    rfsm.data = file;
        //    return rfsm;
        //}
    }
}

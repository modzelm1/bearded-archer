CREATE DATABASE FileStorage
 ON PRIMARY
  (NAME = FileStorage_data, 
   FILENAME = 'C:\TestStorage\Database\FileStorage_data.mdf'),
 FILEGROUP FileStreamGroup_FileStorage CONTAINS FILESTREAM
  (NAME = FileStorage_blobs, 
   FILENAME = 'C:\TestStorage\Database\FileContainer')
 LOG ON 
  (NAME = PhotoLibrary_log,
   FILENAME = 'C:\TestStorage\Database\FileStorage_log.ldf')
GO

USE FileStorage
GO

CREATE TABLE FileDataTable(
 [FileId] uniqueidentifier ROWGUIDCOL NOT NULL UNIQUE DEFAULT NEWSEQUENTIALID(),
 [FileName] varchar(max) NOT NULL,
 [FileData] varbinary(max) FILESTREAM)
GO

CREATE PROCEDURE [GetFileMetadata]
	@fileId UNIQUEIDENTIFIER
AS
	SELECT [FileId]
		  ,[FileName] 
	FROM FileDataTable 
	WHERE FileId = @fileId
GO

CREATE PROCEDURE [dbo].[GetAllFilesMetadata]
AS
	SELECT [FileId]
		  ,[FileName] 
	FROM FileDataTable
GO

CREATE PROCEDURE [GetFileData]
	@fileId UNIQUEIDENTIFIER
AS
	SELECT 
		FileData.PathName(),
		GET_FILESTREAM_TRANSACTION_CONTEXT() 
	FROM FileDataTable 
	WHERE FileId = @fileId
GO

CREATE PROCEDURE AddFileData(
	@fileId UNIQUEIDENTIFIER,
	@fileName varchar(max))
 AS
BEGIN
	INSERT INTO FileDataTable([FileId], [FileName], [FileData])
	OUTPUT inserted.FileData.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT()
	SELECT @fileId, @fileName, 0x
END
GO

CREATE PROCEDURE [dbo].[DeleteFileById]
	@fileId UNIQUEIDENTIFIER
AS
	DELETE 
	FROM FileDataTable
	WHERE FileId = @fileId
GO
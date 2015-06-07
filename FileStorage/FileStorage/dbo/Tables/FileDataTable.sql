CREATE TABLE [dbo].[FileDataTable] (
    [FileId]   UNIQUEIDENTIFIER           DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [FileName] VARCHAR (MAX)              NOT NULL,
    [FileData] VARBINARY (MAX) FILESTREAM NULL,
    UNIQUE NONCLUSTERED ([FileId] ASC)
) FILESTREAM_ON [FileStreamGroup_FileStorage];


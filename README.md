# bearded-archer
This project will be a compilation of solutions for streaming large data files from and to server.


###roadmap
1. Console client, wcf stream service and mock storage (SQL Filestream in next iteration)
2. SQL Filestream storage
3. Test network stream option
3. Business objects in WCF interface (stream inside business object with some extra data)
4. WPF client, asyc download and upload, progres
5. Web API interface
6. ASP MVC Client
7. Publish on Azure


###How to use / test Consol Client 

I current version we can download and uplod files using WCF streaming.

In FileStorage.ConsoleClient project Main method looks like this:

```cs
static void Main(string[] args)
{
        Console.WriteLine("Start!");

        Console.WriteLine("Create file to upload ...");
        GenerateTestFile();

        //Console.WriteLine("Upload file stream ...");
        //TestFileUpload();

        //Console.WriteLine("Download file stream ...");
        //TestFileDownload();

        //Console.WriteLine("Upload message with file stream ...");
        //TestUploadFileWithMetadata();

        //Console.WriteLine("Download message with file stream ...");
        //TestDownloadFileWithMetadata();

        Console.WriteLine("End!");
        Console.ReadKey();
}
```

CreateTestFile method is used to generate test file. You can manipulate generated file size in LongStream class definition which is located in FileStorage.StreamCore project.


###File storage service
File storage service is located in FileStorage.ServiceLibrary project. For current testing purpose it iis hosted in Visual Studio thanks to "start wcf service host when debugging another project" option.

https://msdn.microsoft.com/pl-pl/library/cc668754%28v=vs.110%29.aspx


###Helpful links (Under construction ...)

Below you can find helpful links.
These are links to the articles which I read while I was working on that project.


#####Stream files
https://msdn.microsoft.com/en-us/library/ms733742%28v=vs.110%29.aspx

http://www.codeproject.com/Articles/166763/WCF-Streaming-Upload-Download-Files-Over-HTTP

http://stackoverflow.com/questions/14479885/wcf-streaming-large-data-500mb-1gb-on-a-self-hosted-service

http://stackoverflow.com/questions/13384886/wcf-streaming-who-closes-the-file


#####Stream files with progress
http://www.codeproject.com/Articles/20364/Progress-Indication-while-Uploading-Downloading-Fi

http://www.codeproject.com/Articles/112655/Progress-Streamed-File-download-and-Upload-with-Re


#####Data Contract Known Types
https://msdn.microsoft.com/en-us/library/ms730167.aspx


#####WCF Callbacs
http://stackoverflow.com/questions/10561061/progress-update-while-wcf-is-executed


#####DataContract Vs MessageContract
http://www.codeproject.com/Articles/733660/DataContract-Vs-MessageContract-in-WCF

#####SQL Server Filestream
https://lennilobel.wordpress.com/2015/03/05/integrating-document-blob-storage-with-sql-server/

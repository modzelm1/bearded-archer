# bearded-archer
This project will be a compilation of solutions for streaming large data files from and to server.

Below you can find helpful links section.
These are links to the articles which I read while I was working on that project.

###roadmap
1. Console client, wcf stream service and mock storage (SQL Filestream in next iteration)
2. SQL Filestream storage
3. Test network stream option
3. Business objects in WCF interface (stream inside business object with some extra data)
4. WPF client, asyc download and upload, progres
5. Web API interface
6. ASP MVC Client
7. Publish on Azure


###Helpful links (Under construction ..)

https://msdn.microsoft.com/en-us/library/ms733742%28v=vs.110%29.aspx

http://www.codeproject.com/Articles/20364/Progress-Indication-while-Uploading-Downloading-Fi

http://www.codeproject.com/Articles/112655/Progress-Streamed-File-download-and-Upload-with-Re

http://www.codeproject.com/Articles/166763/WCF-Streaming-Upload-Download-Files-Over-HTTP

http://stackoverflow.com/questions/14479885/wcf-streaming-large-data-500mb-1gb-on-a-self-hosted-service


https://msdn.microsoft.com/en-us/library/ms730167.aspx

http://stackoverflow.com/questions/10561061/progress-update-while-wcf-is-executed


###File storage service
File storage service is located in FileStorage.ServiceLibrary project. For current testing purpose it iis hosted in Visual Studio thanks to "start wcf service host when debugging another project" option.

https://msdn.microsoft.com/pl-pl/library/cc668754%28v=vs.110%29.aspx


###How to use / test Consol Client 

I current version we can download and uplod files using WCF streaming.

In FileStorage.ConsoleClient project Main method looks like this:

```cs
static void Main(string[] args)
{
  CreateTestFile();
  //TestFileUpload();
  //TestFileDownload();
}
```

CreateTestFile method is used to generate test file. You can manipulate generated file size in LongStream class definition which is located in FileStorage.StreamCore project.

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


###How to use / test

There are three projects which shows how to use sql file storage.

#####Client.LocalDatabaseConsoleClient
This project uses SqlFileStorage component to interact with the database. SqlFileStorage component conects dircetly to the database.

#####Client.WCFServiceConsoleClient
This project uses FileStorageService which allows to stream file into WCF service and then through SqlFileStorage to the database.

#####Client.WCFServiceWPFClient
It is a WPF application which sends files through FileStorageService.



###File storage service
File storage service is located in WCFService.ServiceLibrary project. For current testing purpose it iis hosted in Visual Studio thanks to "start wcf service host when debugging another project" option.

https://msdn.microsoft.com/pl-pl/library/cc668754%28v=vs.110%29.aspx


###Helpful links

Below you can find helpful links.
These are links to the articles which I read while I was working on that project.

#####Stream
http://stackoverflow.com/questions/507747/can-you-explain-the-concept-of-streams


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
https://msdn.microsoft.com/en-us/library/cc645583.aspx

https://lennilobel.wordpress.com/2015/03/05/integrating-document-blob-storage-with-sql-server/

http://www.pluralsight.com/courses/sql-server-2012-2014-native-file-streaming

http://stackoverflow.com/questions/7469955/return-stream-from-wcf-service-using-sqlfilestream?lq=1

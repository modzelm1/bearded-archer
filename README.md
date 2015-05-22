# bearded-archer
file storage app, downloading and uploading large files
README file for my project

This project will be something like dropbox service.
First version will allow to upload and download large files from computer to some remote location.
I plan to use WCF for service and SQL Filestream for storage.

###roadmap
1. Console client, wcf stream service and mock storage (SQL Filestream in next iteration)
2. SQL Filestream storage
3. Test network stream option
3. Business objects in WCF interface (stream inside business object with some extra data)
4. WPF client, asyc download and upload, progres
5. Web API interface
6. ASP MVC Client
7. Publish on Azure

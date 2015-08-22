﻿using Client.WCFServiceWPFClient.FileStorageServiceReference;
using FileSystemStreamHelper;
using StreamExtension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace Client.WCFServiceWPFClient
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CommandBase StartDownload  { get; private set; }

        public CommandBase StartUpload { get; private set; }

        public CommandBase UploadAllFilesCommand { get; private set; }

        public CommandBase DeleteAllFilesCommand { get; private set; }

        //Action<long, long, long> reportDownloadProgress;

        Action<long, long, long> reportUploadProgress;

        public MainViewModel()
        {
            UploadAllFilesCommand = new CommandBase(UploadAllFiles);
            DeleteAllFilesCommand = new CommandBase(DeleteAllFiles);
            //StartDownload = new CommandBase(DownloadFile);
            //StartUpload = new CommandBase(UploadFile);
            //reportDownloadProgress = (a, b, c) => DownloadProgressVal = b;
            reportUploadProgress = (a, b, c) => UploadProgressVal = b;
            UploadProgressMaxVal = 100;
            UploadProgressVal = 0;
            //DownloadProgressMaxVal = 100;
            //DownloadProgressVal = 0;
            WorkingDirectory = @"C:\TestStorage\Client\";
            LogInfo = "Log";
        }

        public async void UploadAllFiles()
        {
            using (var FileStorageService = new FileStorageServiceClient())
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(WorkingDirectory);
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    UploadProgressVal = 0;
                    UploadProgressMaxVal = fileInfo.Length;

                    LogInfo = "Upload: " + fileInfo.Name;

                    var streamToUpload = ProgressStreamDecorator.GetProgressStreamDecorator(fileInfo.OpenRead(), reportUploadProgress);
                    await FileStorageService.UploadFileAsync
                        (Guid.NewGuid(), 
                        fileInfo.Name, 
                        fileInfo.Length,
                        streamToUpload);

                    ShowFiles(FileStorageService);
                }
            }
        }

        private void ShowFiles(FileStorageServiceClient fileStorageService)
        {
            RemoteRepositoryFiles = new ObservableCollection<object>();
            var filesList = fileStorageService.GetAllFilesMetadata();
            foreach (var file in filesList)
            {
                RemoteRepositoryFiles.Add(new { Id = file.fileId, Name = file.fileName  });
            }
        }

        private async void DeleteAllFiles()
        {
            using (var FileStorageService = new FileStorageServiceClient())
            {
                var data = FileStorageService.GetAllFilesMetadata();
                foreach (var item in data)
                {
                    await FileStorageService.DeleteFileAsync(item.fileId);

                    ShowFiles(FileStorageService);
                }
            }
        }

        string _LogInfo;
        public string LogInfo
        {
            get
            {
                return _LogInfo;
            }
            set
            {
                _LogInfo = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LogInfo"));
            }
        }

        string _WorkingDirectory;
        public string WorkingDirectory
        {
            get
            {
                return _WorkingDirectory;
            }
            set
            {
                _WorkingDirectory = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkingDirectory"));
            }
        }

        ObservableCollection<object> _RemoteRepositoryFiles;
        public ObservableCollection<object> RemoteRepositoryFiles
        {
            get 
            {
                return _RemoteRepositoryFiles;
            }
            set 
            {
                _RemoteRepositoryFiles = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RemoteRepositoryFiles"));
            }
        }

        //private async void UploadFile()
        //{
        //    StreamHelper sh = new StreamHelper();

        //    FileStorageServiceReference.FileStorageServiceClient TargetFileStorageService =
        //        new FileStorageServiceReference.FileStorageServiceClient();

        //    var streamToUpload = ProgressStreamDecorator.GetProgressStreamDecorator
        //        (sh.GetFileStream(ConfigurationManager.AppSettings["fileToUploadPath"]), reportUploadProgress);
        //    //streamToUpload.ReportReadProgressEvent += reportUploadProgress;

        //    UploadProgressMaxVal = streamToUpload.Length;

        //    await TargetFileStorageService.UploadFileAsync(Guid.NewGuid(), "TestFileName.txt",
        //        streamToUpload.Length, streamToUpload);
        //}

        //private async void DownloadFile()
        //{
        //    StreamHelper sh = new StreamHelper();

        //    using (FileStorageServiceReference.FileStorageServiceClient SourceFileStorageService =
        //        new FileStorageServiceReference.FileStorageServiceClient())
        //    {
        //        Stream downloadedFile = new MemoryStream();
        //        Guid fileId = Guid.NewGuid();
        //        string fileName = string.Empty;
        //        long streamLength = 0;

        //        fileName = SourceFileStorageService.DownloadFile(ref fileId, out streamLength, out downloadedFile);

        //        DownloadProgressMaxVal = streamLength;

        //        Task t = Task.Factory.StartNew(
        //            () => {
        //                using (var downloadedFileWraper =
        //                    ProgressStreamDecorator.GetProgressStreamDecorator(downloadedFile, reportDownloadProgress))
        //                {
        //                    //downloadedFileWraper.ReportReadProgressEvent += reportDownloadProgress;
        //                    sh.SaveFileStream(ConfigurationManager.AppSettings["downloadResultFilePath"], downloadedFileWraper);
        //                }
        //            }
        //                );
        //        await t;
        //    }
        //}

        long _UploadProgressMaxVal;
        public long UploadProgressMaxVal
        {
            get
            {
                return _UploadProgressMaxVal;
            }
            set
            {
                _UploadProgressMaxVal = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("UploadProgressMaxVal"));
            }
        }

        long _UploadProgressVal;
        public long UploadProgressVal
        {
            get
            {
                return _UploadProgressVal;
            }
            set
            {
                _UploadProgressVal = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("UploadProgressVal"));
            }
        }

        //long _DownloadProgressMaxVal;
        //public long DownloadProgressMaxVal 
        //{ 
        //    get
        //    {
        //        return _DownloadProgressMaxVal;
        //    }
        //    set 
        //    {
        //        _DownloadProgressMaxVal = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("DownloadProgressMaxVal"));
        //    } 
        //}

        //long _DownloadProgressVal;
        //public long DownloadProgressVal
        //{
        //    get
        //    {
        //        return _DownloadProgressVal;
        //    }
        //    set
        //    {
        //        _DownloadProgressVal = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("DownloadProgressVal"));
        //    }
        //}
    }
}

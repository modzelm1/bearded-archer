using Client.WCFServiceWPFClient.FileStorageServiceReference;
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

        //public CommandBase StartDownload  { get; private set; }

        //public CommandBase StartUpload { get; private set; }

        public CommandBase UploadAllFilesCommand { get; private set; }

        public CommandBase DownloadFileCommand { get; private set; }

        public CommandBase DeleteFileCommand { get; private set; }

        public CommandBase DeleteAllFilesCommand { get; private set; }

        //Action<long, long, long> reportDownloadProgress;

        Action<long, long, long> reportProgress;

        public MainViewModel()
        {
            UploadAllFilesCommand = new CommandBase(UploadAllFiles);
            DownloadFileCommand = new CommandBase(DownloadFile);
            DeleteFileCommand = new CommandBase(DeleteFile);
            DeleteAllFilesCommand = new CommandBase(DeleteAllFiles);
            //StartDownload = new CommandBase(DownloadFile);
            //StartUpload = new CommandBase(UploadFile);
            //reportDownloadProgress = (a, b, c) => DownloadProgressVal = b;
            reportProgress = (a, b, c) => ProgressVal = b;
            ProgressMaxVal = 100;
            ProgressVal = 0;
            //DownloadProgressMaxVal = 100;
            //DownloadProgressVal = 0;
            WorkingDirectory = @"C:\TestStorage\Client\";
            LogInfo = "Log";
        }

        private void ResetProgress()
        {
            ProgressVal = 0;
        }

        private void SetLog(string message)
        {
            LogInfo = message;
        }

        private void DeleteFile()
        {
            if(SelectedRepositoryItem != null)
            {
                using (var FileStorageService = new FileStorageServiceClient())
                {
                    FileStorageService.DeleteFile(SelectedRepositoryItem.Id);
                    ShowFiles(FileStorageService);
                }
            }
        }

        public async void DownloadFile()
        {
            if (SelectedRepositoryItem != null)
            {
                SetLog("Downloading " + SelectedRepositoryItem.Name);
                ResetProgress();
                using (var FileStorageService = new FileStorageServiceClient())
                {
                    Guid id = SelectedRepositoryItem.Id;
                    long length = 0;
                    Stream data = new MemoryStream();
                    var name = FileStorageService.DownloadFile(ref id, out length, out data);

                    Task t = Task.Factory.StartNew(
                        () =>
                        {
                            StreamHelper sh = new StreamHelper();
                            DirectoryInfo directoryInfo = new DirectoryInfo(WorkingDirectory);
                            string path = Path.Combine(WorkingDirectory, DateTime.Now.Millisecond + "_" + name);
                            using (var downloadedFileWraper =
                                ProgressStreamDecorator.GetProgressStreamDecorator(data, reportProgress))
                            {
                                sh.SaveFileStream(path, downloadedFileWraper);
                            }
                        }
                            );
                    await t;
                }
                SetLog(string.Empty);
                ResetProgress();
            }
        }

        public async void UploadAllFiles()
        {
            using (var FileStorageService = new FileStorageServiceClient())
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(WorkingDirectory);
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    ResetProgress();
                    ProgressMaxVal = fileInfo.Length;

                    SetLog("Upload: " + fileInfo.Name);

                    var streamToUpload = ProgressStreamDecorator.GetProgressStreamDecorator(fileInfo.OpenRead(), reportProgress);
                    await FileStorageService.UploadFileAsync
                        (Guid.NewGuid(), 
                        fileInfo.Name, 
                        fileInfo.Length,
                        streamToUpload);

                    ShowFiles(FileStorageService);
                }

                SetLog(string.Empty);
                ResetProgress();
            }
        }

        private void ShowFiles(FileStorageServiceClient fileStorageService)
        {
            RemoteRepositoryFiles = new ObservableCollection<object>();
            var filesList = fileStorageService.GetAllFilesMetadata();
            foreach (var file in filesList)
            {
                RemoteRepositoryFiles.Add(new { Id = file.fileId, Name = file.fileName, Selected = true  });
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

        ObservableCollection<dynamic> _RemoteRepositoryFiles;
        public ObservableCollection<dynamic> RemoteRepositoryFiles
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

        dynamic _SelectedRepositoryItem;
        public dynamic SelectedRepositoryItem 
        {
            get
            {
                return _SelectedRepositoryItem;
            }
            set
            {
                _SelectedRepositoryItem = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedRepositoryItem"));
            } 
        }

        long _ProgressMaxVal;
        public long ProgressMaxVal
        {
            get
            {
                return _ProgressMaxVal;
            }
            set
            {
                _ProgressMaxVal = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ProgressMaxVal"));
            }
        }

        long _ProgressVal;
        public long ProgressVal
        {
            get
            {
                return _ProgressVal;
            }
            set
            {
                _ProgressVal = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ProgressVal"));
            }
        }
    }
}

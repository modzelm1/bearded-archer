using FileStorageRepository.FileStorageMock;
using SharedKernel.StreamExtension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.WPFClient
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CommandBase StartDownload  { get; private set; }

        public CommandBase StartUpload { get; private set; }

        Action<long, long, long> reportDownloadProgress;

        Action<long, long, long> reportUploadProgress;

        public MainViewModel()
        {
            StartDownload = new CommandBase(DownloadFile);
            StartUpload = new CommandBase(UploadFile);
            reportDownloadProgress = (a, b, c) => DownloadProgressVal = b;
            reportUploadProgress = (a, b, c) => UploadProgressVal = b;
            UploadProgressMaxVal = 100;
            UploadProgressVal = 0;
            DownloadProgressMaxVal = 100;
            DownloadProgressVal = 0;
        }

        private async void UploadFile()
        {
            MockFileStorage LocalFileStorage = 
                new MockFileStorage(ConfigurationManager.AppSettings["fileToUploadPath"]);

            FileStorageServiceReference.FileStorageServiceClient TargetFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient();

            var streamToUpload = new ProgressStreamWrapper(LocalFileStorage.GetFile(Guid.Empty));
            streamToUpload.ReportReadProgressEvent += reportUploadProgress;

            UploadProgressMaxVal = streamToUpload.Length;

            await TargetFileStorageService.UploadFileWithMetadataAsync(Guid.NewGuid(), "TestFileName.txt",
                streamToUpload.Length, streamToUpload);
        }

        private async void DownloadFile()
        {
            MockFileStorage LocalFileStorage = 
                new MockFileStorage(ConfigurationManager.AppSettings["downloadResultFilePath"]);

            using (FileStorageServiceReference.FileStorageServiceClient SourceFileStorageService =
                new FileStorageServiceReference.FileStorageServiceClient())
            {
                Stream downloadedFile = new MemoryStream();
                Guid fileId = Guid.NewGuid();
                string fileName = string.Empty;
                long streamLength = 0;

                SourceFileStorageService.DownloadFileWithMetadata(ref fileId, ref fileName,
                    ref streamLength, ref downloadedFile);

                DownloadProgressMaxVal = streamLength;

                Task t = Task.Factory.StartNew(
                    () => {
                        using (var downloadedFileWraper = new ProgressStreamWrapper(downloadedFile))
                        {
                            downloadedFileWraper.ReportReadProgressEvent += reportDownloadProgress;
                            LocalFileStorage.AddFile(downloadedFileWraper);
                        }
                    }
                        );
                await t;
            }
        }

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

        long _DownloadProgressMaxVal;
        public long DownloadProgressMaxVal 
        { 
            get
            {
                return _DownloadProgressMaxVal;
            }
            set 
            {
                _DownloadProgressMaxVal = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DownloadProgressMaxVal"));
            } 
        }

        long _DownloadProgressVal;
        public long DownloadProgressVal
        {
            get
            {
                return _DownloadProgressVal;
            }
            set
            {
                _DownloadProgressVal = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DownloadProgressVal"));
            }
        }
    }
}

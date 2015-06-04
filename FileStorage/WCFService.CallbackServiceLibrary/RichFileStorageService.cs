using SharedKernel.CallbackInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService.CallbackServiceLibrary
{
    public class RichFileStorageService : IRichFileStorageService
    {
        public void UploadFileEnvelope(RichFileDataEnvelope fileData)
        {
            var callback = OperationContext.Current.GetCallbackChannel<ICallbacks>();

            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["uploadResultFilePath"],
                FileMode.OpenOrCreate,
                FileAccess.Write))
            {
                callback.MyCallbackFunction("Start");
                fileData.FileData.CopyTo(fs, 512);
                callback.MyCallbackFunction("End");
            }
            fileData.FileData.Close();
        }

        public RichFileDataEnvelope GetFileEnvelope(Guid fileId)
        {
            throw new NotImplementedException();
        }
    }
}

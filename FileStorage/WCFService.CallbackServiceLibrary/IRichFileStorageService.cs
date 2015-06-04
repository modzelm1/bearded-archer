using SharedKernel.CallbackInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService.CallbackServiceLibrary
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbacks))]
    public interface IRichFileStorageService
    {
        [OperationContract]
        void UploadFileEnvelope(RichFileDataEnvelope fileData);

        [OperationContract]
        RichFileDataEnvelope GetFileEnvelope(Guid fileId);
    }
}

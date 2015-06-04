using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.ServiceLibrary
{
    [MessageContract]
    public class UploadStreamMessage
    {
        [MessageHeader]
        public string appRef;
        [MessageBodyMember]
        public Stream data;
    } 
}

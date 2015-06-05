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
    public class RemoteFileStreamMessage
    {
        [MessageHeader]
        public Guid fileId;
        [MessageHeader]
        public long streamLength;
        [MessageHeader]
        public string fileName;
        [MessageBodyMember]
        public Stream data;
    } 
}

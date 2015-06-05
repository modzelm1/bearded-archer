using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.ServiceLibrary
{
    [MessageContract]
    public class RemoteFileStreamRequest
    {
        [MessageHeader]
        public Guid fileId;
    }
}

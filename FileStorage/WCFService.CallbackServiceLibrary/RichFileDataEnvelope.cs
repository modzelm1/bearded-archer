using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.CallbackServiceLibrary
{
    [DataContract]
    [KnownType(typeof(System.IO.FileStream))]
    public class RichFileDataEnvelope
    {
        [DataMember]
        public Guid FileId { get; set; }
        [DataMember]
        public long StreamLength { get; set; }
        [DataMember]
        public Stream FileData { get; set; }

        public void Dispose()
        {
            if (FileData != null)
            {
                FileData.Close();
            }
        }
    }
}

using System;
using System.Runtime.Serialization;

namespace WCFService.ServiceLibrary
{
    [DataContract]
    public class FileDataEnvelope// : IDisposable
    {
        [DataMember]
        public Guid FileId { get; set; }

        [DataMember]
        public long StreamLength { get; set; }

        [DataMember]
        public System.IO.Stream FileData { get; set; }

        //public void Dispose()
        //{
        //    if (FileData != null)
        //    {
        //        FileData.Close();
        //    }
        //}
    }
}

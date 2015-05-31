﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.ServiceLibrary.Entities
{
    public class RemoteStreamEnvelope
    {
        public Guid FileId { get; set; }
        public long StreamLength { get; set; }
        public Stream FileData { get; set; }
    }
}

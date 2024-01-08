using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushService_Umeko.Models
{
    public class SyncOutput_PushMessageResultParam
    {
        public string Message { get; set; }
        public long Data { get; set; }
        public bool Result { get; set; }
    }
}
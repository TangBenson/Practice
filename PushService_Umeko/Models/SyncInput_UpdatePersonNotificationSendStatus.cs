using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushService_Umeko.Models
{
    public class SyncInput_UpdatePersonNotificationSendStatus
    {
        public Int64 NotificationID { get; set; }
        public int isSend { get; set; }
        public int NewsID { get; set; }
    }
}
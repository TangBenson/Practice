using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushService_Umeko.Models
{
    public class Sync_PersonNotification
    {
        /// <summary>
        /// 通知號碼
        /// </summary>
        public Int64 NotificationID { get; set; }
        /// <summary>
        /// 訂單編號
        /// </summary>
        public Int64 OrderNum { get; set; }
        /// <summary>
        /// 會員身分證
        /// </summary>
        public string IDNO { get; set; }
        /// <summary>
        /// 訊息類型
        /// 1 取車通知
        /// 2 還車通知
        /// 3 逾期未取車
        /// 4 逾時通知
        /// 5 結帳15分鐘未還車
        /// 9 前預約未還車
        /// 15 電源未關
        /// 16 大燈未關
        /// 18 好友推薦
        /// 19 客製化訊息
        /// </summary>
        public int NType { get; set; }
        /// <summary>
        /// 會員姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 會員推播號
        /// </summary>
        public string UserToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 發送時間
        /// </summary>
        public DateTime? PushTime { get; set; }
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 內容連結
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 內容圖片
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// 發送狀態(0 未發送/1 發送中/2 已發送)
        /// </summary>
        public int isSend { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime? MKTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NewsID { get; set; }
    }
}
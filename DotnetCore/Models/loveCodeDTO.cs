using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Models
{
    public class loveCodeDTO
    {
        //public LoveCodeListData LoveCodeListData { set; get; }
        public List<LoveCodeListData> LoveCodeObj { set; get; }
    }

    public class LoveCodeListData
    {
        /// 機關名稱
        public string LoveName { set; get; }
        /// 捐贈碼
        public string LoveCode { set; get; }
        /// 機關簡稱
        public string LoveShortName { set; get; }
        /// 機關統編
        public string UNICode { set; get; }
    }
}

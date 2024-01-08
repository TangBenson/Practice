using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCore.Models;
using Newtonsoft.Json;

namespace DotnetCore.Service.BonusQuery
{
    public class BonusQuery:IBonusQuery
    {
        private static string connetStr = "";
        private readonly IConfiguration _configuration;     
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public BonusQuery(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            connetStr=_configuration["IRent"];
        }

        [HttpPost]
        public Dictionary<string, object> DoBonusQuery(Dictionary<string, object> value) //[FromBody]沒有加的必要阿
        {
            var objOutput = new Dictionary<string, object>();
            IAPI_BonusQuery apiInput = null;
            string Contentjson = "";
            Contentjson = System.Text.Json.JsonSerializer.Serialize(value);
            apiInput = Newtonsoft.Json.JsonConvert.DeserializeObject<IAPI_BonusQuery>(Contentjson);
            string IDNO = apiInput.IDNO;
            // 上面3行用下面1行取代就好，但之所以要做序列化和反序列化應該還是有其必要性，可能我的例子太簡單看不出來
            //string IDNO = value["IDNO"].ToString();
            objOutput.Add("test","天空一聲巨響，老娘閃亮登場");
            objOutput.Add("test2", "狗若回頭，不是報恩就是摸頭");
            objOutput.Add("test3", IDNO);

            return objOutput;
        }
    }
}
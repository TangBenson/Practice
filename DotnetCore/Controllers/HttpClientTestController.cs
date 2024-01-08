using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientTestController : ControllerBase
    {
        // https://blog.darkthread.net/blog/httpclient-sigleton/
        //private static readonly HttpClient HttpClient; //用readonly就會讓createInstance內無法呼叫
        private static HttpClient HttpClient;
        private static DateTime _TTL;
        private static void createInstance()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("http://blog.yowko.com");
            //設定 dispose HttpClient 的時間
            _TTL = DateTime.UtcNow.AddMinutes(1);
        }

        static HttpClientTestController()
        {
            //HttpClient 應該只建立一份並重複利用。為每次請求建立新的 HttpClient，共用靜態 HttpClient 可共用連線避免 TIME_WAIT 連線殘留
            //HttpClient = new HttpClient();
            createInstance();

            //HttpClient 的以下方法是 Thread - Safe 的，在多執行緒下同時呼叫也不會打架，可安心服用：
            //CancelPendingRequests
            //DeleteAsync
            //GetAsync
            //GetByteArrayAsync
            //GetStreamAsync
            //GetStringAsync
            //PostAsync
            //PutAsync
            //SendAsync
        }

        [Route("[Action]")]
        public string testHttpClientGet()
        {
            HttpClient.BaseAddress = new Uri("https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-D0047-061");
            HttpClient.DefaultRequestHeaders.ConnectionClose = true;
            string directionUrl = "?Authorization=CWB-A0B581C8-BDD4-4809-A61D-DD7FB8DD8105";

            directionUrl += "&limit=1";
            directionUrl += "&locationName=" + HttpUtility.UrlEncode("內湖區");
            directionUrl += "&elementName=Wx,PoP12h,T";
            HttpResponseMessage resp = HttpClient.GetAsync(directionUrl).Result;
            string response = resp.Content.ReadAsStringAsync().Result;
            HttpClient.Dispose();
            //重新建立 HttpClient
            createInstance();

            return response;
        }
    }
}

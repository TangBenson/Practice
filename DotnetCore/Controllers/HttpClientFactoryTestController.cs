using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientFactoryTestController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public HttpClientFactoryTestController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("[Action]")]
        public string testHttpClientGet()
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-D0047-061");
            client.DefaultRequestHeaders.ConnectionClose = true;
            string directionUrl = "?Authorization=CWB-A0B581C8-BDD4-4809-A61D-DD7FB8DD8105";

            directionUrl += "&limit=1";
            directionUrl += "&locationName=" + HttpUtility.UrlEncode("內湖區");
            directionUrl += "&elementName=Wx,PoP12h,T";
            HttpResponseMessage resp = client.GetAsync(directionUrl).Result;
            string response = resp.Content.ReadAsStringAsync().Result;
            client.Dispose();

            return response;
        }
    }
}

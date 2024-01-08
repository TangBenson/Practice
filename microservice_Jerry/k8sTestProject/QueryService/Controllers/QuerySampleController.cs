using QueryService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace QueryService.Controllers
{
    //微服務的專案都會盡量簡化，Service內含介面和實作的部分，以前上擎時代會另建一個介面資料夾，但這樣兩邊找要找好久
    [ApiController]
    [Route("api/[controller]")]
    public class QuerySampleDataController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IQuerySampleData _querySampleData;

        //在startup加一個AddScoped就可以在這直接用IQuerySampleData，詳細問Sam。(第一堂59分鐘)
        public QuerySampleDataController(IConfiguration configuration, IQuerySampleData querySampleData)
        {
            _config = configuration;
            _querySampleData = querySampleData;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //以前都是HttpResponseMessage做return，如成偉寫的悠遊付就是這樣，現在.NetCore直接這樣用，但要做序列化(物件或json轉文字)
            return Ok(JsonConvert.SerializeObject(_querySampleData.QuerySampleDataFromDB()));
        }
    }
}

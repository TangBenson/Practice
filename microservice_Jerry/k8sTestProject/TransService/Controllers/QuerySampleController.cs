// using TransService.Services;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Newtonsoft.Json;

// namespace TransService.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class QuerySampleDataController : ControllerBase
//     {
//         private readonly IConfiguration _config;
//         private readonly IQuerySampleData _querySampleData;

//         public QuerySampleDataController(IConfiguration configuration, IQuerySampleData querySampleData)
//         {
//             _config = configuration;
//             _querySampleData = querySampleData;
//         }

//         [HttpGet]
//         public IActionResult Get()
//         {
//             return Ok(JsonConvert.SerializeObject(_querySampleData.QuerySampleDataFromDB()));
//         }
//     }
// }

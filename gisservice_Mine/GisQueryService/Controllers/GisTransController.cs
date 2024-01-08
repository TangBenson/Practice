using GisQueryService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GisQueryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisTransController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IQueryGisData _queryGisData;

        public GisTransController(IConfiguration configuration,IQueryGisData queryGisData)
        {
            _config = configuration;
            _queryGisData=queryGisData;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_queryGisData.QueryGisDataToQueue());
        }
    }
}

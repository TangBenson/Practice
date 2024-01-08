using GisQueryService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GisQueryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisTransAddrController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IQueryAddrData _queryAddrData;

        public GisTransAddrController(IConfiguration configuration,IQueryAddrData queryAddrData)
        {
            _config = configuration;
            _queryAddrData=queryAddrData;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_queryAddrData.QueryAddrDataToQueue());
        }
    }
}

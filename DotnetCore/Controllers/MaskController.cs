using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DotnetCore.Service;
using Microsoft.AspNetCore.Mvc;
//呼叫 Service，MaskService 是注入進來的。Get() 只是簡單呼叫 Service。

namespace DotnetCore.Controllers
{
    /*
    決定這個 controller 的路由，[controller]表示接採用 controller 的名稱。
    改成 Route("hello") 的話，就會變成 /hello/；
    那如果我們接著在 Function 上掛上 Route("world") 的話，
    該方法的路由就會變成 /hello/world/，以此類推
    */
    [Route("api/[controller]")] // api的路由習慣用api/
    [ApiController] //告訴我們這是個 Api Controller
    public class MaskController : ControllerBase // vs建的專案是繼承ControllerBase，為何這邊是繼承Controller? 然我測試兩個都能正常run，差異是?
    {
        // 這是預設就有，但vs建專案卻沒有，ILogger好像是微軟自己的log機制
        // private readonly ILogger<MaskController> _logger;
        // public MaskController(ILogger<MaskController> logger)
        // {
        //     _logger = logger;
        // }

        private readonly MaskService _maskService;

        public MaskController(MaskService maskService)
        {
            _maskService = maskService;
        }

        // 預設有的
        // public IActionResult Index()
        // {
        //     return View();
        // }
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }

        public async Task<IActionResult> Get()
        {
            try
            {
                var maskCount = await _maskService.GetMaskInfo();
                return Ok(maskCount);
            }
            catch (HttpRequestException ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
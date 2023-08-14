using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using My_Attribute.Models;

namespace My_Attribute.Controllers;

public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        // _logger = logger;
    }

    public IActionResult External()
    {
        ExternalBulletin extBu = new ExternalBulletin();
        ViewBag.xxxx = extBu;
        return View();
    }

    public IActionResult Internal()
    {
        InternalBulletin intBu = new InternalBulletin();
        ViewBag.xxxx = intBu;
        return View();
    }

    public IActionResult Normal()
    {
        NormalBulletin norBu = new NormalBulletin();
        ViewBag.xxxx = norBu;
        return View();
    }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }

}

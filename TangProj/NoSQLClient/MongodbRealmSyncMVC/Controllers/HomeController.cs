using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongodbRealmSyncMVC.Models;
using Realms;
using Realms.Sync;

namespace MongodbRealmSyncMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private App _app;
    private Realm _realm;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        Console.WriteLine("QQQQcontroller");

        //step <1>
        _app = App.Create("application-0-gyubp");
        var credential = Credentials.Anonymous();
        try
        {
            _app.LogInAsync(credential);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
        }

        //step <2>
        var config = new FlexibleSyncConfiguration(_app.CurrentUser);

        //step <3>
        // Realm.DeleteRealm(config);

        //step <4>
        _realm = Realm.GetInstance(config);
        _realm.Subscriptions.Update(() =>
        {
            var myCars = _realm.All<Car>();
            _realm.Subscriptions.Add(myCars);
        });

        //step <5>
        _realm.Subscriptions.WaitForSynchronizationAsync();
    }

    [HttpGet]
    public IActionResult Index()
    {
        Console.WriteLine("IndexAction");

        while (true)
        {
            _realm.Refresh();
            Console.WriteLine($"----------------{_realm.SyncSession.ConnectionState}-------------------");
            Thread.Sleep(2000);
            var myCars = _realm.All<Car>();
            if (myCars.Any())
            {
                ViewBag.count = myCars.Count();
                break;
            }
        }
        // _realm?.Dispose();

        return View();
    }

    [HttpGet]
    public int IndexPost()
    {
        Console.WriteLine("IndexPost");
        Console.WriteLine($"----------------{_realm.SyncSession.ConnectionState}-------------------");
        _realm.Refresh();
        var myCars = _realm.All<Car>();
        // _realm?.Dispose();
        return myCars.Count();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


public partial class Car : IRealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }
    public string CarNo { get; set; }
    [MapTo("available")]
    public int Available { get; set; }
}
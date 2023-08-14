using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewTest.Models;

namespace ViewTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        //回傳給DropDownList
        var lstPeople = new People[]{
            new People{Name="TOM",id=1},
            new People{Name="Tony",id=2},
            new People{Name="Bob",id=3},
            new People{Name="David",id=4}
        };
        SelectList slst = new SelectList(lstPeople, "id", "Name", 2); //預設選2
        ViewBag.CCPP = slst;

        //回傳這個是為了給TextBoxFor，這樣input的value才會被設定值=ss
        TestModel a = new TestModel
        {
            TangText = "ss",
            p = new People { Name = "Jerry", id = 3 },
            lst = slst
        };

        //回傳給DropDownList
        List<SelectListItem> peopleList = new List<SelectListItem>();
        peopleList.Add(new SelectListItem()
        {
            Text = "Tang",
            Value = "1"
        });
        peopleList.Add(new SelectListItem()
        {
            Text = "Peggy",
            Value = "2",
            Selected = true
        });
        peopleList.Add(new SelectListItem()
        {
            Text = "Benson",
            Value = "3"
        });
        ViewBag.CompanyId = peopleList;
        return View(a);
    }

    // FromQuery解析Query String
    public IActionResult Tang([FromQuery] string memo)
    {
        ViewBag.me = (memo == null) ? "nothing" : memo;
        ViewData["mymemo"] = "BBQ~~~~";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken] //登入畫面時會產生一個隱藏的input值，預防跨網站攻擊
    public IActionResult Tang2(string in1)
    {
        ViewBag.me = in1;
        return View();
    }

    //以下是網路的範例，https://dotblogs.com.tw/bda605/2021/10/27/233935
    public ActionResult Create()
    {
        var model = new FormControlsViewModel();
        model.Courses = CreateCourseList();
        model.Countries = CreateCountryList();
        return View(model);
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult Create(FormControlsViewModel model)
    {
        model.Courses = CreateCourseList();
        model.Countries = CreateCountryList();
        //var user = _fRepo.AddUser(model);
        //_fRepo.AddUserCountry(model.Country, null, user);
        //_fRepo.AddUserDescription(user.ID, model.Description);
        //_fRepo.AddUserCourses(model.Courses, user.ID);
        //_fRepo.Save();
        //return RedirectToAction("Index", "Home");
        return View(model);
    }
    public List<Course> CreateCourseList()
    {
        var courses = new List<Course>();

        courses.Add(new Course { Name = "aaaa", ID = "1", Checked = true });
        courses.Add(new Course { Name = "bbbb", ID = "2" });
        courses.Add(new Course { Name = "cccc", ID = "3" });

        return courses;
    }

    public IEnumerable<SelectListItem> CreateCountryList()
    {
        List<SelectListItem> countryNames = new List<SelectListItem>();

        countryNames.Add(new SelectListItem { Text = "aaa", Value = "1" });
        countryNames.Add(new SelectListItem { Text = "bbb", Value = "2" });
        countryNames.Add(new SelectListItem { Text = "ccc", Value = "3" });
        return countryNames;
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

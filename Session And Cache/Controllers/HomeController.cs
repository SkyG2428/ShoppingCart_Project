using Microsoft.AspNetCore.Mvc;
using Session_And_Cache.Models;
using System.Diagnostics;

namespace Session_And_Cache.Controllers
{
    public class HomeController : Controller
    {
        //public const string SessionKeyName = "_Name";
        //public const string SessionKeyAge = "_Age";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

      

        public IActionResult Index()
        {
            ViewData["var1"] = "Data Comes From ViewData";
            ViewBag.var2 = "Data Comes From ViewBag";
            TempData["var3"] = "Data Comes From TempData";
            //Session["var4"] = "Data Comes From ViewData";
            


            return View();
        }

        public IActionResult About()
        {
            return View();
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
}
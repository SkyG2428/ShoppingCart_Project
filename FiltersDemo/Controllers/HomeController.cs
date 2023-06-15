using FiltersDemo.Filters;
using FiltersDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FiltersDemo.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        
        //[AllowAnonymous]
        
        [MyResourceFilter]
        [MyActionFilter]
        [MyExceptionFilter]
        [HttpGet]
        public IActionResult Login()
        {
            Console.WriteLine("Login Start.....");

            //int a = 10, b=0;
            //int c = a / b;

            Console.WriteLine("Login End.....");

            return View();
        }
        [HttpPost]
        public IActionResult login(LoginModel l)
        {
            if (l.username == "akash" || l.password == "akash@123")
            {
                return RedirectToAction("index", "home");

            }

            return View();
        }


        [Authorize]
        //[AllowAnonymous]
        public IActionResult Index()
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

        public IActionResult ErrorView()
        {
            return View();
        }
    }
}
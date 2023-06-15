using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Session_And_Cookies_Practice.Models;
using System.Diagnostics;

namespace Session_And_Cookies_Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            LoginUserModel us = new LoginUserModel();
            us.UserName = form["UserName"];
            us.Password = form["Password"];

            Response.Cookies.Append("user", us.UserName);
            Response.Cookies.Append("pass", us.Password);
            string jsformated = JsonSerializer.Serialize(us);
            Response.Cookies.Append("pass", jsformated);

            //Session
            HttpContext.Session.SetString("Username", us.UserName);

            string jsonSession=JsonSerializer.Serialize(us);
            HttpContext.Session.SetString("Details", jsonSession);


            // Create Model
            LoginUserModel user1 = new LoginUserModel()
            {
                UserName = "Vinod", Password = "Sachin"
            };
            // Serialize
            string user2=JsonSerializer.Serialize(user1);

            HttpContext.Session.SetString("NewUser", user2);




            if (us.UserName == "Akash" && us.Password == "Akash")
            {
                // Create a new cookie with a name "MyCookie" and a value "Hello, World!"
                
                //cookieOptions.Expires = DateTime.Now.AddDays(1);
               

                return RedirectToAction("Index","UserLogin", new {area ="UserArea"});
            }
            else
            {
                return View();
            }

            
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
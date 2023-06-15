using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Session_And_Cookies_Practice.Models;

namespace Session_And_Cookies_Practice.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class UserLoginController : Controller
    {
        public IActionResult Index()
        {
            // Retrieve the value of the "MyCookie" cookie
            ViewBag.cookiedemo = Request.Cookies["user"];

            ViewBag.cookiePAss = Request.Cookies["pass"];//Simple

            ////Desirialize
            ViewBag.cookiePAss = JsonSerializer.Deserialize<LoginUserModel>(Request.Cookies["pass"]);

            // Do something with the cookie value


            //Session Data

            ViewBag.sessionUser= HttpContext.Session.GetString("Username");
            ViewBag.SessionAllDEtails = JsonSerializer.Deserialize<LoginUserModel>(HttpContext.Session.GetString("Details"));

            //New User Session
            ViewBag.NewuserData = JsonSerializer.Deserialize<LoginUserModel>(HttpContext.Session.GetString("NewUser"));


            return View();
        }
    }
}

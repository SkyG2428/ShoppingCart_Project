using AreaPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace AreaPractice.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Login(LoginModel user)
        {
            if(user.Username=="Aani2428@" && user.Password=="Aani2428@")
            {
                return RedirectToAction("Index", "Users", new { area = "user" });
            }
            else if (user.Username == "Aani@2428" && user.Password == "Aani@2428")
            {
                return RedirectToAction("Index", "Admins", new { area = "Admin" });
            }

            return View();
        }
    }
}

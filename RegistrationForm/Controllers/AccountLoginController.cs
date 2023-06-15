using Microsoft.AspNetCore.Mvc;
using RegistrationForm.Models;
using System.Data.SqlClient;
using System.Reflection;

namespace RegistrationForm.Controllers
{
    public class AccountLoginController : Controller
    {
        IConfiguration _config;
        string connectionString = null;

        public AccountLoginController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:login"]; 
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel user)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from RegistrationForm where  UserName='{user.UserName}' and Password='{user.Password}'";
            SqlCommand cmd = new SqlCommand(txt, connection);
            int rows = cmd.ExecuteNonQuery();


            if (user.UserName == "UserName" && user.Password == "Password")
                {
                      return RedirectToAction("info", "RegistrationUser", new { area = "Registration"});
                }
            

            return View();
        }




    }
}

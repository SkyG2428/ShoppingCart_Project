using LoginValidsation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace LoginValidsation.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        IConfiguration _config;
        string connectionString = null;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            connectionString = _config["ConnectionStrings:DefaultLogin"];
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllRegistration()
        {
            List<RegistrationModel> list = new List<RegistrationModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from Registration";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RegistrationModel model = new RegistrationModel();
                    model.id = (int)reader["id"];
                    model.Fullname = reader["FullName"].ToString();
                    model.Email = reader["Email"].ToString();
                    model.UserName = reader["UserName"].ToString();
                    model.UserType = reader["UserType"].ToString();
                    model.mobile = (long)reader["Mobile"];
                    model.DOB = !reader.IsDBNull("DOB") ? (DateTime)reader["DOB"]:DateTime.Parse("01/01/0001");
                    model.Password = reader["Password"].ToString();

                    list.Add(model);
                }
            }
            connection.Close();

            return View(list);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(RegistrationModel C)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into Registration values('{C.Fullname}','{C.Email}','{C.UserName}','{C.UserType}','{C.mobile}','{C.DOB}','{C.Password}')";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Index");
            }

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
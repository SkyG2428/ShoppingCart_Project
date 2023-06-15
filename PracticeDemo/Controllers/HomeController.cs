using Microsoft.AspNetCore.Mvc;
using PracticeDemo.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PracticeDemo.Controllers
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
            connectionString = _config["ConnectionStrings:CompanyData"];
        }

        public IActionResult Index()
        {
            List<CompanyDetailsModel> list = new List<CompanyDetailsModel>();


            SqlConnection connection = new SqlConnection(connectionString);
            
            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from CompanyInfo";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CompanyDetailsModel model = new CompanyDetailsModel();
                    model.CompanyId = (int)reader["CompanyId"];
                    model.CompanyName = reader["CompanyName"].ToString();

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
        public IActionResult Create(CompanyDetailsModel C)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into CompanyInfo values('{C.CompanyName}')";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int rows=cmd.ExecuteNonQuery();
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
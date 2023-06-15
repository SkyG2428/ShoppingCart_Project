using CSCDropDown.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CSCDropDown.Controllers
{
    public class CascadeController : Controller
    {
        IConfiguration _config;
        string connectionString = null;
        public CascadeController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:B20ValidationDB"];
        }


        public IActionResult CascadeDropdown()
        {
            return View();
        }

        public IActionResult Country()
        {
            List<CountryModel> list = new List<CountryModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from Country";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CountryModel model = new CountryModel();
                    model.Countryid = (int)reader["id"];
                    model.CountryName = reader["CountryName"].ToString();

                    list.Add(model);
                }
            }
            connection.Close();

            return View(list);
   
        }



        [HttpGet]
        public IActionResult CountryCreate()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CountryCreate(CountryModel C)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into Country values('{C.CountryName}')";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Country","Cascade");
            }

            return View();
        }



        public IActionResult State()
        {
            List<StateModel> list = new List<StateModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from State";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StateModel model = new StateModel();
                    model.stateid = (int)reader["id"];
                    model.stateName = reader["StateName"].ToString();
                    model.Countryid = (int)reader["CountryId"];

                    list.Add(model);
                }
            }
            connection.Close();

            return View(list);

        }

        [HttpGet]
        public IActionResult StateCreate()
        {

            return View();
        }

        [HttpPost]
        public IActionResult StateCreate(StateModel C)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into State values('{C.stateName}',{C.Countryid})";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("State", "Cascade");
            }

            return View();
        }


        public IActionResult City()
        {
            List<CityModel> list = new List<CityModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from City";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CityModel model = new CityModel();
                    model.Cityid = (int)reader["id"];
                    model.CityName = reader["CityName"].ToString();
                    model.stateid = (int)reader["StateId"];

                    list.Add(model);
                }
            }
            connection.Close();

            return View(list);

        }

        [HttpGet]
        public IActionResult CityCreate()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CityCreate(CityModel C)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into City values('{C.CityName}',{C.stateid})";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("City", "Cascade");
            }

            return View();
        }


        public IActionResult Index()
        {
            return View();
        }






    }
}

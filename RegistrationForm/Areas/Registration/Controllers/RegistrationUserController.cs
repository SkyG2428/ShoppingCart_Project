using Microsoft.AspNetCore.Mvc;
using RegistrationForm.Areas.Registration.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace RegistrationForm.Areas.Registration.Controllers
{
    [Area("Registration")]
    public class RegistrationUserController : Controller
    {
        IConfiguration _config;
        string connectionString = null;

        public RegistrationUserController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:login"]; ;
        }


        
        public IActionResult Index()
        {
            


            List<RegistrationModel> model = new List<RegistrationModel>();
            SqlConnection connect = new SqlConnection(connectionString);
            string Text = "Select * from RegistrationForm";
            SqlCommand cmd = new SqlCommand(Text, connect);
            //SqlDataReader reader = cmd.ExecuteReader();
            //if(reader.HasRows)
            //{
            //    while(reader.Read())
            //    {

            //    }

            //}

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new RegistrationModel
                {
                    UserId = (int)dr["UserId"],
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Phone = dr["Phone"].ToString(),
                    Email = dr["Email"].ToString(),
                    UserName = dr["UserName"].ToString(),
                    Password = dr["Password"].ToString()
                });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegistrationModel model)
        {
           
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into RegistrationForm values('{model.FirstName}','{model.LastName}','{model.Phone}','{model.Email}','{model.UserName}','{model.Password}')";
            SqlCommand cmd = new SqlCommand(txt, connection);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Login","AccountLogin", new { area = ""});
            }

            

            return View();
        }



        public IActionResult info(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from RegistrationForm where UserId={id}";
            SqlCommand cmd = new SqlCommand(txt, connection);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}

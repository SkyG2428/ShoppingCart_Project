using Microsoft.AspNetCore.Mvc;
using PracticeDemo.Areas.EmployeeArea.Models;
using PracticeDemo.Areas.TeamsArea.Models;
using System.Data.SqlClient;

namespace PracticeDemo.Areas.EmployeeArea.Controllers
{
    [Area("EmployeeArea")]
    public class EmployeesController : Controller
    {
        IConfiguration _config;
        string connectionString = null;

        public EmployeesController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:CompanyData"];
        }

        public IActionResult Index()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from Employee";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EmployeeModel model = new EmployeeModel();
                    model.Id = (int)reader["Id"];
                    model.Name = reader["Name"].ToString();
                    model.Gender = reader["Gender"].ToString();
                    
                    model.Address = reader["Address"].ToString();
                    model.Qualification = reader["Qualification"].ToString();
                    model.Experience = reader["Experience"].ToString();
                    model.EmlpoyeeTeamId = (int)reader["EmlpoyeeTeamId"];

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
        public IActionResult Create(EmployeeModel Team)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into Employee values('{Team.Name}','{Team.Gender}','{Team.Address}','{Team.Qualification}','{Team.Experience}','{Team.EmlpoyeeTeamId}')";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Index", "Employees", new { area = "EmployeeArea" });
            }

            return View();
        }




        public IActionResult EmployeeById(int id)
        {
            List<EmployeeModel> list = new List<EmployeeModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from Employee where EmlpoyeeTeamId={id}";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EmployeeModel model = new EmployeeModel();
                    model.Id = (int)reader["Id"];
                    model.Name = reader["Name"].ToString();
                    model.Gender = reader["Gender"].ToString();
                    //model.DOB = (DateOnly)reader["DOB"];
                    model.Address = reader["Address"].ToString();
                    model.Qualification = reader["Qualification"].ToString();
                    model.Experience = reader["Experience"].ToString();
                    model.EmlpoyeeTeamId = (int)reader["EmlpoyeeTeamId"];


                    list.Add(model);
                }
            }
            connection.Close();
            return View(list);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
           

            SqlConnection connection = new SqlConnection(connectionString);

            string txt = $"select * from Employee  where id={id}";

            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            EmployeeModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new EmployeeModel();
                    model.Id = (int)reader["Id"];
                    model.Name = reader["Name"].ToString();
                    model.Gender = reader["Gender"].ToString();

                    model.Address = reader["Address"].ToString();
                    model.Qualification = reader["Qualification"].ToString();
                    model.Experience = reader["Experience"].ToString();
                    model.EmlpoyeeTeamId = (int)reader["EmlpoyeeTeamId"];
                    break;

                }
            }
            connection.Close();

            return View(model);
        }
        [HttpPost]

        public IActionResult Edit(EmployeeModel Emp)
        {
            
            SqlConnection connection = new SqlConnection(connectionString);

            string txt = $"update Employee set Name = '{Emp.Name}',Gender='{Emp.Gender}',Address='{Emp.Address}',Qualification='{Emp.Qualification}',Experience='{Emp.Experience}',EmlpoyeeTeamId='{Emp.EmlpoyeeTeamId}' where id= " + Emp.Id;
            SqlCommand cmd = new SqlCommand(txt, connection);

            connection.Open();

            int Rows1 = cmd.ExecuteNonQuery();
            if (Rows1 > 0)
            {
                return RedirectToAction("Index", "Employees", new { area = "EmployeeArea" });
            }

            return View();
        }



        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {


            SqlConnection connection = new SqlConnection(connectionString);

            string txt = $"select * from Employee  where id={id}";

            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            EmployeeModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new EmployeeModel();
                    model.Id = (int)reader["Id"];
                    model.Name = reader["Name"].ToString();
                    model.Gender = reader["Gender"].ToString();

                    model.Address = reader["Address"].ToString();
                    model.Qualification = reader["Qualification"].ToString();
                    model.Experience = reader["Experience"].ToString();
                    model.EmlpoyeeTeamId = (int)reader["EmlpoyeeTeamId"];
                    break;

                }
            }
            connection.Close();

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete_Confirm(int? id)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            string txt = $"Delete from Employee where id={id}";
            SqlCommand cmd = new SqlCommand(txt, connection);

            connection.Open();

            int Rows1 = cmd.ExecuteNonQuery();
            if (Rows1 > 0)
            {
                return RedirectToAction("Index", "Employees", new { area = "EmployeeArea" });
            }

            return View();
        }



        public IActionResult Details(int id)
        {
            

            SqlConnection connection = new SqlConnection(connectionString);


            string txt = $"select * from Employee where id={id}";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            EmployeeModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new EmployeeModel();
                    model.Id = (int)reader["Id"];
                    model.Name = reader["Name"].ToString();
                    model.Gender = reader["Gender"].ToString();

                    model.Address = reader["Address"].ToString();
                    model.Qualification = reader["Qualification"].ToString();
                    model.Experience = reader["Experience"].ToString();
                    model.EmlpoyeeTeamId = (int)reader["EmlpoyeeTeamId"];
                    break;
                    
                }
            }

            return View(model);
        }

    }
}

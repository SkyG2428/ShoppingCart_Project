using Microsoft.AspNetCore.Mvc;
using PracticeDemo.Areas.TeamsArea.Models;
using PracticeDemo.Models;
using System.Data.SqlClient;

namespace PracticeDemo.Areas.TeamsArea.Controllers
{
    [Area("TeamsArea")]
    public class TeamController : Controller
    {
        IConfiguration _config;
        string connectionString = null;
        public TeamController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:CompanyData"];
        }

        public IActionResult Index()
        {
            List<TeamDetailsModel> list = new List<TeamDetailsModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from Teams";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TeamDetailsModel model = new TeamDetailsModel();
                    model.TeamId = (int)reader["TeamId"];
                    model.TeamName = reader["TeamName"].ToString();
                    model.Batch = reader["Batch"].ToString();
                    model.TeamCompanyId = (int)reader["TeamCompanyId"];

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
        public IActionResult Create(TeamDetailsModel Team)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"insert into Teams values('{Team.TeamName}','{Team.Batch}','{Team.TeamCompanyId}')";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Index", "Team", new { area = "TeamsArea" });
            }

            return View();
        }


         

        ///Id Basis Find Team

        public IActionResult TeamsById(int id)
        {
            List<TeamDetailsModel> list = new List<TeamDetailsModel>();


            SqlConnection connection = new SqlConnection(connectionString);

            //string txt = $"insert into production.brands values('{bike.BrandName}')";
            string txt = $"Select * from Teams where TeamCompanyId={id}";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TeamDetailsModel model = new TeamDetailsModel();
                    model.TeamId = (int)reader["TeamId"];
                    model.TeamName = reader["TeamName"].ToString();
                    model.Batch = reader["Batch"].ToString();
                    model.TeamCompanyId = (int)reader["TeamCompanyId"];

                    list.Add(model);
                }
            }
            connection.Close();
            return View(list);
        }

    }
}

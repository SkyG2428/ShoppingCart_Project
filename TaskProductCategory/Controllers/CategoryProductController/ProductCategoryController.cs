using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TaskProductCategory.Models.E_Commerce;

namespace TaskProductCategory.Controllers.CategoryProductController
{
    public class ProductCategoryController : Controller
    {
        
        string _connectionString = null;
        IConfiguration _configuration;

        public ProductCategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:CPdata"];
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Category()
        {
            List<CategoryProductsModel> list = new List<CategoryProductsModel>();
            SqlConnection con = new SqlConnection(_connectionString);

            string query = "Select * from Category";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CategoryProductsModel model = new CategoryProductsModel();
                    model.CategoryId = (int)dr["CategoryId"];
                    model.CategoryName = dr["CategoryName"].ToString();
                    
                    list.Add(model);
                }
            }

            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryProductsModel cp)
        {
            
        SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            string txt = $"insert into Category values('{cp.CategoryName}')";
            SqlCommand cmd = new SqlCommand(txt, con);
            
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Category");
            }
            

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //List<CategoryProductsModel> list = new List<CategoryProductsModel>();

            SqlConnection con = new SqlConnection(_connectionString);


            string txt = $"select * from Category where CategoryId={id}";
            SqlCommand cmd = new SqlCommand(txt, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            CategoryProductsModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new CategoryProductsModel()
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    break;

                }
            }
            con.Close();

            return View(model);


        }

        [HttpPost]
        public IActionResult Edit(CategoryProductsModel ck)
        {
            
            SqlConnection connection = new SqlConnection(_connectionString);

            string txt = $"update Category set CategoryName = '{ck.CategoryName}' where CategoryID= " + ck.CategoryId;
            SqlCommand cmd = new SqlCommand(txt, connection);

            connection.Open();

            int Rows1 = cmd.ExecuteNonQuery();
            if (Rows1 > 0)
            {
                return RedirectToAction("Category");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            string txt = $"select * from Category where CategoryId={id}";
            SqlCommand cmd= new SqlCommand(txt, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            CategoryProductsModel model = null;
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                     model = new CategoryProductsModel()
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    break;
                }
            }
            con.Close();

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete_Confirm(int? id)
        {
            SqlConnection connection= new SqlConnection(_connectionString);
            string txt = $"Delete from Category where CategoryId={id}";
            SqlCommand cmd=new SqlCommand(txt, connection);
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            if (row > 0)
            {
               return RedirectToAction("Category");
            }
            connection.Close();

            return View();
        }


        public IActionResult Details(int id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            string txt = $"Select * from Category Where CategoryId={id}";
            SqlCommand cmd = new SqlCommand(txt, con);
            con.Open();
            SqlDataReader reader= cmd.ExecuteReader();
            CategoryProductsModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new CategoryProductsModel()
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    break;
                }
            }
            con.Close();
            return View(model);
        }

  
        public IActionResult Products()
        {
            List<CategoryProductsModel> list = new List<CategoryProductsModel>();
            SqlConnection con = new SqlConnection(_connectionString);

            string query = "Select * from Products";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CategoryProductsModel model = new CategoryProductsModel();
                    model.ProductId = (int)dr["ProductId"];
                    model.ProductName = dr["ProductName"].ToString();
                    model.ProductDescription = dr["ProductDescription"].ToString();
                    model.Price = (decimal)dr["UnitPrice"];
                    model.CategoryId = (int)dr["CategoryId"];
                    model.Image = dr["ProductImage"].ToString();

                    list.Add(model);
                }
            }

            return View(list);
        }




        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreate(CategoryProductsModel cp)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            string txt = $"insert into Products values('{cp.ProductId}','{cp.ProductName}','{cp.ProductDescription}','{cp.Price}','{cp.CategoryId}','{cp.Image}')";
            SqlCommand cmd = new SqlCommand(txt, con);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                return RedirectToAction("Products");
            }

            return View();
        }


        [HttpGet]
        public IActionResult ProductEdit(int? id)
        {
            //List<CategoryProductsModel> list = new List<CategoryProductsModel>();

            SqlConnection con = new SqlConnection(_connectionString);


            string txt = $"select * from Products where ProductId={id}";
            SqlCommand cmd = new SqlCommand(txt, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            CategoryProductsModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new CategoryProductsModel()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        ProductDescription = reader["ProductDescription"].ToString(),
                        Price = (decimal)reader["UnitPrice"],
                        CategoryId = (int)reader["CategoryId"],
                        Image = reader["ProductImage"].ToString()
                    };
                    break;

                }
            }
            con.Close();

            return View(model);


        }

        [HttpPost]
        public IActionResult ProductEdit(CategoryProductsModel ck)
        {

            SqlConnection connection = new SqlConnection(_connectionString);

            string txt = $"update Products set ProductName = '{ck.ProductName}',ProductDescription='{ck.ProductDescription}',UnitPrice='{ck.Price}',CategoryId='{ck.CategoryId}',ProductImage='{ck.Image}' where CategoryID= " + ck.CategoryId;
            SqlCommand cmd = new SqlCommand(txt, connection);

            connection.Open();

            int Rows1 = cmd.ExecuteNonQuery();
            if (Rows1 > 0)
            {
                return RedirectToAction("Products");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ProductDelete(int? id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            string txt = $"select * from Products where ProductId={id}";
            SqlCommand cmd = new SqlCommand(txt, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            CategoryProductsModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new CategoryProductsModel()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        ProductDescription = reader["ProductDescription"].ToString(),
                        Price = (decimal)reader["UnitPrice"],
                        CategoryId = (int)reader["CategoryId"],
                        Image = reader["ProductImage"].ToString()
                    };
                    break;
                }
            }
            con.Close();

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ProductDelete_confirm(int? id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string txt = $"Delete from Products where ProductId={id}";
            SqlCommand cmd = new SqlCommand(txt, connection);
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            if (row > 0)
            {
                return RedirectToAction("Products");
            }
            connection.Close();

            return View();
        }


        public IActionResult ProductDetails(int id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            string txt = $"Select * from Products Where ProductId={id}";
            SqlCommand cmd = new SqlCommand(txt, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            CategoryProductsModel model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new CategoryProductsModel()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        ProductDescription = reader["ProductDescription"].ToString(),
                        Price = (decimal)reader["UnitPrice"],
                        CategoryId = (int)reader["CategoryId"],
                        Image = reader["ProductImage"].ToString()
                    };
                    break;
                }
            }
            con.Close();
            return View(model);
        }


    }
}

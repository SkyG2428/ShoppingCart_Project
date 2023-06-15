using Microsoft.AspNetCore.Mvc;
using ProductADO.Models.Products;
using System.Data;
using System.Data.SqlClient;

namespace ProductADO.Data
{
    public class Product_Data
    {
        IConfiguration _config;
        string _connectionString = null;
        public Product_Data(IConfiguration config)
        {
            _config = config;
            _connectionString = _config["ConnectionStrings:ProductData"];

        }


        //Get All Products
        public List<ProductModule> AllProducts()
        {
            List<ProductModule> products = new List<ProductModule>();

            SqlConnection connect = new SqlConnection(_connectionString);
            string Text = "proc_product";
            SqlCommand cmd = new SqlCommand(Text, connect);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();


            adapter.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                products.Add(new ProductModule
                {
                    ProductId = (int)dr["ProductId"],
                    ProductName = dr["ProductName"].ToString(),
                    ProductDescription = dr["Discription"].ToString(),
                    ProductPrice = (decimal)dr["Price"],
                    ProductQuantity = (int)dr["quantity"],
                    Remark = dr["Remark"].ToString()
                });
            }


            return products;
        }

        //Insert Products
        public bool InsertProduct(ProductModule prod)
        {
            SqlConnection connect = new SqlConnection(_connectionString);
            string Text = "SP_InsertProduct";
            SqlCommand cmd = new SqlCommand(Text, connect);
            cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("@ProductId",prod.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", prod.ProductName);
            cmd.Parameters.AddWithValue("@Discription", prod.ProductDescription);
            cmd.Parameters.AddWithValue("@Price", prod.ProductPrice);
            cmd.Parameters.AddWithValue("@quantity", prod.ProductQuantity);
            cmd.Parameters.AddWithValue("@remark", prod.Remark);

            connect.Open();
            int id = cmd.ExecuteNonQuery();
            connect.Close();
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //Get Products by Specific Id
        public List<ProductModule> GetProductById(int? id)
        {
            List<ProductModule> products = new List<ProductModule>();

            SqlConnection connect = new SqlConnection(_connectionString);
            string Text = "sp_UpdateProduct";

            SqlCommand cmd = new SqlCommand(Text, connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();


            adapter.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                products.Add(new ProductModule
                {
                    ProductId = (int)dr["ProductId"],
                    ProductName = dr["ProductName"].ToString(),
                    ProductDescription = dr["Discription"].ToString(),
                    ProductPrice = (decimal)dr["Price"],
                    ProductQuantity = (int)dr["quantity"],
                    Remark = dr["Remark"].ToString()
                });
            }


            return products;
        }


        //Update Products
        public bool UpdateProduct(ProductModule prod)
        {
            SqlConnection connect = new SqlConnection(_connectionString);
            string Text = "SP_UpdatedProductDetails";
            SqlCommand cmd = new SqlCommand(Text, connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", prod.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", prod.ProductName);
            cmd.Parameters.AddWithValue("@Discription", prod.ProductDescription);
            cmd.Parameters.AddWithValue("@Price", prod.ProductPrice);
            cmd.Parameters.AddWithValue("@quantity", prod.ProductQuantity);
            cmd.Parameters.AddWithValue("@remark", prod.Remark);

            connect.Open();
            int id = cmd.ExecuteNonQuery();
            connect.Close();
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        //Delete Products


        public bool Delete(int? id)
        {
          

            SqlConnection connect = new SqlConnection(_connectionString);
            string Text = "Sp_DeleteProduct";

            SqlCommand cmd = new SqlCommand(Text, connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", id);
            connect.Open();
            int reader = cmd.ExecuteNonQuery();

            connect.Close();
            if(reader > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}

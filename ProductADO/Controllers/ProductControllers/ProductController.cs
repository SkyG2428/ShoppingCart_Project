using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductADO.Data;
using ProductADO.Models.Products;
using System.ComponentModel;

namespace ProductADO.Controllers.ProductControllers
{
    public class ProductController : Controller
    {

        IConfiguration _config;

        public ProductController(IConfiguration config)
        {
            _config = config;


        }

        

        // GET: ProductController
        public ActionResult Index()
        {
            Product_Data data=new Product_Data(_config);
            var All = data.AllProducts();

            if(All.Count==2)
            {
                TempData["InfoMessage"] = "Currently Products not available in the Database.";
            }

            return View(All);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        
        public ActionResult Create(ProductModule prod)
        {
            Product_Data data = new Product_Data(_config);
            bool isInserted=false;
            try
            {
                //if (ModelState.IsValid)
                //{
                   
                    isInserted = data.InsertProduct(prod);
                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Product Details Saved Successfully....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product is Alredy Available/Unable To Save product details....";
                    }
                    
                //}
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product_Data data = new Product_Data(_config);
            var All = data.GetProductById(id).FirstOrDefault();

            if(All==null)
            {
                TempData["InfoMessage"] = "Product not available on This Id " +id.ToString();
                return RedirectToAction("Index");
            }

            return View(All);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductModule prod)
        {
            Product_Data data = new Product_Data(_config);


             if (ModelState.IsValid)
             {

                    bool isUpdated = data.UpdateProduct(prod);
                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Product Details Update Successfully....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product is Alredy Available/Unable To Update product details....";
                    }
                    
             }

            return RedirectToAction("Index");
        }


        [HttpGet]
        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product_Data data = new Product_Data(_config);
            var All = data.GetProductById(id).FirstOrDefault();

            if (All == null)
            {
                TempData["InfoMessage"] = "Product not available on This Id " + id.ToString();
                return RedirectToAction("Index");
            }


            return View(All);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [DisplayName("Delete")]
        public ActionResult Delete_confirm(int id)
        {
            Product_Data _data = new Product_Data(_config);
            if (ModelState.IsValid)
            {

                bool isUpdated = _data.Delete(id); 
                if (isUpdated)
                {
                    TempData["SuccessMessage"] = "Product Details Update Successfully....!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Product is Alredy Available/Unable To Update product details....";
                }

            }

            return RedirectToAction("Index");
        }
    }
}

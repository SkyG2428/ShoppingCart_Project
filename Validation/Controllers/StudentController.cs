using Microsoft.AspNetCore.Mvc;
using Validation.Models;

namespace Validation.Controllers
{
    public class StudentController : Controller
    {
        
            IConfiguration _config;
            StudentDataBase _db;
            public StudentController(IConfiguration config)
            {
                _config = config;
                _db = new StudentDataBase(_config["ConnectionStrings:B20ValidationDB"]);
            }

            public IActionResult Index()
            {
                var students = _db.Students();
                return View(students);
            }

            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(StudentModel student)
            {
                if (ModelState.IsValid)
                {
                    var status = _db.Insert(student);
                    if (status)
                        return RedirectToAction("Index");
                }

                return View();
            }


            
            
            
        }
    }


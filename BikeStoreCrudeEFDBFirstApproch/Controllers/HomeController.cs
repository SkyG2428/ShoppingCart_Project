using BikeStoreCrudeEFDBFirstApproch.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//Hello!!!
//Welcome To BikeStore
//Akash Updated
//Again Changes
//Conflict
//Jayashree
//Akash
//Again Conflict
// Shyam is good boy
namespace BikeStoreCrudeEFDBFirstApproch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
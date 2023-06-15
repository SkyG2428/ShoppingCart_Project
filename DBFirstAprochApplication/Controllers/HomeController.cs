using DBFirstAprochApplication.Models;
using DBFirstLayer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DBFirstAprochApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolSystemContext _context;
        public HomeController(ILogger<HomeController> logger,SchoolSystemContext context)
        {
            _logger = logger;
            _context = context;
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
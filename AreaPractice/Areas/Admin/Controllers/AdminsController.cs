using Microsoft.AspNetCore.Mvc;

namespace AreaPractice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

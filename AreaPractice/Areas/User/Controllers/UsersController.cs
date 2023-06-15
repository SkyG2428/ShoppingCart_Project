using Microsoft.AspNetCore.Mvc;

namespace AreaPractice.Areas.User.Controllers
{
    [Area("User")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

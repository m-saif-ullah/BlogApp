using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

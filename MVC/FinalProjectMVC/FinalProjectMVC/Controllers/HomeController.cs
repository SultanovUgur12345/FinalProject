using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

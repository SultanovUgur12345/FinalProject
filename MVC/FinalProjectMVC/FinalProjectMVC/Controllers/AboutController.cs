using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

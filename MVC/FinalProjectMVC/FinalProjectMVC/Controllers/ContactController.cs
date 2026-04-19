using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

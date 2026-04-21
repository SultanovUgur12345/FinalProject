using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

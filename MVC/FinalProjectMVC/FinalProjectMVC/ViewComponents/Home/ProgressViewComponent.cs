using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Header
{
    public class ProgressViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
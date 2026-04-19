using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Header
{
    public class DiscoverViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
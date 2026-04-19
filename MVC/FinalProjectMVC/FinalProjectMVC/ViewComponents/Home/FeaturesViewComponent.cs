using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Header
{
    public class FeaturesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
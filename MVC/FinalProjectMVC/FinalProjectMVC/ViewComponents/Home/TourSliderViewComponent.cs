using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Header
{
    public class TourSliderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
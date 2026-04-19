using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Footer
{
    public class ContactMapViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
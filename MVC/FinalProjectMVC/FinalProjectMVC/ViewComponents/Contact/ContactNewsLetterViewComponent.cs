using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Footer
{
    public class ContactNewsLetterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
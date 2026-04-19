using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Footer
{
    public class ContactHeroViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
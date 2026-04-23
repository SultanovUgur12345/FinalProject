using FinalProjectMVC.Services;
using FinalProjectMVC.ViewModels.Faq;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.About
{
    public class AboutUsViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

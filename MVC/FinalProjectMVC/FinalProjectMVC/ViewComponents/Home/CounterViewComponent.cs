using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Header
{
    public class CounterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
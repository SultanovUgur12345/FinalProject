using FinalProjectMVC.ViewModels.AboutUs;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.About
{
    public class AboutUsViewComponent : ViewComponent
    {
        private readonly IAboutUsApiService _service;

        public AboutUsViewComponent(IAboutUsApiService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
    }
}

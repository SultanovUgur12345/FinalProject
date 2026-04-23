using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Header
{
    public class FeaturesViewComponent : ViewComponent
    {
        private readonly IFeatureApiService _service;

        public FeaturesViewComponent(IFeatureApiService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var features = await _service.GetAllAsync();
            return View(features);
        }
    }
}

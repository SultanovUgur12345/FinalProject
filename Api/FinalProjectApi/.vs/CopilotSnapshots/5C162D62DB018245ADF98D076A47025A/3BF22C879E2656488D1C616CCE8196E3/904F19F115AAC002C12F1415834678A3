using FinalProjectMVC.ViewModels.Faq;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.ViewComponents.Footer
{
    public class FaqViewComponent : ViewComponent
    {
        private readonly IFaqApiService _faqApiService;

        public FaqViewComponent(IFaqApiService faqApiService)
        {
            _faqApiService = faqApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<FaqGetVM> faqs = await _faqApiService.GetAllAsync();
            return View(faqs);
        }
    }
}
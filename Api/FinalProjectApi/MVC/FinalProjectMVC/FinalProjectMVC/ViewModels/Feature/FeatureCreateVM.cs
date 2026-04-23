namespace FinalProjectMVC.ViewModels.Feature
{
    public class FeatureCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

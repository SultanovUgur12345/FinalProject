namespace FinalProjectApi.DTOs.Feature
{
    public class FeatureUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string CurrentImage { get; set; }
    }
}

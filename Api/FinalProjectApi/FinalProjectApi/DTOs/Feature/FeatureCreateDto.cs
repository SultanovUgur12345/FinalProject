namespace FinalProjectApi.DTOs.Feature
{
    public class FeatureCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

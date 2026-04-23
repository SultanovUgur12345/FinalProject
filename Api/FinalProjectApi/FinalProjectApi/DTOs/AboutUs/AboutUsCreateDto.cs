namespace FinalProjectApi.DTOs.AboutUs
{
    public class AboutUsCreateDto
    {
        public IFormFile? Image { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BottomText { get; set; }
        public string VideoUrl { get; set; }
        public List<AboutUsAchievementCreateDto> Achievements { get; set; } = new();
    }
}

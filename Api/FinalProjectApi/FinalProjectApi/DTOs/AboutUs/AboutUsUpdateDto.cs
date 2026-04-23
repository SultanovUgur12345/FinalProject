namespace FinalProjectApi.DTOs.AboutUs
{
    public class AboutUsUpdateDto
    {
        public IFormFile? Image { get; set; }
        public string? CurrentImage { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BottomText { get; set; }
        public string VideoUrl { get; set; }
        public List<AboutUsAchievementUpdateDto> Achievements { get; set; } = new();
    }
}

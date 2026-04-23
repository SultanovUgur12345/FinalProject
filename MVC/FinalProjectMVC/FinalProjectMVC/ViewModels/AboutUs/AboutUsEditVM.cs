namespace FinalProjectMVC.ViewModels.AboutUs
{
    public class AboutUsEditVM
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public string? CurrentImage { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BottomText { get; set; }
        public string VideoUrl { get; set; }
        public List<AboutUsAchievementEditVM> Achievements { get; set; } = new();
    }
}

namespace FinalProjectMVC.ViewModels.AboutUs
{
    public class AboutUsCreateVM
    {
        public IFormFile? Image { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BottomText { get; set; }
        public string VideoUrl { get; set; }
        public List<AboutUsAchievementCreateVM> Achievements { get; set; } = new();
    }
}

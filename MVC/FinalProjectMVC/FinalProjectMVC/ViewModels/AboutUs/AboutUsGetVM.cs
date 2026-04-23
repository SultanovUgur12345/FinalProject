namespace FinalProjectMVC.ViewModels.AboutUs
{
    public class AboutUsGetVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BottomText { get; set; }
        public string VideoUrl { get; set; }
        public List<AboutUsAchievementGetVM> Achievements { get; set; } = new();
    }
}

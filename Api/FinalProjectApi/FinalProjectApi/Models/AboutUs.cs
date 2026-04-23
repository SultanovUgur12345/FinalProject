namespace FinalProjectApi.Models
{
    public class AboutUs:BaseEntity
    {
        public string SubTitle { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string BottomText { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;

        public ICollection<AboutUsAchievement> Achievements { get; set; }
    }
}

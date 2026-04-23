namespace FinalProjectApi.Models
{
    public class AboutUs:BaseEntity
    {
        public string Image { get; set; }

        public string SubTitle { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string BottomText { get; set; }

        public string VideoUrl { get; set; }

        public ICollection<AboutUsAchievement> Achievements { get; set; }
    }
}

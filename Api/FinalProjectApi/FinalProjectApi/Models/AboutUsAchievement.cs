namespace FinalProjectApi.Models
{
    public class AboutUsAchievement:BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int AboutUsId { get; set; }

        public AboutUs AboutUs { get; set; }
    }
}

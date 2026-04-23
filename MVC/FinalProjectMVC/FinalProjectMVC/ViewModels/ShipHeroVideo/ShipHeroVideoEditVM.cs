namespace FinalProjectMVC.ViewModels.ShipHeroVideo
{
    public class ShipHeroVideoEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public IFormFile? Video { get; set; }
        public string CurrentVideo { get; set; }
    }
}

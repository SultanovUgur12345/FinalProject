namespace FinalProjectApi.DTOs.ShipHeroVideoDtos
{
    public class ShipHeroVideoUpdateDto
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public IFormFile? Video { get; set; }
        public string CurrentVideo { get; set; }
    }
}

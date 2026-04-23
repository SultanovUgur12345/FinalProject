namespace FinalProjectApi.DTOs.ShipSlider
{
    public class ShipSliderCreateDto
    {
        public string PreTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

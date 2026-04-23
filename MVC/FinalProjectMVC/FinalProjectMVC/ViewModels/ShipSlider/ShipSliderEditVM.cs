namespace FinalProjectMVC.ViewModels.ShipSlider
{
    public class ShipSliderEditVM
    {
        public int Id { get; set; }
        public string PreTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string CurrentImage { get; set; }
    }
}

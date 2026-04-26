namespace FinalProjectApi.Models
{
    public class Hotel:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<HoteImage> HotelImages { get; set; }

    }
}

namespace FinalProjectApi.Models
{
    public class HoteImage:BaseEntity
    {
        public string Image { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}

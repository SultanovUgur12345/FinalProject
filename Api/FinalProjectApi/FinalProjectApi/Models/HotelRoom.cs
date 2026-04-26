namespace FinalProjectApi.Models
{
    public class Room:BaseEntity
    {
        public int Count { get; set; }
        public int Capacity { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }

    }
}

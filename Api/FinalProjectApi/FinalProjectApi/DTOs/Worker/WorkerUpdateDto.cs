namespace FinalProjectApi.DTOs.Worker
{
    public class WorkerUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public double ExperienceYears { get; set; }
        public IFormFile? Image { get; set; }
        public string Languages { get; set; }
        public string CurrentImage { get; set; }
        public string Certificates { get; set; }
    }
}







using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Faq
{
    public class FaqEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}

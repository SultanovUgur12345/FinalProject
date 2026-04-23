using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Faq
{
    public class FaqCreateVM
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}

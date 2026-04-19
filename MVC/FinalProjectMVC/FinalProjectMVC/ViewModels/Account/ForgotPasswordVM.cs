using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Email bos ola bilmez")]
        [EmailAddress(ErrorMessage = "Email formati sehvdir")]
        public string Email { get; set; }
    }
}
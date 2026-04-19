using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Yeni sifre bos ola bilmez")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password bos ola bilmez")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password ve ConfirmPassword eyni deyil")]
        public string ConfirmPassword { get; set; }
    }
}
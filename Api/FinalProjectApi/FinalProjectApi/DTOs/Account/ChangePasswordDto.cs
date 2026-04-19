using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.DTOs.Account
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Current password bos ola bilmez")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password bos ola bilmez")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password bos ola bilmez")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwordler eyni deyil")]
        public string ConfirmPassword { get; set; }
    }
}
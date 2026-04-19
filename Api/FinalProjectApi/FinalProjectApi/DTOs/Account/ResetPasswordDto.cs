using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.DTOs.Account
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Email bos ola bilmez")]
        [EmailAddress(ErrorMessage = "Email formati sehvdir")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Token bos ola bilmez")]
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
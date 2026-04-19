using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.DTOs.Account
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email bos ola bilmez")]
        [EmailAddress(ErrorMessage = "Email formati sehvdir")]
        public string Email { get; set; }
    }
}
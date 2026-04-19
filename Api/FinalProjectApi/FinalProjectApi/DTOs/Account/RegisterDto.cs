using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.DTOs.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ad soyad bos ola bilmez")]
        [StringLength(100, ErrorMessage = "Ad soyad maksimum 100 simvol ola biler")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Username bos ola bilmez")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username 3-50 simvol arasynda olmalidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email bos ola bilmez")]
        [EmailAddress(ErrorMessage = "Email formati sehvdir")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password bos ola bilmez")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password minimum 4 simvol olmalidir")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword bos ola bilmez")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password ve ConfirmPassword eyni deyil")]
        public string ConfirmPassword { get; set; }
    }
}

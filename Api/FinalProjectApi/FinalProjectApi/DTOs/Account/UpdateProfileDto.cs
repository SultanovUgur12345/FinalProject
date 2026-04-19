using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.DTOs.Account
{
    public class UpdateProfileDto
    {
        [Required(ErrorMessage = "Ad soyad bos ola bilmez")]
        [StringLength(100, ErrorMessage = "Ad soyad maksimum 100 simvol ola biler")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Username bos ola bilmez")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username 3-50 simvol arasynda olmalidir")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Passwordler eyni deyil")]
        public string? ConfirmPassword { get; set; }
    }
}
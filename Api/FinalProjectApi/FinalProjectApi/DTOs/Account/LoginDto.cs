using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

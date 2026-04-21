using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Account
{
    public class AssignRoleVM
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = null!;
    }
}

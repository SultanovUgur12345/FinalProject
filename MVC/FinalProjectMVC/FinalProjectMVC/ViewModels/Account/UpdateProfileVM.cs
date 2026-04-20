using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Account
{
    public class UpdateProfileVM
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
    }
}
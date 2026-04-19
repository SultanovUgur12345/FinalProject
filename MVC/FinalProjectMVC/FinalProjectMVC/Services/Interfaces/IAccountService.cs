using FinalProjectMVC.ViewModels.Account;

namespace FinalProjectMVC.Services.Interfaces
{
    public interface IAccountService
    {
        Task<(bool success, string message)> RegisterAsync(RegisterVM model);
        Task<(bool success, string message, string token, string userName)> LoginAsync(LoginVM model);
        Task<(bool success, string message)> ForgotPasswordAsync(ForgotPasswordVM model);
        Task<(bool success, string message)> ResetPasswordAsync(ResetPasswordVM model);
        //Task<ProfileEditVM> GetProfileAsync();
        //Task<ResponceVM> UpdateProfileAsync(ProfileEditVM vm);
    }
}

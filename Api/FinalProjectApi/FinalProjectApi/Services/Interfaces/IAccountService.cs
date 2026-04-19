using FinalProjectApi.DTOs.Account;
using Org.BouncyCastle.Asn1.Ocsp;
public interface IAccountService
{
    Task<ResponceDto> RegisterAsync(RegisterDto dto, string scheme, string host);

    Task<ResponceDto> ConfirmEmailAsync(string userId, string token);

    Task<ResponceDto> LoginAsync(LoginDto dto);

    Task<ResponceDto> ForgotPasswordAsync(ForgotPasswordDto dto);

    Task<ResponceDto> ResetPasswordAsync(ResetPasswordDto dto);
    Task<ResponceDto> UpdateProfileAsync(string userId, UpdateProfileDto dto);
}
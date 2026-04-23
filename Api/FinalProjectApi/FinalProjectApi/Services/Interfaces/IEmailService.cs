namespace FinalProjectApi.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody);
        Task SendConfirmEmailAsync(string toEmail, string fullName, string confirmUrl);
        Task SendResetPasswordEmailAsync(string toEmail, string fullName, string resetUrl);
    }
}

using FinalProjectApi.Services.Interfaces;
using FinalProjectApi.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FinalProjectApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly string _templatesPath;

        public EmailService(IOptions<EmailSettings> options, IWebHostEnvironment env)
        {
            _settings = options.Value;
            _templatesPath = Path.Combine(env.ContentRootPath, "EmailTemplates");
        }

        public async Task SendConfirmEmailAsync(string toEmail, string fullName, string confirmUrl)
        {
            var html = await RenderTemplateAsync("ConfirmEmail.cshtml", new Dictionary<string, string>
            {
                { "{{FullName}}", fullName },
                { "{{ConfirmUrl}}", confirmUrl }
            });

            await SendEmailAsync(toEmail, "Email Tesdiqlemesi", html);
        }

        public async Task SendResetPasswordEmailAsync(string toEmail, string fullName, string resetUrl)
        {
            var html = await RenderTemplateAsync("ResetPassword.cshtml", new Dictionary<string, string>
            {
                { "{{FullName}}", fullName },
                { "{{ResetUrl}}", resetUrl }
            });

            await SendEmailAsync(toEmail, "Sifre Sifirlama", html);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.UserName, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        private async Task<string> RenderTemplateAsync(string fileName, Dictionary<string, string> replacements)
        {
            var filePath = Path.Combine(_templatesPath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Email template tapilmadi: {fileName}", filePath);

            var html = await File.ReadAllTextAsync(filePath);

            foreach (var kv in replacements)
                html = html.Replace(kv.Key, kv.Value);

            return html;
        }
    }
}

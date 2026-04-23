using FinalProjectMVC.Services.Interfaces;
using FinalProjectMVC.ViewModels.Account;
using System.Text.Json;

namespace FinalProjectMVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string message)> RegisterAsync(RegisterVM model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/register", model);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using var doc = JsonDocument.Parse(content);
                    var message = doc.RootElement.GetProperty("message").GetString();
                    return (true, message ?? "Qeydiyyat ugurludur");
                }
                catch
                {
                    return (true, "Qeydiyyat ugurludur");
                }
            }

            try
            {
                using var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("message", out var msg))
                    return (false, msg.GetString() ?? "Xeta bas verdi");
            }
            catch
            {
            }

            return (false, content);
        }

        public async Task<(bool success, string message, string token, string userName)> LoginAsync(LoginVM model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/login", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    using var doc = JsonDocument.Parse(content);
                    if (doc.RootElement.TryGetProperty("message", out var msg))
                        return (false, msg.GetString() ?? "Xeta bas verdi", null, null);
                }
                catch
                {
                }

                return (false, content, null, null);
            }

            using var successDoc = JsonDocument.Parse(content);
            var message = successDoc.RootElement.GetProperty("message").GetString();
            var token = successDoc.RootElement.GetProperty("token").GetString();
            var userName = successDoc.RootElement.GetProperty("userName").GetString();

            return (true, message ?? "Login ugurludur", token, userName);
        }

        public async Task<(bool success, string message)> ForgotPasswordAsync(ForgotPasswordVM model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/forgot-password", model);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using var doc = JsonDocument.Parse(content);
                    var message = doc.RootElement.GetProperty("message").GetString();
                    return (true, message ?? "Reset linki gonderildi");
                }
                catch
                {
                    return (true, "Reset linki gonderildi");
                }
            }

            try
            {
                using var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("message", out var msg))
                    return (false, msg.GetString() ?? "Xeta bas verdi");
            }
            catch
            {
            }

            return (false, content);
        }

        public async Task<(bool success, string message)> ResetPasswordAsync(ResetPasswordVM model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/reset-password", model);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using var doc = JsonDocument.Parse(content);
                    var message = doc.RootElement.GetProperty("message").GetString();
                    return (true, message ?? "Sifre ugurla yenilendi");
                }
                catch
                {
                    return (true, "Sifre ugurla yenilendi");
                }
            }

            try
            {
                using var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("message", out var msg))
                    return (false, msg.GetString() ?? "Xeta bas verdi");
            }
            catch
            {
            }

            return (false, content);
        }
    }
}

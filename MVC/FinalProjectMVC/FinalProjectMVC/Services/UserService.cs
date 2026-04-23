using FinalProjectMVC.Services.Interfaces;
using FinalProjectMVC.ViewModels.Account;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FinalProjectMVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetProfileVM> GetProfileAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/User/GetProfile");

            if (!response.IsSuccessStatusCode)
                return new GetProfileVM();

            var result = await response.Content.ReadFromJsonAsync<GetProfileVM>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new GetProfileVM();
        }

        public async Task<(bool success, string message, string? userName, string? profileImageUrl)> UpdateProfileAsync(UpdateProfileVM model, IFormFile? file, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var formContent = new MultipartFormDataContent();

            if (!string.IsNullOrWhiteSpace(model.FullName))
                formContent.Add(new StringContent(model.FullName), "FullName");

            if (!string.IsNullOrWhiteSpace(model.UserName))
                formContent.Add(new StringContent(model.UserName), "UserName");

            if (!string.IsNullOrWhiteSpace(model.CurrentPassword))
                formContent.Add(new StringContent(model.CurrentPassword), "CurrentPassword");

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
                formContent.Add(new StringContent(model.NewPassword), "NewPassword");

            if (file != null && file.Length > 0)
            {
                var stream = file.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                formContent.Add(fileContent, "file", file.FileName);
            }

            var response = await _httpClient.PutAsync("api/User/UpdateProfile", formContent);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using var doc = JsonDocument.Parse(content);
                    string message = "Profil ugurla yenilendi";
                    string? userName = null;
                    string? profileImageUrl = null;

                    if (doc.RootElement.TryGetProperty("message", out var msgProp))
                        message = msgProp.GetString() ?? message;

                    if (doc.RootElement.TryGetProperty("userName", out var userNameProp))
                        userName = userNameProp.GetString();

                    if (doc.RootElement.TryGetProperty("profileImage", out var imgProp))
                        profileImageUrl = imgProp.GetString();

                    return (true, message, userName, profileImageUrl);
                }
                catch
                {
                    return (true, "Profil ugurla yenilendi", model.UserName, null);
                }
            }

            try
            {
                using var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("message", out var msgProp))
                    return (false, msgProp.GetString() ?? "Xeta bas verdi", null, null);
            }
            catch { }

            return (false, "Xeta bas verdi", null, null);
        }

        public async Task<List<UserListVM>> GetAllUsersAsync(string token, string callerRole)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/User/GetAll");

            if (!response.IsSuccessStatusCode)
                return new List<UserListVM>();

            var users = await response.Content.ReadFromJsonAsync<List<UserListVM>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (users == null)
                return new List<UserListVM>();

            foreach (var user in users)
                user.CanChangeRole = user.Role != "SuperAdmin" && callerRole == "SuperAdmin";

            return users;
        }

        public async Task<PaginatedResult<UserListVM>> GetPagedUsersAsync(int page, int pageSize, string token, string callerRole)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/User/GetPaged?page={page}&pageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<UserListVM> { Page = page, PageSize = pageSize };

            var result = await response.Content.ReadFromJsonAsync<PaginatedResult<UserListVM>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null)
                return new PaginatedResult<UserListVM> { Page = page, PageSize = pageSize };

            foreach (var user in result.Items)
                user.CanChangeRole = user.Role != "SuperAdmin" && callerRole == "SuperAdmin";

            return result;
        }

        public async Task<List<UserListVM>> SearchByFullNameAsync(string fullName, string token, string callerRole)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/User/SearchByFullName?fullName={Uri.EscapeDataString(fullName)}");

            if (!response.IsSuccessStatusCode)
                return new List<UserListVM>();

            var users = await response.Content.ReadFromJsonAsync<List<UserListVM>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (users == null)
                return new List<UserListVM>();

            foreach (var user in users)
                user.CanChangeRole = user.Role != "SuperAdmin" && callerRole == "SuperAdmin";

            return users;
        }

        public async Task<(bool success, string message)> AssignRoleAsync(AssignRoleVM model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("api/User/AssignRole", new { model.UserId, Role = model.Role });
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using var doc = JsonDocument.Parse(content);
                    var message = doc.RootElement.GetProperty("message").GetString();
                    return (true, message ?? "Rol ugurla deyisdirildi");
                }
                catch
                {
                    return (true, "Rol ugurla deyisdirildi");
                }
            }

            try
            {
                using var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("message", out var msg))
                    return (false, msg.GetString() ?? "Xeta bas verdi");
            }
            catch { }

            return (false, content);
        }
    }
}

using FinalProjectMVC.Services.Interfaces;
using FinalProjectMVC.ViewModels.Setting;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FinalProjectMVC.Services
{
    public class SettingApiService : ISettingApiService
    {
        private readonly HttpClient _httpClient;

        public SettingApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SettingGetVM>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SettingGetVM>>("api/Setting/GetAll");
        }

        public async Task<SettingEditVM> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SettingEditVM>($"api/Setting/Get/{id}");
        }

        public async Task<(bool success, string error)> UpdateAsync(int id, SettingEditVM dto)
        {
            try
            {
                var payload = new { Value = dto.Value };
                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Setting/Update/{id}", content);
                if (response.IsSuccessStatusCode) return (true, null);
                var body = await response.Content.ReadAsStringAsync();
                return (false, $"{(int)response.StatusCode}: {body}");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}

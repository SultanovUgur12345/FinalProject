using FinalProjectMVC.ViewModels.AboutUs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

public class AboutUsApiService : IAboutUsApiService
{
    private readonly HttpClient _httpClient;

    public AboutUsApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AboutUsGetVM>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<AboutUsGetVM>>("api/AboutUs/GetAll");
    }

    public async Task<List<AboutUsGetVM>> GetAllAdminAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<AboutUsGetVM>>("api/AboutUs/GetAllAdmin");
    }

    public async Task<AboutUsGetVM> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<AboutUsGetVM>($"api/AboutUs/Get/{id}");
    }

    public async Task<AboutUsEditVM> GetByIdForEditAsync(int id)
    {
        var data = await _httpClient.GetFromJsonAsync<AboutUsGetVM>($"api/AboutUs/Get/{id}");

        if (data == null)
            throw new Exception($"AboutUs tapilmadi. Id: {id}");

        return new AboutUsEditVM
        {
            Id = data.Id,
            CurrentImage = data.Image,
            SubTitle = data.SubTitle,
            Title = data.Title,
            Description = data.Description,
            BottomText = data.BottomText,
            VideoUrl = data.VideoUrl,
            Achievements = data.Achievements.Select(a => new AboutUsAchievementEditVM
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description
            }).ToList()
        };
    }

    public async Task CreateAsync(AboutUsCreateVM dto)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.SubTitle ?? ""), "SubTitle");
        content.Add(new StringContent(dto.Title ?? ""), "Title");
        content.Add(new StringContent(dto.Description ?? ""), "Description");
        content.Add(new StringContent(dto.BottomText ?? ""), "BottomText");
        content.Add(new StringContent(dto.VideoUrl ?? ""), "VideoUrl");

        for (int i = 0; i < dto.Achievements.Count; i++)
        {
            content.Add(new StringContent(dto.Achievements[i].Title ?? ""), $"Achievements[{i}].Title");
            content.Add(new StringContent(dto.Achievements[i].Description ?? ""), $"Achievements[{i}].Description");
        }

        if (dto.Image != null)
        {
            var fileContent = new StreamContent(dto.Image.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        var response = await _httpClient.PostAsync("api/AboutUs/Create", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }

    public async Task UpdateAsync(int id, AboutUsEditVM dto)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.SubTitle ?? ""), "SubTitle");
        content.Add(new StringContent(dto.Title ?? ""), "Title");
        content.Add(new StringContent(dto.Description ?? ""), "Description");
        content.Add(new StringContent(dto.BottomText ?? ""), "BottomText");
        content.Add(new StringContent(dto.VideoUrl ?? ""), "VideoUrl");
        content.Add(new StringContent(dto.CurrentImage ?? ""), "CurrentImage");

        for (int i = 0; i < dto.Achievements.Count; i++)
        {
            content.Add(new StringContent(dto.Achievements[i].Id.ToString()), $"Achievements[{i}].Id");
            content.Add(new StringContent(dto.Achievements[i].Title ?? ""), $"Achievements[{i}].Title");
            content.Add(new StringContent(dto.Achievements[i].Description ?? ""), $"Achievements[{i}].Description");
        }

        if (dto.Image != null)
        {
            var fileContent = new StreamContent(dto.Image.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        var response = await _httpClient.PutAsync($"api/AboutUs/Update/{id}", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/AboutUs/Delete/{id}");
        response.EnsureSuccessStatusCode();
    }
}

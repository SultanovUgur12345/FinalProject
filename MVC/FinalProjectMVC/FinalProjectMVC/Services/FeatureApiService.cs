using FinalProjectMVC.ViewModels.Feature;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class FeatureApiService : IFeatureApiService
{
    private readonly HttpClient _httpClient;

    public FeatureApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FeatureGetVM>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<FeatureGetVM>>("api/Feature/GetAll");
    }

    public async Task<FeatureGetVM> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<FeatureGetVM>($"api/Feature/Get/{id}");
    }

    public async Task<FeatureEditVM> GetByIdForEditAsync(int id)
    {
        var data = await _httpClient.GetFromJsonAsync<FeatureGetVM>($"api/Feature/Get/{id}");

        if (data == null)
            throw new Exception($"Feature tapilmadi. Id: {id}");

        return new FeatureEditVM
        {
            Id = data.Id,
            Title = data.Title,
            Description = data.Description,
            CurrentImage = data.Image
        };
    }

    public async Task CreateAsync(FeatureCreateVM vm)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(vm.Title ?? ""), "Title");
        content.Add(new StringContent(vm.Description ?? ""), "Description");

        if (vm.Image != null)
        {
            var fileContent = new StreamContent(vm.Image.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(vm.Image.ContentType);
            content.Add(fileContent, "Image", vm.Image.FileName);
        }

        var response = await _httpClient.PostAsync("api/Feature/Create", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }

    public async Task UpdateAsync(int id, FeatureEditVM vm)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(vm.Title ?? ""), "Title");
        content.Add(new StringContent(vm.Description ?? ""), "Description");
        content.Add(new StringContent(vm.CurrentImage ?? ""), "CurrentImage");

        if (vm.Image != null)
        {
            var fileContent = new StreamContent(vm.Image.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(vm.Image.ContentType);
            content.Add(fileContent, "Image", vm.Image.FileName);
        }

        var response = await _httpClient.PutAsync($"api/Feature/Update/{id}", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Feature/Delete/{id}");
        response.EnsureSuccessStatusCode();
    }
}

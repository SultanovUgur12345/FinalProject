using FinalProjectMVC.ViewModels.Worker;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static IWorkerService;

public class WorkerApiService : IWorkerApiService
{
    private readonly HttpClient _httpClient;

    public WorkerApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<WorkerGetVM>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<WorkerGetVM>>("api/Worker/GetAll");
    }

    public async Task<PaginatedResult<WorkerGetVM>> GetPagedAsync(int page, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<WorkerGetVM>>(
            $"api/Worker/GetPaged?page={page}&pageSize={pageSize}");
    }

    public async Task<List<WorkerGetVM>> SearchByNameAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<List<WorkerGetVM>>(
            $"api/Worker/Search?name={Uri.EscapeDataString(name)}");
    }

    public async Task<WorkerDetailVM> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<WorkerDetailVM>($"api/Worker/Get/{id}");
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Worker/Delete/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateAsync(WorkerCreateVM dto)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.FullName ?? ""), "FullName");
        content.Add(new StringContent(dto.Position ?? ""), "Position");
        content.Add(new StringContent(dto.Description ?? ""), "Description");
        content.Add(new StringContent(dto.ExperienceYears.ToString()), "ExperienceYears");
        content.Add(new StringContent(dto.Languages ?? ""), "Languages");
        content.Add(new StringContent(dto.Certificates ?? ""), "Certificates");

        if (dto.Image != null)
        {
            var fileContent = new StreamContent(dto.Image.OpenReadStream());
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        var response = await _httpClient.PostAsync("api/Worker/Create", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }


    public async Task<WorkerEditVM> GetByIdForEditAsync(int id)
    {
        var data = await _httpClient.GetFromJsonAsync<WorkerDetailVM>($"api/Worker/Get/{id}");

        if (data == null)
            throw new Exception($"Worker tapilmadi. Id: {id}");

        return new WorkerEditVM
        {
            Id = data.Id,
            FullName = data.FullName,
            Position = data.Position,
            Description = data.Description,
            ExperienceYears = data.ExperienceYears,
            CurrentImage = data.Image,
            Languages = data.Languages,
            Certificates = data.Certificates
        };
    }

    public async Task UpdateAsync(int id, WorkerEditVM dto)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.Id.ToString()), "Id");
        content.Add(new StringContent(dto.FullName ?? ""), "FullName");
        content.Add(new StringContent(dto.Position ?? ""), "Position");
        content.Add(new StringContent(dto.Description ?? ""), "Description");
        content.Add(new StringContent(dto.ExperienceYears.ToString(CultureInfo.InvariantCulture)), "ExperienceYears");
        content.Add(new StringContent(dto.Languages ?? ""), "Languages");
        content.Add(new StringContent(dto.Certificates ?? ""), "Certificates");
        content.Add(new StringContent(dto.CurrentImage ?? ""), "CurrentImage");

        if (dto.Image != null)
        {
            var fileContent = new StreamContent(dto.Image.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        var response = await _httpClient.PutAsync($"api/Worker/Update/{id}", content);
        response.EnsureSuccessStatusCode();
    }

   
}
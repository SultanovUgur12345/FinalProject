using FinalProjectApi.DTOs.AboutUs;

namespace FinalProjectApi.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<List<AboutUsGetDto>> GetAllAsync();
        Task<List<AboutUsGetDto>> GetAllAdminAsync();
        Task<AboutUsGetDto> GetByIdAsync(int id);
        Task CreateAsync(AboutUsCreateDto dto);
        Task UpdateAsync(int id, AboutUsUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

using FinalProjectApi.DTOs.Feature;

namespace FinalProjectApi.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<List<FeatureGetDto>> GetAllAsync();
        Task<FeatureGetDto> GetByIdAsync(int id);
        Task CreateAsync(FeatureCreateDto dto);
        Task UpdateAsync(int id, FeatureUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

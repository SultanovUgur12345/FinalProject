using FinalProjectApi.DTOs.Faq;

namespace FinalProjectApi.Services.Interfaces
{
    public interface IFaqService
    {
        Task<List<FaqGetDto>> GetAllAsync();
        Task<FaqGetDto> GetByIdAsync(int id);
        Task CreateAsync(FaqCreateDto dto);
        Task UpdateAsync(int id, FaqUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

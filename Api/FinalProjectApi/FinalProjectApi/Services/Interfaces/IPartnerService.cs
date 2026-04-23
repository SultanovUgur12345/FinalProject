using FinalProjectApi.DTOs.Partner;

namespace FinalProjectApi.Services.Interfaces
{
    public interface IPartnerService
    {
        Task<List<PartnerGetDto>> GetAllAsync();
        Task<PartnerGetDto> GetByIdAsync(int id);
        Task CreateAsync(PartnerCreateDto dto);
        Task UpdateAsync(int id, PartnerUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

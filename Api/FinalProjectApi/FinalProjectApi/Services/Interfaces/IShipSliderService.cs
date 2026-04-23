using FinalProjectApi.DTOs.ShipSlider;

namespace FinalProjectApi.Services.Interfaces
{
    public interface IShipSliderService
    {
        Task<List<ShipSliderGetDto>> GetAllAsync();
        Task<List<ShipSliderGetDto>> GetAllAdminAsync();
        Task<ShipSliderGetDto> GetByIdAsync(int id);
        Task CreateAsync(ShipSliderCreateDto dto);
        Task UpdateAsync(int id, ShipSliderUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

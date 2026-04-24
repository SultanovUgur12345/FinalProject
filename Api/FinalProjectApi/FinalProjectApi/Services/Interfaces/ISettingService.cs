using FinalProjectApi.DTOs.Setting;

namespace FinalProjectApi.Services.Interfaces
{
    public interface ISettingService
    {
        Task<List<SettingGetDto>> GetAllAsync();
        Task<SettingGetDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, SettingUpdateDto dto);
    }
}

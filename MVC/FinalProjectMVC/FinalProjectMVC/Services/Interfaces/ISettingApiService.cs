using FinalProjectMVC.ViewModels.Setting;

namespace FinalProjectMVC.Services.Interfaces
{
    public interface ISettingApiService
    {
        Task<List<SettingGetVM>> GetAllAsync();
        Task<SettingEditVM> GetByIdAsync(int id);
        Task<(bool success, string error)> UpdateAsync(int id, SettingEditVM dto);
    }
}

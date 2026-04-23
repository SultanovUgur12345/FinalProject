using FinalProjectMVC.ViewModels.ShipSlider;

public interface IShipSliderApiService
{
    Task<List<ShipSliderGetVM>> GetAllAsync();
    Task<List<ShipSliderGetVM>> GetAllAdminAsync();
    Task<ShipSliderGetVM> GetByIdAsync(int id);
    Task<ShipSliderEditVM> GetByIdForEditAsync(int id);
    Task CreateAsync(ShipSliderCreateVM dto);
    Task UpdateAsync(int id, ShipSliderEditVM dto);
    Task DeleteAsync(int id);
}

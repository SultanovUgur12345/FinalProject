using FinalProjectMVC.ViewModels.ShipHeroVideo;

public interface IShipHeroVideoApiService
{
    Task<List<ShipHeroVideoGetVM>> GetAllAsync();
    Task<ShipHeroVideoGetVM> GetByIdAsync(int id);
    Task<ShipHeroVideoEditVM> GetByIdForEditAsync(int id);
    Task CreateAsync(ShipHeroVideoCreateVM dto);
    Task UpdateAsync(int id, ShipHeroVideoEditVM dto);
    Task DeleteAsync(int id);
}

using FinalProjectMVC.ViewModels.AboutUs;

public interface IAboutUsApiService
{
    Task<List<AboutUsGetVM>> GetAllAsync();
    Task<List<AboutUsGetVM>> GetAllAdminAsync();
    Task<AboutUsGetVM> GetByIdAsync(int id);
    Task<AboutUsEditVM> GetByIdForEditAsync(int id);
    Task CreateAsync(AboutUsCreateVM dto);
    Task UpdateAsync(int id, AboutUsEditVM dto);
    Task DeleteAsync(int id);
}

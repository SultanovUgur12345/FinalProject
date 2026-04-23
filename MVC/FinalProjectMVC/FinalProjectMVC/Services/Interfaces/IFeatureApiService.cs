using FinalProjectMVC.ViewModels.Feature;

public interface IFeatureApiService
{
    Task<List<FeatureGetVM>> GetAllAsync();
    Task<FeatureGetVM> GetByIdAsync(int id);
    Task<FeatureEditVM> GetByIdForEditAsync(int id);
    Task CreateAsync(FeatureCreateVM vm);
    Task UpdateAsync(int id, FeatureEditVM vm);
    Task DeleteAsync(int id);
}


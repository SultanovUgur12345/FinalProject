using FinalProjectMVC.ViewModels.Faq;

public interface IFaqApiService
{
    Task<List<FaqGetVM>> GetAllAsync();
    Task<FaqGetVM> GetByIdAsync(int id);
    Task<FaqEditVM> GetByIdForEditAsync(int id);
    Task CreateAsync(FaqCreateVM vm);
    Task UpdateAsync(int id, FaqEditVM vm);
    Task DeleteAsync(int id);
}

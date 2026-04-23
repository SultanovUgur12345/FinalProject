using FinalProjectMVC.ViewModels.Partner;

public interface IPartnerApiService
{
    Task<List<PartnerGetVM>> GetAllAsync();
    Task<PartnerGetVM> GetByIdAsync(int id);
    Task<PartnerEditVM> GetByIdForEditAsync(int id);
    Task CreateAsync(PartnerCreateVM vm);
    Task UpdateAsync(int id, PartnerEditVM vm);
    Task DeleteAsync(int id);
}
